using System;

namespace KozzionMachineLearning.Clustering
{
	public interface IClusteringHierarchy<DomainType, DissimilarityType, CentroidType> : IClustering
        where DissimilarityType : IComparable<DissimilarityType>
	{
        IClusteringCentroid<DomainType, DissimilarityType> GetCentroidClustering(DissimilarityType level);
    
    }
}
