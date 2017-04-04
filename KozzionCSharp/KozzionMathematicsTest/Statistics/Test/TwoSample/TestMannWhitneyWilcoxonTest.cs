using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Numeric.linear_solver;
using KozzionMathematics.Numeric.Solver.LinearSolver;
using KozzionMathematics.Statistics.Test.TwoSample;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Statistics.Test
{
    [TestClass]
    public class TestMannWhitneyWilcoxonTest
    {
        [TestMethod]
        public void TestTestMannWhitneyWilcoxonP0()
        {
            // Larsen Marc 4Th editiopn P824
            double[] sample_0 = new double[] { 177, 177, 165, 172, 172, 179, 163, 175, 166, 182, 177, 168, 179, 177};
            double[] sample_1 = new double[] { 166, 154, 159, 168, 174, 174, 177, 167, 165, 161, 164, 161 };
            double p_value = TestMannWhitneyWilcoxon.TestStatic(sample_0, sample_1);
            Assert.IsTrue(p_value < 0.005);
            Assert.IsTrue(0.004 < p_value);
        }


        [TestMethod]
        public void TestTestMannWhitneyWilcoxonP1()
        {
            // experminet CTAP 1
            double[] velt_b = new double[] { 14.58, 20.81, 168.53, 67.40, 2.62, 1.74, 85.00, 94.29, 165.01, 43.03, 74.64, 346.16, 94.38, 129.95, 24.00, 64.50, 33.81, 3.84, 102.54, 64.47, 36.78, 41.27, 52.81, 176.76, 162.11, 9.88, 155.58, 188.31 };
            double[] velt_g = new double[] { 143.74, 14.97, 8.80, 85.56, 4.78, 5.75, 65.97, 12.01, 40.90, 114.84, 7.70, 29.21, 14.25, 39.74, 117.88, 90.93, 278.59, 31.99, 1.57, 102.44, 5.39, 45.08, 50.04, 343.34, 22.18, 12.96, 90.65, 40.40, 96.01, 12.87, 240.75, 146.52, 82.62, 0.90, 27.24, 106.68, 36.65, 98.10, 4.24 };
            double[] velg_b = new double[] { 27.68, 33.85, 29.88, 38.71, 39.63, 8.32, 49.92, 12.01, 55.59, 14.31, 14.21, 9.12, 4.18, 62.38, 56.10, 65.49, 23.41, 10.51, 22.02, 16.57, 21.34, 33.10, 56.28, 62.11, 30.18, 79.45, 63.39, 70.13 };
            double[] velg_g = new double[] { 108.79, 93.19, 13.13, 122.15, 178.51, 53.13, 32.28, 80.97, 34.21, 48.04, 30.60, 92.30, 28.74, 45.41, 27.80, 49.54, 233.67, 16.50, 47.82, 89.74, 31.95, 62.34, 38.59, 133.61, 49.40, 30.11, 70.61, 69.49, 44.81, 39.36, 22.36, 41.04, 24.25, 54.66, 18.80, 47.21, 124.39, 51.10, 33.31 };
            double p_value_t = TestMannWhitneyWilcoxon.TestStatic(velt_b, velt_g);
            double p_value_g = TestMannWhitneyWilcoxon.TestStatic(velg_b, velg_g);
            Assert.IsTrue(0.009 < p_value_t);
            Assert.IsTrue(p_value_t < 0.010);
            Assert.IsTrue(0.999 < p_value_g);
            Assert.IsTrue(p_value_g < 1.000);
        }

    }
}
