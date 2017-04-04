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
    public class TestBartlett : ATestMultiSample
    {
        // https://en.wikipedia.org/wiki/Bartlett%27s_test
        public override string TestName { get { return "TestBartlett"; } }

        public override string TestStatisticName { get { return "TestBartlett???"; } }


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

        public static double TestStatic(IList<IList<double>> samples)
        {
            double pooled_varriance = ToolsMathStatistics.VariancePooled(samples);
            double total_count = ToolsCollection.CountElements(samples);

            double nom_part = 0;
            double denom_part = 0;
            for (int sample_index = 0; sample_index < samples.Count; sample_index++)
            {
                nom_part += (samples[sample_index].Count - 1.0) * Math.Log(ToolsMathStatistics.Variance(samples[sample_index]));
                denom_part += (1.0 / (samples[sample_index].Count - 1));
            }

            double nominator = (total_count - samples.Count) * Math.Log(pooled_varriance) - nom_part;
            double denominator = 1 + ((1.0 / (3 * (samples.Count - 1))) * (denom_part - (1 / (total_count - samples.Count))));
            double chi_square_statisic = nominator / denominator;
            double degrees_of_freedom = samples.Count - 1;
            return ChiSquared.CDF(degrees_of_freedom, chi_square_statisic); ;
        }
    }
}
