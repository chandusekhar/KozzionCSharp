using DisproveGravity.Tools;
using KozzionMathematics.Statistics.Test;
using KozzionMathematics.Statistics.Test.OneSample;
using KozzionMathematics.Statistics.Test.TwoSample;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisproveGravity.Model
{
    public class ModelApplication: ReactiveObject
    {
        private string error_text;
        public string ErrorText
        {
            get { return this.error_text; }
            set { this.RaiseAndSetIfChanged(ref this.error_text, value); }
        }

        private string sample_0_text;
        public string Sample0Text
        {
            get { return this.sample_0_text; }
            set { this.RaiseAndSetIfChanged(ref this.sample_0_text, value); }
        }

        private string sample_1_text;
        public string Sample1Text
        {
            get { return this.sample_1_text; }
            set { this.RaiseAndSetIfChanged(ref this.sample_1_text, value); }
        }

        private string label_text;
        public string LabelText
        {
            get { return this.label_text; }
            set { this.RaiseAndSetIfChanged(ref this.label_text, value); }
        }





        private bool user_assume_measurements_paired;
        public bool UserAssumeMeasurementsPaired
        {
            get { return this.user_assume_measurements_paired; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_measurements_paired, value); }
        }

        private bool user_assume_measurements_independant;
        public bool UserAssumeMeasurementsIndependant
        {
            get { return this.user_assume_measurements_independant; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_measurements_independant, value); Check(); }
        }

        private bool user_assume_samples_independant;
        public bool UserAssumeSamplesIndependant
        {
            get { return this.user_assume_samples_independant; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_samples_independant, value); }
        }

        private bool user_assume_samples_have_equal_means;
        public bool UserAssumeSamplesHaveEqualMeans
        {
            get { return this.user_assume_samples_have_equal_means; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_samples_have_equal_means, value); }
        }

        private bool user_assume_samples_have_equal_variances;
        public bool UserAssumeSamplesHaveEqualVariances
        {
            get { return this.user_assume_samples_have_equal_variances; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_samples_have_equal_variances, value);  }
        }

        private bool user_assume_samples_drawn_from_normal_distribution;
        public bool UserAssumeSamplesDrawnFromNormalDistribution
        {
            get { return this.user_assume_samples_drawn_from_normal_distribution; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_samples_drawn_from_normal_distribution, value); }
        }

        private bool user_assume_samples_drawn_from_binominal_distribution;
        public bool UserAssumeSamplesDrawnFromBinominalDistribution
        {
            get { return this.user_assume_samples_drawn_from_binominal_distribution; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_samples_drawn_from_binominal_distribution, value); }
        }

        private bool user_assume_samples_have_no_correlation;
        public bool UserAssumeSamplesHaveNoCorrelation
        {
            get { return this.user_assume_samples_have_no_correlation; }
            set { this.RaiseAndSetIfChanged(ref this.user_assume_samples_have_no_correlation, value); }
        }





        public IReactiveCommand<object> CommandStart { get; private set; }


        private ModelTest test_selected;
        public ModelTest TestSelected
        {
            get { return this.test_selected; }
            set { this.RaiseAndSetIfChanged(ref this.test_selected, value); }
        }


        private IList<TestRequirement> requirements;
        public IList<ModelTest> TestList { get; private set; }

        public ModelApplication()
        {
            this.TestList = new ObservableCollection<ModelTest>();

            this.CommandStart = ReactiveCommand.Create(Observable.Return(true));
            this.CommandStart.Subscribe(_ => ExecuteStart());

            //Test
            Sample0Text = "1009001\r\n1009002\r\n1009008\r\n1009010\r\n1009011\r\n1009012\r\n1009014\r\n1009015\r\n1010002\r\n1010004\r\n1010005\r\n1010007\r\n";
            Sample1Text = "1009001\r\n1009002\r\n1009006\r\n1009008\r\n1009010\r\n1009011\r\n1009012\r\n1009014\r\n1010002\r\n1010004\r\n1010005\r\n1010007\r\n";
            LabelText   = "1      \r\n1      \r\n0      \r\n1      \r\n0      \r\n1      \r\n0      \r\n1      \r\n0      \r\n0      \r\n0      \r\n1      \r\n";
        }

        public void ExecuteStart()
        {
            TestList.Clear();
            IList<double> sample_0 = new List<double>();
            string [] sample_0_split = Sample0Text.Split("\r\n".ToCharArray());
            for (int index = 0; index < sample_0_split.Length; index++)
            {
                double parce = 0;
                if (double.TryParse(sample_0_split[index], out parce))
                {
                    sample_0.Add(parce);
                }
                else
                {
                    ErrorText = "Error parcing sample 0 at line: " + index + " value: " + sample_0_split[index];
                }                    

            }
            IList<double> sample_1 = new List<double>();
            string[] sample_1_split = Sample1Text.Split("\r\n".ToCharArray());
            for (int index = 0; index < sample_0_split.Length; index++)
            {
                double parce = 0;
                if (double.TryParse(sample_1_split[index], out parce))
                {
                    sample_1.Add(parce);
                }
                else
                {
                    ErrorText = "Error parcing sample 1 at line: " + index + " value: " + sample_0_split[index];
                }

            }

            IList<ModelTest> tests = ToolsDisprove.BuildTreeTwoSampleDifferent(sample_0, sample_1);
            foreach (ModelTest test in tests)
            {
                TestList.Add(test);
            }
            requirements = ToolsDisprove.CheckRequirements(sample_0, sample_1);
            Check();
       }

        private void Check()
        {
            List < TestAssertion > user_forced = new List<TestAssertion>();
            if (UserAssumeMeasurementsIndependant)
            {
                user_forced.Add(TestAssertion.MeasurementsIndependant);
            }
            ToolsDisprove.CheckTestAssertions(requirements, user_forced, TestList);
        }

    }
}
