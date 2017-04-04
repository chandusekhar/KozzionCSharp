using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public class TestAdapterTwoSampleFromOneSample : ATestTwoSample
    {
        private ATestOneSample inner_test;

        public override string TestName { get { return this.inner_test.TestName; } }

        public override string TestStatisticName { get { return this.inner_test.TestStatisticName; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return this.inner_test.Requirements;
            }
        }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return this.inner_test.Assumptions;
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return this.inner_test.NullHypothesis;
            }
        }

        public TestAdapterTwoSampleFromOneSample(ATestOneSample inner_test)
        {
            this.inner_test = inner_test;
        }

        public override double Test(IList<double> sample_0, IList<double> sample_1)
        {
            double p_value_0 = this.inner_test.Test(sample_0);
            double p_value_1 = this.inner_test.Test(sample_1);

            //TODO add p value combiner
            if (Math.Abs(p_value_1 - 0.5) < Math.Abs(p_value_0 - 0.5))
            {
                return p_value_0;
            }
            else
            {
                return p_value_1;
            }          
        }
    }
}
