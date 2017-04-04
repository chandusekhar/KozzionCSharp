using KozzionMathematics.Statistics.Test.OneSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Statistics.Test.OneSample
{
    [TestClass]
    public class TestSampleIndependanceRunCountTest
    {
        [TestMethod]
        public void TestSampleIndependanceRunCountTestP()
        {
            // Larsen Marx 4Th edition P837
            double[] sample_0 = new double[] { 61,53,58,51,52,34,45,52,46,52,37,39,50,38,55,59,57,64,73,46,48,47,40,35,40 };
            double p_value = TestRunCount.TestStatic(sample_0);
            Assert.IsTrue(p_value < 0.21);
            Assert.IsTrue(0.20 < p_value);
        }
    }
}
