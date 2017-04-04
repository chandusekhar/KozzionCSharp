using System.Collections.Generic;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public abstract class ATestTwoSample : ATestStatistics
    {


        public abstract double Test(IList<double> sample_0, IList<double> sample_1);

    }
}