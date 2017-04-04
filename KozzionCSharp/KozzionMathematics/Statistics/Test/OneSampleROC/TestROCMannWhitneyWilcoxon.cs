using KozzionCore.Tools;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.OneSampleROC
{
    public class TestROCMannWhitneyWilcoxon : ATestOneSampleROC
    {
        // https://en.wikipedia.org/wiki/Mann%E2%80%93Whitney_U_test
        //AKA Mann–Whitney U test
        //AKA Wilcoxon rank-sum test
        //AKA Wilcoxon–Mann–Whitney test
        public override string TestName { get { return "TestROCMannWhitneyWilcoxon"; } }

        public override string TestStatisticName { get { return "TestROCMannWhitneyWilcoxon???"; } }

        public override TestRequirement[] Requirements { get { return new TestRequirement[] { }; } }

        public override TestAssertion[] Assumptions { get { return new TestAssertion[] { TestAssertion.SamplesDrawnFromBinominalDistribution }; } }

        public override TestAssertion NullHypothesis { get { return TestAssertion.SamplesHaveNoCorrelation; } }

   

        public override double Test(IList<bool> labels, IList<double> sample_0)
        {
            return TestStatic(labels, sample_0);
        }

        public static double TestStatic(IList<bool> labels, IList<double> sample_0)
        {
            if (sample_0.Count != labels.Count)
            {
                throw new Exception("Labels not of equal size");
            }
            //U = AUC∗nP∗nN
            int positive_count = ToolsCollection.CountOccurance(labels, true);
            int negative_count = sample_0.Count - positive_count;
            double auc = ToolsMathStatistics.ComputeROCAUCTrapeziod(labels, sample_0);
            double u_statistic = positive_count * negative_count * auc;

            //Z transform
            double expected_value = (positive_count * negative_count) / 2.0;
            double variance = (positive_count * negative_count * (positive_count + negative_count + 1)) / 12.0;
            double z_value = (u_statistic - expected_value) / Math.Sqrt(variance);
            return 1 - Normal.CDF(0.0, 1.0, z_value);
        }
    }
}
