using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ITemplateClusteringCentroid<DomainType, DataSetType>
    {
        IClusteringCentroid<DomainType> Cluster(DataSetType data_set);
    }
}
