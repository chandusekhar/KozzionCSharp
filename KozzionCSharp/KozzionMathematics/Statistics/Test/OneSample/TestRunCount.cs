using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.OneSample
{
    public class TestRunCount : ATestOneSample
    {
        // Larsen Marx p837
        public override string TestName { get { return "TestSampleIndependanceRunCount"; } }

        public override string TestStatisticName { get { return "TestSampleIndependanceRunCount???"; } }

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
                return new TestAssertion[] { TestAssertion.SampleSizeAtLeast20 };
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.MeasurementsIndependant;
            }
        }

        public override double Test(IList<double> sample_0)
        {
            return TestStatic(sample_0);
        }

        public static double TestStatic(IList<double> sample_0)
        {
            if (sample_0.Count < 2)
            {
                throw new Exception("Need at least 2 measurements");
            }
            int statistic = 1;
            bool sign = sample_0[0] < sample_0[1];
            for (int index = 1; index < sample_0.Count; index++)
            {
                bool new_sign = sample_0[index - 1] < sample_0[index];
                if(sign != new_sign)
                {
                    statistic++;
                    sign = new_sign;
                }
            }
            double expected_value = ((2.0 * sample_0.Count) - 1.0) / 3.0;
            double variance = ((16.0 * sample_0.Count) - 29.0) / 90.0;
            double z_value = (statistic - expected_value) / Math.Sqrt(variance);
            return 1 - Normal.CDF(0.0, 1.0, z_value);
        }
    }
}
