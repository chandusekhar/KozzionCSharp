using KozzionCore.Tools;
using KozzionMathematics.Statistics.Test;
using KozzionMathematics.Statistics.Test.TwoSample;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisproveGravity.Model
{
    /// <summary>
    ///  Should be applicable to all tests
    /// </summary>
    public class ModelTest : ReactiveObject
    {
        public IList<ModelTestRequirement> TestRequirementList { get; private set; }
        public IList<ModelTestAssertion> TestAssumptionist { get; private set; }      

        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.RaiseAndSetIfChanged(ref this.title, value); }
        }


        private TestStatus test_status;
        public TestStatus TestStatus
        {
            get { return this.test_status; }
            set { this.RaiseAndSetIfChanged(ref this.test_status, value); }
        }

        private TestAssertion null_hypothesis;
        public TestAssertion NullHypothesis
        {
            get { return this.null_hypothesis; }
            set { this.RaiseAndSetIfChanged(ref this.null_hypothesis, value); }
        }

        private string null_hypothesis_string;
        public string NullHypothesisString
        {
            get { return this.null_hypothesis_string; }
            set { this.RaiseAndSetIfChanged(ref this.null_hypothesis_string, value); }
        }

        private string applicability;
        public string Applicability
        {
            get { return this.applicability; }
            set { this.RaiseAndSetIfChanged(ref this.applicability, value); }
        }


        private string test_statistic_name; //TODO remove name
        public string TestStatisticName
        {
            get { return this.test_statistic_name; }
            set { this.RaiseAndSetIfChanged(ref this.test_statistic_name, value); }
        }

        private double test_statistic_value;
        private string test_statistic_value_string;
        public string TestStatisticValueString
        {
            get { return this.test_statistic_value_string; }
            set { this.RaiseAndSetIfChanged(ref this.test_statistic_value_string, value); }
        }

        private double p_value;
        private string p_value_string;
        public string PValueString
        {
            get { return this.p_value_string; }
            set { this.RaiseAndSetIfChanged(ref this.p_value_string, value); }
        } 


        public ModelTest(ATestTwoSample test,
            string applicability,
            double test_statistic_value,
            double p_value)
        {
            this.Title = test.TestName;
            this.Applicability = applicability;
            this.null_hypothesis = test.NullHypothesis;
            this.NullHypothesisString = ToolsEnum.EnumToString(test.NullHypothesis);
            this.TestStatisticName = test.TestStatisticName;
            this.test_statistic_value = test_statistic_value;
            this.TestStatisticValueString = test_statistic_value.ToString("0.####");
            this.p_value = p_value;
            this.PValueString = p_value.ToString("0.####");

            this.TestRequirementList = new ObservableCollection<ModelTestRequirement>();
            foreach (TestRequirement requirement in test.Requirements)
            {
                TestRequirementList.Add(new ModelTestRequirement(requirement));
            }

            this.TestAssumptionist = new ObservableCollection<ModelTestAssertion>();
            foreach (TestAssertion assertion in test.Assumptions)
            {
                TestAssumptionist.Add(new ModelTestAssertion(assertion));
            }
        }

        public void CheckTestStatus(IList<TestRequirement> requirements, List<TestAssertion> assertions, IList<TestAssertion> user_forced)
        {
            foreach (ModelTestRequirement requirement in TestRequirementList)
            {
                if (!requirements.Contains(requirement.TestRequirement))
                {
                    //TODO color the requiremet    
                    TestStatus = TestStatus.NotApplicable;
                    return;
                }
            }

            bool founded = true;
            foreach (ModelTestAssertion assumption in TestAssumptionist)
            {
                if (!assertions.Contains(assumption.TestAssertion))
                {
                    //TODO color the asserion                 
                    founded = false;
                }
            }

            if (founded)
            {
                if (0.45 < Math.Abs(0.5 - p_value))
                {
                    if (user_forced.Contains(null_hypothesis))
                    {
                        TestStatus = TestStatus.FoundedContradictory;
                        return;
                    }
                    else
                    {
                        TestStatus = TestStatus.FoundedSuccesfull;
                        return;
                    }
                }
                else
                {
                    TestStatus = TestStatus.FoundedFailure;
                    return;
                }
            }
            else
            {
                if (0.45 < Math.Abs(0.5 - p_value))
                {
                    TestStatus = TestStatus.UnfoundedSuccesfull;
                }
                else
                {
                    TestStatus = TestStatus.UnfoundedFailure;
                }
            }

        }

        //public ModelTest(ModelTest other)
        //    : this(other.title, other.null_hypothesis, other.applicability, other.p_value)
        //{

        //}
    }
}
