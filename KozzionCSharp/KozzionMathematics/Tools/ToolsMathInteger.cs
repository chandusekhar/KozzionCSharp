using System;


namespace KozzionMathematics.Tools
{
    public class ToolsMathInteger
    {
        public static int compute_first_mulptiple_equal_or_larger(
            int limit,
            int base_value)
        {
            return (limit / base_value) + 1;
        }

        public static int Pow(
            int base_value,
            int exponent)
        {
            if (exponent == 0)
            {
                return 1;
            }

            int result = base_value;
            for (int index = 1; index < exponent; index++)
            {
                result = result * base_value;
            }
            return result;
        }

        public static int pow_safe(
            int base_value,
            int exponent)
        {
            if (exponent == 0)
            {
                return 0;
            }

            if (31 < exponent) // TODO HAXXX!!! only for base 2
            {
                throw new Exception("Out of integer_range");
            }

            for (int i = 1; i < exponent; i++)
            {
                base_value = base_value * base_value;
            }
            return base_value;
        }

        public static int Max(
            int value_1,
            int value_2,
            int value_3)
        {
            return Math.Max(Math.Max(value_1, value_2), value_3);
        }

        public static bool IsPrime(
            int value)
        {
            if (value < 2)
            {
                return false;
            }
            else
            {
                return ToolsMathBigIntegerPrime.IsPrime(value);
            }
        }


    }
}