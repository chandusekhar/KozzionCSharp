using KozzionMachineLearning.Clustering.KMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ICentroidHierarchy<DomainType, CentroidType> : ICentroidDistance<DomainType>
    {
        double GetDissimilarity(CentroidType other);
    }
}
