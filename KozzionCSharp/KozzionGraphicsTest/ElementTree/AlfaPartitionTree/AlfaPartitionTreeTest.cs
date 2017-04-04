using KozzionGraphics.ElementTree.AlphaPartition;
using KozzionGraphics.ElementTree.MaxTree;
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

namespace KozzionGraphicsTest.ElementTree.AlfaPartitionTree
{
    [TestClass]
    public class AlfaPartitionTreeTest
    {
        [TestMethod]
        public void TestAlphaPartitionTreeBuilderMinTree()
        {
            IMaxTreeBuilder<float> builder_max = new MaxTreeBuilderSingleQueue<float>();
            IAlphaPartitionTreeBuilder<float, float>  builder_alfa_partition = new   AlphaPartitionTreeBuilderMinTree<float, float>(new AlgebraRealFloat32(), builder_max);
            TestTrivial0(builder_alfa_partition);
            TestTrivial1(builder_alfa_partition);
            TestSimple0(builder_alfa_partition);
            TestSimple1(builder_alfa_partition);
        }

        public void TestTrivial0(IAlphaPartitionTreeBuilder<float, float> builder)
        {
            ImageRaster3D<float> image = new ImageRaster3D<float>();
            ITopologyElementEdge element_topology = new TopologyElementRaster3D6Connectivity(image.Raster).GetElementEdgeRasterTopology();
            IFunctionDissimilarity<float, float> edge_function = new FunctionDistanceAbsoluteDifference();
            IAlphaPartitionTree<float> tree = builder.BuildAlphaPartitionTree(element_topology, edge_function, image.GetElementValues(false));
            Assert.AreEqual(1, tree.GetRealElementsIndexesWithMaxAlfa(0, 0).Length);
            Assert.AreEqual(1, tree.GetFullElementsIndexesWithMaxAlfa(0, 0).Length);
        } 
            
        public void TestTrivial1(IAlphaPartitionTreeBuilder<float, float> builder)
        {
            ImageRaster3D<float> image = new ImageRaster3D<float>(10,10,10);
            ITopologyElementEdge element_topology = new TopologyElementRaster3D6Connectivity(image.Raster).GetElementEdgeRasterTopology();
            IFunctionDissimilarity<float, float> edge_function = new FunctionDistanceAbsoluteDifference();
            IAlphaPartitionTree<float> tree = builder.BuildAlphaPartitionTree(element_topology, edge_function, image.GetElementValues(false));
            Assert.AreEqual(1000, tree.GetRealElementsIndexesWithMaxAlfa(0, 0).Length);
            Assert.AreEqual(3700, tree.GetFullElementsIndexesWithMaxAlfa(0, 0).Length);


            ToolsIOSerialization.SerializeToFile(@"E:\Data\Dropbox\Dropbox\TestData\Tree.blb", tree);

        }


        public void TestSimple0(IAlphaPartitionTreeBuilder<float, float> builder)
        {
            ImageRaster3D<float> image = new ImageRaster3D<float>(2, 1, 1, new float [] {0, 1}, false);
            ITopologyElementEdge element_topology = new TopologyElementRaster3D6Connectivity(image.Raster).GetElementEdgeRasterTopology();
            IFunctionDissimilarity<float, float> edge_function = new FunctionDistanceAbsoluteDifference();
            IAlphaPartitionTree<float> tree = builder.BuildAlphaPartitionTree(element_topology, edge_function, image.GetElementValues(false));
            Assert.AreEqual(1, tree.GetRealElementsIndexesWithMaxAlfa(0, 0.0f).Length);
            Assert.AreEqual(1, tree.GetFullElementsIndexesWithMaxAlfa(0, 0.0f).Length);
            Assert.AreEqual(2, tree.GetRealElementsIndexesWithMaxAlfa(0, 1.5f).Length);
            Assert.AreEqual(3, tree.GetFullElementsIndexesWithMaxAlfa(0, 1.5f).Length);
            Assert.AreEqual(1, tree.GetRealElementsIndexesWithMaxAlfa(1, 0.0f).Length);
            Assert.AreEqual(1, tree.GetFullElementsIndexesWithMaxAlfa(1, 0.0f).Length);
            Assert.AreEqual(2, tree.GetRealElementsIndexesWithMaxAlfa(1, 1.5f).Length);
            Assert.AreEqual(3, tree.GetFullElementsIndexesWithMaxAlfa(1, 1.5f).Length);
        }



        public void TestSimple1(IAlphaPartitionTreeBuilder<float, float> builder)
        {
            ImageRaster3D<float> image = new ImageRaster3D<float>(9, 1, 1, new float[] { 1, 2, 2, 3, 4, 2, 4, 4, 1}, false);
            ITopologyElementEdge element_topology = new TopologyElementRaster3D6Connectivity(image.Raster).GetElementEdgeRasterTopology();
            IFunctionDissimilarity<float, float> edge_function = new FunctionDistanceAbsoluteDifference();
            IAlphaPartitionTree<float> tree = builder.BuildAlphaPartitionTree(element_topology, edge_function, image.GetElementValues(false));
            Assert.AreEqual(1, tree.GetRealElementsIndexesWithMaxAlfa(0, 0.5f).Length);
            Assert.AreEqual(5, tree.GetRealElementsIndexesWithMaxAlfa(0, 1.5f).Length);
            Assert.AreEqual(8, tree.GetRealElementsIndexesWithMaxAlfa(0, 2.5f).Length);
            Assert.AreEqual(9, tree.GetRealElementsIndexesWithMaxAlfa(0, 3.5f).Length);

            Assert.AreEqual(2, tree.GetRealElementsIndexesWithMaxAlfa(6, 0.5f).Length);
            Assert.AreEqual(2, tree.GetRealElementsIndexesWithMaxAlfa(6, 1.5f).Length);
            Assert.AreEqual(8, tree.GetRealElementsIndexesWithMaxAlfa(6, 2.5f).Length);
            Assert.AreEqual(9, tree.GetRealElementsIndexesWithMaxAlfa(6, 3.5f).Length);
        } 
    }
}
