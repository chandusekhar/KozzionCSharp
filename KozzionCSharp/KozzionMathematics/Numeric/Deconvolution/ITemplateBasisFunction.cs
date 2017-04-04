using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric
{
    public interface ITemplateBasisFunction
    {
        IFunction<double, double> Generate(IFunction<double, double> input_function, double[] signal_sample_times, int signal_index);
    }
}