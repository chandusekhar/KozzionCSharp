using KozzionCore.Tools;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Numeric.linear_solver;
using KozzionMathematics.Numeric.Solver.LinearSolver;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Solvers
{
    [TestClass]
    public class TestGMRES
    {
        //from matlab test grmes
        [TestMethod]
        public void TestGMRESTrivial()
        {
            AlgebraLinearReal64MathNet algebra = new AlgebraLinearReal64MathNet();
            SolverLinearGMRES<Matrix<double>> gmres = new SolverLinearGMRES<Matrix<double>>(algebra);
            double[,] a = new double[5, 5];
            a[0, 0] = 1;
            a[1, 1] = 2;
            a[2, 2] = 3;
            a[3, 3] = 4;
            a[4, 4] = 5;

            AMatrix<Matrix<double>> A = algebra.Create(a);
            AMatrix<Matrix<double>> x0 = algebra.Create(new double[5], true);
            AMatrix<Matrix<double>> b = algebra.Create(new double[] { 1, 2, 3, 4, 5 }, true);
            double[,] mat = gmres.Solve(A, b, x0, 4).ToArray2DFloat64();
            Assert.AreEqual(mat[0, 0], 0.9203, 0.001);
            Assert.AreEqual(mat[1, 0], 1.0399, 0.001);
            Assert.AreEqual(mat[2, 0], 0.9823, 0.001);
            Assert.AreEqual(mat[3, 0], 1.0050, 0.001);
            Assert.AreEqual(mat[4, 0], 0.9994, 0.001);
            //[~, solutions, ~, ~] = gmres_simple(A, b, x0, 4, 1)
            //0         0         0         0         0
            //0.2298    0.4597    0.6895    0.9193    1.1491
            //0.4897    0.8318    1.0262    1.0729    0.9719
            //0.7346    1.0209    1.0404    0.9747    1.0050
            //0.9203    1.0399    0.9823    1.0050    0.9994  

        }
    }
}
