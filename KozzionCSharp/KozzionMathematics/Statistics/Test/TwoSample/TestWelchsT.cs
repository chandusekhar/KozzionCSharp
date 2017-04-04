using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    // After https://en.wikipedia.org/wiki/Welch%27s_t-test
    public class TestWelchsT : ATestTwoSample
    {
        public override string TestName { get { return "TestWelchsTTest"; } }

        public override string TestStatisticName { get { return "TestWelchsTTest???"; } }

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
                return new TestAssertion[] {
                    TestAssertion.MeasurementsIndependant,
                    TestAssertion.SamplesDrawnFromNormalDistribution,
                    TestAssertion.SamplesHaveEqualVariances,
                    TestAssertion.SamplesHaveEqualSizes};
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesHaveEqualMeans;
            }
        }

        public override double Test(IList<double> sample_0, IList<double> sample_1)
        {

            double mean_0 = ToolsMathStatistics.Mean(sample_0);
            double mean_1 = ToolsMathStatistics.Mean(sample_1);
            double variance_0 = ToolsMathStatistics.Variance(sample_0, mean_0);
            double variance_1 = ToolsMathStatistics.Variance(sample_0, mean_1);


            double t_statistic = (mean_0 - mean_1) / Math.Sqrt((variance_0 / sample_0.Count) + (variance_1 / sample_1.Count));

            //Welch–Satterthwaite equation:
            double dof_nominator = ToolsMath.Sqr((variance_0 / sample_0.Count) + (variance_1 / sample_1.Count));
            double dof_denominator = 
            (Math.Pow(variance_0, 4) / (sample_0.Count * sample_0.Count * (sample_0.Count - 1))) +
            (Math.Pow(variance_1, 4) / (sample_1.Count * sample_1.Count * (sample_1.Count - 1)));
            double degrees_of_freedom = dof_nominator / dof_denominator;

            StudentT distribution = new StudentT(0.1, 1.0, degrees_of_freedom);
            return distribution.CumulativeDistribution(-t_statistic);
        }


    


    }
}