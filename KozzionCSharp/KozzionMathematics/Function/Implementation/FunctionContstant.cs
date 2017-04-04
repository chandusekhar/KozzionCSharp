namespace KozzionMathematics.Function.Implementation
{
    public class FunctionContstant<DomainType, RangeType> : IFunction<DomainType, RangeType>
    {
        public string FunctionType { get { return "FunctionContstant"; } }

        private RangeType value;
        public FunctionContstant(RangeType value)
        {
            this.value = value;
        }
    
        public RangeType Compute(DomainType value_domain)
        {
 	        return value;
        }
    }
}
