using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface IClusteringCentroid<DomainType, DistanceType> : IClustering
    {
        IReadOnlyList<ICentroidDistance<DomainType, DistanceType>> Clusters { get; }

        ICentroidDistance<DomainType, DistanceType> GetCluster(DomainType[] instance_features);

        DistanceType [] Transform(DomainType[] instance_features);

        int GetClusterIndex(DomainType[] instance_features);
    }
}
