using KozzionMathematics.Statistics.Test.MultiSample;
using KozzionMathematics.Statistics.Test.TwoSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Statistics.MultiSample.Test
{
    [TestClass]
    public class TestFriedmanTest
    {

        [TestMethod]
        public void TestFriedmanP()
        {
            // Larsen Marc 4Th editiopn P833
            double[] sample_0 = new double[] { 5.50, 5.70, 5.60, 5.50, 5.85, 5.55, 5.40, 5.50, 5.15, 5.80, 5.20, 5.55, 5.35, 5.00, 5.50, 5.55, 5.55, 5.50, 5.45, 5.60, 5.65, 6.30};
            double[] sample_1 = new double[] { 5.55, 5.75, 5.50, 5.40, 5.70, 5.60, 5.35, 5.35, 5.00, 5.70, 5.10, 5.45, 5.45, 4.95, 5.40, 5.50, 5.35, 5.55, 5.25, 5.40, 5.55, 6.25};
            double[][] samples = new double[][] { sample_0, sample_1 };
            double p_value = TestFriedman.TestStatic(samples);     
            Assert.IsTrue(0.96 < p_value);
            Assert.IsTrue(p_value < 0.97);
        }
    }
}