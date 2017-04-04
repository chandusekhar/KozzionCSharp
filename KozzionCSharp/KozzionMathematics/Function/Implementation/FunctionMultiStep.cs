using KozzionMathematics.Tools;
using System;

namespace KozzionMathematics.Function.Implementation
{
    public class FunctionMultiStep<DomainType, RangeType> : IFunction<DomainType, RangeType>
        where DomainType : IComparable<DomainType>
    {
        public string FunctionType { get { return "FunctionMultiStep"; } }

        private DomainType[] thresholds;
        private RangeType[] values;
        public bool ResultIncludesThreshold { get; private set; }

        public FunctionMultiStep(DomainType [] thresholds, RangeType[] values, bool result_includes_threshold)
        {
            this.thresholds = thresholds;
            this.values = values;
            this.ResultIncludesThreshold = result_includes_threshold; 
        }

        public FunctionMultiStep(DomainType[] threshols, RangeType[] values)
           : this(threshols, values, true)
        {
     
        }

        public RangeType Compute(DomainType domain_value_0)
        {
            int index = -1;
            if (ResultIncludesThreshold)
            {
                index = ToolsMathCollection.FirstEqualOrLargerIndex(this.thresholds, domain_value_0);          
            }
            else
            {
                index = ToolsMathCollection.FirstLargerIndex(this.thresholds, domain_value_0);
            }

            if (index == -1)
            {
                return values[values.Length - 1];
            }
            else
            {
                return values[index];
            }
        }
    }
}
