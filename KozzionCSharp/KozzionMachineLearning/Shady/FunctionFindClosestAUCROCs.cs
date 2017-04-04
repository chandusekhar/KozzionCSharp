using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Shady
{
    public class FunctionFindClosestAUCROCs : IFunction<ISet<int>, double>
    {
        public string FunctionType { get { return "FunctionFindClosestAUCROCs"; } }

        private bool[] labels;
        private double[][] values;
        private double[] desired_aucs;
        private double[] weigths;

        public FunctionFindClosestAUCROCs(bool [] labels, double[][] values, double [] desired_aucs, double[] weigths)
        {
            this.labels = labels;
            this.values = values;
            this.desired_aucs = desired_aucs;
            this.weigths = weigths;
        }

        public double Compute(ISet<int> selection)
        {
            bool[] selected_labels = labels.Select(selection);
            double error = 0;
            for (int index = 0; index < values.Length; index++)
            {
                double auc = ToolsMathStatistics.ComputeROCAUCTrapeziod(selected_labels, values[index].Select(selection));
                error += Math.Abs(desired_aucs[index] - auc) * weigths[index];
            }         
            return error;
        }
    }
}
