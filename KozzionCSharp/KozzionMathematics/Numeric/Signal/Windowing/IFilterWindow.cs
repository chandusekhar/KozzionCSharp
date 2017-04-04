using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric.Signal
{
    public interface IFilterWindow<RealValueType> : IFunction<RealValueType, RealValueType>
	{
        RealValueType WindowUpperBound { get; }

        RealValueType WindowLowerBound { get; }

        RealValueType FilterWidth { get; }
    }
}