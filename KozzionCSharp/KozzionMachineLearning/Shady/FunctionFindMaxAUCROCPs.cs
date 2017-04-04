using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Test.OneSampleROC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Shady
{
    public class FunctionFindMaxAUCROCPs : IFunction<ISet<int>, double>
    {
        public string FunctionType { get { return "FunctionFindClosestAUCROCs"; } }

        private bool[] labels;
        private double[] values;

        public FunctionFindMaxAUCROCPs(bool[] labels, double[] values)
        {
            this.labels = labels;
            this.values = values;

        }

        public double Compute(ISet<int> selection)
        {
            IList<int> selection_list = ToolsCollection.ConvertToSortedList(selection);
            return TestROCMannWhitneyWilcoxon.TestStatic(labels.Select(selection_list), values.Select(selection_list));
        }
    }
}
