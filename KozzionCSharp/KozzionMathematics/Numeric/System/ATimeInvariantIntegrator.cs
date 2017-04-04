using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
namespace KozzionMathematics.Numeric.system
{
    public abstract class ATimeInvariantIntegrator<MatrixType> : ITimeInvariantIntegrator<MatrixType>
	{

        public AMatrix<MatrixType>[] Integrate(IFunction<AMatrix<MatrixType>, AMatrix<MatrixType>> differential_equation, AMatrix<MatrixType> initial_value, double step_size, int step_count)
		{
            AMatrix<MatrixType>[] steps = new AMatrix<MatrixType>[step_count + 1]; // Add initial step
			steps[0] = initial_value;
			for (int time_step = 0; time_step < step_count; time_step++)
			{
				steps[time_step + 1] = Integrate(differential_equation, steps[time_step], step_size);
			}
			return steps;
		}

        public abstract AMatrix<MatrixType> Integrate(IFunction<AMatrix<MatrixType>, AMatrix<MatrixType>> differential_equation, AMatrix<MatrixType> value, double step_size);

    }
}