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
    public class TestIndependanceTest
    {
        [TestMethod]
        public void TestIndependanceP()
        {
            // Larsen Marc 4Th editiopn P632

            IList<double> sample_0 = new List<double>();
            IList<double> sample_1 = new List<double>();

            for (int index = 0; index < 70; index++)
            {
                sample_0.Add(1.99);
            }

            for (int index = 0; index < 39; index++)
            {
                sample_0.Add(2.00);
            }

            for (int index = 0; index < 14; index++)
            {
                sample_0.Add(2.50);
            }

            for (int index = 0; index < 13; index++)
            {
                sample_0.Add(3.00);
            }


            for (int index = 0; index < 65; index++)
            {
                sample_1.Add(1.99);
            }

            for (int index = 0; index < 28; index++)
            {
                sample_1.Add(2.00);
            }

            for (int index = 0; index < 3; index++)
            {
                sample_1.Add(2.50);
            }

            for (int index = 0; index < 2; index++)
            {
                sample_1.Add(3.00);
            }

            IList < IList < double>> samples = new List<IList<double>>();
            samples.Add(sample_0);
            samples.Add(sample_1);
            double p_value = TestIndependance.TestStatic(samples, new double[] { 2.00, 2.50, 3.00});
            Assert.IsTrue(0.989 < p_value);
            Assert.IsTrue(p_value < 0.990);
        }
    }
}
