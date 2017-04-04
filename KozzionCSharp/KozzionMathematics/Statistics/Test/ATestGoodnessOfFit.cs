using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test
{
    public interface ITestGoodnessOfFitInterval<DomainType>
    {
        double Test(IDistribution<DomainType, double> distribution, IList<DomainType> sample);
    }

    public interface ITestGoodnessOfFitDiscrete<DomainType>
    {
        double Test(IDistribution<DomainType, double> distribution, IList<DomainType> sample);
    }
}
