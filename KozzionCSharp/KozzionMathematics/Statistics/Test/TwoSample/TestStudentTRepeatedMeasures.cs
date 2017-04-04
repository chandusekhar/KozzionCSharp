using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public class TestStudentTRepeatedMeasures : ATestTwoSample
    {
        public override string TestName { get { return "TestStudentTRepeatedMeasures"; } }

        public override string TestStatisticName { get { return "TestStudentTRepeatedMeasures???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] {
                    TestRequirement.SamplesAreOfEqualSize };
            }
        }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] {
                    TestAssertion.MeasurementsIndependant,
                    TestAssertion.MeasurementsPaired,
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
            return TestStatic(sample_0, sample_1);
        }

        public static double TestStatic(IList<double> sample_0, IList<double> sample_1)
        {
            if (sample_0.Count != sample_1.Count)
            {
                throw new Exception("Samples not of equal size");
            }
            double[] difference = ToolsMathCollection.SubtractElements(sample_0, sample_1);
            double mean_difference = ToolsMathStatistics.Mean(difference);
            double variance = ToolsMathStatistics.Variance(difference);
            double t_statistic = mean_difference / (Math.Sqrt(variance) / Math.Sqrt(sample_0.Count));
            double degrees_of_freedom = (sample_0.Count - 1);

            StudentT distribution = new StudentT(0.1, 1.0, degrees_of_freedom);
            return distribution.CumulativeDistribution(t_statistic);
        }


    }
}