using System;
using System.Numerics;

namespace KozzionMathematics.Algebra
{
    public sealed class AlgebraSymbolInt32 : IAlgebraInteger<int>
    {
        public int AddIdentity { get { return 0; } }

        public int MultiplyIdentity { get { return 1; } }

        public int Add(int value_0, int value_1)
        {
            return value_0 + value_1;
        }

        public int Subtract(int value_0, int value_1)
        {
            return value_0 - value_1;
        }

        public int Multiply(int value_0, int value_1)
        {
            return value_0 * value_1;
        }

        public int Divide(int value_0, int value_1)
        {
            return value_0 / value_1;
        }

        public int Modulo(int value_0, int value_1)
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
            return obj is AlgebraSymbolInt32;
        }

        public override int GetHashCode()
        {
            return base.GetType().GetHashCode();
        }

        public int Compare(int value_0, int value_1)
        {
            return value_0.CompareTo(value_1);
        }

        public int Abs(int value_0)
        {
            return Math.Abs(value_0);
        }

        public int ToDomain(int value)
        {
            return value;
        }

        public int ToDomain(BigInteger value)
        {
            if (int.MaxValue < value)
            {
                throw new Exception(value + " out of integer domain");
            }
            return (int)value;
        }

        public int ToInt32(int value)
        {
            return value;
        }

        public BigInteger ToBigInteger(int value)
        {
            return value;
        }
    }
}
