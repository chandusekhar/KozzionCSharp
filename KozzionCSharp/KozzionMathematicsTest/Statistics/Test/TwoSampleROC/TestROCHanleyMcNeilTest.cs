using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Numeric.linear_solver;
using KozzionMathematics.Numeric.Solver.LinearSolver;
using KozzionMathematics.Statistics.Test.TwoSample;
using KozzionMathematics.Statistics.Test.TwoSampleROC;
using KozzionMathematics.Tools;
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
    public class TestROCHanleyMcNeilTest
    {
        [TestMethod]
        public void TestROCHanleyMcNeilTestP0()
        {
            // Larsen Marc 4Th editiopn P824
            Random random = new Random(0);
            double[] sample_0 = new double[] { 177,    177, 165, 172,    172, 179,     163, 175,    166,   182, 177, 168,     179, 177 };
            double[] sample_1 = new double[] { 166,    154, 159, 168,    174, 174,     177, 167,    165,   161, 164, 161,     179, 177 };
            bool[] labels     = new bool[]   { true, false, true, false, true, false, true, false, true, false, true, false, true, false};
            double p_value = TestROCHanleyMcNeil.TestStatic(labels, sample_0, sample_1, random, 1000);
            Assert.IsTrue(0.10 < p_value);
            Assert.IsTrue(p_value < 0.11);
        }

        [TestMethod]
        public void TestROCHanleyMcNeilTestComputeStandardError0()
        {
            Random random = new Random(0);
            double[] sample_0 = new double[] { 177, 177, 165, 172, 172, 179, 163, 175, 166, 182, 177, 168, 179, 177 };
            bool[] labels = new bool[] { true, false, true, false, true, false, true, false, true, false, true, false, true, false };
            double auc_0 = ToolsMathStatistics.ComputeROCAUCTrapeziod(labels, sample_0);
            double se_0 = TestROCHanleyMcNeil.ComputeStandardError(auc_0, sample_0, labels, random, 1000);
            Assert.IsTrue(0.145< se_0);
            Assert.IsTrue(se_0 < 0.146);
        }

        // as in : [The Meaning and Use of the Area nder a Receiver Operating Characteristic(ROC) Curve] p31
        // Some of the values need to be changed in order to make the math make sence
        [TestMethod]
        public void TestROCHanleyMcNeilTestComputeStandardCombine0()
        {

            double theta = 0.887877;// Paper says 0.893;
            double Q1 = 0.78908; // Paper says 0.8192;
            double Q2 = 0.8215443; // Paper says 0.8313;
            double na = 51;
            double nn = 58;
            //se_0 = 0.0262 (about) instead of 0.032 as stated
            double se_0 = Math.Sqrt(((theta * (1 - theta)) + ((na - 1) * (Q1 - (theta * theta))) + ((nn - 1) * (Q2 - (theta * theta)))) / (na * nn));
            Assert.IsTrue(0.0262 < se_0);
            Assert.IsTrue(se_0 < 0.0263);
        }

    
        [TestMethod]
        public void TestROCHanleyMcNeilTestKendrall0()
        {
            //After matlab example
            double[] a = new double[]{ 0.5201,   -0.0200,   -0.0348,   -0.7982,    1.0187,   -0.1332,   -0.7145,    1.3514,   -0.2248,   -0.5890,   -0.2938,   -0.8479,   -1.1201,    2.5260,    1.6555,    0.3075,   -1.2571,   -0.8655,   -0.1765,    0.7914};
            double[] b = new double[]{ -1.3320,   -2.3299,   -1.4491,    0.3335 ,   0.3914,    0.4517,   -0.1303,    0.1837,   -0.4762,    0.8620,   -1.3617,    0.4550,   -0.8487,   -0.3349,    0.5528,    1.0391,   -1.1176,    1.2607,    0.6601,   -0.0679};
            double kendrall = TestROCHanleyMcNeil.ComputeKendalTauA(a, b); // = -0.0211
            Assert.IsTrue(-0.022 < kendrall);
            Assert.IsTrue(kendrall < -0.021);
        }


        [TestMethod]
        public void TestROCHanleyMcNeilTestKendrall1()
        {
            //After matlab example
            double[] c = new double[] { -0.1952, -0.2176, -0.3031, 0.0230, 0.0513, 0.8261, 1.5270, 0.4669, -0.2097, 0.6252, 0.1832, -1.0298, 0.9492, 0.3071, 0.1352, 0.5152, 0.2614, -0.9415, -0.1623, -0.1461 };
            double[] d = new double[] { -0.5320, 1.6821, -0.8757, -0.4838, -0.7120, -1.1742, -0.1922, -0.2741, 1.5301, -0.2490, -1.0642, 1.6035, 1.2347, -0.2296, -1.5062, -0.4446, -0.1559, 0.2761, -0.2612, 0.4434 };
            double kendrall = TestROCHanleyMcNeil.ComputeKendalTauA(c, d); // = -0.1684
            Assert.IsTrue(-0.17 < kendrall);
            Assert.IsTrue(kendrall < -0.16);
        }

        [TestMethod]
        public void TestROCHanleyMcNeilTestKendrall2()
        {
            //After matlab example
            double[] c = new double[] { 1, 2, 3, -1, 0.0230,   0.0513,  0.8261, 1.5270 };
            double[] d = new double[] { 2, 1, 3, -2, -0.4838, -0.7120, -1.1742, -0.1922 };
            double kendrall = TestROCHanleyMcNeil.ComputeKendalTauA(c, d); // = 0.33 
            Assert.IsTrue(0.64 < kendrall);
            Assert.IsTrue(kendrall < 0.65);
        }
    }
}
