using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.NaiveBayes
{
    public class FunctionMarginalIntervalNormal : IFunction<double, double>
    {
        public string FunctionType { get { return "FunctionMarginalIntervalNormal"; } }
        private double mean;
        private double standard_deviation;

        public FunctionMarginalIntervalNormal(IDataContext data_context, double[][] feature_data, int[] label_data, int feature_index, int label_index, double epsilon)
        {
            List<double> occurances = new List<double>();
            for (int instance_index = 0; instance_index < feature_data.GetLength(0); instance_index++)
            {
                if (label_index == label_data[instance_index])
                {
                    occurances.Add(feature_data[instance_index][feature_index]);
                }
            }
            this.mean = ToolsMathStatistics.Mean(occurances);
            this.standard_deviation = ToolsMathStatistics.StandardDeviation(occurances, mean);
        }

        public double Compute(double feature_value)
        {
            return DistributionNormalUnivariateFloat64.ComputeProbabilityDensity(mean, standard_deviation, feature_value);
        }
    }
}
