using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution;
using KozzionMathematics.Statistics.Test;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.OneSample
{
    public class TestKolmogorovSmirnov : ATestOneSample
    {

        public override string TestName { get { return "TestNormaletyKolmogorovSmirnov"; } }

        public override string TestStatisticName { get { return "TestNormaletyKolmogorovSmirnov???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { };
            }
        }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] { TestAssertion.MeasurementsIndependant }; //TODO check
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesDrawnFromNormalDistribution;
            }
        }

        public override double Test(IList<double> sample)
        {
            return TestStatic(sample);
        }

        public static double TestStatic(IList<double> sample)
        {
            double mean = ToolsMathStatistics.Mean(sample);
            double standard_deviation = ToolsMathStatistics.StandardDeviation(sample, mean);
            //TODO it is actually illigal to use estimated paramter for the tested distribution
            return TestGoodnessOfFitKolmogorovSmirnov.TestStatic(new DistributionNormalUnivariateFloat64(mean, standard_deviation), sample);
        }
    }
}