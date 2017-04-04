using KozzionGraphics.Image;
using KozzionGraphics.Tools;
using KozzionPerfusionCL.Experiments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphicsTest.Tools
{
    [TestClass]
    public class ToolsImageRasterTest
    {
        //TODO add asymetric test cases

        [TestMethod]
        public void TestMorphologicalOpeningRBA()
        {
            ImageRaster3D<float> source = new ImageRaster3D<float>(5, 5, 5);
            ImageRaster3D<float> temp = new ImageRaster3D<float>(5, 5, 5);
            ImageRaster3D<float> target = new ImageRaster3D<float>(5, 5, 5);

            source.SetElementValue(2, 2, 2, 1);
            List<int[]> offsets = new List<int[]>();
            offsets.Add(new int[] { 0, 0, 0 });
            StructuringElement3D structure_0 = new StructuringElement3D(offsets);
            ToolsImageRaster.MorphologicalOpeningRBA(source, structure_0, 0, temp, target);
            Assert.AreEqual(1, target.GetElementIndexesWithValue(1).Count);
            offsets.Add(new int[] { 1, 0, 0 });
            offsets.Add(new int[] { -1, 0, 0 });
            offsets.Add(new int[] { 0, 1, 0 });
            offsets.Add(new int[] { 0, -1, 0 });
            offsets.Add(new int[] { 0, 0, 1 });
            offsets.Add(new int[] { 0, 0, -1 });
            StructuringElement3D structure_1 = new StructuringElement3D(offsets);
            ToolsImageRaster.MorphologicalOpeningRBA(source, structure_1, 0, temp, target);
            Assert.AreEqual(0, target.GetElementIndexesWithValue(1).Count);

            //Add cross
            source.SetElementValue(1, 2, 2, 1);
            source.SetElementValue(3, 2, 2, 1);
            source.SetElementValue(2, 1, 2, 1);
            source.SetElementValue(2, 3, 2, 1);
            source.SetElementValue(2, 2, 1, 1);
            source.SetElementValue(2, 2, 3, 1);
            ToolsImageRaster.MorphologicalOpeningRBA(source, structure_1, 0, temp, target);
            Assert.AreEqual(7, target.GetElementIndexesWithValue(1).Count);

            //Add line
            source.SetElementValue(0, 0, 0, 1);
            source.SetElementValue(0, 0, 1, 1);
            source.SetElementValue(0, 0, 2, 1);
            source.SetElementValue(0, 0, 3, 1);
            Assert.AreEqual(7, target.GetElementIndexesWithValue(1).Count);

        }
        [TestMethod]
        public void TestMorphologicalErosionRBA()
        {
            ImageRaster3D<float> source = new ImageRaster3D<float>(5, 5, 5);
            ImageRaster3D<float> target = new ImageRaster3D<float>(5, 5, 5);

            source.SetElementValue(2, 2, 2, 1);
            List<int[]> offsets = new List<int[]>();
            offsets.Add(new int[] { 0, 0, 0 });
            StructuringElement3D structure_0 = new StructuringElement3D(offsets);
            ToolsImageRaster.MorphologicalErosionRBA(source, structure_0, 0, target);
            Assert.AreEqual(1, target.GetElementIndexesWithValue(1).Count);
            offsets.Add(new int[] { 1, 0, 0 });
            offsets.Add(new int[] { -1, 0, 0 });
            offsets.Add(new int[] { 0, 1, 0 });
            offsets.Add(new int[] { 0, -1, 0 });
            offsets.Add(new int[] { 0, 0, 1 });
            offsets.Add(new int[] { 0, 0, -1 });
            StructuringElement3D structure_1 = new StructuringElement3D(offsets);
            ToolsImageRaster.MorphologicalErosionRBA(source, structure_1, 0, target);
            Assert.AreEqual(0, target.GetElementIndexesWithValue(1).Count);
            source.SetElementValue(1, 2, 2, 1);
            source.SetElementValue(3, 2, 2, 1);
            source.SetElementValue(2, 1, 2, 1);
            source.SetElementValue(2, 3, 2, 1);
            source.SetElementValue(2, 2, 1, 1);
            source.SetElementValue(2, 2, 3, 1);
            ToolsImageRaster.MorphologicalErosionRBA(source, structure_1, 0, target);
            Assert.AreEqual(1, target.GetElementIndexesWithValue(1).Count);
        }
        [TestMethod]
        public void TestMorphologicalDilationRBA()
        {
            ImageRaster3D<float> source = new ImageRaster3D<float>(5,5,5);      
            ImageRaster3D<float> target = new ImageRaster3D<float>(5, 5, 5);

            source.SetElementValue(2, 2, 2, 1);
            List<int[]> offsets = new List<int[]>();
            offsets.Add(new int[] { 0, 0, 0});
            StructuringElement3D structure_0 = new StructuringElement3D(offsets);
            ToolsImageRaster.MorphologicalDilationRBA(source, structure_0, 0, target);
            Assert.AreEqual(1, target.GetElementIndexesWithValue(1).Count);
            offsets.Add(new int[] { 1, 0, 0 });
            offsets.Add(new int[] { -1, 0, 0 });
            offsets.Add(new int[] { 0, 1, 0 });
            offsets.Add(new int[] { 0, -1, 0 });
            offsets.Add(new int[] { 0, 0, 1 });
            offsets.Add(new int[] { 0, 0, -1 });
            StructuringElement3D structure_1 = new StructuringElement3D(offsets);
            ToolsImageRaster.MorphologicalDilationRBA(source, structure_1, 0, target);
            Assert.AreEqual(7, target.GetElementIndexesWithValue(1).Count);
        }
    }
}
