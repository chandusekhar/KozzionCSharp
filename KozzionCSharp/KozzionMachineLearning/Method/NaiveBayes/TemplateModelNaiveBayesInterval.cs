using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.NaiveBayes
{
    public class TemplateModelNaiveBayesInterval : ATemplateModelLikelihood<double, int>
    {
        public override IModelLikelihood<double, int, double> GenerateModelLikelihood(IDataSet<double, int> training_set)
        {
            int label_value_count = training_set.DataContext.GetLabelDescriptor(0).ValueCount;
            int[] label_data = training_set.GetLabelDataColumn(0);

            //Compute class priors
            int[] label_occurances = new int[label_value_count];
            foreach (int label in label_data)
            {
                label_occurances[label]++;
            }
            double[] class_priors = new double[label_value_count];
            for (int labe_index = 0; labe_index < label_value_count; labe_index++)
            {
                class_priors[labe_index] = ((double)label_occurances[labe_index]) / label_data.Length;
            }

            //Compute marginals
            double[][] feature_data = training_set.FeatureData;
            IFunction<double, double>[,] marginals = new IFunction<double, double>[training_set.FeatureCount, label_value_count];
            for (int feature_index = 0; feature_index < training_set.FeatureCount; feature_index++)
            {
                for (int label_index = 0; label_index < label_value_count; label_index++)
                {
                    marginals[feature_index, label_index] = new FunctionMarginalIntervalNormal(training_set.DataContext, feature_data, label_data, feature_index, label_index, double.Epsilon);
                }
            }

            return new ModelNaiveBayesInterval(training_set.DataContext, class_priors, marginals);
        }
    }

}
