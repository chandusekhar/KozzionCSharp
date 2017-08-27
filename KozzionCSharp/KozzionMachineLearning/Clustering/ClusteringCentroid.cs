using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Clustering.KMeans
{
    public class ClusteringCentroid<DomainType> : AClustering, IClusteringCentroid<DomainType>
    {
        public IReadOnlyList<ICentroidDistance<DomainType>> Clusters { get; private set; }

        public ClusteringCentroid(IDataContext data_context, IList<ICentroidDistance<DomainType>> centroids)
            :base(data_context)
        {
            this.Clusters = new List<ICentroidDistance<DomainType>>(centroids).AsReadOnly();
        }        

        public ICentroidDistance<DomainType> GetCluster(DomainType[] instance_features)
        {
            throw new NotImplementedException();
        }

        public int GetClusterIndex(DomainType[] instance_features)
        {
            throw new NotImplementedException();
        }

        public double[] Transform(DomainType[] instance_features)
        {
            double[] projection = new double[this.Clusters.Count];
            for (int cluster_index = 0; cluster_index < this.Clusters.Count; cluster_index++)
            {
                projection[cluster_index] = Clusters[cluster_index].ComputeDistance(instance_features);
            }
            return projection;
        }
    }
}