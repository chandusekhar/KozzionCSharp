namespace KozzionMathematics.Function
{
	public interface IFunctionDerivable<DomainType, RangeType> : IFunction<DomainType, RangeType>
	{
		IFunction<DomainType, RangeType> get_derivative();
	}
}