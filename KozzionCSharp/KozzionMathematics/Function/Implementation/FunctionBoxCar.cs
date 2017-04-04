using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Function.Implementation
{

     public class FunctionBoxCar<DomainType> : IFunction<DomainType, bool>
        where DomainType : IComparable<DomainType>
    {
        public string FunctionType { get { return "FunctionBoxCar"; } }

        public DomainType LowerThreshold { get; private set; }
        public DomainType UpperThreshold { get; private set; }
        public bool InsideResultIncludesLowerThreshold { get; private set; }
        public bool InsideResultIncludesUpperThreshold { get; private set; }
        public bool InsideResult { get; private set; }

        public FunctionBoxCar(DomainType lower_theshold, bool inside_result_includes_lower_threshold,  DomainType upper_threshold, bool inside_result_includes_upper_threshold, bool inside_result)
        {
            this.LowerThreshold = lower_theshold;
            this.UpperThreshold = upper_threshold;
            this.InsideResult = inside_result;
            this.InsideResultIncludesLowerThreshold = inside_result_includes_lower_threshold;
            this.InsideResultIncludesUpperThreshold = inside_result_includes_upper_threshold;
        }

        public FunctionBoxCar(DomainType lower_theshold, DomainType upper_threshold)
           : this(lower_theshold, true, upper_threshold, true, true)
        {

        }

        public bool Compute(DomainType domain_value_0)
        {
            if ( domain_value_0.CompareTo(this.LowerThreshold) < 0)
            {
                return !this.InsideResult;
            }
            else if (domain_value_0.CompareTo(this.LowerThreshold) == 0)
            {
                return this.InsideResultIncludesLowerThreshold == this.InsideResult;
            }
            else
            {
                if (0 < domain_value_0.CompareTo(this.UpperThreshold))
                {
                    return !this.InsideResult;
                }
                else if (domain_value_0.CompareTo(this.UpperThreshold) == 0)
                {
                    return this.InsideResultIncludesLowerThreshold == this.InsideResult;
                }
                else
                {
                    return this.InsideResult;
                }
            }

        }
    }
}
