using System;
using System.Collections.Generic;
using System.Numerics;
using KozzionCore.Tools;
using KozzionMathematics.ToolsBigInteger;



namespace KozzionMathematics.Tools
{

    

    public class ToolsMathBigInteger
    {
        

        public static int CompareAbs(
            BigInteger value_0,
            BigInteger value_1)
        {
            return value_0.Abs().CompareTo(value_1.Abs());
        }

         public static BigInteger get_smallest_common_multiple(
            List<BigInteger> input)
        {
            List<BigIntegerPrimeFactorial> prime_factorial_list = new List<BigIntegerPrimeFactorial>();
            foreach (BigInteger input_element in input)
            {
                prime_factorial_list.Add(new BigIntegerPrimeFactorial(input_element));
            }
            return BigIntegerPrimeFactorial.get_common_basis(prime_factorial_list).get_factorized_number();
        }

        public static List<BigInteger> factorize(
            BigInteger input)
        {
            List<BigInteger> factors = new List<BigInteger>();
            BigInteger last_divisor = get_smallest_divisor(input);

            if (last_divisor == null)
            {
                return factors;
            }
            else
            {
                factors.Add(last_divisor);
            }

            input /= last_divisor;

            while (true)
            {
                BigInteger new_divisor = get_smallest_divisor(input);
                if (new_divisor == null)
                {
                    factors.Add(input);
                    return factors;
                }
                else
                {
                    factors.Add(new_divisor);
                    last_divisor = new_divisor;
                    input /= last_divisor;
                }
            }
        }

        //TODO check for weird inputs
        public static BigInteger get_smallest_divisor(
            BigInteger input)
        {
 
            BigInteger candidate = 2;
            while ((candidate * candidate) < input)
            {
                if ((input % candidate) == new BigInteger())
                {
                    return candidate;
                }
                //candidate = candidate.nextProbablePrime();
                candidate = candidate + 2;
            }
            throw new Exception("Every integer has a divisor");
        }

        public static BigInteger get_largest_prime_factor(
            BigInteger input)
        {
            BigInteger last_divisor = get_smallest_divisor(input);      
            input = input % last_divisor;
            while (true)
            {
                BigInteger new_divisor = get_smallest_divisor(input);
                if (new_divisor == input)
                {
                    return input;
                }
                else
                {
                    last_divisor = new_divisor;
                    input = input / last_divisor;
                }
            }
        }

        public static BigInteger get_next_divisor(
            BigInteger input,
            BigInteger starting_point)
        {
            BigInteger candidate = starting_point + 1;
            while ((candidate * candidate) < input)
            {
                if (input % candidate == 0)
                {
                    return candidate;
                }
                candidate = candidate + 1;
            }
            return 0;
        }

        public static BigInteger sum(
            List<BigInteger> list)
        {
            BigInteger sum = 0;
            foreach (BigInteger element in list)
            {
                sum = sum + element;
            }
            return sum;
        }

        public static BigInteger ReverseDigits(
            BigInteger value)
        {
            return BigInteger.Parse(ToolsString.Reverse(value.ToString()));
        }  

        public static BigInteger compute_totient(
            BigInteger value)
        {
            BigInteger result = value;

            for (BigInteger i = 2; i * i < value; i = i++)
            {
                if (value % i == 0)
                {
                    result = result - (result / i);
                }

                while (value % i == 0)
                {
                    value = value / i;
                }
            }

            if (1 < value)
            {
                result = result - (result / value);
            }

            return result;
        }

        // TODO this is good! we can use this to do a faster co prime test...
        // maybe also common base factorization? GCD should be the product of all common factors
        public static BigInteger GetGCDByModulus(BigInteger value_0, BigInteger value_1)
        {
            while (value_0 != 0 && value_1 != 0)
            {
                if (value_0 > value_1)
                    value_0 %= value_1;
                else
                    value_1 %= value_0;
            }
            return Max(value_0, value_1);
        }

        public static BigInteger Max(BigInteger value_0, BigInteger value_1)
        {
            if (value_0 <= value_1)
            {
                return value_1;
            }
            else
            {
                return value_0;
            }
        }

        public static bool AreCoprime(BigInteger value_0, BigInteger value_1)
        {
            return GetGCDByModulus(value_0, value_1) == 1;
        }
    }
}
