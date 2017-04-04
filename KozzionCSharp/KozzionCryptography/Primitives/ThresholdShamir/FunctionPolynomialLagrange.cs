using System;
using System.Collections.Generic;
using System.Numerics;
using KozzionMathematics.Function;
using KozzionMathematics.Algebra;

namespace KozzionCryptography.Primitives.Threshold
{
    public class FunctionPolynomialLagrange<SymbolType> : IFunction<SymbolType, SymbolType>
    {
        public string FunctionType
        {
            get
            {
                return "FunctionPolynomialLagrangeGeneric";
            }
        }


        private IAlgebraInteger<SymbolType> algebra;

        private IList<SymbolType> domain;
        private IList<SymbolType> range;

        public FunctionPolynomialLagrange(IAlgebraInteger<SymbolType> algebra, IList<Tuple<SymbolType, SymbolType>> points)
        {
            this.algebra = algebra;
            this.domain = new SymbolType[points.Count];
            this.range = new SymbolType[points.Count];
            for (int point_index = 0; point_index < points.Count; point_index++)
            {
                domain[point_index] = points[point_index].Item1;
                range[point_index] = points[point_index].Item2;
            }
        }


        public SymbolType Compute(SymbolType domain_value_0)
        {
            SymbolType result = algebra.AddIdentity;
            for (int coefficient_index = 0; coefficient_index < range.Count; coefficient_index++)
            {
                algebra.Add(result, algebra.Multiply(range[coefficient_index], ComputeBasis(domain_value_0, coefficient_index)));
            }
            return result;
        }

        public SymbolType ComputeBasis(SymbolType domain_value_0, int coefficient_index)
        {
            SymbolType result = algebra.MultiplyIdentity;
            for (int basis_index = 0; basis_index < range.Count; basis_index++)
            {
                if (coefficient_index != basis_index)
                {

                    SymbolType term = algebra.Divide(
                        algebra.Subtract(domain_value_0, domain[basis_index]),
                        algebra.Subtract(domain[coefficient_index], domain[basis_index]));
                    algebra.Multiply(result, term);
                }

            }
            return result;
        }
    }
}