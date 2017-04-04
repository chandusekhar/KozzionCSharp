using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public class TestStudentTEqualVariance : ATestTwoSample
    {
        public override string TestName { get { return "TestStudentTEqualVariance"; } }

        public override string TestStatisticName { get { return "TestStudentTEqualVariance???"; } }

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
                    TestAssertion.SamplesHaveEqualVariances};
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

            double mean_1 = ToolsMathStatistics.Mean(sample_0);
            double mean_2 = ToolsMathStatistics.Mean(sample_1);
            double variance = ToolsMathStatistics.VariancePooled(sample_0, sample_1);

            double t_statistic = (mean_1 - mean_2) / (Math.Sqrt(variance) * Math.Sqrt((1.0 / sample_0.Count) + (1.0 / sample_1.Count)));
			int degrees_of_freedom = (sample_0.Count + sample_1.Count) - 2;

            StudentT distribution = new StudentT(0.1, 1.0, degrees_of_freedom);
            return distribution.CumulativeDistribution(-t_statistic);
        }



     
    }
}