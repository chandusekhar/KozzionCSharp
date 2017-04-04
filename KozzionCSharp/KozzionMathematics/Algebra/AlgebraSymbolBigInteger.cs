using System;
using System.Numerics;

namespace KozzionMathematics.Algebra
{
    public class AlgebraSymbolBigInteger : IAlgebraInteger<BigInteger>
    {
        public BigInteger AddIdentity { get { return 0; } }

        public BigInteger MultiplyIdentity { get { return 1; } }

        public BigInteger Add(BigInteger value_0, BigInteger value_1)
        {
            return value_0 + value_1;
        }

        public BigInteger Subtract(BigInteger value_0, BigInteger value_1)
        {
            return value_0 - value_1;
        }

        public BigInteger Multiply(BigInteger value_0, BigInteger value_1)
        {
            return value_0 * value_1;
        }

        public BigInteger Divide(BigInteger value_0, BigInteger value_1)
        {
            return value_0 / value_1;
        }

        public BigInteger Modulo(BigInteger value_0, BigInteger value_1)
        {
            if (0 <= value_0)
            {
                return value_0 % value_1;
            }
            else
            {
                return value_1 - (Abs(value_0) % value_1);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is AlgebraSymbolBigInteger;
        }

        public override int GetHashCode()
        {
            return base.GetType().GetHashCode();
        }


        public int Compare(BigInteger value_0, BigInteger value_1)
        {
            
            return value_0.CompareTo(value_1);
        }

        public BigInteger Abs(BigInteger value_0)
        {
            return value_0.Abs();
        }

        public BigInteger ToBigInteger(BigInteger value)
        {
            return value;
        }

        public BigInteger ToDomain(int value)
        {
            return value;
        }

        public int ToInt32(BigInteger value)
        {
            if (int.MaxValue < value)
            {
                throw new Exception(value + " out of integer domain");
            }
            return (int)value;
        }

        public BigInteger ToDomain(BigInteger value)
        {
            return value;
        }
    }
}
 