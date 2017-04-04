using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Function.polynomial;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KozzionMathematicsTest.functions.polynomial
{
    [TestClass]
    public class TestPolynomialInt32
    {

        [TestMethod]
        public void TestToString()
        {
            PolynomialInt32 polynomial = new PolynomialInt32(new int [] {0, 1, 0, 3});
            Assert.AreEqual("0 + 1x^1 + 0x^2 + 3x^3", polynomial.ToString());
        }


        [TestMethod]
        public void TestAdd()
        {
            Polynomial<int> polynomial_0 = new PolynomialInt32(new int[] { 0, 1, 1, 1 });
            Polynomial<int> polynomial_1 = new PolynomialInt32(new int[] { 0, 0, 0, 0 });
            Polynomial<int> polynomial_2 = polynomial_0.Add(polynomial_1);
            Assert.AreEqual("0 + 1x^1 + 1x^2 + 1x^3", polynomial_2.ToString());

            Polynomial<int> polynomial_3 = new PolynomialInt32(new int[] { 6, 1});
            Polynomial<int> polynomial_4 = new PolynomialInt32(new int[] { 0, 2, -2, 4 });
            Polynomial<int> polynomial_5 = polynomial_3.Add(polynomial_4);
            Assert.AreEqual("6 + 3x^1 + -2x^2 + 4x^3", polynomial_5.ToString());
        }

        [TestMethod]
        public void TestSubtract()
        {
            Polynomial<int> polynomial_0 = new PolynomialInt32(new int[] { 0, 1, 1});
            Polynomial<int> polynomial_1 = new PolynomialInt32(new int[] { 0, 0, 1, 1});
            Polynomial<int> polynomial_2 = polynomial_0.Subtract(polynomial_1);
            Assert.AreEqual("0 + 1x^1 + 0x^2 + -1x^3", polynomial_2.ToString());

            Polynomial<int> polynomial_3 = new PolynomialInt32(new int[] { 6, 1,  2, 4});
            Polynomial<int> polynomial_4 = new PolynomialInt32(new int[] { 0, 2, -2, 4});
            Polynomial<int> polynomial_5 = polynomial_3.Subtract(polynomial_4);
            Assert.AreEqual("6 + -1x^1 + 4x^2", polynomial_5.ToString());

            Polynomial<int> polynomial_7 = new PolynomialInt32(new int[] { 6, 1, 2, 4 });
            Polynomial<int> polynomial_8 = new PolynomialInt32(new int[] { 0, 2, 2});
            Polynomial<int> polynomial_9 = polynomial_7.Subtract(polynomial_8);
            Assert.AreEqual("6 + -1x^1 + 0x^2 + 4x^3", polynomial_9.ToString());
        }

        [TestMethod]
        public void TestMultiply()
        {
            Polynomial<int> polynomial_0 = new PolynomialInt32(new int[] { 0, 1, 1 });
            Polynomial<int> polynomial_1 = new PolynomialInt32(new int[] { 1, 1 });
            Polynomial<int> polynomial_2 = polynomial_0.Multiply(polynomial_1);
            Assert.AreEqual("0 + 1x^1 + 2x^2 + 1x^3", polynomial_2.ToString());

            Polynomial<int> polynomial_3 = new PolynomialInt32(new int[] { 1, 4 });
            Polynomial<int> polynomial_4 = new PolynomialInt32(new int[] { 0, 2, 0, 4 });
            Polynomial<int> polynomial_5 = polynomial_3.Multiply(polynomial_4);
            Assert.AreEqual("0 + 2x^1 + 8x^2 + 4x^3 + 16x^4", polynomial_5.ToString());
        }

        [TestMethod]
        public void TestDivide()
        {
            Polynomial<int> polynomial_0 = new PolynomialInt32(new int[] { 0,  0, 2, 0, 1});
            Polynomial<int> polynomial_1 = new PolynomialInt32(new int[] { -1, 0, 1 });
            Polynomial<int> polynomial_2 = polynomial_0.Divide(polynomial_1);
            Assert.AreEqual("0 + 1x^1 + 2x^2 + 1x^3", polynomial_2.ToString());

            Polynomial<int> polynomial_3 = new PolynomialInt32(new int[] { 1, 4 });
            Polynomial<int> polynomial_4 = new PolynomialInt32(new int[] { 0, 2, 0, 4 });
            Polynomial<int> polynomial_5 = polynomial_3.Divide(polynomial_4);
            Assert.AreEqual("0 + 2x^1 + 8x^2 + 4x^3 + 16x^4", polynomial_5.ToString());

            //(x5 + 3x3 + 4)+(6x6 + 4x3) = 6x6 + x5 + 7x3 + 4
            //(x5 + 3x3 + 4)-(6x6 + 4x3) = 5x6 + x5 +10x3 + 4
            //(x5 + 3x3 + 4)*(6x6 + 4x3) = 6x11 + 7x9 + 4x8 + 3x6 + 5x3
            //(3x6 + 7x4 + 4x3 + 5) ÷ (x4 + 3x3 + 4) = 3x2 + 3x + 3 with remainder x3 + 10x2 + 4x +1
        }

        [TestMethod]
        public void TestModulo()
        {
            Polynomial<int> polynomial_0 = new PolynomialInt32(new int[] { 0, 1, 1 });
            Polynomial<int> polynomial_1 = new PolynomialInt32(new int[] { 1, 1 });
            Polynomial<int> polynomial_2 = polynomial_0.Modulo(polynomial_1);
            Assert.AreEqual("0 + 1x^1 + 2x^2 + 1x^3", polynomial_2.ToString());

            Polynomial<int> polynomial_3 = new PolynomialInt32(new int[] { 1, 4 });
            Polynomial<int> polynomial_4 = new PolynomialInt32(new int[] { 0, 2, 0, 4 });
            Polynomial<int> polynomial_5 = polynomial_3.Modulo(polynomial_4);
            Assert.AreEqual("0 + 2x^1 + 8x^2 + 4x^3 +  16x^4", polynomial_5.ToString());
        }
    }
}
