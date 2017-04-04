using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public class TestAdapterMultiSample :ATestTwoSample
    {
        private ATestMultiSample inner_test;

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

        public TestAdapterMultiSample(ATestMultiSample inner_test)
        {
            this.inner_test = inner_test;
        }

        public override double Test(IList<double> sample_0, IList<double> sample_1)
        {
            IList<IList<double>> samples = new IList<double>[2];
            samples[0] = sample_0;
            samples[1] = sample_1;
            return this.inner_test.Test(samples);
        }


    }
}
