using System.Collections.Generic;

namespace KozzionMathematics.Statistics.Test
{
    public abstract class ATestOneSample: ATestStatistics
    {
        public abstract double Test(IList<double> sample_0);
  
    }
}