using KozzionMachineLearningCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Transform
{
    public abstract class ATransform<DomainType, RangeType> :  ITransform<DomainType, RangeType>
    {

        private ATransform<DomainType, RangeType> inner;
        public string FunctionType { get; }
        public bool IsInverse { get; }

        public ATransform(string function_type)
        {
            this.inner = null;
            this.FunctionType = function_type;
            this.IsInverse = false;
        }

        public ATransform(ATransform<DomainType, RangeType> inner, string function_type)
        {
            this.inner = inner;
            this.FunctionType = function_type;
            this.IsInverse = false;
        }
      
        public IFunctionBijective<RangeType, DomainType> GetInverse()
        {
            return GetInverseTransform();
        }



        public abstract RangeType Compute(DomainType domain_value_0);

        public abstract DomainType ComputeInverse(RangeType domain_value_0);
 
        public abstract ITransform<RangeType, DomainType> GetInverseTransform();
      
    }
}
