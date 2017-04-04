using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering.KMeans
{
    public class CentroidCalculatorNominal : IFunction<IList<int[]>, ICentroidDistance<int, double>>
    {
        public string FunctionType { get { return "CentroidCalculatorNominal"; } }

        private IList<int> feature_value_counts;

        public CentroidCalculatorNominal(IDataContext data_context)
        {
            this.feature_value_counts = new List<int>();
            for (int feature_index = 0; feature_index < data_context.FeatureCount; feature_index++)
            {
                feature_value_counts.Add(data_context.FeatureDescriptors[feature_index].ValueCount);
            }
        }


        public ICentroidDistance<int, double> Compute(IList<int[]> instance_features_list)
        {
            return new CentroidNominal(this.feature_value_counts, instance_features_list);
        }
    }
}
