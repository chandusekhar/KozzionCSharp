using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Security.Cryptography
{
    [TestClass]
    public class TestRandomNumberGeneratorExtensions
    {
        [TestMethod]
        public void TestRandomPositiveBigIntegerOfSizeInBits()
        {
            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            BigInteger value = random.RandomPositiveBigIntegerOfSizeInBits(1023);
            Assert.IsTrue(value.BitLength() == 1023);
        }

        [TestMethod]
        public void TestBitlenght()
        {
            BigInteger value = new BigInteger(7);
            //System.Diagnostics.Debug.WriteLine(value.ToByteArray().ToStringHexadecimal());
            //System.Diagnostics.Debug.WriteLine(value.BitLength());
            Assert.IsTrue(value.BitLength() == 3);
            value = value << 1;
            //System.Diagnostics.Debug.WriteLine(value.BitLength());
            Assert.IsTrue(value.BitLength() == 4);
            value = value >> 1;
            //System.Diagnostics.Debug.WriteLine(value.BitLength());
            Assert.IsTrue(value.BitLength() == 3);  
        }

        [TestMethod]
        public void TestIsProbablePrime()
        {
            BigInteger q = 7;
            Assert.IsTrue(q.IsProbablePrime(1000));

            q = 64;//TODO probablistic unit test
            Assert.IsFalse(q.IsProbablePrime(1000));
        }         	
    }
}
