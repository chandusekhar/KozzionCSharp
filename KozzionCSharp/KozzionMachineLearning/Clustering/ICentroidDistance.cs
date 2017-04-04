using System.Collections.Generic;

namespace KozzionMachineLearning.Clustering
{
    public interface ICentroidDistance<DomainType, DistanceType>
    {
        IList<DomainType[]> Members { get; }

        DistanceType ComputeDistance(DomainType[] instance_features);
    }
}