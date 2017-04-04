using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionCore.Collections;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Topology;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KozzionGraphicsTest.ElementTree.MaxTree
{
    [TestClass]
    public class MaxTreeBuilderTest
    {
        public void TestDefaultBuilderTrivial0(IMaxTreeBuilder<int> builder)
        {
            int[] data = new int[] { 0, 0, 0, 0 };
            IImageRaster2D<int> image = new ImageRaster2D<int>(2, 2, data, false);
            ITopologyElement topology = new TopologyElementRaster2D4Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);
        }

        public void TestDefaultBuilderTrivial1(IMaxTreeBuilder<int> builder)
        {
            int[] data = new int[] { 0, 1, 1, 2 };
            IImageRaster2D<int> image = new ImageRaster2D<int>(2, 2, data, false);
            ITopologyElement topology = new TopologyElementRaster2D4Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);
            int[] element_indexes_0 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(0);
            Assert.AreEqual(4, element_indexes_0.Length);
            int[] element_indexes_1 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(1);
            Assert.AreEqual(3, element_indexes_1.Length);
            int[] element_indexes_2 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(2);
            Assert.AreEqual(3, element_indexes_2.Length);
            int[] element_indexes_3 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(3);
            Assert.AreEqual(1, element_indexes_3.Length);
        }

        public void TestDefaultBuilderTrivial2(IMaxTreeBuilder<int> builder)
        {
            int[] data = new int[] { 0, 1, 1, 2, 5, 6, 7, 7, 2, 2, 1, 0, 0, 0, 0, 0 };
            IImageRaster2D<int> image = new ImageRaster2D<int>(4, 4, data, false);
            ITopologyElement topology = new TopologyElementRaster2D4Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);

            int[] element_indexes_0 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(1);
            Assert.AreEqual(10, element_indexes_0.Length);
            int[] element_indexes_1 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(4);
            Assert.AreEqual(4, element_indexes_1.Length);
            int[] element_indexes_2 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(6);
            Assert.AreEqual(2, element_indexes_2.Length);
            int[] element_indexes_3 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(15);
            Assert.AreEqual(16, element_indexes_3.Length);
        }

        public void TestDefaultBuilderTrivial3(IMaxTreeBuilder<int> builder)
        {
            int[] data = new int[] { 1, 2, 2, 0, 2, 1};
            IImageRaster2D<int> image = new ImageRaster2D<int>(2, 3, data, false);
            ITopologyElement topology = new TopologyElementRaster2D4Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);

            int[] element_indexes_0 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(1);
            Assert.AreEqual(1, element_indexes_0.Length);
            int[] element_indexes_1 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(4);
            Assert.AreEqual(2, element_indexes_1.Length);
            int[] element_indexes_2 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(0);
            Assert.AreEqual(5, element_indexes_2.Length);
            int[] element_indexes_3 = maxtree.GetFullElementsIndexesOfElementLevelAndAbove(3);
            Assert.AreEqual(6, element_indexes_3.Length);
        }

        public void TestDefaultBuilderTrivial4(IMaxTreeBuilder<int> builder)
        {
            int[] data = new int[] { 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 };
            IImageRaster3D<int> image = new ImageRaster3D<int>(3, 3, 1, data, false);
            ITopologyElement topology = new TopologyElementRaster3D6Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);
            maxtree.GetDisplayValues();
        }

        [TestMethod]
        public void TestMaxTreeBuilderSingleQueue()
        {
            IMaxTreeBuilder<int> builder = new MaxTreeBuilderSingleQueue<int>();
            TestDefaultBuilderTrivial0(builder);
            TestDefaultBuilderTrivial1(builder);
            TestDefaultBuilderTrivial2(builder);
            TestDefaultBuilderTrivial3(builder);
            TestDefaultBuilderTrivial4(builder);
        }
   
        [TestMethod]
        public void TestMaxTreeBuilderMultiQueue()
        {
            IMaxTreeBuilder<int> builder = new MaxTreeBuilderMultiQueue<int>();
            TestDefaultBuilderTrivial0(builder);
            TestDefaultBuilderTrivial1(builder);
            TestDefaultBuilderTrivial2(builder);
            TestDefaultBuilderTrivial3(builder);
        }

        [TestMethod]
        public void TestMaxTreeBuilderSingleQueueBig()
        {
            IMaxTreeBuilder<int> builder = new MaxTreeBuilderSingleQueue<int>();
            TestDefaultBuilderBig(builder);
        }

        [TestMethod]
        public void TestMaxTreeBuilderMultiQueueBig()
        {
            IMaxTreeBuilder<int> builder = new MaxTreeBuilderMultiQueue<int>();
            TestDefaultBuilderBig(builder);
        }

        [TestMethod]
        public void TestMaxTreeBuilderSingleQueueVeryBig()
        {
            IMaxTreeBuilder<int> builder = new MaxTreeBuilderSingleQueue<int>();
            TestDefaultBuilderVeryBig(builder);
        }

        [TestMethod]
        public void TestMaxTreeBuilderMultiQueueVeryBig()
        {
            IMaxTreeBuilder<int> builder = new MaxTreeBuilderMultiQueue<int>();
            TestDefaultBuilderVeryBig(builder);
        }

        public void TestDefaultBuilderBig(IMaxTreeBuilder<int> builder)
        {
            Random random = new Random(0);

            int[] data = new int [512*512];
            for (int index = 1; index < data.Length; index++)
            {
                data[index] = (int)(random.NextDouble() * 100.0);
            }
            IImageRaster2D<int> image = new ImageRaster2D<int>(512, 512, data, false);
            ITopologyElement topology = new TopologyElementRaster2D4Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);
            maxtree.GetFullElementsIndexesOfElementLevelAndAbove(0);
            maxtree.GetFullElementsIndexesOfElementLevelAndAbove(500);
            maxtree.GetFullElementsIndexesOfElementLevelAndAbove(1800);
        }

        public void TestDefaultBuilderVeryBig(IMaxTreeBuilder<int> builder)
        {
            Random random = new Random(0);

            int[] data = new int[512 * 512 * 10];
            for (int index = 1; index < data.Length; index++)
            {
                data[index] = (int)(random.NextDouble() * 100.0);
            }
            IImageRaster3D<int> image = new ImageRaster3D<int>(512, 512, 10, data, false);
            ITopologyElement topology = new TopologyElementRaster3D6Connectivity(image.Raster);
            IMaxTree<int> maxtree = builder.BuildMaxTree(data, new ComparerNatural<int>(), topology, data.Length);
 
        }


        [TestMethod]
        public void TestPriorityQueue()
        {

            IComparer<int> comparer_value = new ComparerNatural<int>();
            Assert.AreEqual(1, comparer_value.Compare(5, -1));
            IComparer<int> comparer_index = new ComparerArrayIndex<int>(new ComparerNatural<int>(), new int[] { 5, -1});
            Assert.AreEqual(1, comparer_index.Compare(0, 1));

            int[] values = new int[] { 0, 1, 1, 2, -5, 6, 7, 7, 19, -20 };
            PriorityQueueC5<int> queue = new PriorityQueueC5<int>(new ComparerArrayIndex<int>(new ComparerNatural<int>(), values));
            queue.Enqueue(0);
            queue.Enqueue(4);
            queue.Enqueue(8);
            Assert.AreEqual(8, queue.DequeueLast());
            Assert.AreEqual(0, queue.DequeueLast());
            Assert.AreEqual(4, queue.DequeueLast());
            queue.Enqueue(4);
            queue.Enqueue(4);
            queue.Enqueue(8);
            queue.Enqueue(4);
            queue.Enqueue(9);
            Assert.AreEqual(8, queue.DequeueLast());
            Assert.AreEqual(4, queue.DequeueLast());
            Assert.AreEqual(4, queue.DequeueLast());
            Assert.AreEqual(4, queue.DequeueLast());
            Assert.AreEqual(9, queue.DequeueLast());

        }


    }
}
