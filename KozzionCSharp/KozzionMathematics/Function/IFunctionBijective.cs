namespace KozzionMathematics.Function
{
    public interface IFunctionBijective<DomainType, RangeType> : IFunction<DomainType, RangeType>
    {
        DomainType ComputeInverse(RangeType input);

        IFunctionBijective<RangeType, DomainType> GetInverse();

    }
}