using KozzionMathematics.Statistics.Test.TwoSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Statistics.Test
{
    [TestClass]
    public class TestStudentTRepeatedMeasuresTest
    {

        [TestMethod]
        public void TestTestStudentTRepeatedMeasuresP0()
        {
            // Larsen Marc 4Th editiopn P790
            double[] sample_0 = new double[] { 14.6, 17.3, 10.9, 12.8, 16.6, 12.2, 11.2, 15.4, 14.8, 16.2};
            double[] sample_1 = new double[] { 13.8, 15.4, 11.3, 11.6, 16.4, 12.6, 11.8, 15.0, 14.4, 15.0};
            double p_value = TestStudentTRepeatedMeasures.TestStatic(sample_0, sample_1);     
            Assert.IsTrue(0.94 < p_value);
            Assert.IsTrue(p_value < 0.95);
        }
    }
}