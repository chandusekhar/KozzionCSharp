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
    public class TestANOVARepeatedMeasuresTest
    {
        [TestMethod]
        public void TestANOVARepeatedMeasuresp0()
        {
            // Larsen Marx 4Th editiopn P779

            IList<double> sample_0 = new double[] { 8, 11, 9, 16, 24};
            IList<double> sample_1 = new double[] { 2, 1, 12, 11, 19 };
            IList<double> sample_2 = new double[] { -2, 0, 6, 2 , 11};

            IList < IList < double>> samples = new List<IList<double>>();
            samples.Add(sample_0);
            samples.Add(sample_1);
            samples.Add(sample_2);
            Tuple< double ,double >p_values = TestANOVARepeatedMeasures.TestStatic(samples);
            Assert.IsTrue(0.998 < p_values.Item1);
            Assert.IsTrue(p_values.Item1<  0.999);
            Assert.IsTrue(0.998 < p_values.Item2);
            Assert.IsTrue(p_values.Item2 <0.999);
        }


        [TestMethod]
        public void TestANOVAReapetedMeasuresp1()
        {
            // Larsen Marx 4Th editiopn P781

            IList<double> sample_0 = new double[] { 13.8, 12.9, 25.9, 18.0, 15.2 };
            IList<double> sample_1 = new double[] { 11.7, 16.7, 29.8, 23.1, 20.2 };
            IList<double> sample_2 = new double[] { 14.0, 15.5, 27.8, 23.0, 19.0 };
            IList<double> sample_3 = new double[] { 12.6, 13.8, 25.0, 16.9, 13.7 };

            IList<IList<double>> samples = new List<IList<double>>();
            samples.Add(sample_0);
            samples.Add(sample_1);
            samples.Add(sample_2);
            samples.Add(sample_3);

            Tuple<double, double> p_values = TestANOVARepeatedMeasures.TestStatic(samples);
            Assert.IsTrue(0.995 < p_values.Item1);
            Assert.IsTrue(p_values.Item1 < 0.996);
            Assert.IsTrue(0.999 < p_values.Item2);
            Assert.IsTrue(p_values.Item2 < 1.000);
        }
    }
}