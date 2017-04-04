using KozzionMathematics.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Tools
{
    [TestClass]
    public class ToolsMathConvolutionTest
    {
        [TestMethod]
        public void TestConvolveUniformFull()
        {
            double[] result_0 = ToolsMathConvolution.ConvolveUniformFull(new double[] { 1.0 }, new double[] { 1.0 });
            Assert.AreEqual(1, result_0.Length);
            Assert.AreEqual(1.0, result_0[0]);
    
            double[] result_1 = ToolsMathConvolution.ConvolveUniformFull(new double[] { 1.0, 2.0 }, new double[] { 1.0, 2.0 });
            Assert.AreEqual(3, result_1.Length);
            Assert.AreEqual(1.0, result_1[0]);
            Assert.AreEqual(4.0, result_1[1]);
            Assert.AreEqual(4.0, result_1[2]);

            double[] result_2 = ToolsMathConvolution.ConvolveUniformFull(new double[] { 1.0, 2.0, 3.0, 4.0}, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(7, result_2.Length);
            Assert.AreEqual(1.0, result_2[0]);
            Assert.AreEqual(4.0, result_2[1]);
            Assert.AreEqual(10.0, result_2[2]);
            Assert.AreEqual(20.0, result_2[3]);
            Assert.AreEqual(25.0, result_2[4]);
            Assert.AreEqual(24.0, result_2[5]);
            Assert.AreEqual(16.0, result_2[6]);   
        }

        [TestMethod]
        public void TestConvolveUniformSameUncorrectedUnoffsetted()
        {
            double[] result_0 = ToolsMathConvolution.ConvolveUniformSameUncorrectedUnoffsetted(new double[] { 1.0 }, new double[] { 1.0 });
            Assert.AreEqual(1, result_0.Length);
            Assert.AreEqual(1.0, result_0[0]);

            double[] result_1 = ToolsMathConvolution.ConvolveUniformSameUncorrectedUnoffsetted(new double[] { 1.0, 2.0 }, new double[] { 1.0, 2.0 });
            Assert.AreEqual(2, result_1.Length);
            Assert.AreEqual(1.0, result_1[0]);
            Assert.AreEqual(4.0, result_1[1]);

            double[] result_2 = ToolsMathConvolution.ConvolveUniformSameUncorrectedUnoffsetted(new double[] { 1.0, 2.0, 3.0, 4.0 }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(4, result_2.Length);
            Assert.AreEqual(1.0, result_2[0]);
            Assert.AreEqual(4.0, result_2[1]);
            Assert.AreEqual(10.0, result_2[2]);
            Assert.AreEqual(20.0, result_2[3]);
        }

        [TestMethod]
        public void TestConvolveUniformSameUncorrectedOffsetted()
        {
            double[] result_0 = ToolsMathConvolution.ConvolveUniformSameUncorrectedOffsetted(new double[] { 1.0 }, new double[] { 1.0 });
            Assert.AreEqual(1, result_0.Length);
            Assert.AreEqual(1.0, result_0[0]);

            double[] result_1 = ToolsMathConvolution.ConvolveUniformSameUncorrectedOffsetted(new double[] { 1.0, 2.0 }, new double[] { 1.0, 2.0 });
            Assert.AreEqual(2, result_1.Length);
            Assert.AreEqual(4.0, result_1[0]);
            Assert.AreEqual(4.0, result_1[1]);

            double[] result_2 = ToolsMathConvolution.ConvolveUniformSameUncorrectedOffsetted(new double[] { 1.0, 2.0, 3.0, 4.0 }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(4, result_2.Length);
            Assert.AreEqual(10.0, result_2[0]);
            Assert.AreEqual(20.0, result_2[1]);
            Assert.AreEqual(25.0, result_2[2]);
            Assert.AreEqual(24.0, result_2[3]);
        }

        [TestMethod]
        public void TestConvolveUniformSameCorrectedUnoffsetted()
        {
            double[] result_0 = ToolsMathConvolution.ConvolveUniformSameCorrectedUnoffsetted(new double[] { 1.0 }, new double[] { 1.0 });
            Assert.AreEqual(1, result_0.Length);
            Assert.AreEqual(1.0, result_0[0]);

            double[] result_1 = ToolsMathConvolution.ConvolveUniformSameCorrectedUnoffsetted(new double[] { 1.0, 2.0 }, new double[] { 1.0, 2.0 });
            Assert.AreEqual(2, result_1.Length);
            Assert.AreEqual(3.0, result_1[0]);
            Assert.AreEqual(4.0, result_1[1]);

            double[] result_2 = ToolsMathConvolution.ConvolveUniformSameCorrectedUnoffsetted(new double[] { 1.0, 2.0, 3.0, 4.0 }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(4, result_2.Length);
            Assert.AreEqual(10.0, result_2[0]);
            Assert.AreEqual(40.0 / 3.0, result_2[1]);
            Assert.AreEqual(50.0 / 3.0, result_2[2]);
            Assert.AreEqual(20.0, result_2[3]);
        }

        [TestMethod]
        public void TestConvolveUniformSameCorrectedOffsetted()
        {
            double[] result_0 = ToolsMathConvolution.ConvolveUniformSameCorrectedOffsetted(new double[] { 1.0 }, new double[] { 1.0 });
            Assert.AreEqual(1, result_0.Length);
            Assert.AreEqual(1.0, result_0[0]);

            double[] result_1 = ToolsMathConvolution.ConvolveUniformSameCorrectedOffsetted(new double[] { 1.0, 2.0 }, new double[] { 1.0, 2.0 });
            Assert.AreEqual(2, result_1.Length);
            Assert.AreEqual(4.0, result_1[0]);
            Assert.AreEqual(6.0, result_1[1]);

            double[] result_2 = ToolsMathConvolution.ConvolveUniformSameCorrectedOffsetted(new double[] { 1.0, 2.0, 3.0, 4.0 }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(4, result_2.Length);
            Assert.AreEqual(50.0 / 3.0, result_2[0]);
            Assert.AreEqual(20, result_2[1]);
            Assert.AreEqual(250.0 / 9.0, result_2[2]);
            Assert.AreEqual(240.0 / 7.0, result_2[3]);
        }

        [TestMethod]
        public void TestConvolveUniformValid()
        {
            double[] result_0 = ToolsMathConvolution.ConvolveUniformValid(new double[] { 1.0 }, new double[] { 1.0 });
            Assert.AreEqual(1, result_0.Length);
            Assert.AreEqual(1.0, result_0[0]);

            double[] result_1 = ToolsMathConvolution.ConvolveUniformValid(new double[] { 1.0, 2.0 }, new double[] { 1.0, 2.0 });
            Assert.AreEqual(1, result_1.Length);
            Assert.AreEqual(4.0, result_1[0]);
            
            double[] result_2 = ToolsMathConvolution.ConvolveUniformValid(new double[] { 1.0, 2.0, 3.0, 4.0 }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(1, result_2.Length);
            Assert.AreEqual(20.0, result_2[0]);

            double[] result_3 = ToolsMathConvolution.ConvolveUniformValid(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            Assert.AreEqual(3, result_3.Length);
            Assert.AreEqual(20.0, result_3[0]);
            Assert.AreEqual(30.0, result_3[1]);
            Assert.AreEqual(40.0, result_3[2]);
        }
    }
}
