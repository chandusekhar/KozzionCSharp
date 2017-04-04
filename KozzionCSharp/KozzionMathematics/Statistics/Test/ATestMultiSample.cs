using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test
{
    public abstract class ATestMultiSample : ATestStatistics
    {
        public abstract double Test(IList<IList<double>> samples);

 
    }
}
