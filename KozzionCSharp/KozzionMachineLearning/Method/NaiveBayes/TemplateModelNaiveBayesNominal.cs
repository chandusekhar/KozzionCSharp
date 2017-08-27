
using System;
using System.Collections.Generic;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;
using KozzionMachineLearning.Reporting;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Method.NaiveBayes
{
    public class TemplateModelNaiveBayesNominal : ATemplateModelLikelihood<int, int>
    {    

        public override IModelLikelihood<int, int> GenerateModelLikelihood(IDataSet<int, int> training_set)
        {
            int value_count = training_set.DataContext.LabelDescriptors[0].ValueCount;
            int [] label_data = training_set.GetLabelDataColumn(0);

            //Compute class priors
            int [] label_occurances = new int[value_count];
            foreach (int label  in label_data)
            {
                label_occurances[label]++;
            }
            double[] class_priors = new double [value_count];
            for (int labe_index = 0; labe_index < value_count; labe_index++)
            {
                class_priors[labe_index] =  ((double)label_occurances[labe_index])/ label_data.Length;
            }

            //Compute marginals
            int[][]feature_data  = training_set.FeatureData;
            IFunction<int, double>[,] marginals = new  IFunction<int, double>[training_set.FeatureCount, value_count];
            for (int feature_index = 0; feature_index < training_set.FeatureCount; feature_index++)
            {
                for (int label_index = 0; label_index < value_count; label_index++)
                {
                    marginals[feature_index, label_index] = new FunctionMarginalNominal(training_set.DataContext, feature_data, label_data, feature_index, label_index, double.Epsilon);
                }
            }

            return new ModelNaiveBayesNominal(training_set.DataContext, class_priors, marginals);
        } 
    }
}