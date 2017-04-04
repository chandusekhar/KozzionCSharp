using DisproveGravity.Model;
using KozzionMathematics.Statistics.Test;
using KozzionMathematics.Statistics.Test.MultiSample;
using KozzionMathematics.Statistics.Test.OneSample;
using KozzionMathematics.Statistics.Test.TwoSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisproveGravity.Tools
{
    public static class ToolsDisprove
    {
        public static IList<ATestTwoSample> GetTestTwoSample()
        {
            IList<ATestTwoSample> tests = new List<ATestTwoSample>();
            tests.Add(new TestFisherSnedecor());
            tests.Add(new TestMannWhitneyWilcoxon());
            tests.Add(new TestStudentTEqualVariance());
            tests.Add(new TestStudentTRepeatedMeasures());
            tests.Add(new TestWelchsT());
            tests.Add(new TestWilcoxonSingedRankPaired());
            tests.Add(new TestAdapterMultiSample(new TestANOVAOneWay()));
            tests.Add(new TestAdapterMultiSample(new TestANOVARepeatedMeasures()));
            //tests.Add(new TestAdapterMultiSample(new TestANOVATwoWay())); //IS repeated for 2 classes for more??
            tests.Add(new TestAdapterMultiSample(new TestBartlett()));
            tests.Add(new TestAdapterMultiSample(new TestBrownForsythe()));
            tests.Add(new TestAdapterMultiSample(new TestFriedman()));
            tests.Add(new TestAdapterMultiSample(new TestIndependance()));
            tests.Add(new TestAdapterMultiSample(new TestKruskalWallis()));
            tests.Add(new TestAdapterMultiSample(new TestLevene()));

            tests.Add(new TestAdapterTwoSampleFromOneSample(new TestKolmogorovSmirnov()));
            tests.Add(new TestAdapterTwoSampleFromOneSample(new TestShapiroWilk()));
            tests.Add(new TestAdapterTwoSampleFromOneSample(new TestRunCount()));
            return tests;
        }

        public static List<ModelTest> ApplyTwoSampleTest(IList<TestRequirement> requirements, IList<double> sample_0, IList<double> sample_1)
        {
            List<ModelTest> tests = new List<ModelTest>();
            foreach (ATestTwoSample test in GetTestTwoSample())
            {
                string title = test.TestName;
                string null_hypothesis = GetTestAssertionString(test.NullHypothesis);
                string applicability = "Not Applicable";
                double p_value = 0;
                if (test.IsApplicable(requirements))
                {
                    applicability = "Applicable";
                    p_value = test.Test(sample_0, sample_1);
                }     
                tests.Add(new ModelTest(test, applicability, 0.0, p_value));
            }
            return tests;
        }


        public static IList<TestRequirement> CheckRequirements(IList<double> sample_0, IList<double> sample_1)
        {
            IList<TestRequirement> requirements = new List<TestRequirement>();

            if (sample_0.Count == sample_1.Count)
            {
                requirements.Add(TestRequirement.SamplesAreOfEqualSize);
            }

            if ((1 < sample_0.Count) && (1 < sample_1.Count))
            {
                requirements.Add(TestRequirement.SamplesAreAtLeastOfSize2);
            }
            return requirements;
        }


        public static IList<ModelTest> BuildTreeTwoSampleDifferent(IList<double> sample_0, IList<double> sample_1)
        {
            IList<TestRequirement> requirements = CheckRequirements(sample_0, sample_1);
            return ApplyTwoSampleTest(requirements, sample_0, sample_1);
        }

        public static void CheckTestStates(IList<TestRequirement> requirements, IList<TestRequirement> assertions, IList<ModelTest> test_list)
        {
            //test_list.
        }

        public static void CheckTestAssertions(IList<TestRequirement> requirements, IList<TestAssertion> user_forced, IList<ModelTest> test_list)
        {

            List<TestAssertion> assertions = GetAllAssertions();

            bool has_changes = true;
            while (has_changes)
            {
                has_changes = false;
                foreach (ModelTest model_test in test_list)
                {
                    model_test.CheckTestStatus(requirements, assertions, user_forced);
                    if (model_test.TestStatus == TestStatus.FoundedSuccesfull && assertions.Contains(model_test.NullHypothesis))
                    {
                        assertions.Remove(model_test.NullHypothesis);
                        has_changes = true;
                    }
                }
            } 

        }


        public static List<TestAssertion> GetAllAssertions()
        {
            return new List<TestAssertion>(Enum.GetValues(typeof(TestAssertion)).Cast<TestAssertion>());
        }

        //private static IList<ModelTest> BuildTreeTwoSampleDifferent(Dictionary<TestAssertion, List<ModelTest>> all_two_sample_results)
        //{
        //    IList<ModelTest> top_level_tests = new List<ModelTest>();
        //    IList<TestAssertion> assertions = new TestAssertion[] { };
        //    foreach (TestAssertion assertion in assertions)
        //    {
        //        if (all_two_sample_results.ContainsKey(assertion))
        //        {
        //            IList<ModelTest> assertion_tests = all_two_sample_results[assertion];
        //            foreach (ModelTest assertion_test in assertion_tests)
        //            {
        //                ModelTest copy = new ModelTest(assertion_test);
        //                top_level_tests.Add(copy);
        //            }
        //        }
        //    }

        //    return top_level_tests;
        //}

        public static IList<ATestTwoSample> GetTestTwoSampleTestList()
        {
            IList<ATestTwoSample> tests = new List<ATestTwoSample>();
            tests.Add(new TestFisherSnedecor());
            tests.Add(new TestMannWhitneyWilcoxon());
            tests.Add(new TestStudentTEqualVariance());
            tests.Add(new TestStudentTRepeatedMeasures());
            tests.Add(new TestWelchsT());
            tests.Add(new TestWilcoxonSingedRankPaired());
            tests.Add(new TestAdapterMultiSample(new TestANOVAOneWay()));
            tests.Add(new TestAdapterMultiSample(new TestANOVARepeatedMeasures()));
            //tests.Add(new TestAdapterMultiSample(new TestANOVATwoWay())); //IS repeated for 2 classes
            tests.Add(new TestAdapterMultiSample(new TestBartlett()));
            tests.Add(new TestAdapterMultiSample(new TestBrownForsythe())); //TODO
            tests.Add(new TestAdapterMultiSample(new TestFriedman()));
            tests.Add(new TestAdapterMultiSample(new TestIndependance()));//TODO
            tests.Add(new TestAdapterMultiSample(new TestKruskalWallis()));
            tests.Add(new TestAdapterMultiSample(new TestLevene()));//TODO

            return tests;
        }

        private static string GetTestAssertionString(TestAssertion test_assertion)
        {
            switch (test_assertion)
            {
                case TestAssertion.MeasurementsIndependant: return "Measurements are independant";
                case TestAssertion.MeasurementsPaired: return "Measurements are paired";
                case TestAssertion.SamplesIndependant: return "SampleIndependant";
                case TestAssertion.SamplesLabelCorrelationIsEqual: return "SamplesCorrelationEqual";
                case TestAssertion.SamplesDrawnFromBinominalDistribution: return "SamplesDrawnFromBinominalDistribution";
                case TestAssertion.SamplesDrawnFromNormalDistribution: return "SamplesDrawnFromNormalDistribution";
                case TestAssertion.SamplesHaveNoCorrelation: return "SamplesHaveNoCorrelation";
                case TestAssertion.SamplesHaveEqualMeans: return "SamplesHaveEqualMeans";
                case TestAssertion.SamplesHaveEqualVariances: return "SamplesHaveEqualVariances";

                default:
                    throw new Exception("Unknown test assertion: " + test_assertion);
            }
        }
    }
}
