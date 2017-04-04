using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Clustering.KMeans
{
    public class ClusteringCentroid<DomainType, DistanceType> : AClustering, IClusteringCentroid<DomainType, DistanceType>
        where DistanceType : IComparable<DistanceType>
    {
        public IReadOnlyList<ICentroidDistance<DomainType, DistanceType>> Clusters { get; private set; }

        public ClusteringCentroid(IDataContext data_context, IList<ICentroidDistance<DomainType, DistanceType>> centroids)
            :base(data_context)
        {
            this.Clusters = new List<ICentroidDistance<DomainType, DistanceType>>(centroids).AsReadOnly();
        }        

        public ICentroidDistance<DomainType, DistanceType> GetCluster(DomainType[] instance_features)
        {
            throw new NotImplementedException();
        }

        public int GetClusterIndex(DomainType[] instance_features)
        {
            throw new NotImplementedException();
        }

        public DistanceType[] Transform(DomainType[] instance_features)
        {
            DistanceType[] projection = new DistanceType[this.Clusters.Count];
            for (int cluster_index = 0; cluster_index < this.Clusters.Count; cluster_index++)
            {
                projection[cluster_index] = Clusters[cluster_index].ComputeDistance(instance_features);
            }
            return projection;
        }
    }
}