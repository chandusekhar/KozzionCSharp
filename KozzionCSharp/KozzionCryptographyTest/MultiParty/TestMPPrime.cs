using System;
using System.Numerics;
using System.Diagnostics;
using KozzionMathematics.Function;
using KozzionCryptography.multiparty;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KozzionCryptography.multiparty
{
    [TestClass]
    public class MPPrimeTest
	{
        [TestMethod]
        public void TestMPZLPrime()
        {
            long psize = 1024;
			long qsize = 160;
			int mr_iterations = 64;    
            Tuple<BigInteger, BigInteger, BigInteger> tuple = MPPrime.MPZLPrime(psize, qsize, mr_iterations);
        }

        [TestMethod]
        public void TestFindBigPrime()
        {
            int iteration = 0;
            long size = 160;
            int iteration_count = 64;
      
            BigInteger q;
            do
            {
                iteration++;
                q = mpz_srandom.mpz_wrandomb(size);
            } while ((q.BitLength() < size) || !q.IsProbablePrime(iteration_count));

            long bits = q.BitLength();
            bits = q.BitLength();
        }

	}
}