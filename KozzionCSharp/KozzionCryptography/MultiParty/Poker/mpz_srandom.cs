
using System;
using System.Numerics;
using System.Security.Cryptography;


namespace KozzionCryptography.multiparty
{
    public class mpz_srandom
    {
        private static RandomNumberGenerator d_random = new RNGCryptoServiceProvider();

        public static long mpz_grandom_ui()
        {
            return Math.Abs(d_random.RandomInt64());          
        }

        public static long mpz_grandom_ui_nomodbias(
            long modulo)
        {
            long div, max, rnd = 0;

            if ((modulo == 0) || (modulo == 1))
                return 0;

            /* Remove ``modulo bias'' by limiting the return values */
            div = (long.MaxValue - modulo + 1) / modulo;
            max = ((div + 1) * modulo) - 1;
            do
                rnd = mpz_grandom_ui();
            while (rnd > max);

            return rnd;
        }

        public static  long mpz_ssrandom_ui()
        {
            return mpz_grandom_ui();
        }

        public static long mpz_srandom_ui()
        {
            return mpz_grandom_ui();
        }

        public static long mpz_wrandom_ui()
        {
            return mpz_grandom_ui();
        }

        public static long mpz_ssrandom_mod(
            long modulo)
        {
            return mpz_grandom_ui_nomodbias(modulo) % modulo;
        }

        public static long mpz_srandom_mod(
            long modulo)
        {
            return mpz_grandom_ui_nomodbias(modulo) % modulo;
        }

        public static long mpz_wrandom_mod(
            long modulo)
        {
            return mpz_grandom_ui_nomodbias(modulo) % modulo;
        }

        public static BigInteger mpz_grandomb(
            long size_in_bits)
        {
            return d_random.RandomPositiveBigIntegerOfSizeInBits(size_in_bits);
        }

        public static BigInteger mpz_ssrandomb(
            long size)
        {
            return mpz_grandomb(size);
        }

        public static BigInteger mpz_srandomb(
            long size)
        {
            return mpz_grandomb(size);
        }

        public static BigInteger mpz_wrandomb(
            long size)
        {
            return mpz_grandomb(size);
        }

        public static BigInteger mpz_grandomm(
            BigInteger m)
        {
            return d_random.RandomPositiveBigIntegerBelow(m);
        }

        public BigInteger mpz_ssrandomm(
            BigInteger m)
        {
            return mpz_grandomm(m);
        }

        public static BigInteger mpz_srandomm(
            BigInteger m)
        {
            return mpz_grandomm(m);
        }

        public static BigInteger mpz_wrandomm(
            BigInteger m)
        {
            return mpz_grandomm(m);
        }
    }
}