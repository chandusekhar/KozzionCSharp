using KozzionMathematics.Statistics.Test.MultiSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Statistics.Test.MultiSample
{
    [TestClass]
    public class TestBartlettTest
    {
        [TestMethod]
        public void TestBartlettP0()
        {
            // http://www.itl.nist.gov/div898/handbook/eda/section3/eda3581.htm
            double[] sample_0 = new double[] { 1.006, 0.996, 0.998, 1.000, 0.992, 0.993, 1.002, 0.999, 0.994, 1.000 };
            double[] sample_1 = new double[] { 0.998, 1.006, 1.000, 1.002, 0.997, 0.998, 0.996, 1.000, 1.006, 0.988 };
            double[] sample_2 = new double[] { 0.991, 0.987, 0.997, 0.999, 0.995, 0.994, 1.000, 0.999, 0.996, 0.996 };
            double[] sample_3 = new double[] { 1.005, 1.002, 0.994, 1.000, 0.995, 0.994, 0.998, 0.996, 1.002, 0.996 };
            double[] sample_4 = new double[] { 0.998, 0.998, 0.982, 0.990, 1.002, 0.984, 0.996, 0.993, 0.980, 0.996 };
            double[] sample_5 = new double[] { 1.009, 1.013, 1.009, 0.997, 0.988, 1.002, 0.995, 0.998, 0.981, 0.996 };
            double[] sample_6 = new double[] { 0.990, 1.004, 0.996, 1.001, 0.998, 1.000, 1.018, 1.010, 0.996, 1.002 };
            double[] sample_7 = new double[] { 0.998, 1.000, 1.006, 1.000, 1.002, 0.996, 0.998, 0.996, 1.002, 1.006 };
            double[] sample_8 = new double[] { 1.002, 0.998, 0.996, 0.995, 0.996, 1.004, 1.004, 0.998, 0.999, 0.991 };
            double[] sample_9 = new double[] { 0.991, 0.995, 0.984, 0.994, 0.997, 0.997, 0.991, 0.998, 1.004, 0.997 };
            double[][] samples = new double[][] { sample_0, sample_1, sample_2, sample_3, sample_4, sample_5, sample_6, sample_7, sample_8, sample_9 };
            double p_value = TestBartlett.TestStatic(samples);
            Assert.IsTrue(0.98 < p_value);
            Assert.IsTrue(p_value < 0.99);
        }
    }
}
