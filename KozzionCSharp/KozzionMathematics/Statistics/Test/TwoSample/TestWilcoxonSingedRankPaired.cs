using KozzionCore.Collections;
using KozzionCore.Tools;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public class TestWilcoxonSingedRankPaired : ATestTwoSample
    {
        public override string TestName { get { return "TestWilcoxonSingedRankPaired"; } }

        public override string TestStatisticName { get { return "TestWilcoxonSingedRankPaired???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { TestRequirement.SamplesAreOfEqualSize};
            }
        }

        public override TestAssertion NullHypothesis { get { return TestAssertion.SamplesHaveEqualMeans; } }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] {
                    TestAssertion.MeasurementsIndependant,
                    TestAssertion.MeasurementsPaired };
            }
        }



        public override double Test(IList<double> sample_0, IList<double> sample_1)
        {
            return TestStatic(sample_0, sample_1);
        }

        public static double TestStatic(IList<double> sample_0, IList<double> sample_1)
        {
            if (sample_0.Count != sample_1.Count)
            {
                throw new Exception("Sample sizes do not match");
            }
            double statistic = ComputeSingedRankPairedStatistic(sample_0, sample_1);
            double z_value = ComputeZTransform(sample_0.Count,  statistic);
            return 1- Normal.CDF(0.0, 1.0, z_value);
        }



        // based on first bigger than second means positive
        public static double ComputeSingedRankPairedStatistic(IList<double> sample_0, IList<double> sample_1)
        {

            double[] absolute_difference = ToolsMathCollection.AbsoluteDifference(sample_0, sample_1);

            // compute ranks
            DictionaryCount<double> counts = new DictionaryCount<double>();
            foreach (double item in absolute_difference)
            {
                counts.Increment(item);
            }
     
            Dictionary<double, double> ranks = new Dictionary<double, double>();
            List<double> keys = new List<double>(counts.Keys);
            keys.Sort();
            double rank = 1;
            foreach (double key in keys)
            {
                int count = counts[key];
                ranks[key] = (rank + rank + count - 1) / 2.0;
                rank += count;
            }

            double statistic = 0;
            for (int index = 0; index < sample_0.Count; index++)
            {
                if (sample_1[index] < sample_0[index])
                {
                    statistic += ranks[absolute_difference[index]];
                }
            }
            return statistic;
        }



        public static double ComputeZTransform(double data1_count, double statistic)
        {
            double expected_value = (data1_count * (data1_count + 1)) / 4.0;
            double variance = (data1_count * (data1_count + 1)) * ((data1_count * 2) + 1) / 24;
            double z_statistic = (statistic - expected_value) / Math.Sqrt(variance);
            return z_statistic;
        }

    }
}
