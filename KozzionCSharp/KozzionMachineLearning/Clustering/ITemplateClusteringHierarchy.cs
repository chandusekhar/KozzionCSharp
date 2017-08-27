using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ITemplateClusteringHierarchy<DomainType, ControidType, DataSetType>
    {
        IClusteringHierarchy<DomainType, ControidType> Cluster(DataSetType data_set);

    }
}
