namespace KozzionMathematics.Function.Implementation
{
    public class FunctionIdentity<DomainType> :
			IFunctionBijective<DomainType, DomainType>
	{
        public string FunctionType { get { return "FunctionIdentity"; } }
        public DomainType Compute(
			DomainType input)
		{
			return input;
		}

        public DomainType ComputeInverse(DomainType input)
        {
            return input;
        }

        public IFunctionBijective<DomainType, DomainType> GetInverse()
		{
            return this;
		}

     
    }
}