using KozzionCore.Tools;
using KozzionMathematics.Numeric.Solver.LinearSolver;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kozzionmathematicstest.Numeric.Solvers.Linear
{
    [TestClass]
    public class SolverLinearGMRESMathNetTest
    {
        //from matlab test grmes
        [TestMethod]
        public void SolverLinearGMRESMathNetTrivial0()
        {
        
            SolverLinearGMRESMathNet gmres = new SolverLinearGMRESMathNet();
            double[,] a = new double[5, 5];
            a[0, 0] = 1;
            a[1, 1] = 2;
            a[2, 2] = 3;
            a[3, 3] = 4;
            a[4, 4] = 5;

            Matrix<double> A = new DenseMatrix(5, 5, ToolsCollection.ConvertToArray1D(a));
            Vector<double> x0 = new DenseVector(new double[5]);
            Vector<double> b = new DenseVector(new double[] { 1, 2, 3, 4, 5 });
            SolverLinearResult result = gmres.Solve(A, b, x0, 4);
            Assert.AreEqual(0.9203, result.SolutionList[4][0], 0.001);
            Assert.AreEqual(1.0399, result.SolutionList[4][1], 0.001);
            Assert.AreEqual(0.9823, result.SolutionList[4][2], 0.001);
            Assert.AreEqual(1.0050, result.SolutionList[4][3], 0.001);
            Assert.AreEqual(0.9994, result.SolutionList[4][4], 0.001);
            //[~, solutions, ~, ~] = gmres_simple(A, b, x0, 4, 1)
            //0         0         0         0         0   d
            //0.2298    0.4597    0.6895    0.9193    1.1491
            //0.4897    0.8318    1.0262    1.0729    0.9719
            //0.7346    1.0209    1.0404    0.9747    1.0050
            //0.9203    1.0399    0.9823    1.0050    0.9994  

        }
    }
}
