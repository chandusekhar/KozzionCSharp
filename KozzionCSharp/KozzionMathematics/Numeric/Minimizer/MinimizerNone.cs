using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;
using KozzionCore.Tools;
using System;

namespace KozzionMathematics.Numeric.Minimizer
{
    public class MinimizerNone : AMinimizer<double[], Simplex>
    {
        public MinimizerNone()
        {
        }  

        public override MinimizerResult Minimize(
                IFunction<double[], double> minimization_function,
                IFunction<double[], bool> validation_function,
                IMinimizerHaltingCriterion<Simplex> halting_criterion,
                double[] parameters_initial,
                double[] initial_vextex_size)
        {  
            Simplex simplex = new Simplex(parameters_initial, minimization_function, validation_function); /* Holds vertices of simplex */
            MinimizerResult result = new MinimizerResult(simplex);
            result.IsHalted = false;  
            result.IsSuccesFull = true;
            return result;
        }
    }
}
