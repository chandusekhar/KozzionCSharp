using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public class CentroidNominal : ICentroidDistance<int, double>
    {
        public IList<int[]> Members { get; private set; }

        private double[][] location;

        public CentroidNominal(IList<int> feature_value_counts, IList<int[]> instances)
        {
            //TODO make this run on DataContexts
            this.Members = instances;
            location = new double[feature_value_counts.Count][];
            for (int feature_index = 0; feature_index < feature_value_counts.Count; feature_index++)
            {
                location[feature_index] = new double[feature_value_counts[feature_index]];
            }

            for (int instance_index = 0; instance_index < instances.Count; instance_index++)
            {
                int[] instance_features = instances[instance_index];
                for (int feature_index = 0; feature_index < feature_value_counts.Count; feature_index++)
                {
                    int feature_value_index = instance_features[feature_index];
                    location[feature_index][feature_value_index] += 1.0 / instances.Count;
                }
            }
        }

        public double ComputeDistance(int[] instance_features)
        {
            if (location.Length != instance_features.Length)
            {
                throw new Exception("instance features of incorrect lenght");
            }
            double distance = 0;
            for (int index = 0; index < instance_features.Length; index++)
            {
                distance += 1.0 - location[index][instance_features[index]];
            }
            return distance;
        }
    }
}
