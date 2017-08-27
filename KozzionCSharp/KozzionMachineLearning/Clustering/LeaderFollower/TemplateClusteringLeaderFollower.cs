using System;
using System.Collections.Generic;
using KozzionMathematics.Function;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Clustering.KMeans;

namespace KozzionMachineLearning.Clustering.LeaderFollower
{
public class TemplateClusteringLeaderFollower<DomainType, DataSetType> : ITemplateClusteringCentroid<DomainType, IDataSet<DomainType>>
{
        private ITemplateCentroidCalculator<DomainType, ICentroidDistance<DomainType>> centroid_calculator_template;
        private double critical_distance;

        public TemplateClusteringLeaderFollower(
            ITemplateCentroidCalculator<DomainType, ICentroidDistance<DomainType>> centroid_calculator_template,
            double critical_distance)
        {
            this.centroid_calculator_template = centroid_calculator_template;
            this.critical_distance = critical_distance;
        }

        public IClusteringCentroid<DomainType> Cluster(IDataSet<DomainType> data_set)
        {

            IFunction<IList<DomainType[]>, ICentroidDistance<DomainType>> centroid_calculator = centroid_calculator_template.Generate(data_set.DataContext);
            IList<DomainType[]> instance_features_list = data_set.FeatureData;
            IList<IList<DomainType[]>> cluster_members = new List<IList<DomainType[]>>();
            IList<ICentroidDistance<DomainType>> centroids = new List<ICentroidDistance<DomainType>>();

            // add first cluster
            cluster_members.Add(new List<DomainType[]>());
            cluster_members[0].Add(instance_features_list[0]);
            centroids.Add(centroid_calculator.Compute(cluster_members[0]));

            // assign to clusters
            for (int index_instance = 1; index_instance < instance_features_list.Count; index_instance++)
            {
                DomainType[] instance_features = instance_features_list[index_instance];

                int best_cluster_index = 0;
                double best_distance = centroids[0].ComputeDistance(instance_features);

                for (int cluster_index = 1; cluster_index < centroids.Count; cluster_index++)
                {
                    double distance = centroids[cluster_index].ComputeDistance(instance_features);

                    if (distance.CompareTo(best_distance) == 1)
                    {
                        best_distance = distance;
                        best_cluster_index = cluster_index;
                    }
                }

                if (best_distance.CompareTo(critical_distance) == 1)
                {
                    // create new cluster
                    IList<DomainType[]> new_cluster = new List<DomainType[]>();
                    new_cluster.Add(instance_features);
                    cluster_members.Add(new_cluster);
                    centroids.Add(centroid_calculator.Compute(new_cluster));
                }
                else
                {
                    // add to existing cluster and recompute centroid
                    cluster_members[best_cluster_index].Add(instance_features);
                    centroids[best_cluster_index] = centroid_calculator.Compute(cluster_members[best_cluster_index]);
                }
            }

            return new ClusteringCentroid<DomainType>(data_set.DataContext, centroids);
        }
    }
}