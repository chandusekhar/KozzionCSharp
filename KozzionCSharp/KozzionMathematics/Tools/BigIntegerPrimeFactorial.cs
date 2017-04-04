using System;
using System.Collections.Generic;
using KozzionCore.Collections;
using KozzionMathematics.Tools;
using System.Numerics;

namespace KozzionMathematics.ToolsBigInteger
{
    public class BigIntegerPrimeFactorial
    {
        private BigInteger           d_factorized_number;
        private DictionaryCount<BigInteger> d_factor_counts;

        public BigIntegerPrimeFactorial(BigInteger to_factor)
        {
            d_factorized_number = to_factor;
            d_factor_counts = new DictionaryCount<BigInteger>();
            List<BigInteger> factors = ToolsMathBigInteger.factorize(to_factor);
            foreach (BigInteger factor in factors)
            {
                d_factor_counts.Increment(factor);
            }
        }

        private BigIntegerPrimeFactorial(DictionaryCount<BigInteger> factor_counts)
        {
            d_factor_counts = factor_counts;
            d_factorized_number = 1;
            foreach (BigInteger key in d_factor_counts.Keys)
            {
                int times = d_factor_counts[key];
                for (int times_index = 0; times_index < times; times_index++)
                {
                    d_factorized_number = d_factorized_number * key;
                }
            }
        }

        public static BigIntegerPrimeFactorial get_common_basis(List<BigIntegerPrimeFactorial> prime_factorial_list)
        {
            HashSet<BigInteger> prime_set = new HashSet<BigInteger>();
            HashSet<BigInteger> factor_set = new HashSet<BigInteger>();
            foreach (BigIntegerPrimeFactorial prime_factorial in prime_factorial_list)
            {
                if (prime_factorial.is_prime())
                {
                    prime_set.Add(prime_factorial.get_factorized_number());
                }
                else
                {
                    foreach (BigInteger key in prime_factorial.d_factor_counts.Keys)
                    {
                        factor_set.Add(key);
                    }
                }
            }

            DictionaryCount<BigInteger> factor_counts = new DictionaryCount<BigInteger>();
            // build count map
            foreach (BigInteger key in factor_set)
            {
                int max_count = 1;
                foreach (BigIntegerPrimeFactorial prime_factorial in prime_factorial_list)
                {
                    max_count = Math.Max(max_count, prime_factorial.d_factor_counts[key]);
                }
                factor_counts.Add(key, max_count);
            }

            // build add primes
            foreach (BigInteger prime in prime_set)
            {
                if (!factor_set.Contains(prime))
                {
                    factor_counts.Increment(prime);
                }
            }

            return new BigIntegerPrimeFactorial(factor_counts);
        }

        private bool is_prime()
        {
            if (d_factor_counts.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void print()
        {
            System.Diagnostics.Debug.WriteLine("factorized number: " + d_factorized_number);
            System.Diagnostics.Debug.WriteLine("factors: ");
            foreach (BigInteger key in d_factor_counts.Keys)
            {
                 int times = d_factor_counts[key];
                for (int times_index = 0; times_index < times; times_index++)
                {
                    System.Diagnostics.Debug.WriteLine(key);
                }
            }
        }

        public BigInteger get_factorized_number()
        {
            return d_factorized_number;
        }

    }
}
