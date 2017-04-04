using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KozzionGraphicsTest.ImageIO;
using KozzionGraphics.Tools;
using KozzionGraphics.Image;
using KozzionMathematics.Tools;


namespace KozzionGraphicsTest.Tools
{
    [TestClass]
    public class ToolsDistanceTest
    {

        [TestMethod]
        public void TestDistance1D()
        {
            ImageRaster3D<bool> mask = new ImageRaster3D<bool>(5, 1, 1);
            mask[0] = true;
            mask[3] = true;
            ImageRaster3D<float> distance = ToolsDistance.DistanceTransform3D(mask, new float[] { 1.5f, 1.5f, 1.5f });
            Assert.AreEqual(0.0f, distance[0]);
            Assert.AreEqual(1.5f, distance[1]);
            Assert.AreEqual(1.5f, distance[2]);
            Assert.AreEqual(0.0f, distance[3]);
            Assert.AreEqual(1.5f, distance[4]);
        }


        [TestMethod]
        public void TestDistance2D()
        {
            ImageRaster3D<bool> mask = new ImageRaster3D<bool>(5, 5, 1);
            mask[0] = true;
            mask[6] = true;
            mask[12] = true;
            mask[13] = true;
            mask[19] = true;
            ImageRaster3D<float> distance = ToolsDistance.DistanceTransform3D(mask, new float[] { 1.0f, 1.0f, 1.0f });
            Assert.AreEqual(0.0f, distance[0]);
            Assert.AreEqual(1.0f, distance[1]);
            Assert.AreEqual(ToolsMath.Sqrt(2.0f), distance[2]);
            Assert.AreEqual(2.0f, distance[3]);
            Assert.AreEqual(ToolsMath.Sqrt(5.0f), distance[4]);
            Assert.AreEqual(ToolsMath.Sqrt(8.0f), distance[20]);
        }


        [TestMethod]
        public void TestDistance3DMedium()
        {
            ImageRaster3D<bool> mask = CreateTestMask3D0();
            ImageRaster3D<float> distance = new ImageRaster3D<float>(mask.Raster);
            ToolsDistance.DistanceTransform3DMediumRBA(mask, new float[] { 1.0f, 1.0f, 1.0f }, distance);
            Assert.AreEqual(0.0f, distance[0]);
            Assert.AreEqual(1.0f, distance[1]);
            Assert.AreEqual(ToolsMath.Sqrt(2.0f), distance[2]);
            Assert.AreEqual(2.0f, distance[3]);
            Assert.AreEqual(ToolsMath.Sqrt(5.0f), distance[4]);
            Assert.AreEqual(ToolsMath.Sqrt(8.0f), distance[20]);
            Assert.AreEqual(ToolsMath.Sqrt(24.0f), distance[120]);
            Assert.AreEqual(ToolsMath.Sqrt(17.0f), distance[124]);
        }




        [TestMethod]
        public void TestDistance2DSlow()
        {
            ImageRaster2D<bool> mask = new ImageRaster2D<bool>(5, 5);
            mask[0] = true;
            mask[6] = true;
            mask[12] = true;
            mask[13] = true;
            mask[19] = true;
            ImageRaster2D<float> distance = new ImageRaster2D<float>(mask.Raster);
            ToolsDistance.DistanceTransform2DSlowRBA(mask, new float[] { 1.0f, 1.0f, 1.0f }, distance);
            Assert.AreEqual(0.0f, distance[0]);
            Assert.AreEqual(1.0f, distance[1]);
            Assert.AreEqual(ToolsMath.Sqrt(2.0f), distance[2]);
            Assert.AreEqual(2.0f, distance[3]);
            Assert.AreEqual(ToolsMath.Sqrt(5.0f), distance[4]);
            Assert.AreEqual(ToolsMath.Sqrt(8.0f), distance[20]);
        }


        [TestMethod]
        public void TestDistance2DOosterbroek()
        {
            ImageRaster2D<bool> mask = new ImageRaster2D<bool>(5, 5);
            mask[0] = true;
            mask[6] = true;
            mask[12] = true;
            mask[13] = true;
            mask[19] = true;
            ImageRaster2D<float> distance = new ImageRaster2D<float>(mask.Raster);
            ToolsDistance.DistanceTransform2DOosterbroekRBA(mask, new float[] { 1.0f, 1.0f, 1.0f }, distance);
            Assert.AreEqual(0.0f, distance[0]);
            Assert.AreEqual(1.0f, distance[1]);
            Assert.AreEqual(ToolsMath.Sqrt(2.0f), distance[2]);
            Assert.AreEqual(2.0f, distance[3]);
            Assert.AreEqual(ToolsMath.Sqrt(5.0f), distance[4]);
            Assert.AreEqual(ToolsMath.Sqrt(8.0f), distance[20]);
        }

        [TestMethod]
        public void TestDistance3DOosterbroek0()
        {
            ImageRaster3D<bool> mask = CreateTestMask3D0();
            ImageRaster3D<float> distance_true = new ImageRaster3D<float>(mask.Raster);
            ImageRaster3D<float> distance_test = new ImageRaster3D<float>(mask.Raster);
            float[] voxel_size = new float[] { 1, 1, 1 };
            ToolsDistance.DistanceTransform3DMediumRBA(mask, voxel_size, distance_true);
            ToolsDistance.DistanceTransform3DOosterbroekRBA(mask, voxel_size, distance_test);
            for (int index = 0; index < distance_test.ElementCount; index++)
            {
                Assert.AreEqual(distance_true[index], distance_test[index]);
            }
        }

        [TestMethod]
        public void TestDistance3DOosterbroek1()
        {
            ImageRaster3D<bool> mask = CreateTestMask3D1();
            ImageRaster3D<float> distance_true = new ImageRaster3D<float>(mask.Raster);
            ImageRaster3D<float> distance_test = new ImageRaster3D<float>(mask.Raster);
            float[] voxel_size = new float[] { 0.31f, 0.31f, 1.25f };
            ToolsDistance.DistanceTransform3DMediumRBA(mask, voxel_size, distance_true);
            ToolsDistance.DistanceTransform3DOosterbroekRBA(mask, voxel_size, distance_test);
            float Tol = 0.1f;
            for (int index = 0; index < distance_test.ElementCount; index++)
            {
                Assert.IsTrue( Math.Abs(distance_true[index] - distance_test[index]) < Tol);
            }
        }        

        public static ImageRaster3D<bool> CreateTestMask3D0()
        {
            ImageRaster3D<bool> mask = new ImageRaster3D<bool>(5, 5, 5);
            mask.SetElementValue(0, 0, 0, true);
            mask.SetElementValue(1, 1, 0, true);
            mask.SetElementValue(2, 2, 0, true);
            mask.SetElementValue(3, 2, 0, true);
            mask.SetElementValue(4, 3, 0, true);
            return mask;
        }


        public static ImageRaster3D<bool> CreateTestMask3D1()
        {
            ImageRaster3D<bool> mask = new ImageRaster3D<bool>(75, 75, 75);
            mask.SetElementValue(50, 50, 50, true);
            mask.SetElementValue(51, 51, 50, true);
            mask.SetElementValue(52, 52, 50, true);
            mask.SetElementValue(53, 52, 50, true);
            mask.SetElementValue(54, 53, 50, true);
            mask.SetElementValue(57, 57, 54, true);
            mask.SetElementValue(58, 58, 56, true);
            mask.SetElementValue(50, 60, 50, true);
            return mask;
        }



      
    }
}
