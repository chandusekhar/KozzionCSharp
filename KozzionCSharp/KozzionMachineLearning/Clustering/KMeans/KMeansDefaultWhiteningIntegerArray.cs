using KozzionCore.Tools;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace KozzionMachineLearning.Clustering.KMeans
{
    public class KMeansDefaultWhiteningIntegerArray 
	{
		TemplateClusteringKMeansFloat32                  d_inner;

		public KMeansDefaultWhiteningIntegerArray(
			int cluster_count)
		{
			d_inner = new TemplateClusteringKMeansFloat32 (cluster_count);
		}
		public void get_model(IList<int []> instances)
		{
			// Convert
			List<float []> instances_converted = new List<float []>();
			foreach (int [] instance in instances)
			{
                instances_converted.Add(ToolsCollection.ConvertToFloatArray(instance));
			}

            // Transform
            TransformWhitening<Matrix<double>> transform = new TransformWhitening<Matrix<double>>(new AlgebraLinearReal64MathNet(), ToolsCollection.ConvertToTable(instances_converted));
            IList<float[]> instances_transformed = new List<float[]>();
			foreach (float [] instance in instances_converted)
			{
				instances_transformed.Add(transform.Compute(instance));
			}

			// Cluster
			//IClusteringModel<float []> model = d_inner.get_model(instances_transformed); //TODO

            //return new KMeansDefaultWhiteningIntegerArrayModel(transform, model);
            //return null; // TODO
		}

		public int [] cluster_instances(IList<int []> instances)
		{
			IList<float []> instances_converted = new List<float []>();
			foreach (int [] instance in instances)
			{
                instances_converted.Add(ToolsCollection.ConvertToFloatArray(instance));
			}
            //return d_inner.cluster_instances(instances_converted);
            return null;
		}

	}
}