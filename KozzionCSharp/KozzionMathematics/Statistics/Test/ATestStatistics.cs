using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test
{
    public abstract class ATestStatistics
    {
        public abstract string TestName { get; }

        public abstract string TestStatisticName { get; }

        public abstract TestRequirement[] Requirements { get; }

        public abstract TestAssertion[] Assumptions { get; }

        public abstract TestAssertion NullHypothesis { get; }

        public bool IsApplicable(IList<TestRequirement> given_requirements)
        {
            foreach (TestRequirement requirement in Requirements)
            {
                if (!given_requirements.Contains(requirement))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
