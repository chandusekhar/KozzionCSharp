using System;
using System.Collections.Generic;
using System.Numerics;

namespace KozzionMathematics.Tools
{
    public class ToolsMathBigIntegerPrime
    {
        private static BigInteger MILLER_RABIN_CIELING = BigInteger.Parse("3317044064679887385961981");
        private static BigInteger[] MILLER_RABIN_CIELING_WITNESSES = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 };

        public static List<BigInteger> get_prime_between(
            BigInteger lower_bound,
            BigInteger upper_bound)
        {
            List<BigInteger> primes = new List<BigInteger>();
            BigInteger prime = get_next_prime(lower_bound);
            while (prime.CompareTo(upper_bound) != 1)
            {
                primes.Add(prime);
                prime = get_next_prime(prime);
            }
            return primes;
        }

        public static BigInteger get_prime_number(
            BigInteger starting_point,
            BigInteger target_number)
        {
            BigInteger current_prime = starting_point;
            for (BigInteger index_current_prime = 0; index_current_prime <target_number; index_current_prime++)
            {
                current_prime = get_next_prime(current_prime);
            }
            return current_prime;

        }


        // TODO this can be smarter, we now use the miller rabin upper bound of 1/4^k but actually we should trade-off the cost of the probabelistic and the deterministic tests
        public static BigInteger get_next_prime(
            BigInteger input)
        {
            BigInteger candidate = input.NextProbablePrime();
            while (!IsPrime(candidate))
            {
                candidate = candidate.NextProbablePrime();
            }
            return candidate;
        }

        public static bool IsPrime(
            BigInteger input)
        {
            // one is not a prime number!!!
            if (input == 1)
            {
                return false;
            }

            // Do some seaving stuff to accelerate
            if (input == 2)
            {
                return true;
            }
   
            if (input.IsEven)
            {
                return false;
            }

            if (input == 3)
            {
                return true;
            }

            if (input % 3 == 0)
            {
                return false;
            }

            if (input == 5)
            {
                return true;
            }

            if (input % 5 == 0)
            {
                return false;
            }

            // If it is a Mersenne number lucal lehmer will find if it is prime
            if (input.IsMersenne())
		    {
			    return IsPrimeByLucasLehmer(input);
		    }

            // If input is not a Mersenne number using miller rabin for small numbers
            if (input < MILLER_RABIN_CIELING)
            {
                return IsPrimeByMillerRabin(input);
            }

            // If all else fails use AKS
            return IsPrimeByAgrawalKayalSaxena(input);

            // TODO If all else fails use elliptic curve_primality test
            //return IsPrimeByEllipticCurve(input);

            // TODO If all else fails use general miller rabin implementation and wait for riemann hypothesis to be proven
            // return IsPrimeByMillerRabinRiemann(input);
        }

        private static bool IsPrimeByEllipticCurve(
            BigInteger input)
        {
            // TODO If all else fails use elliptic curve_primality test
            throw new NotImplementedException();
        }

        //Prime test is in p for O:log6(N)
        private static bool IsPrimeByAgrawalKayalSaxena(
            BigInteger n)
        {
            BigInteger X = 0;
            BigInteger r= 0;
            BigInteger a = 0;
            BigInteger b = 0;
            if (a.Pow(b) == n)
            {
                return false;
            }

            //TODO Find the smallest r such that Or(n) > (log2 n) 2.
      

            for (a = r; a > 1; a--)
            {
                BigInteger gcd = ToolsMathBigInteger.GetGCDByModulus(a, n);
                if (1< gcd && gcd < n)
                {
                    return false;
                }
      
            }

            
            if (n <= r)
            {
                return true;                     
            }


            BigInteger upper_bound = ToolsMath.Sqrt(EulerPhi(r))*1  ;
            for (a = 0; a < upper_bound; a++)
            {  
                if ((X + a).Pow(n) != X.Pow(n) + a* (X.Pow(r)-1) % n)
                {
                    return false;
                }
            }
            return true;
        }

        // Mersenne prime are in P and at the cost of very little
        public static BigInteger EulerPhi(BigInteger input)
        {
            throw new NotImplementedException();
        }

        // Mersenne prime are in P and at the cost of very little
        public static bool IsPrimeByLucasLehmer(BigInteger input)
	    {
            if (input == 3)
            {
                return true;
            }

            long p = input.BitLength();
            BigInteger s = 4;
            for (int i = 0; i < p - 2; i++)
            {
                s = ((s * s) - 2) % input;
            }
		    return s == BigInteger.Zero;    
	    }

        // Primes are in P and at the cost of log^2(n) (bounded by rymann hypothesis)
        public static bool IsPrimeByMillerRabin(
            BigInteger input)
        {
            if (input == 2)
            {
                return true;
            }
            if (input < 2 || input % 2 == 0)
            {
                return false;
            }

            BigInteger power = input - 1;
            int trailing_count = 0;

            while ((power % 2 == 0) && (power != 0))
            {
                power /= 2;
                trailing_count += 1;
            }

            foreach (BigInteger witness in MILLER_RABIN_CIELING_WITNESSES)
            {
                if (IsCompositeByMillerRabinWitness(input, witness, trailing_count, power))
                {
                    return false;
                }            
            }
            return true;
        }



        // Uses the riemann hypothesis
        public static bool IsPrimeByMillerRabinRiemann(
            BigInteger input)
        {
            BigInteger upper_bound = BigInteger.Min(input - 1, 2 * ((BigInteger)BigInteger.Log(input)).Pow(2));
            for (BigInteger a = 2; a <= upper_bound; a++)
            {
                // https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test#Deterministic_variants
              
            }
            throw new NotImplementedException();
            //return true;
        }

        /**
         * 
         * @param input
         * @param witness
         * @return returns false if input is composite.
         */
        public static bool IsCompositeByMillerRabinWitness(
            BigInteger input,
            BigInteger witness)
        {
            BigInteger power = input - 1;
            int trailing_count = 0;

            while ((power % 2 == 0) && (power != 0))
            {
                power /= 2;
                trailing_count += 1;
            }
            return IsCompositeByMillerRabinWitness(input, witness, trailing_count, power);
        }


        public static bool IsCompositeByMillerRabinWitness(
           BigInteger input,
           BigInteger witness,
           BigInteger trailing_count,
           BigInteger power)
        {
            if (witness == input)
            {
                return false; //(witnesses are all prime, or below input
            }

            BigInteger witnes_base = witness.ModPow(power, input);
            if (witnes_base == 1 || witnes_base == input - 1)
            {
                return false;
            }

            for (int r = 1; r < trailing_count; r++)
            {
                witnes_base = witnes_base.ModPow(2, input);
                if (witnes_base == 1)
                {
                    return true;
                }
                if (witnes_base == input - 1)
                {
                    return false;
                }
            }

            if (witnes_base != input - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool IsTruncatablePrime(
            BigInteger current)
        {
            if (!IsPrime(current))
            {
                return false;
            }
            while (1 < current.ToString().Length)
            {
                current = BigInteger.Parse(current.ToString().Substring(1));

                if (!IsPrime(current))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsReverseTruncatablePrime(
            BigInteger current)
        {
            if (!IsPrime(current))
            {
                return false;
            }
            while (1 < current.ToString().Length)
            {

                current = ToolsMathBigInteger.ReverseDigits(current);
                current = BigInteger.Parse(current.ToString().Substring(1));
                current = ToolsMathBigInteger.ReverseDigits(current);

                if (!IsPrime(current))
                {
                    return false;
                }
            }
            return true;
        }
    }
}