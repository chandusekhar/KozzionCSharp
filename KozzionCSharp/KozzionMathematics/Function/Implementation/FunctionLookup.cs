using System;
using System.Collections.Generic;

namespace KozzionMathematics.Function.Implementation
{
    public class FunctionLookup<DomainType, RangeType> : IFunction<DomainType, RangeType>
    {
        public string FunctionType { get { return "FunctionLookup"; } }

        private IDictionary<DomainType, RangeType> mapping;

        public FunctionLookup(DomainType [] domain,  RangeType [] range)
        {
            if (domain.Length != range.Length)
            {
                throw new Exception("Arrays should be of equal length");
            }
            else
            {
                mapping = new Dictionary<DomainType, RangeType>();
                for (int index = 0; index < domain.Length; index++)
                {
                    mapping[domain[index]] = range[index];
                }
            }
        }

        public RangeType Compute(DomainType value_domain)
        {
            if (mapping.ContainsKey(value_domain))
            {
                return mapping[value_domain];
            }
            else
            {
                throw new Exception("Unknown mapping: " + value_domain);
            }
        }
    }
}
