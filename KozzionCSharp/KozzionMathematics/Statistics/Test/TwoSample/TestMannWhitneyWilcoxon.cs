using KozzionCore.Collections;
using KozzionCore.Tools;
using KozzionMathematics.Statistics.Test.TwoSample;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSample
{
    public class TestMannWhitneyWilcoxon : ATestTwoSample
    {
        // Larsen Marc 4Th editiopn P822

        // TODO compare to https://en.wikipedia.org/wiki/Mann%E2%80%93Whitney_U_test
        //AKA Mann–Whitney U test
        //AKA Wilcoxon rank-sum test
        //AKA Wilcoxon–Mann–Whitney test

        // 1. All the observations from both groups are independent of each other.
        // 2. The responses are ordinal(i.e.one can at least say, of any two observations, which is the greater).

        // Under the null hypothesis H0, the probability of an observation from the population X exceeding an 
        // observation from the second population Y equals the probability of an observation from Y exceeding 
        // an observation from X: P(X>Y)=P(Y>X) or P(X>Y) + 0.5 · P(X= Y) = 0.5. 

        // A stronger null hypothesis commonly used is "The distributions of both populations are equal" which 
        // implies the previous hypothesis.

        // The alternative hypothesis H1 is "the probability of an observation from the population X exceeding 
        // an observation from the second population Y is different from the probability of an observation from 
        // Y exceeding an observation from X: P(X>Y) ≠ P(Y>X)." The alternative may also be stated in terms of 
        // a one-sided test, for example: P(X>Y) > P(Y>X).

        public override string TestName { get { return "TestMannWhitneyWilcoxon"; } }

        public override string TestStatisticName { get { return "TestMannWhitneyWilcoxon???"; } }

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
        public override TestAssertion NullHypothesis { get { return TestAssertion.SamplesHaveEqualMeans; } } //TODO

        public override double Test(IList<double> sample_0, IList<double> sample_1)
        {
            return TestStatic(sample_0, sample_1);
        }

        public static double TestStatic(IList<double> sample_0, IList<double> sample_1)
        {   
            //U = AUC∗nP∗nN
            //int positive_count = ToolsCollection.CountOccurance(labels, true);
            double wilcoxon_statistic = ComputeRankSumStatistic(sample_0, sample_1);
            double z_value = ComputeZTransform(sample_0.Count, sample_1.Count, wilcoxon_statistic);
            return 1 - Normal.CDF(0.0, 1.0, z_value);
        }




        public static double ComputeZTransform(double sample_0_size, double sample_1_size, double rank_sum_statistic)
        {
            double expected_value = (sample_0_size * (sample_0_size + sample_1_size + 1)) / 2.0;
            double variance = (sample_0_size * sample_1_size * (sample_0_size + sample_1_size + 1)) / sample_1_size;
            double z_statistic = (rank_sum_statistic - expected_value) / Math.Sqrt(variance);
            return z_statistic;
        }

        // based on first bigger than second means positive
        public static double ComputeRankSumStatistic(IList<double> sample_0, IList<double> sample_1)
        {     
            // compute ranks
            DictionaryCount<double> counts = new DictionaryCount<double>();
            foreach (double item in sample_0)
            {
                counts.Increment(item);
            }

            foreach (double item in sample_1)
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

            List<double> rank_list = new List<double>();
            foreach (double item in sample_0)
            {
                rank_list.Add(ranks[item]);
            }

            return ToolsMathCollection.Sum(rank_list);
        }

    }

}

