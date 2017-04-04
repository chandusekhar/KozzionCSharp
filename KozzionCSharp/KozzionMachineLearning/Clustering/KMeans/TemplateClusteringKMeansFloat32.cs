using KozzionMachineLearning.Clustering.KMeans;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function.Implementation.Distance;

namespace KozzionMachineLearning.Clustering.KMeans
{
    public class TemplateClusteringKMeansFloat32 : TemplateClusteringKMeans<float, float, IDataSet<float>>
	{

		public TemplateClusteringKMeansFloat32 (
			int cluster_count):
            base(new TemplateCentroidCalculatorMeanFloatArray(new FunctionDistanceEuclidean()), cluster_count)
		{
			
		}

		public TemplateClusteringKMeansFloat32 (
			int cluster_count,
			int max_iteration_count):
            base(new TemplateCentroidCalculatorMeanFloatArray(new FunctionDistanceEuclidean()), cluster_count, max_iteration_count)
		{
	
		}

	}
}
