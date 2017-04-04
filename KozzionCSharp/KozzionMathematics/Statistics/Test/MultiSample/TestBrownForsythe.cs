using KozzionCore.Tools;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.MultiSample
{
    public class TestBrownForsythe : ATestMultiSample
    {
        // https://en.wikipedia.org/wiki/Levene%27s_test

        public override string TestName { get { return "TestBrownForsythe"; } }

        public override string TestStatisticName { get { return "TestBrownForsythe???"; } }

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
                return new TestAssertion[] { TestAssertion.MeasurementsIndependant, TestAssertion.SamplesDrawnFromNormalDistribution};
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesHaveEqualVariances;
            }
        }



        public override double Test(IList<IList<double>> samples)
        {
            double total_count = ToolsCollection.CountElements(samples);
            double pooled_mean = ToolsMathStatistics.MeanAll(samples);
            double nominator_part = 0.0;
            double denominator_part = 0.0;
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                double sample_mean = ToolsMathStatistics.Mean(samples[sample_index]);
                double sample_median = ToolsMathStatistics.Quantile(samples[sample_index], 0.5f);
                nominator_part += (sample_mean - pooled_mean) * (sample_mean - pooled_mean) * samples[sample_index].Count;

                for (int measurement_index = 0; measurement_index < samples[sample_index].Count; measurement_index++)
                {
                    double diff = Math.Abs(sample_median - samples[sample_index][measurement_index]) - sample_mean; //This is the difference with brown forsythe test
                    denominator_part += diff * diff;
                }
            }

            double degrees_of_freedom_0 = samples.Count - 1;
            double degrees_of_freedom_1 = total_count - samples.Count;
            double f_statistic = (degrees_of_freedom_1 * nominator_part) / (degrees_of_freedom_0 * denominator_part);
            FisherSnedecor distribution = new FisherSnedecor(degrees_of_freedom_0, degrees_of_freedom_1, new Random());
            return distribution.CumulativeDistribution(f_statistic);
        }
    }
}
