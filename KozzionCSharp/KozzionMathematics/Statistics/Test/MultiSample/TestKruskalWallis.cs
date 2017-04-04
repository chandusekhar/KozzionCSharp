using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.MultiSample
{
    // Larsen Marx 4Th editiopn P826
    public class TestKruskalWallis : ATestMultiSample
    {

        public override string TestName { get { return "TestKruskalWallis"; } }

        public override string TestStatisticName { get { return "TestKruskalWallis???"; } }

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
                return new TestAssertion[] { TestAssertion.MeasurementsIndependant };
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesHaveEqualMeans;
            }
        }

 
        /**

		* For k samples of sizes n1, n2, n3 ... nk from same shaped distributions 
		* with different means test if all means are equal. (H0 means are equal H1 means are not equal)
		*/

        public override double Test(IList<IList<double>> samples)
        {
            return TestStatic(samples);
        }

        public static double TestStatic(IList<IList<double>> samples)
        {
            double total_size = 0.0f;
            double squared_rank_mean_sum = 0.0f;

            double[][] rank_samples = ToolsMathStatistics.ConvertToAccendingRanks(samples);

            foreach (double[] sample in rank_samples)
            {
                total_size += sample.Length;
                double sum = ToolsMathCollection.Sum(sample);

                squared_rank_mean_sum += (sum * sum) / ((double)sample.Length);
            }

            double statistic = (12.0 * squared_rank_mean_sum) / (total_size * (total_size + 1)) - 3 * (total_size + 1);

            return 1 - ChiSquared.CDF(samples.Count - 1, statistic);

        }


    }
}