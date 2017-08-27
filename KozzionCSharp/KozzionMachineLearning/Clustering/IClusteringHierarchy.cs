using System;

namespace KozzionMachineLearning.Clustering
{
	public interface IClusteringHierarchy<DomainType, CentroidType> : IClustering
	{
        IClusteringCentroid<DomainType> GetCentroidClustering(double level);
    
    }
}
