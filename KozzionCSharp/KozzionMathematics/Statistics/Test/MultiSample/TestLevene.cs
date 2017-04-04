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
    public class TestLevene : ATestMultiSample
    {
        // https://en.wikipedia.org/wiki/Levene%27s_test
        public override string TestName { get { return "TestLevene"; } }

        public override string TestStatisticName { get { return "TestLevene???"; } }

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
                return new TestAssertion[] {
                    TestAssertion.MeasurementsIndependant,
                    TestAssertion.SamplesDrawnFromNormalDistribution };
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
            return TestStatic(samples);
        }

        //TODO implement 3 mean variants
        //http://www.itl.nist.gov/div898/handbook/eda/section3/eda35a.htm

        public static double TestStatic(IList<IList<double>> samples)
        {
            double total_count = ToolsCollection.CountElements(samples);
            double total_mean = ToolsMathStatistics.MeanAll(samples);
            //double[] sample_means = ToolsMathStatistics.Means0(samples);
            //double total_mean = ToolsMathStatistics.MedianAll(samples);
            double[] sample_means = ToolsMathStatistics.Medians0(samples);

            double summed_varriance = 0.0;       
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                summed_varriance += samples[sample_index].Count * ToolsMath.Sqr(sample_means[sample_index] - total_mean);
            }

            double total_variance = 0.0;
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                for (int measurement_index = 0; measurement_index < samples[sample_index].Count; measurement_index++)
                {
                    total_variance += ToolsMath.Sqr(samples[sample_index][measurement_index] - sample_means[sample_index]);
                }
            }
            double degrees_of_freedom_0 = samples.Count - 1;
            double degrees_of_freedom_1 = total_count - samples.Count;


            double f_statistic = (degrees_of_freedom_1 * summed_varriance) / (degrees_of_freedom_0 * total_variance);
            return FisherSnedecor.CDF(degrees_of_freedom_0, degrees_of_freedom_1, f_statistic);
        }
    }
}
