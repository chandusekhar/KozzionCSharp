using KozzionGraphics.ElementTree.AlphaPartition;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.Filter;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation.Distance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphicsTest.ElementTree.Filter
{
        [TestClass]
    public class TestFilterAlfaPartitionMean
    {
        [TestMethod]
        public void TestTrival0()
        {
            IMaxTreeBuilder<float> builder_max = new MaxTreeBuilderSingleQueue<float>();
            IAlphaPartitionTreeBuilder<float, float>  builder_alfa_partition = new   AlphaPartitionTreeBuilderMinTree<float, float>(new AlgebraRealFloat32(), builder_max);
  
            ImageRaster3D<float> image = new ImageRaster3D<float>();
            ITopologyElementEdge element_topology = new TopologyElementRaster3D6Connectivity(image.Raster).GetElementEdgeRasterTopology();
            IFunctionDissimilarity<float, float> edge_function = new FunctionDistanceAbsoluteDifference();
            IAlphaPartitionTree<float> tree = builder_alfa_partition.BuildAlphaPartitionTree(element_topology, edge_function, image.GetElementValues(false));

            float [] values_input = new float []{555};
            FilterAlfaPartitionMean<float> filter= new FilterAlfaPartitionMean<float>(new AlgebraRealFloat32());
            float[] values_filtered = filter.Filter(tree, values_input, 0, 0);
            Assert.AreEqual(555, values_filtered[0]);
        }


        [TestMethod]
        public void TestSimple0()
        {
            IMaxTreeBuilder<float> builder_max = new MaxTreeBuilderSingleQueue<float>();
            IAlphaPartitionTreeBuilder<float, float> builder_alfa_partition = new AlphaPartitionTreeBuilderMinTree<float, float>(new AlgebraRealFloat32(), builder_max);


            float[] values_input = new float[] { 1, 2, 2, 3, 4, 2, 4, 4, 1 };
            ImageRaster3D<float> image = new ImageRaster3D<float>(9, 1, 1, values_input,  false);
            ITopologyElementEdge element_topology = new TopologyElementRaster3D6Connectivity(image.Raster).GetElementEdgeRasterTopology();
            IFunctionDissimilarity<float, float> edge_function = new FunctionDistanceAbsoluteDifference();
            IAlphaPartitionTree<float> tree = builder_alfa_partition.BuildAlphaPartitionTree(element_topology, edge_function, image.GetElementValues(false));

 
            FilterAlfaPartitionMean<float> filter = new FilterAlfaPartitionMean<float>(new AlgebraRealFloat32());
            int max_size = 1000;
            float[] values_filtered05 = filter.Filter(tree, values_input, max_size, 0.5f);
            float[] values_filtered15 = filter.Filter(tree, values_input, max_size, 1.5f);
            float[] values_filtered25 = filter.Filter(tree, values_input, max_size, 2.5f);
        }


    }
}
