using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric.Minimizer
{
    public class MinimizerBroydenFletcherGoldfarbShanno : AMinimizer<double[], Simplex>
    {


        public override MinimizerResult Minimize(
            IFunction<double[], double> function_to_minimize, 
            IFunction<double[], bool> validation_function, 
            IMinimizerHaltingCriterion<Simplex> halting_criterion, 
            double[] initial_value, 
            double[] step_size)
        {
            throw new NotImplementedException();
        }
    }
}
