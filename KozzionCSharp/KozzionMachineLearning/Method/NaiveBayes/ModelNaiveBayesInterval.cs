using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KozzionCore.IO;

namespace KozzionMachineLearning.Method.NaiveBayes
{

    public class ModelNaiveBayesInterval :
        AModelLikelihood<double, int>
    {
        private IFunction<double, double>[,] marginals;
        private double[] class_priors;
        private int feature_count; //redundant
        private int class_count; //redundant

        public ModelNaiveBayesInterval(
            IDataContext data_context,
            double[] class_priors,
            IFunction<double, double>[,] marginals,
            string model_type)
            : base(data_context, model_type)
        {
            this.class_priors = ToolsMathCollectionDouble.LogE(class_priors); //These need to be log likelyhoods
            this.marginals = ToolsCollection.Copy(marginals); //TODO gutted object    
            this.feature_count = this.marginals.GetLength(0);
            this.class_count = this.marginals.GetLength(1);
            Debug.Assert(class_priors.Length == class_count);
        }


        public ModelNaiveBayesInterval(
            IDataContext data_context,
            double[] class_priors,
            IFunction<double, double>[,] marginals)
            : this(data_context, class_priors, marginals, "ModelNaiveBayesInterval")
        {

        }

        public override double[] GetLikelihoods(double[] instance_features)
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


        internal static IModelLikelihood<double, int> Read(BinaryReader reader)
        {
            IDataContext data_context = DataSet.DataContext.Read(reader);
            double[] class_priors = reader.ReadFloat64Array1D();
            IFunction<double, double>[,] marginals = null;
            return new ModelNaiveBayesInterval(data_context, class_priors, marginals);
        }


        internal static void Write(BinaryWriter writer, ModelNaiveBayesInterval model)
        {
            model.DataContext.Write(writer);
            writer.Write(model.class_priors);
        }
    }
}
