using KozzionMathematics.Function;

namespace KozzionMachineLearning.Transform
{
    public interface ITransform<DomainType, RangeType> : IFunctionBijective<DomainType, RangeType>
    {
    }
}