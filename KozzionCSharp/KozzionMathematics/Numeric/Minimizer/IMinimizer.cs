using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;

namespace KozzionMathematics.Numeric.Minimizer
{
    public abstract class AMinimizer<DomainType, HaltingInfoType>
    {
        public MinimizerResult Minimize(
            IFunction<double[], double> function_to_minimize,
            IMinimizerHaltingCriterion<HaltingInfoType> halting_criterion,
            double[] parameters_initial,
            double[] initial_vextex_size)
        {
            return Minimize(
                function_to_minimize,
                new FunctionContstant<double[], bool>(true),
                halting_criterion,
                parameters_initial,
                initial_vextex_size);
        }

        public abstract MinimizerResult Minimize(
            IFunction<double[], double> function_to_minimize,
            IFunction<double[], bool> validation_function,
            IMinimizerHaltingCriterion<HaltingInfoType> halting_criterion,
            double[] initial_value,
            double[] step_size);
    }
}
