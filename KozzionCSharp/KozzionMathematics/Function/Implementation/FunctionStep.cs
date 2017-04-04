using System;

namespace KozzionMathematics.Function.Implementation
{
    public class FunctionStep<DomainType> : IFunction<DomainType, bool>
        where DomainType : IComparable<DomainType>
    {
        public string FunctionType { get { return "FunctionStep"; } }

        public DomainType Threshold {get; private set;}
        public bool UpperResult { get; private set; }
        public bool UpperResultIncludesThreshold { get; private set; }
        public  FunctionStep(DomainType threshold, bool upper_result, bool upper_result_includes_threshold)
        {
            this.Threshold = threshold;
            this.UpperResult = upper_result;
            this.UpperResultIncludesThreshold = upper_result_includes_threshold; 
        }

        public FunctionStep(DomainType threshold)
           : this(threshold, true, true)
        {
     
        }

        public bool Compute(DomainType domain_value_0)
        {
            if (0 < domain_value_0.CompareTo(this.Threshold ))
            {
                return this.UpperResult;
            }
            else if (domain_value_0.CompareTo(this.Threshold) < 0)
            {
                return !this.UpperResult;
            }
            else
            {
                return this.UpperResultIncludesThreshold == this.UpperResult;
            }

        }
    }
}
