using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
    public class ClusteringHierarchy<DomainType, CentroidType> : AClustering, IClusteringHierarchy<DomainType, CentroidType>
    {
        private ICentroidHierarchy<DomainType, CentroidType> root_node;

        public ClusteringHierarchy(IDataContext data_context, ICentroidHierarchy<DomainType, CentroidType> root_node)
            : base(data_context)
        {
            this.root_node = root_node;
        }

        public IClusteringCentroid<DomainType> GetCentroidClustering(double level)
        {
            throw new NotImplementedException();
        }

    }
}
