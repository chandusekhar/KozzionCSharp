using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering.KMeans
{
    public class TemplateClusteringKMeansNominal : TemplateClusteringKMeans<int, double, IDataSet<int>>
    {
        public TemplateClusteringKMeansNominal(int desired_cluster_count)
            : base(new TemplateCentroidCalculatorNominal(), desired_cluster_count)
        {
        }
      
    }
}
