using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface IClusteringCentroid<DomainType> : IClustering
    {
        IReadOnlyList<ICentroidDistance<DomainType>> Clusters { get; }

        ICentroidDistance<DomainType> GetCluster(DomainType[] instance_features);

        double[] Transform(DomainType[] instance_features);

        int GetClusterIndex(DomainType[] instance_features);
    }
}
