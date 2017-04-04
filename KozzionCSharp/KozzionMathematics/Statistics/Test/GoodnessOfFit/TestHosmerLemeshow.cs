using KozzionMathematics.Statistics.Distribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.GoodnessOfFit
{
    public class TestHosmerLemeshow : ITestGoodnessOfFitInterval<double>
    {

        public double Test(IDistribution<double, double> distribution, IList<double> sample)
        {
            return TestStatic(distribution, sample);
        }

        public static double TestStatic(IDistribution<double, double> distribution, IList<double> sample)
        {

            return 0.0;
        }

    }
}
