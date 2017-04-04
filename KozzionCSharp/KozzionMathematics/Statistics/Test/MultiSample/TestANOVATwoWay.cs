using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.MultiSample
{
    public class TestANOVATwoWay : ATestMultiSample
    {
        public override string TestName { get { return "TestANOVATwoWay"; } }

        public override string TestStatisticName { get { return "TestANOVATwoWay???"; } }

        public override TestAssertion[] Assumptions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesHaveEqualMeans;
            }
        }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { TestRequirement.Implementation };
            }
        }

        public override double Test(IList<IList<double>> samples)
        {
       
            throw new NotImplementedException();
        }
    }
}
