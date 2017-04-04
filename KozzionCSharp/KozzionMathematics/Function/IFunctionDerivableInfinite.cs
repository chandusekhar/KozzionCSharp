namespace KozzionMathematics.Function
{
    public interface IFunctionDerivableInfinite<DomainType, RangeType> : IFunctionDerivable<DomainType, RangeType>
    {
        IFunctionDerivableInfinite<DomainType, RangeType> get_derivable_infinite();
    }
}