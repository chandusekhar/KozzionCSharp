using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Transform
{
    public class TransformInverse<DomainType, RangeType>
    { 
        public string FunctionType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RangeType Compute(DomainType domain_value_0)
        {
            throw new NotImplementedException();
        }

        public DomainType ComputeInverse(RangeType input)
        {
            throw new NotImplementedException();
        }

        public IFunctionBijective<RangeType, DomainType> GetInverse()
        {
            throw new NotImplementedException();
        }
    }
}
