using KozzionMathematics.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Tools
{
    [TestClass]
    public class ToolsMathBigIntegerPrimeTest
    {
        [TestMethod]
        public void TestIsPrime()
        {
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrime(4), "4 is not prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrime(8), "8 is not prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrime(9), "9 is not prime");

            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(2), "2 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(3), "3 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(5), "5 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(7), "7 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(11), "11 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(13), "13 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(17), "17 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrime(23), "23 is prime");
        }

        [TestMethod]
        public void TestIsPrimeByMillerRabin()

        {
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(4), "4 is not prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(8), "8 is not prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(9), "9 is not prime");

            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(2), "2 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(3), "3 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(11), "11 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(13), "13 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(17), "17 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(23), "23 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabin(997), "997  is prime");
        }


        [TestMethod]
        public void TestIsPrimeByMillerRabinRiemann()

        {
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(4), "4 is not prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(8), "8 is not prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(9), "9 is not prime");

            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(2), "2 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(3), "3 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(11), "11 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(13), "13 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(17), "17 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(23), "23 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByMillerRabinRiemann(997), "997  is prime");
        }

        [TestMethod]
        public void TestPrimeByLucasLehmer()
        {
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(3), "3 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(7), "7 is prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(15), "15 is not prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(31), "31 is prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(63), "63 is not prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(127), "127 is prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(255), "255 is not prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(8191), "8191 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(524287), "524287 is prime");
            Assert.IsTrue(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(2147483647), "2147483647 is prime");
            Assert.IsFalse(ToolsMathBigIntegerPrime.IsPrimeByLucasLehmer(2147483645), "2147483645 not prime");
        }
        


    }
}
