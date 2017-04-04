using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
namespace KozzionMathematics.Numeric.system
{
    public interface ITimeInvariantIntegrator<MatrixType>
	{

        AMatrix<MatrixType>[] Integrate(IFunction<AMatrix<MatrixType>, AMatrix<MatrixType>> differential_equation, AMatrix<MatrixType> initial_value, double step_size, int step_count);

        AMatrix<MatrixType> Integrate(IFunction<AMatrix<MatrixType>, AMatrix<MatrixType>> differential_equation, AMatrix<MatrixType> initial_value, double step_size);

	}
}