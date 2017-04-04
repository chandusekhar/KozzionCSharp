using KozzionCore.Tools;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.TwoSampleROC
{
    public class TestROCHanleyMcNeil : ATestTwoSampleROC
    {
        // This test is modeled rougly after [A Method of Comparing the Areas under Receiver Operating Characteristic Curves Derived from the Same Cases]
        // It takes some liberties when computeing the standard errors and aucs.
        private int trail_count;
        private Random random; //TODO make deterministic

        public override string TestName { get { return "TestHanleyMcNeil"; } }

        public override string TestStatisticName { get { return "TestHanleyMcNeil???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { TestRequirement.SamplesAreOfEqualSize };
            }
        }

        public override TestAssertion[] Assumptions { get { return new TestAssertion[] {
            TestAssertion.MeasurementsIndependant,
            TestAssertion.MeasurementsPaired }; } }

        public override TestAssertion NullHypothesis { get { return TestAssertion.SamplesLabelCorrelationIsEqual; } }



        public TestROCHanleyMcNeil(int sample_count, Random random)
        {
            this.trail_count = sample_count;
            this.random = random;
        }

        public TestROCHanleyMcNeil()
            : this(1000, new Random())
        {
 
        }


        public override double Test(IList<bool> labels, IList<double> sample_0, IList<double> sample_1)
        {
            return TestStatic(labels, sample_0, sample_1, random, trail_count);

        }

        public static double TestStatic(IList<bool> labels, IList<double> sample_0, IList<double> sample_1, Random random, int trial_count)
        {
            if (sample_0.Count != sample_1.Count)
            {
                throw new Exception("Samples not of equal size");
            }

            if (sample_0.Count != labels.Count)
            {
                throw new Exception("Labels not of equal size");
            }

            double auc_0 = ToolsMathStatistics.ComputeROCAUCTrapeziod(labels, sample_0);
            double auc_1 = ToolsMathStatistics.ComputeROCAUCTrapeziod(labels, sample_1);
            double se_0 = ComputeStandardError(auc_0, sample_0, labels, random, trial_count);
            double se_1 = ComputeStandardError(auc_1, sample_1, labels, random, trial_count);
            double r = ComputeKendalTauA(sample_0, sample_1);
            double z_value = ZTransform(auc_0, auc_1, se_0, se_1, r);
            double p_value = 1 - Normal.CDF(0.0, 1.0, z_value);
            return p_value;

        }


        // as in : [The Meaning and Use of the Area nder a Receiver Operating Characteristic(ROC) Curve] part 3 equation 1
        public static double ComputeStandardError(double auc, IList<double> sample, IList<bool> labels, Random random, int trial_count)
        {
            double[] true_instances = ToolsCollection.SelectWithSelectorValue(sample, labels, true);
            double[] false_instances = ToolsCollection.SelectWithSelectorValue(sample, labels, true);  
            double theta = auc;
            //double Q1 = ComputeQ1Simple(true_instances, false_instances, random, trial_count);
            //double Q2 = ComputeQ2Simple(true_instances, false_instances, random, trial_count);

            //If we asume theta follow a negative exponential distribution
            double Q1 = theta / (2 - theta); 
            double Q2 = (2 * theta  * theta ) / (1 + theta);

            double na = true_instances.Length;
            double nn = false_instances.Length;

            return Math.Sqrt(((theta * (1 - theta)) + ((na - 1) * (Q1 - (theta * theta))) + ((nn - 1) * (Q2 - (theta * theta)))) / (na * nn));
        }
     




        //The likelyhood that one random positive instance scores higher than two random negative instances.
        private static double ComputeQ1(double[] positive_instances, double[] negative_instances, Random random, int trail_count)
        {
            int succes = 0;
            for (int trail = 0; trail < trail_count; trail++)
            {
                double positive_0 = positive_instances[random.Next() % positive_instances.Length];
                double negative_0 = negative_instances[random.Next() % positive_instances.Length];
                double negative_1 = negative_instances[random.Next() % positive_instances.Length];
                if ((negative_0 < positive_0) && (negative_1 < positive_0))
                {
                    succes++;
                }
            }
            return ((double)succes) / ((double)trail_count);
        }

        //The likelyhood that two random positive instance both score higher than a single random negative instance.
        private static double ComputeQ2(double[] positive_instances, double[] negative_instances, Random random, int trail_count)
        {
            int succes = 0;
            for (int trail = 0; trail < trail_count; trail++)
            {
                double positive_0 = positive_instances[random.Next() % positive_instances.Length];
                double positive_1 = positive_instances[random.Next() % positive_instances.Length];
                double negative_0 = negative_instances[random.Next() % positive_instances.Length];
                if ((negative_0 < positive_0) && (negative_0 < positive_1))
                {
                    succes++;
                }
            }
            return ((double)succes) / ((double)trail_count);
        }

        private static double ZTransform(double auc_0, double auc_1, double se_0, double se_1, double r)
        {
            return (auc_0 - auc_1) / Math.Sqrt(se_0 * se_0 + se_1 * se_1 - 2.0 * r * se_0 * se_1);
        }

        public static double ComputeKendalTauA(IList<double> sample_0, IList<double> sample_1)
        {
            if (sample_0.Count != sample_1.Count)
            {
                throw new Exception("Samples not of equal size");
            }

            // as in https://en.wikipedia.org/wiki/Kendall_rank_correlation_coefficient
            int n = sample_0.Count;
            int difference = 0;
            for (int pair_0_index = 0; pair_0_index < n; pair_0_index++)
            {
                for (int pair_1_index = pair_0_index + 1; pair_1_index < n; pair_1_index++)
                {
                    difference += Math.Sign(sample_0[pair_0_index] - sample_0[pair_1_index]) *
                                  Math.Sign(sample_1[pair_0_index] - sample_1[pair_1_index]);
                }
            }
            return (2 * difference) / (n * (n - 1.0));
        }       
    }
}
