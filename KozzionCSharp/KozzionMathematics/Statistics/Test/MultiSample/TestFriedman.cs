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
    // Larsen Marx 4Th editiopn P832
    public class TestFriedman : ATestMultiSample
    {

        public override string TestName { get { return "TestFriedman"; } }

        public override string TestStatisticName { get { return "TestFriedman???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] {
                    TestRequirement.SamplesAreOfEqualSize};
            }
        }


        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] {
                    TestAssertion.MeasurementsIndependant,
                    TestAssertion.MeasurementsPaired };
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
            int sample_size = samples[0].Count;
            foreach (IList<double> sample in samples)
            {
                if (sample.Count != sample_size)
                {
                    throw new Exception("samples must be of equal size");
                }

            }


            double[,] measurement_ranks = ToolsMathStatistics.Ranks1(samples);
            double[] rank_sums = ToolsMathCollection.Sums0(measurement_ranks);


            double chi_square_statistic_part = 0.0;
            for (int treatment_index = 0; treatment_index < rank_sums.Length; treatment_index++)
            {
                chi_square_statistic_part += ToolsMath.Sqr(rank_sums[treatment_index]);
            }
            double treatment_count = samples.Count;
            double block_count = sample_size;

            double chi_square_statistic = ((12.0 / (block_count * treatment_count * (treatment_count + 1.0))) * chi_square_statistic_part) - (3 * block_count * (treatment_count + 1.0));

            return ChiSquared.CDF(rank_sums.Length, chi_square_statistic);
        }

    }
}
