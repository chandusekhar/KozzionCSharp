using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    //Larsen Marx 4th edition P569
    // AKA F-test
    public class TestFisherSnedecor :ATestTwoSample
    {
        public override string TestName { get { return "TestFisherSnedecor"; } }

        public override string TestStatisticName { get { return "TestFisherSnedecor???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { };
            }
        }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] { TestAssertion.MeasurementsIndependant, TestAssertion.SamplesDrawnFromNormalDistribution };
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesHaveEqualVariances;
            }
        }

      
        public override double Test(IList<double> sample_0, IList<double> sample_1)
        {
            return TestStatic(sample_0, sample_1);
        }

        public static double TestStatic(IList<double> sample_0, IList<double> sample_1)
        {
            // as in https://en.wikipedia.org/wiki/F-test_of_equality_of_variances
            // and
            //Larsen Marx 4th edition P569
            double variance_0 = ToolsMathStatistics.Variance(sample_0);
            double variance_1 = ToolsMathStatistics.Variance(sample_1);
            double f_statistic = variance_0 / variance_1;
            double degrees_of_freedom_0 = (sample_0.Count - 1);
            double degrees_of_freedom_1 = (sample_1.Count - 1);
            return FisherSnedecor.CDF(degrees_of_freedom_0, degrees_of_freedom_1, f_statistic);
        }

    }
}
