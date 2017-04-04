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
    public class TestAlgebraLinear
    {
        public static void TestAddOne<MatrixType>(IAlgebraLinear<MatrixType> algebra)
        {   
            AMatrix<MatrixType> A = algebra.Create(new double[,] { { 1, 2 }, { 3, 4 } });
            AMatrix<MatrixType> B = A + 1;

            Assert.AreEqual(2, B.GetElement(0, 0));
            Assert.AreEqual(3, B.GetElement(0, 1));
            Assert.AreEqual(4, B.GetElement(1, 0));
            Assert.AreEqual(5, B.GetElement(1, 1));
        }

        public static void TestAddMany<MatrixType>(IAlgebraLinear<MatrixType> algebra)
        {
            AMatrix<MatrixType> A = algebra.Create(new double[,] { { 1, 2 }, { 3, 4 } });
            AMatrix<MatrixType> B = algebra.Create(new double[,] { { 1, 2 }, { 3, 4 } });
            AMatrix<MatrixType> C = A + B;

            Assert.AreEqual(2, C.GetElement(0, 0));
            Assert.AreEqual(4, C.GetElement(0, 1));
            Assert.AreEqual(6, C.GetElement(1, 0));
            Assert.AreEqual(8, C.GetElement(1, 1));
        }

        public static void TestMultiplyScalar<MatrixType>(IAlgebraLinear<MatrixType> algebra)
        {
            AMatrix<MatrixType> A = algebra.Create(new double[,] { { 1, 2 }, { 3, 4 } });
            AMatrix<MatrixType> C = A * 2;

            Assert.AreEqual(2, C.GetElement(0, 0));
            Assert.AreEqual(4, C.GetElement(0, 1));
            Assert.AreEqual(6, C.GetElement(1, 0));
            Assert.AreEqual(8, C.GetElement(1, 1));
        }

        public static void TestMatrixMatrixProduct0<MatrixType>(IAlgebraLinear<MatrixType> algebra)
        {
            AMatrix<MatrixType> A = algebra.Create(new double[,] { { 1, 0 }, { 0, 1 } });
            AMatrix<MatrixType> B = algebra.Create(new double[,] { { 1, 2 }, { 3, 4 } });
            AMatrix<MatrixType> C = A * B;

            Assert.AreEqual(1, C.GetElement(0, 0));
            Assert.AreEqual(2, C.GetElement(0, 1));
            Assert.AreEqual(3, C.GetElement(1, 0));
            Assert.AreEqual(4, C.GetElement(1, 1));
        }


        public static void TestMatrixMatrixProduct1<MatrixType>(IAlgebraLinear<MatrixType> algebra)
        {
            AMatrix<MatrixType> A = algebra.Create(new double[,] { { 1, 0 ,0 },{0,1,0}, {0, 0, 1 } });
            AMatrix<MatrixType> B = algebra.Create(new double[,] { { 1, 2 }, { 3, 4 }, {5, 6}});
            AMatrix<MatrixType> C = A * B;

            Assert.AreEqual(1, C.GetElement(0, 0));
            Assert.AreEqual(1, C.GetElement(0, 0));
            Assert.AreEqual(1, C.GetElement(0, 0));
            Assert.AreEqual(2, C.GetElement(0, 1));
            Assert.AreEqual(3, C.GetElement(1, 0));
            Assert.AreEqual(4, C.GetElement(1, 1));
        }
    }
}
