using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Numeric.solver.linear_solver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KozzionMathematicsTest.Tools
{
    [TestClass]
    public class ToolsMathLinearRegressionTest
    {
        [TestMethod]
        public void TestLinearRegesion2D0()
        {
            double[] array_x = new double[] { 1, 2, 3, 4 };
            double[] array_y = new double[] { 1, 2, 3, 4 };
            double slope = 0;
            double intercept = 0;
            ToolsMathLinearRegression.LinearRegesion2D(array_x, array_y, out slope, out intercept);
            Assert.AreEqual(1, slope);
            Assert.AreEqual(0, intercept);


            array_x = new double[] { 4, 2, 1, 8 };
            array_y = new double[] { 4, 2, 1, 8 };
            ToolsMathLinearRegression.LinearRegesion2D(array_x, array_y, out slope, out intercept);
            Assert.AreEqual(1, slope);
            Assert.AreEqual(0, intercept);
        }


        [TestMethod]
        public void TestLinearRegesion2D1()
        {
            double[] array_x = new double[] { 22.5867, 32.1380, 35.2100, 45.2020, 56.7907, 66.3023, 72.1531, 77.0046, 82.9991, 90.8921, 100.2835, 107.4833, 112.2747, 117.6054, 124.8846, 134.3533, 146.5220, 157.8951, 165.8712, 179.8755, 194.8358, 203.1848, 221.6798, 240.6766, 240.0721, 240.9096, 249.5503, 254.8084, 259.7760, 343.4806, 434.5163, 308.3983, 368.7436 };
            double[] array_y = new double[] { 1.5400,    2.2532,    2.7141,    3.6176,    4.6203,    5.6477,    6.7507, 8.2355 ,  10.3604,   13.2713  , 16.9068,   20.5094 ,  23.7627,   27.1097,   30.8831,   35.2275 ,  40.3621  , 45.3761  , 49.4898   ,60.0970  , 68.1982   ,74.0544,   83.7352,   93.7495 ,  96.2610 ,  99.4325 , 105.8514  ,110.8796,  115.8668,  158.9793,  207.6487,  154.0527,  193.5244 };
            
            double slope = 0;
            double intercept = 0;
            ToolsMathLinearRegression.LinearRegesion2D(array_x, array_y, out slope, out intercept);
            Assert.AreEqual(0.5508, slope, 0.001);
            Assert.AreEqual(-31.8814, intercept, 0.001);



        }


    }
}
