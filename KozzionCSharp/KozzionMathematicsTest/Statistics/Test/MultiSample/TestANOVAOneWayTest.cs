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
    public class TestANOVAOneWayTest
    {
        [TestMethod]
        public void TestANOVAOneWayp0()
        {
            // Larsen Marx 4Th editiopn P740

            IList<double> sample_0 = new double[] { 69, 52, 71, 58, 59, 65 };
            IList<double> sample_1 = new double[] { 55, 60, 78, 58, 62, 66 };
            IList<double> sample_2 = new double[] { 66, 81, 70, 77, 57, 79 };
            IList<double> sample_3 = new double[] { 91, 72, 81, 67, 95, 84 };

            IList < IList < double>> samples = new List<IList<double>>();
            samples.Add(sample_0);
            samples.Add(sample_1);
            samples.Add(sample_2);
            samples.Add(sample_3);
            double p_value = TestANOVAOneWay.TestStatic(samples);
            Assert.IsTrue(0.996 < p_value);
            Assert.IsTrue(p_value < 0.997);
        }
    }
}
