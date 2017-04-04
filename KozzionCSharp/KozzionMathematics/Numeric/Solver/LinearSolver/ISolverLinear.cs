using KozzionMathematics.Datastructure.Matrix;

namespace KozzionMathematics.Numeric.linear_solver
{
    public interface ISolverLinear<MatrixType>
    {
        AMatrix<MatrixType> Solve(AMatrix<MatrixType> A, AMatrix<MatrixType> b);

        AMatrix<MatrixType> Solve(AMatrix<MatrixType> A, AMatrix<MatrixType> b, AMatrix<MatrixType> initial);

        AMatrix<MatrixType> Solve(AMatrix<MatrixType> A, AMatrix<MatrixType> b, AMatrix<MatrixType> initial, int max_iter);

    }
}