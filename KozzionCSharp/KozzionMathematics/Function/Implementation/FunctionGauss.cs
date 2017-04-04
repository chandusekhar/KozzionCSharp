using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Function.Implementation
{
    public class FunctionGauss : IFunction<double[], double>
    {
        public string FunctionType { get { return "FunctionGauss"; } }
     
        public double Compute(double[] input)
        {

            throw new NotImplementedException();
            //return input[0] * Math.Exp(- ToolsMath((input[2] - input[1]) * (input[2] - input[1])) / ();
        }
    }
}
