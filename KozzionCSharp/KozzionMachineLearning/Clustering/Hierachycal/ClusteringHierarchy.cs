using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
    public class ClusteringHierarchy<DomainType, DissimilarityType, CentroidType> : AClustering, IClusteringHierarchy<DomainType, DissimilarityType, CentroidType>
        where DissimilarityType : IComparable<DissimilarityType>
    {
        private ICentroidHierarchy<DomainType, DissimilarityType, CentroidType> root_node;

        public ClusteringHierarchy(IDataContext data_context, ICentroidHierarchy<DomainType, DissimilarityType, CentroidType> root_node)
            : base(data_context)
        {
            this.root_node = root_node;
        }

        public IClusteringCentroid<DomainType, DissimilarityType> GetCentroidClustering(DissimilarityType level)
        {
            throw new NotImplementedException();
        }

    }
}
