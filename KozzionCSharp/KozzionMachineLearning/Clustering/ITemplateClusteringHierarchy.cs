using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ITemplateClusteringHierarchy<DomainType, DissimilarityType, ControidType, DataSetType>
        where DissimilarityType : IComparable<DissimilarityType>
    {
        IClusteringHierarchy<DomainType, DissimilarityType, ControidType> Cluster(DataSetType data_set);

    }
}
