using System;

namespace KozzionMathematics.Algebra
{
    public class AlgebraIntegerInt32 :IAlgebraReal<int>
    {
        public int PositiveInfinity
        {
            get { throw new NotImplementedException(); }
        }

        public int NegativeInfinity
        {
            get { throw new NotImplementedException(); }
        }

        public int MinValue
        {
            get { return int.MinValue; }
        }

        public int MaxValue
        {
            get { return int.MaxValue; }
        }

        public int AddIdentity
        {
            get { return 0; }
        }

        public int MultiplyIdentity
        {
            get { return 1; }
        }

        public int NaN
        {
            get { throw new NotImplementedException(); }
        }

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
            throw new NotImplementedException();
        }

        public int Min(int value_0, int value_1)
        {
            throw new NotImplementedException();
        }

        public int Mean(int value_0, int value_1)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(int value_0, int value_1)
        {
            throw new NotImplementedException();
        }

        public bool IsNaN(int p)
        {
            throw new NotImplementedException();
        }

        public int LogE(int real)
        {
            throw new NotImplementedException();
        }

        public int Log10(int value)
        {
            throw new NotImplementedException();
        }

        public int Sqrt(int realType)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int Sqr(int value_0)
        {
            return value_0 * value_0;
        }
    }

}
