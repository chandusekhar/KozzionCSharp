using KozzionCore.Tools;
using KozzionMachineLearning.Clustering.KMeans;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace KozzionMachineLearning.Clustering.KMeans
{

    public class TemplateClusteringKMeans<DomainType, DistanceType, DataSetType> : ITemplateClusteringCentroid<DomainType, DistanceType, IDataSet<DomainType>>
        where DistanceType : IComparable<DistanceType>
    {
        private ITemplateCentroidCalculator<DomainType, ICentroidDistance<DomainType, DistanceType>> centroid_calculator_template;
        private int desired_cluster_count;
        private int d_max_iteration_count;

        public TemplateClusteringKMeans(
            ITemplateCentroidCalculator<DomainType, ICentroidDistance<DomainType, DistanceType>> centroid_calculator_template,
            int cluster_count,
            int max_iteration_count)
        {
            this.centroid_calculator_template = centroid_calculator_template;
            desired_cluster_count = cluster_count;
            d_max_iteration_count = max_iteration_count;
        }


        public TemplateClusteringKMeans(
            ITemplateCentroidCalculator<DomainType, ICentroidDistance<DomainType, DistanceType>> centroid_calculator_template,
            int cluster_count)
            : this(centroid_calculator_template, cluster_count, int.MaxValue)
        {

        }

     
     
        private void Cluster(
            IList<DomainType []> instance_features_list,
            IFunction<IList<DomainType[]>, ICentroidDistance<DomainType, DistanceType>> centroid_calculator,
            IList<ICentroidDistance<DomainType, DistanceType>> centroids)
        {

            int[] centroid_assignment = new int[instance_features_list.Count];       
            int iterations = 0;
            int reassignments = 1;
            while ((reassignments != 0) && (iterations < d_max_iteration_count))
            {
                reassignments = 0;
                iterations++;       
                // assign to centroids
                for (int index_instance = 0; index_instance < instance_features_list.Count; index_instance++)
                {
                    DomainType [] instance_features = instance_features_list[index_instance];

                    int best_centroid_index = 0;
                    DistanceType best_distance = centroids[0].ComputeDistance(instance_features);

                    for (int centroid_index = 1; centroid_index < centroids.Count; centroid_index++)
                    {
                        DistanceType distance = centroids[centroid_index].ComputeDistance(instance_features);

                        if (distance.CompareTo(best_distance) == -1)
                        {
                            best_distance = distance;
                            best_centroid_index = centroid_index;
                        }
                    }

                    if (centroid_assignment[index_instance] != best_centroid_index)
                    {
                        centroid_assignment[index_instance] = best_centroid_index;
                        reassignments++;
                    }
                }


                if (reassignments != 0)
                {
                    // reset clusters
                    int old_centroid_count = centroids.Count;
                    centroids.Clear();
                    for (int centroid_index = 0; centroid_index < old_centroid_count; centroid_index++)
                    {
                        IList<DomainType[]> centroid_members = new List<DomainType[]>();
                        for (int index_instance = 0; index_instance < instance_features_list.Count; index_instance++)
                        {
                            if (centroid_assignment[index_instance] == centroid_index)
                            {
                                centroid_members.Add(instance_features_list[index_instance]);
                            }
                        }

                        if (centroid_members.Count != 0)
                        {
                            centroids.Add(centroid_calculator.Compute(centroid_members));
                        }
                    }
                }
               
            }
        }
    
        public IClusteringCentroid<DomainType, DistanceType> Cluster(IDataSet<DomainType> data_set)
        {
            IFunction<IList<DomainType[]>, ICentroidDistance<DomainType, DistanceType>> centroid_calculator = centroid_calculator_template.Generate(data_set.DataContext);
            IList<DomainType[]> instance_features_list = data_set.FeatureData;
            RandomNumberGenerator generator = new RNGCryptoServiceProvider();
            IList<ICentroidDistance<DomainType, DistanceType>> centroids = new List<ICentroidDistance<DomainType, DistanceType>>();
            int[] permutation = generator.RandomPermutation(instance_features_list.Count);

            for (int centroid_index = 0; centroid_index < this.desired_cluster_count; centroid_index++)
            {
                IList<DomainType[]> centroid_members = new List<DomainType[]>();
                centroid_members.Add(instance_features_list[permutation[centroid_index]]);

                if (centroid_members.Count != 0)
                {
                    centroids.Add(centroid_calculator.Compute(centroid_members));
                }
            }

            Cluster(instance_features_list, centroid_calculator, centroids);
            return new ClusteringCentroid<DomainType, DistanceType>(data_set.DataContext, centroids);
        }

   
    }
}