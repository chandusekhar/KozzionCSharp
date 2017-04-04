using System;

namespace KozzionMathematics.Algebra
{
    [Serializable]
    public class AlgebraRealFloat32 : IAlgebraReal<float>
    {
        public float MinValue
        {
            get { return float.MinValue; }
        }

        public float MaxValue
        {
            get { return float.MaxValue; }
        }

        public float PositiveInfinity { get { return float.PositiveInfinity; } }

        public float NegativeInfinity { get { return float.NegativeInfinity; } }

        public float AddIdentity
        {
            get { return 0; }
        }

        public float MultiplyIdentity
        {
            get { return 1; }
        }

        public float NaN
        {
            get { return float.NaN; }
        }

        public float Add(float value_0, float value_1)
        {
            return value_0 + value_1;
        }

        public float Subtract(float value_0, float value_1)
        {
            return value_0 - value_1;
        }

        public float Multiply(float value_0, float value_1)
        {
            return value_0 * value_1;
        }

        public float Divide(float value_0, float value_1)
        {
            return value_0 / value_1;
        }

        public float Modulo(float value_0, float value_1)
        {
            return value_0 % value_1;
        }

        public float Max(float value_0, float value_1)
        {
            return Math.Max(value_0, value_1);
        }

        public float Min(float value_0, float value_1)
        {
            return Math.Min(value_0, value_1);
        }

        public float Mean(float value_0, float value_1)
        {
            return (value_0 + value_1) / 2;
        }

        public int CompareTo(float value_0, float value_1)
        {
            return value_0.CompareTo(value_1);
        }

        public bool IsNaN(float value)
        {
            return float.IsNaN(value);
        }





        public float LogE(float value)
        {
            return (float)Math.Log(value);
        }

        public float Log10(float value)
        {
            return (float)Math.Log10(value);
        }


        public int FloorInt(float value)
        {
            return (int)Math.Floor(value);
        }

        public int Compare(float x, float y)
        {
            return x.CompareTo(y);
        }


        public float ToDomain(int value)
        {
            return value;
        }

        public float ToDomain(float value)
        {
            return value;
        }

        public float ToDomain(double value)
        {
            return (float)value;
        }

        public float Sqrt(float value_0)
        {
            return (float)Math.Sqrt(value_0);
        }

        public float Pow(float base_value, float power)
        {
            return (float)Math.Pow(base_value, power);
        }

        public float Abs(float value_0)
        {
            return Math.Abs(value_0);
        }

        public float Sqr(float value_0)
        {
            return value_0 * value_0;
        }
    }
}
