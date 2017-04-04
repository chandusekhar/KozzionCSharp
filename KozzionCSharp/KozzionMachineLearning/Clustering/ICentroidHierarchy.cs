using KozzionMachineLearning.Clustering.KMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ICentroidHierarchy<DomainType, DissimilarityType, CentroidType> : ICentroidDistance<DomainType, DissimilarityType>
    {
        DissimilarityType GetDissimilarity(CentroidType other);
    }
}
