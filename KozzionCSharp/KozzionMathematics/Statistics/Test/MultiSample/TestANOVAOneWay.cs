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
    public class TestANOVAOneWay : ATestMultiSample
    {
        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[]
                {
                    TestAssertion.SamplesDrawnFromNormalDistribution,
                    TestAssertion.SamplesIndependant,
                    TestAssertion.SamplesHaveEqualVariances
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
                return new TestRequirement[] { TestRequirement.SamplesAreAtLeastOfSize2};
            }
        }

        public override string TestName   { get { return "TestANOVAOneWay"; }        }

        public override string TestStatisticName { get { return "TestANOVAOneWay???"; } }

        public override double Test(IList<IList<double>> samples)
        {
            return TestStatic(samples);
        }

        public static double TestStatic(IList<IList<double>> samples)
        {

            double sample_count = samples.Count;
            double total_count = ToolsCollection.CountElements(samples);
            double total_mean = ToolsMathStatistics.MeanAll(samples);
            IList < double > sample_means = ToolsMathStatistics.Means0(samples);

            double sstr = 0.0;
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                sstr += samples[sample_index].Count * ToolsMath.Sqr(sample_means[sample_index] - total_mean);
            }

            double sse = 0.0;
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                for (int measurement_index = 0; measurement_index < samples[sample_index].Count; measurement_index++)
                {
                    sse += ToolsMath.Sqr(samples[sample_index][measurement_index] - sample_means[sample_index]);
                }
            }
            //FTransform

            double degrees_of_freedom_0 = (sample_count - 1.0);
            double degrees_of_freedom_1 = (total_count - sample_count);
            double summed_variance = sstr / degrees_of_freedom_0;
            double total_varaiance = sse / degrees_of_freedom_1;
            double f_statistic = summed_variance / total_varaiance;


            return FisherSnedecor.CDF(degrees_of_freedom_0, degrees_of_freedom_1, f_statistic);
        }
    }
}
