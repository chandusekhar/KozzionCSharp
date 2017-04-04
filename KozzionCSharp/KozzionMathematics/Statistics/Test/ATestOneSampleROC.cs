using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test
{
    public abstract class ATestOneSampleROC : ATestStatistics
    {
        public abstract double Test(IList<bool> labels, IList<double> sample_0);
    }
}
