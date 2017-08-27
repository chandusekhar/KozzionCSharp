using System.Collections.Generic;

namespace KozzionMachineLearning.Clustering
{
    public interface ICentroidDistance<DomainType>
    {
        IList<DomainType[]> Members { get; }

        double ComputeDistance(DomainType[] instance_features);
    }
}