using KozzionMathematics.Numeric.linear_solver;
using MathNet.Numerics.LinearAlgebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Algebra;
using System;

namespace KozzionMathematics.Numeric.Solver.LinearSolver
{
    public class SolverLinearMathNetQR : ISolverLinear<Matrix<double>>
    {
        public AMatrix<Matrix<double>> Solve(AMatrix<Matrix<double>> A, AMatrix<Matrix<double>> b)
        {
            return new MatrixMathNet(A.Data.Solve(b.Data));   
        }

        public AMatrix<Matrix<double>> Solve(AMatrix<Matrix<double>> A, AMatrix<Matrix<double>> b, AMatrix<Matrix<double>> initial)
        {
            return new MatrixMathNet(A.Data.Solve(b.Data));
        }

        public AMatrix<Matrix<double>> Solve(AMatrix<Matrix<double>> A, AMatrix<Matrix<double>> b, AMatrix<Matrix<double>> initial, int max_iter)
        {
            return new MatrixMathNet(A.Data.Solve(b.Data));
        }
    }
}
