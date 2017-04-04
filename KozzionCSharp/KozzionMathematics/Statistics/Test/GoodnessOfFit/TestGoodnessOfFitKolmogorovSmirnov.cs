using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Statistics.Distribution;

namespace KozzionMathematics.Statistics.Test.OneSample
{
    public class TestGoodnessOfFitKolmogorovSmirnov : ITestGoodnessOfFitInterval<double>
    {

        public double Test(IDistribution<double, double> distribution, IList<double> sample)
        {
            return TestStatic(distribution, sample);
        }

        public static double TestStatic(IDistribution<double, double> distribution, IList<double> sample)
        {

            double ks_statistic = 0.0;
            for (int index = 0; index < sample.Count; index++)
            {
                double distance = Math.Abs(distribution.ComputeCulmativeDensity(sample[index]) - ((index + 1.0) / sample.Count));
                ks_statistic = Math.Max(ks_statistic, distance);
            }
            return new DistributionKolmogorovSmirnov(sample.Count).ComputeCDF(ks_statistic);
        }

    }
}
