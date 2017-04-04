
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image;
using KozzionMachineLearning.Method.SLIC;

namespace KozzionMachineLearningTest.Methods.SLIC
{   
    [TestClass]   
    public class TestSLIC3D
    {
        [TestMethod]
        public void TestInitialize()
        {
            int iteration_count = 1;
            int [] desired_cluster_dimensions = new int []{ 3, 3, 3};
            SLIC3D slick = new SLIC3D(iteration_count, desired_cluster_dimensions, 1);

            Tuple<IImageRaster<IRaster3DInteger, int>, IImageRaster<IRaster3DInteger, float>, IList<int[]>, IList<float[]>> initialization = slick.Initialize(new Raster3DInteger(9, 9, 9));
            IImageRaster<IRaster3DInteger, int> image_labeling = initialization.Item1;
            IImageRaster<IRaster3DInteger, float> image_distance = initialization.Item2;
            IList<int[]> cluster_spatial_centroids = initialization.Item3;
            IList<float[]> cluster_feature_centroids = initialization.Item4;
            Assert.AreEqual(27, cluster_spatial_centroids.Count);
        }


        [TestMethod]
        public void TestComputeCentroids()
        {
            int iteration_count = 1;
            int[] desired_cluster_dimensions = new int[] { 5, 5, 1 };
            SLIC3D slick = new SLIC3D(iteration_count, desired_cluster_dimensions, 1);
            IRaster3DInteger raster = new Raster3DInteger(100, 100, 1);

            IList<IImageRaster<IRaster3DInteger, float>> stack = new List<IImageRaster<IRaster3DInteger, float>>();
            stack.Add(new ImageRaster3D<float>(raster, 0));
            stack.Add(new ImageRaster3D<float>(raster, 0));
            stack.Add(new ImageRaster3D<float>(raster, 0));
            IImageRaster<IRaster3DInteger, float[]> image_features = new ImageRasterWrapperStack<IRaster3DInteger, float>(stack);
            Tuple<IImageRaster<IRaster3DInteger, int>, IImageRaster<IRaster3DInteger, float>, IList<int[]>, IList<float[]>> initialization = slick.Initialize(raster);
            IImageRaster<IRaster3DInteger, int> image_labeling = initialization.Item1;
            IImageRaster<IRaster3DInteger, float> image_distance = initialization.Item2;
            IList<int[]> cluster_spatial_centroids = initialization.Item3;
            IList<float[]> cluster_feature_centroids = initialization.Item4;
            slick.ComputeCentroids(image_labeling, image_features, cluster_spatial_centroids, cluster_feature_centroids);
        }

        [TestMethod]
        public void TestSlick3Cluster()
        {
            int iteration_count = 5;
            int[] desired_cluster_dimensions = new int[] { 5, 1, 1 };
            SLIC3D slick = new SLIC3D(iteration_count, desired_cluster_dimensions, 1);
            IRaster3DInteger raster = new Raster3DInteger(15, 1, 1);
    
            IList<IImageRaster<IRaster3DInteger, float>> stack = new List<IImageRaster<IRaster3DInteger, float>>();
            IImageRaster<IRaster3DInteger, float> feature_image = new ImageRaster3D<float>(raster, 0);
            for (int element_index = 3; element_index < 12; element_index++)
            {
                feature_image.SetElementValue(element_index, 1);
            }
            stack.Add(feature_image);
            IImageRaster<IRaster3DInteger, float[]> image_features = new ImageRasterWrapperStack<IRaster3DInteger, float>(stack);
            Tuple<IImageRaster<IRaster3DInteger, int>, IImageRaster<IRaster3DInteger, float>, IList<int[]>, IList<float[]>> initialization = slick.Initialize(raster);
            IImageRaster<IRaster3DInteger, int> image_labeling = initialization.Item1;
            IImageRaster<IRaster3DInteger, float> image_distance = initialization.Item2;
            IList<int[]> cluster_spatial_centroids = initialization.Item3;
            IList<float[]> cluster_feature_centroids = initialization.Item4;
            Assert.AreEqual(3, cluster_spatial_centroids.Count);
            slick.ComputeCentroids(image_labeling, image_features, cluster_spatial_centroids, cluster_feature_centroids);

            int [] neigbourhood_element_indexes = new int [99];
            for (int iteration_index = 0; iteration_index < iteration_count; iteration_index++)
            {
                slick.AssignElements(image_labeling, image_distance, image_features, cluster_spatial_centroids, cluster_feature_centroids, neigbourhood_element_indexes);
                slick.ComputeCentroids(image_labeling, image_features, cluster_spatial_centroids, cluster_feature_centroids);
            }
            Assert.AreEqual(3, cluster_spatial_centroids.Count);
            for (int element_index = 3; element_index < 12; element_index++)
            {
                Assert.AreEqual(1, feature_image.GetElementValue(element_index));
            }
        }
    }
}
