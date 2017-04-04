using System;
using System.Collections.Generic;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric
{
    public class FunctionWeigthed<DomainType, RealType> : IFunction<DomainType, RealType>
    {
        public string FunctionType { get { return "FunctionWeigthed"; } }
        private IAlgebraReal<RealType> algebra;
        private IList<IFunction<DomainType, RealType>> basis_function_list;
        private IList<RealType> weight_list;

        public FunctionWeigthed(IAlgebraReal<RealType> algebra, IList<IFunction<DomainType, RealType>> basis_function_list, IList<RealType> weight_list)
        {
            this.algebra = algebra;
            this.basis_function_list = basis_function_list;
            this.weight_list = weight_list;
        }

        public RealType Compute(DomainType domain_value_0)
        {
            RealType value = algebra.AddIdentity;
            for (int index = 0; index < basis_function_list.Count; index++)
            {
                value = algebra.Add(value, algebra.Multiply(basis_function_list[index].Compute(domain_value_0), weight_list[index]));
            }
            return value;
        }
    }
}