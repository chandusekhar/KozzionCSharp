using System;

namespace KozzionMathematics.Algebra
{
    public class AlgebraRealFloat64 :IAlgebraReal<double>
    {
        public double MinValue
        {
            get { return double.MinValue; }
        }

        public double MaxValue
        {
            get { return double.MaxValue; }
        }

        public double PositiveInfinity { get { return double.PositiveInfinity; } }

        public double NegativeInfinity { get { return double.NegativeInfinity; } }

        public double AddIdentity
        {
            get { return 0; }
        }

        public double MultiplyIdentity
        {
            get { return 1; }
        }

        public double NaN
        {
            get { return double.NaN; }
        }

        public double Add(double value_0, double value_1)
        {
            return value_0 + value_1;
        }

        public double Subtract(double value_0, double value_1)
        {
            return value_0 - value_1;
        }

        public double Multiply(double value_0, double value_1)
        {
            return value_0 * value_1;
        }

        public double Divide(double value_0, double value_1)
        {
            return value_0 / value_1;
        }

        public double Modulo(double value_0, double value_1)
        {
            return value_0 % value_1;
        }

        public double Max(double value_0, double value_1)
        {
            return Math.Max(value_0, value_1);
        }

        public double Min(double value_0, double value_1)
        {
            return Math.Min(value_0, value_1);
        }

        public double Mean(double value_0, double value_1)
        {
            return (value_0 + value_1) / 2;
        }

        public int CompareTo(double value_0, double value_1)
        {
            return value_0.CompareTo(value_1);
        }

        public bool IsNaN(double value)
        {
            return Double.IsNaN(value);
        }





        public double LogE(double value)
        {
            return Math.Log(value);
        }

        public double Log10(double value)
        {
            return Math.Log10(value);
        }


        public int FloorInt(double value)
        {
            return (int)Math.Floor(value);
        }

        public int Compare(double x, double y)
        {
            return x.CompareTo(y);
        }


        public double ToDomain(int value)
        {
            return value;
        }

        public double ToDomain(float value)
        {
            return value;
        }

        public double ToDomain(double value)
        {
            return value;
        }

        public double Sqrt(double value)
        {
            return Math.Sqrt(value);
        }

        public double Pow(double base_value, double power)
        {
            return Math.Pow(base_value, power);
        }

        public double Abs(double value)
        {
            return Math.Abs(value);
        }

        public double Sqr(double value_0)
        {
            return value_0 * value_0;
        }
    }
}
