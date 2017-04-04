using KozzionMathematics.Algebra;
using KozzionMathematics.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Tools
{
    [TestClass]
    public class ToolsMathBigIntegerTest
    {
        [TestMethod]
        public void TestExtendedEuclideanAlgorithm0()
        {
            Tuple<int, int, int> tuple_0 = ToolsMath.ExtendedEuclideanAlgorithm(4864, 3458);
            Assert.AreEqual(32, tuple_0.Item1);
            Assert.AreEqual(-45, tuple_0.Item2);
            Assert.AreEqual(38, tuple_0.Item3); //GCD


            Tuple<int, int, int> tuple_1 = ToolsMath.ExtendedEuclideanAlgorithm(6, 3);
            Assert.AreEqual(3, tuple_1.Item3);
            Tuple<int, int, int> tuple_2 = ToolsMath.ExtendedEuclideanAlgorithm(39, 26);
            Assert.AreEqual(13, tuple_2.Item3);

            Tuple<int, int, int> tuple_3 = ToolsMath.ExtendedEuclideanAlgorithm(7, 23);
            Assert.AreEqual(10, tuple_3.Item1);
            Tuple<int, int, int> tuple_4 = ToolsMath.ExtendedEuclideanAlgorithm(2, 23);
            Assert.AreEqual(-11, tuple_4.Item1);
        }

        [TestMethod]
        public void TestExtendedEuclideanAlgorithm1()
        {
            Tuple<BigInteger, BigInteger, BigInteger> tuple_0 = ToolsMath.ExtendedEuclideanAlgorithm(new AlgebraSymbolBigInteger(), 4864, 3458);
            Assert.AreEqual(32, tuple_0.Item1);
            Assert.AreEqual(-45, tuple_0.Item2);
            Assert.AreEqual(38, tuple_0.Item3); //GCD



            Tuple<BigInteger, BigInteger, BigInteger> tuple_1 = ToolsMath.ExtendedEuclideanAlgorithm(new AlgebraSymbolBigInteger(), 6, 3);
            Assert.AreEqual(3, tuple_1.Item3);
            Tuple<BigInteger, BigInteger, BigInteger> tuple_2 = ToolsMath.ExtendedEuclideanAlgorithm(new AlgebraSymbolBigInteger(), 39, 26);
            Assert.AreEqual(13, tuple_2.Item3);

            Tuple<BigInteger, BigInteger, BigInteger> tuple_3 = ToolsMath.ExtendedEuclideanAlgorithm(new AlgebraSymbolBigInteger(), 7, 23);
            Assert.AreEqual(10, tuple_3.Item1);
            Tuple<BigInteger, BigInteger, BigInteger> tuple_4 = ToolsMath.ExtendedEuclideanAlgorithm(new AlgebraSymbolBigInteger(), 2, 23);
            Assert.AreEqual(-11, tuple_4.Item1);
        }


    }
}
