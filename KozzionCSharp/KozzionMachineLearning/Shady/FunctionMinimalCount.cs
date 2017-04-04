using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Shady
{
    public class FunctionMinimalCount : IFunction<ISet<int>, bool>
    {
        public string FunctionType { get { return "FunctionMinimalCount"; } }

        private int minimal_count;

        public FunctionMinimalCount(int minimal_count)
        {
            this.minimal_count = minimal_count;
        }

        public bool Compute(ISet<int> domain_value_0)
        {
            return minimal_count <= domain_value_0.Count; 
        }
    }
}
