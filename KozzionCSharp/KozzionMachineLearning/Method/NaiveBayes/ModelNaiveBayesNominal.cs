using System.Diagnostics;
using KozzionCore.Tools;
using KozzionMachineLearning.Model;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using KozzionMachineLearning.Method.JointTable;
using System;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Method.NaiveBayes
{
    public class ModelNaiveBayesNominal:
        AModelLikelihood<int, int, double>
    {
        private IFunction<int, double> [,]  marginals;
        private double[] class_priors;
        private int feature_count;
        private int class_count;

        public ModelNaiveBayesNominal(
            IDataContext data_context,
            double[] class_priors,
            IFunction<int, double>[,] marginals)
            : base(data_context, "ModelNaiveBayesNominal")
        {
            this.class_priors = ToolsMathCollectionDouble.LogE(class_priors); //These need to be log likelyhoods
            this.marginals = ToolsCollection.Copy(marginals); //TODO gutted object    
            this.feature_count = this.marginals.GetLength(0);
            this.class_count = this.marginals.GetLength(1);
            Debug.Assert(class_priors.Length == class_count);
        }

        public override double[] GetLikelihoods(int[] instance_features)
        {
            Debug.Assert(instance_features.Length == feature_count);
            double[] class_likelyhoods = ToolsCollection.Copy(class_priors); //These need to be log likelyhoods

            for (int index_feature = 0; index_feature < feature_count; index_feature++)
            {
                for (int index_class = 0; index_class < class_count; index_class++)
                {
                    double likelyhood = marginals[index_feature, index_class].Compute(instance_features[index_feature]);
                    class_likelyhoods[index_class] = class_likelyhoods[index_class] + Math.Log(likelyhood);
                }
            }
            return ToolsMathCollectionDouble.Exp(class_likelyhoods); //TODO use exp?
        }

 
    }
}