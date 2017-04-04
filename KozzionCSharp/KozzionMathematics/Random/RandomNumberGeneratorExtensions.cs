using System.Linq;
using System.Numerics;
using KozzionMathematics.Tools;

namespace System.Security.Cryptography
{
    public static class RandomNumberGeneratorExtensions
    {
        public static bool Toss(this RandomNumberGenerator random, float true_chance)
        {
            return random.RandomFloat32Unit() < true_chance;
        }

        public static bool Toss(this RandomNumberGenerator random, double true_chance)
        {
            return random.RandomFloat64Unit() < true_chance;
        }

        public static long RandomInt64(this RandomNumberGenerator random)
        {
            byte[] buffer = new byte[8];
            random.GetBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static ulong RandomUInt64(this RandomNumberGenerator random)
        {
            byte[] buffer = new byte[8];
            random.GetBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static int RandomInt32(this RandomNumberGenerator random)
        {
            byte[] buffer = new byte[4];
            random.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static int RandomInt32(this RandomNumberGenerator random, int max_value_exclusive)
        {
            int random_int = random.RandomInt32();
            if (random_int < 0)
            {
                return (random_int * -1) % max_value_exclusive;
            }
            else
            {
                return random_int % max_value_exclusive;
            }
            
        }

        public static uint RandomUInt32(this RandomNumberGenerator random)
        {
            byte[] buffer = new byte[4];
            random.GetBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }



   

        public static float RandomFloat32(this RandomNumberGenerator random)
        {
            byte[] buffer = new byte[4];
            random.GetBytes(buffer);
            float value = BitConverter.ToSingle(buffer, 0);
            while (Single.IsNaN(value) || Single.IsInfinity(value))
            {
                random.GetBytes(buffer);
                value = BitConverter.ToSingle(buffer, 0);
            }
            return value;
        }

        // Less than 1
        public static float RandomFloat32Unit(this RandomNumberGenerator random)
        {
            byte [] bytes = new byte[8];
            random.GetBytes(bytes);
            ulong number = BitConverter.ToUInt64(bytes, 0);
            return number / (float)(UInt64.MaxValue + 1.0);
        }


        public static double RandomFloat64(this RandomNumberGenerator random)
        {
            byte [] buffer = new byte[8];
            random.GetBytes(buffer);
            double value = BitConverter.ToDouble(buffer, 0);
            while (Double.IsNaN(value) || Double.IsInfinity(value))
            {
                random.GetBytes(buffer);
                value = BitConverter.ToSingle(buffer, 0);
            }
            return value;
        }

        public static int[] RandomPermutation(this RandomNumberGenerator random, int count)
        {
            int[] numbers = ToolsMathSeries.RangeInt32(count);           
            return numbers.OrderBy(x => random.RandomFloat32()).ToArray(); //TODO is a bit expencive
        }
        

        // Less than 1
        public static double RandomFloat64Unit(this RandomNumberGenerator random)
        {
            byte[] bytes = new byte[8];
            random.GetBytes(bytes);
            ulong number = BitConverter.ToUInt64(bytes, 0);
            return number / (UInt64.MaxValue + 1.0);
        }




        public static long RandomLong(this RandomNumberGenerator random, long min, long max)
        {
            EnsureMinLEQMax(ref min, ref max);
            long numbersInRange = unchecked(max - min + 1);
            if (numbersInRange < 0)
            {
                throw new ArgumentException("Size of range between min and max must be less than or equal to Int64.MaxValue");
            }
            long randomOffset = random.RandomInt64();
            if (IsModuloBiased(randomOffset, numbersInRange))
            {
                return RandomLong(random, min, max); // Try again TODO recusion is evil
            }
            else
            {
                return min + PositiveModuloOrZero(randomOffset, numbersInRange);
            }
        }


        private static bool IsModuloBiased(long random_offset, long numbers_in_range)
        {
            long greatest_complete_range = numbers_in_range * (long.MaxValue / numbers_in_range);
            return random_offset > greatest_complete_range;
        }

        private static long PositiveModuloOrZero(long dividend, long divisor)
        {
            long mod;
            Math.DivRem(dividend, divisor, out mod);
            if (mod < 0)
            {
                mod += divisor;
            }
            return mod;
        }

        private static void EnsureMinLEQMax(ref long min, ref long max)
        {
            if (min <= max)
            {
                return;
            }
            long temp = min;
            min = max;
            max = temp;
        }

        /// <summary>
        /// Runturns a random integer x with  0 < x <  exclusive_upper_bound
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="exclusive_upper_bound"></param>
        /// <returns></returns>
        public static BigInteger RandomPositiveBigIntegerBelow(this RandomNumberGenerator random, BigInteger exclusive_upper_bound)
        {
            byte[] bytes = exclusive_upper_bound.ToByteArray();
            BigInteger result = 0;
            do
            {
                random.GetBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                result = new BigInteger(bytes);
            } while (exclusive_upper_bound <= result || result < 1);

            return result;
        }

        public static BigInteger RandomPositiveBigIntegerOfSizeInBits(this RandomNumberGenerator random, long size_in_bits)
        {
            long size_in_bytes = (size_in_bits / 8);
            if (size_in_bits % 8 != 0)
            {
                size_in_bytes++;
            }
 
            BigInteger result = RandomPostiveBigIntegerOfSizeInBytes(random, size_in_bytes);
            while (size_in_bits < result.BitLength())
            {
                result = result >> 1;
            }
            return result;
        }

        public static BigInteger RandomPostiveBigIntegerOfSizeInBytes(this RandomNumberGenerator random, long size_in_bytes)
        {
            byte[] bytes = new byte[size_in_bytes];
            random.GetBytes(bytes);
            bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
            return new BigInteger(bytes);
        }     
    }
}
