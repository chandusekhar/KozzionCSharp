using System;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Method.NaiveBayes
{
    internal class FunctionMarginalNominal : IFunction<int, double>
    {
        public string FunctionType { get { return "FunctionMarginalNominal"; } }
        private double [] marginals;

        public FunctionMarginalNominal(IDataContext data_context, int[][] feature_data, int[] label_data, int feature_index, int label_index, double epsilon)
        {
            int [] occurances = new int[data_context.GetFeatureDescriptor(feature_index).ValueCount];
            int total_occurances = 0;
            for (int instance_index = 0; instance_index < feature_data.GetLength(0); instance_index++)
            {
                if (label_index == label_data[instance_index])
                {
                    occurances[feature_data[instance_index][feature_index]]++;
                    total_occurances++;
                }
            }
            this.marginals = new double[occurances.Length];
            for (int feature_value_index = 0; feature_value_index < occurances.Length; feature_value_index++)
            {
                if (occurances[feature_value_index] == 0)
                {
                    marginals[feature_value_index] = epsilon;
                }
                else
                {
                    marginals[feature_value_index] = ((double)occurances[feature_value_index]) / total_occurances;
                }
            }
        }

        public double Compute(int feature_value)
        {
            return marginals[feature_value];
        }
    }
}