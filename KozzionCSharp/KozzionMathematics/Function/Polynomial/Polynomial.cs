using System;
using System.Diagnostics;
using System.Text;
using KozzionCore.Tools;
using KozzionMathematics.Algebra;
using KozzionMathematics.Tools;

namespace KozzionMathematics.Function.polynomial
{
    public class Polynomial<DomainType> :IFunction<DomainType, DomainType>
    {
        public string FunctionType { get { return "Polynomial"; } }
        IAlgebraInteger<DomainType> algebra;

        private DomainType [] coeffecients;

        public Polynomial(IAlgebraInteger<DomainType> algebra)
        {
            this.algebra = algebra;
            coeffecients = new DomainType[0];
        }

        public Polynomial(IAlgebraInteger<DomainType> algebra, DomainType [] coeffecients)
        {
            this.algebra = algebra;
            this.coeffecients = ToolsCollection.Copy(coeffecients);
        }

        public DomainType Compute(DomainType value_domain)
        {
            DomainType power = algebra.MultiplyIdentity;
            DomainType result = algebra.AddIdentity;
            for (int index = 0; index < coeffecients.Length; index++)
            {
                result = algebra.Add(result, algebra.Multiply(coeffecients[index], power));
                power = algebra.Multiply(power, value_domain);
            }
            return result;
        }

        public Polynomial<DomainType> Add(Polynomial<DomainType> other)
        {
            Debug.Assert(algebra.Equals(other.algebra));

            if (other.coeffecients.Length < coeffecients.Length)
            {
                DomainType[] coeffecients = ToolsCollection.Copy(this.coeffecients);
                for (int index = 0; index < other.coeffecients.Length; index++)
                {
                    coeffecients[index] = algebra.Add(this.coeffecients[index], other.coeffecients[index]);
                }
                return new Polynomial<DomainType>(algebra, coeffecients);
            }
            else
            {
                DomainType[] coeffecients = ToolsCollection.Copy(other.coeffecients);
                for (int index = 0; index < this.coeffecients.Length; index++)
                {
                    coeffecients[index] = algebra.Add(this.coeffecients[index], other.coeffecients[index]);
                }
                return new Polynomial<DomainType>(algebra, coeffecients);
            }          
        }

        public Polynomial<DomainType> Subtract(Polynomial<DomainType> other)
        {
            Debug.Assert(algebra.Equals(other.algebra));

            DomainType[] coeffecients = new DomainType[Math.Max(this.coeffecients.Length, other.coeffecients.Length)];
            for (int index = 0; index < coeffecients.Length; index++)
            {
                if (index < this.coeffecients.Length)
                {
                    coeffecients[index] = this.coeffecients[index];
                }
                else
                {
                    coeffecients[index] = algebra.AddIdentity;
                }
            }

            for (int index = 0; index < other.coeffecients.Length; index++)
            {
                coeffecients[index] = algebra.Subtract(coeffecients[index],other.coeffecients[index] );
            }

            return new Polynomial<DomainType>(algebra,  ToolsMathCollectionInteger.CropValuesEnd(coeffecients, algebra.AddIdentity));
        }

        public Polynomial<DomainType> Multiply(Polynomial<DomainType> other)
        {
            Debug.Assert(algebra.Equals(other.algebra));

            DomainType[] coeffecients = new DomainType[this.coeffecients.Length + other.coeffecients.Length - 1];

            for (int index_offset = 0; index_offset < this.coeffecients.Length; index_offset++)
            {
                for (int index_coeffecient = 0; index_coeffecient < other.coeffecients.Length; index_coeffecient++)
                {
                    coeffecients[index_offset + index_coeffecient] = algebra.Add(coeffecients[index_offset + index_coeffecient], algebra.Multiply(this.coeffecients[index_offset], other.coeffecients[index_coeffecient]));
                }
            }
            return new Polynomial<DomainType>(algebra, coeffecients);
        }


        public Tuple<Polynomial<DomainType>,Polynomial<DomainType>> DivideRemainder(Polynomial<DomainType> divisor_poly)
        {
            Debug.Assert(algebra.Equals(divisor_poly.algebra));
            // if divisor is too big
            if (coeffecients.Length < divisor_poly.coeffecients.Length)
            {
                return new Tuple<Polynomial<DomainType>, Polynomial<DomainType>>(new Polynomial<DomainType>(algebra), new Polynomial<DomainType>(algebra, coeffecients));           
            }
            // if divisor is too big
            if ((coeffecients.Length == divisor_poly.coeffecients.Length) && (algebra.Compare(coeffecients[coeffecients.Length - 1], coeffecients[coeffecients.Length - 1]) == -1))
            {
                return new Tuple<Polynomial<DomainType>, Polynomial<DomainType>>(new Polynomial<DomainType>(algebra), new Polynomial<DomainType>(algebra, coeffecients));
            }

            //Else do long division
            DomainType[] remainder = ToolsCollection.Copy(coeffecients);
            DomainType[] results = new DomainType[coeffecients.Length];
            DomainType[] divisor = divisor_poly.coeffecients;            
            for (int index = 0; index < results.Length; index++)
            {
                results[index] = algebra.AddIdentity;
            }
            int max_shift = remainder.Length - divisor.Length;
     
           
            for (int shift = max_shift; 0 <= shift; shift--)
            {
                DomainType multiplier = algebra.Divide(remainder[shift], divisor[divisor.Length - 1]);
                for (int index = shift; index < coeffecients.Length; index++)
                {
                    remainder[index] = algebra.Subtract(remainder[index], algebra.Multiply(divisor[index + shift], multiplier));
                }
                results[shift] = multiplier;
            }

            return new Tuple<Polynomial<DomainType>,Polynomial<DomainType>>(new Polynomial<DomainType>(algebra, results), new Polynomial<DomainType>(algebra, remainder));           
        }

        public Polynomial<DomainType> Divide(Polynomial<DomainType> divisor)
        {
            return DivideRemainder(divisor).Item1;           
        }

        public Polynomial<DomainType> Modulo(Polynomial<DomainType> divisor)
        {
            return DivideRemainder(divisor).Item2;
        }



        public override String ToString() 
        {
            StringBuilder builder = new StringBuilder();
            if(0 < coeffecients.Length)
            {
                builder.Append(        coeffecients[0]);
            }
            for (int index = 1; index < coeffecients.Length; index++)
            {
                builder.Append(" + " + coeffecients[index] + "x^" + index);
            }
            return builder.ToString();
        }
    }
}
