using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.algebra
{
    [TestClass]
    public class TestAlgebraLinearReal64MathNet
    {
        [TestMethod]
        public void TestAddOne()
        {
            TestAlgebraLinear.TestAddOne(new AlgebraLinearReal64MathNet());     
        }

        [TestMethod]
        public void TestAddMany()
        {
            TestAlgebraLinear.TestAddMany(new AlgebraLinearReal64MathNet());
        }


        [TestMethod]
        public void TestMultiplyScalar()
        {
            TestAlgebraLinear.TestMultiplyScalar(new AlgebraLinearReal64MathNet());
        }


        [TestMethod]
        public void TestMatrixMatrixProduct0()
        {
            TestAlgebraLinear.TestMatrixMatrixProduct0(new AlgebraLinearReal64MathNet());
        }

        [TestMethod]
        public void TestMatrixMatrixProduct1()
        {
            TestAlgebraLinear.TestMatrixMatrixProduct1(new AlgebraLinearReal64MathNet());
        }
    }
}
