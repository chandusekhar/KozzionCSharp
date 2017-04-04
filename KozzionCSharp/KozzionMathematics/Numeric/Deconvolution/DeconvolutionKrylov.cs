using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using KozzionMathematics.Numeric.linear_solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric
{
    public class DeconvolutionKrylov<MatrixType>
    {
        IAlgebraLinear<MatrixType> algebra;
        ISolverLinear<MatrixType> solver;
        IList<IFunction<double, double>> basis_function_list;
        AMatrix<MatrixType> forward_matrix;

        public DeconvolutionKrylov(IAlgebraLinear<MatrixType> algebra, ITemplateBasisFunction template, IFunction<double, double> input_function, double[] signal_sample_times)
        {
            this.algebra = algebra;
            this.solver = new SolverLinearGMRES<MatrixType>(algebra);
            this.basis_function_list = new List<IFunction<double, double>>();           
            double[,] forward_array = new double[signal_sample_times.Length, signal_sample_times.Length];

            for (int column_index = 0; column_index < signal_sample_times.Length; column_index++)
            {
                IFunction<double, double> basis_function = template.Generate(input_function, signal_sample_times, column_index);
                basis_function_list.Add(basis_function);
                for (int row_index = 0; row_index < signal_sample_times.Length; row_index++)
                {
                    forward_array[row_index, column_index] = basis_function.Compute(signal_sample_times[row_index]);
                }
            }
            forward_matrix = algebra.Create(forward_array);
        }


        public IFunction<double, double> GetIRF(double[] signal)
        {
            AMatrix<MatrixType> weights = solver.Solve(forward_matrix, algebra.Create(signal,true));
            return new FunctionWeigthed<double,double>(new AlgebraRealFloat64(), basis_function_list, weights.ToArray1DFloat64());
        }
    }
}
