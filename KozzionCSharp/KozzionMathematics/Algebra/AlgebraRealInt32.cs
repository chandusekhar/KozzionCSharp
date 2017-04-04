using System;

namespace KozzionMathematics.Algebra
{
    public class AlgebraRealInt32 : IAlgebraReal<int>
    {
        public int MinValue { get { return int.MinValue; } }

        public int MaxValue { get { return int.MaxValue; } }

        public int NaN  { get { throw new NotImplementedException(); } }

        public int PositiveInfinity { get { return MaxValue; } }

        public int NegativeInfinity { get { return MinValue; } }

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
            return value_0 % value_1;
        }

        public int Max(int value_0, int value_1)
        {
            return Math.Max(value_0, value_1);
        }

        public int Min(int value_0, int value_1)
        {
            return Math.Min(value_0, value_1);
        }

        public int Mean(int value_0, int value_1)
        {
            return (value_0 + value_1) / 2;
        }

        public int CompareTo(int value_0, int value_1)
        {
            return value_0.CompareTo(value_1);
        }

        public bool IsNaN(int value_0)
        {
            return false;
        }

        public int Sqrt(int value_0)
        {
            return (int)Math.Sqrt(value_0);
        }

        public int LogE(int value_0)
        {
            return (int)Math.Log(value_0);
        }

        public int Log10(int value_0)
        {
            return (int)Math.Log10(value_0);
        }


        public int FloorInt(int value)
        {
            return value;
        }

        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }


        public int ToDomain(int value)
        {
            return value;
        }

        public int ToDomain(float value)
        {
            return (int)value;
        }

        public int ToDomain(double value)
        {
            return (int)value;
        }

        public RealType Pow<RealType>(RealType base_value, RealType power)
        {
            throw new NotImplementedException();
        }

        public int Pow(int base_value, int power)
        {
            throw new NotImplementedException();
        }

        public int Abs(int realType)
        {
            return Math.Abs(realType);
        }

        public int Sqr(int value_0)
        {
            return value_0 * value_0;
        }
    }
}
