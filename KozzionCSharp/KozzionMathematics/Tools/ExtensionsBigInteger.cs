using KozzionMathematics.Tools;
using System.Security.Cryptography;

namespace System.Numerics
{
    public static class ExtensionsBigInteger
    {
        /// <summary>
        /// Returns a number that is biggere than the input and has at most 1/4^50 chance of being composite
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BigInteger NextProbablePrime(this BigInteger value)
        {
            if (value < 0)
            {
                throw new Exception("This fuction does not accept negative values");
            }

            if (value < 2)
            {
                return 2;
            }
            if (value.IsEven)
            {
                return value++;
            }

            while (!value.IsProbablePrime(50))
            {
                value += 2;
            }
            return value;

        }

        public static bool IsProbablePrime(this BigInteger input, int number_of_iterations)
        {
            //TODO could be paralel
            if (input == 2 || input == 3)
                return true;
            if (input < 2 || input % 2 == 0)
                return false;

            BigInteger power = input - 1;
            int trailing_count = 0;

            while ((power % 2 == 0) && (power != 0))
            {
                power /= 2;
                trailing_count += 1;
            }

            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            for (int index_witness = 0; index_witness < number_of_iterations; index_witness++)
            {
                BigInteger witness = random.RandomPositiveBigIntegerBelow(input);
                if (ToolsMathBigIntegerPrime.IsCompositeByMillerRabinWitness(input, witness, trailing_count, power))
                {
                    return false;
                }
            }
            return true;
        }

        public static BigInteger Abs(this BigInteger value)
        {
            return BigInteger.Abs(value);
        }

        public static BigInteger FlipSign(this BigInteger value)
        {
            return value * -1;
        }

        public static BigInteger Pow(this BigInteger value, BigInteger exponent)
        {
            return BigInteger.Pow(value, (int)exponent);
        }

        public static BigInteger ModPow(this BigInteger value, BigInteger exponent, BigInteger modulus)
        {
            return BigInteger.ModPow(value, exponent, modulus);
        }



        public static BigInteger ModInverse(this BigInteger value, BigInteger exponent)
        {
            throw new NotImplementedException();
        }

        public static bool IsMersenne(this BigInteger input)
        {
            return (input + 1).IsPowerOfTwo;
        }

        public static long BitLength(this BigInteger value)
        {
            byte[] bytes = value.ToByteArray();
            long bit_length = (bytes.Length - 1) * 8;
            byte front_byte = bytes[bytes.Length - 1];
            do
            {
                front_byte = (byte)(front_byte >> 1);
                bit_length++;
            }
            while (front_byte != 0);
            return bit_length;
        }

        public static bool IsBitSet(this BigInteger value, int bit_index)
        {
            throw new NotImplementedException();
        }
    }
}