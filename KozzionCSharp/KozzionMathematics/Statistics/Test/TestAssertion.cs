using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test
{
    public enum TestAssertion
    {
        MeasurementsIndependant,
        MeasurementsPaired,
        SamplesIndependant, 
        SamplesHaveEqualMeans,
        SamplesHaveEqualVariances,
        SamplesDrawnFromNormalDistribution,
        SamplesDrawnFromBinominalDistribution,
        SamplesHaveNoCorrelation,
        SamplesLabelCorrelationIsEqual,
        SampleSizeAtLeast20,
        SamplesHaveEqualSizes //When it is not a requirement but an assertion
    }
}
