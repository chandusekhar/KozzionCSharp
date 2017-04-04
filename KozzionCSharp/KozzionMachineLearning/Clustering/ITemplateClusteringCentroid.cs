using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ITemplateClusteringCentroid<DomainType, DistanceType, DataSetType>
        where DistanceType : IComparable<DistanceType>
    {
        IClusteringCentroid<DomainType, DistanceType> Cluster(DataSetType data_set);
    }
}
