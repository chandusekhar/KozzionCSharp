using System;
using KozzionMathematics.DataStructure.BigDecimal;

namespace KozzionMathematics.Algebra
{
    public class AlgebraRealBigDecimal : IAlgebraReal<BigDecimal>
    {
        public BigDecimal MinValue
        {
            get { return BigDecimal.MinValue; }
        }

        public BigDecimal MaxValue
        {
            get { return BigDecimal.MaxValue; }
        }

        public BigDecimal PositiveInfinity { get { return BigDecimal.PositiveInfinity; } }

        public BigDecimal NegativeInfinity { get { return BigDecimal.NegativeInfinity; } }

        public BigDecimal AddIdentity
        {
            get { return 0; }
        }

        public BigDecimal MultiplyIdentity
        {
            get { return 1; }
        }

        public BigDecimal NaN
        {
            get { return BigDecimal.NaN; }
        }

        public BigDecimal Add(BigDecimal value_0, BigDecimal value_1)
        {
            return value_0 + value_1;
        }

        public BigDecimal Subtract(BigDecimal value_0, BigDecimal value_1)
        {
            return value_0 - value_1;
        }

        public BigDecimal Multiply(BigDecimal value_0, BigDecimal value_1)
        {
            return value_0 * value_1;
        }

        public BigDecimal Divide(BigDecimal value_0, BigDecimal value_1)
        {
            return value_0 / value_1;
        }

        public BigDecimal Modulo(BigDecimal value_0, BigDecimal value_1)
        {
            return value_0.Modulo(value_1);
        }

        public BigDecimal Max(BigDecimal value_0, BigDecimal value_1)
        {
            if (value_0 < value_1)
            {
                return value_1;
            }
            else
            {
                return value_0;
            }
        }

        public BigDecimal Min(BigDecimal value_0, BigDecimal value_1)
        {
            if (value_0 < value_1)
            {
                return value_0;
            }
            else
            {
                return value_1;
            }
        }

        public BigDecimal Mean(BigDecimal value_0, BigDecimal value_1)
        {
            return (value_0 + value_1) / 2;
        }

        public int CompareTo(BigDecimal value_0, BigDecimal value_1)
        {
            return value_0.CompareTo(value_1);
        }

        public bool IsNaN(BigDecimal value)
        {
            return BigDecimal.IsNaN(value);
        }

        public BigDecimal LogE(BigDecimal value)
        {
            return BigDecimal.LogE(value);
        }

        public BigDecimal Log10(BigDecimal value)
        {
            return BigDecimal.Log10(value);
        }

        public BigDecimal Sqrt(BigDecimal value_0)
        {
            return BigDecimal.Pow((double)value_0, 0.5);
        }

        public int FloorInt(BigDecimal value)
        {
            return (int)Math.Floor((double)value);
        }

  
        public int Compare(BigDecimal x, BigDecimal y)
        {
            return x.CompareTo(y);
        }


        public BigDecimal ToDomain(int value)
        {
            return value;
        }

        public BigDecimal ToDomain(float value)
        {
            return value;
        }

        public BigDecimal ToDomain(double value)
        {
            return value;
        }

        public BigDecimal CreateFrom(BigDecimal value)
        {
            return value;
        }

        public RealType Pow<RealType>(RealType base_value, RealType power)
        {
            throw new NotImplementedException();
        }

        public BigDecimal Pow(BigDecimal base_value, BigDecimal power)
        {
            throw new NotImplementedException();
        }

        public BigDecimal Abs(BigDecimal realType)
        {
            throw new NotImplementedException();
        }

        public BigDecimal Sqr(BigDecimal value_0)
        {
            return value_0 * value_0;
        }
    }
}
