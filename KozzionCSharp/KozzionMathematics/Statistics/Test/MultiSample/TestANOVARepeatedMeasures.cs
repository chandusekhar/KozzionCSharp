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
    public class TestANOVARepeatedMeasures : ATestMultiSample
    {
        public override string TestName { get { return "TestANOVAReapetedMeasures"; } }

        public override string TestStatisticName { get { return "TestANOVAReapetedMeasures???"; } }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[]
                {
                    TestAssertion.SamplesDrawnFromNormalDistribution,
                    TestAssertion.SamplesIndependant,
                    TestAssertion.SamplesHaveEqualVariances,
                    TestAssertion.MeasurementsPaired
                };
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesHaveEqualMeans;
            }
        }
        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { TestRequirement.SamplesAreOfEqualSize };
            }
        }

        public override double Test(IList<IList<double>> samples)
        {
            return TestStatic(samples).Item1;
        }

        public static Tuple<double,double> TestStatic(IList<IList<double>> samples)
        {
            int sample_size = samples[0].Count;
            foreach (IList<double> sample in samples)
            {
                if (sample.Count != sample_size)
                {
                    throw new Exception("samples must be of equal size");
                }
            }

            // Larsen Marx 4Th editiopn P779
            double sample_count = samples.Count;
            double total_count = ToolsCollection.CountElements(samples);
            double total_mean = ToolsMathStatistics.MeanAll(samples);
            double sum_squared_all = ToolsMathStatistics.SumSquaredAll(samples);
            IList<double> sample_sums = ToolsMathCollection.Sums0(samples);
            IList<double> measurement_sums = ToolsMathCollection.Sums1(samples);

            // compute C
            double c = ToolsMath.Sqr(total_mean * total_count) / (sample_size * sample_count);


            double sstot = sum_squared_all - c;

            double ssb = 0;
            for (int measurement_index = 0; measurement_index < sample_size; measurement_index++)
            {
                ssb += ToolsMath.Sqr(measurement_sums[measurement_index]) / sample_count;
            }
            ssb -= c;

            double sstr = 0.0;
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                sstr += ToolsMath.Sqr(sample_sums[sample_index]) / sample_size;
            }
            sstr -= c;

            double sse = sstot - ssb - sstr;


            double degrees_of_freedom_0_samples      = (sample_count - 1.0);
            double degrees_of_freedom_0_measurements = (sample_size - 1.0);
            double degrees_of_freedom_1              = degrees_of_freedom_0_samples * degrees_of_freedom_0_measurements;

            //F-Transform samples
            double f_statistic_samples = (sstr/ degrees_of_freedom_0_samples) / (sse / degrees_of_freedom_1);

            //F-Transform measurements
            double f_statistic_measurements = (ssb / degrees_of_freedom_0_measurements) / (sse / degrees_of_freedom_1);


            return new Tuple<double, double>(
                FisherSnedecor.CDF(degrees_of_freedom_0_samples, degrees_of_freedom_1, f_statistic_samples),
                FisherSnedecor.CDF(degrees_of_freedom_0_measurements, degrees_of_freedom_1, f_statistic_measurements));
        }
    }
}
