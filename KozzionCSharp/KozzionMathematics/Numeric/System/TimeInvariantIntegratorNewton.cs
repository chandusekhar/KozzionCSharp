using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
namespace KozzionMathematics.Numeric.system
{
    public class TimeInvariantIntegratorNewton<MatrixType, DomainType> : ATimeInvariantIntegrator<MatrixType>
	{

        public override AMatrix<MatrixType> Integrate(IFunction<AMatrix<MatrixType>, AMatrix<MatrixType>> differential_equation, AMatrix<MatrixType> initial_value, double step_size)
		{
            AMatrix<MatrixType> result = differential_equation.Compute(initial_value);
            return initial_value + (result * step_size);
		}

	}
}