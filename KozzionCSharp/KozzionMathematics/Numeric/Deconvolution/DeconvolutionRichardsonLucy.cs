using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Deconvolution
{
    class DeconvolutionRichardsonLucy<MatrixType>
    {
        IAlgebraLinear<MatrixType> algebra;
        IList<IFunction<double, double>> basis_function_list;
        AMatrix<MatrixType> forward_matrix;
        //AMatrix<MatrixType> forward_matrix_flipped;

        public DeconvolutionRichardsonLucy(IAlgebraLinear<MatrixType> algebra, ITemplateBasisFunction template, IFunction<double, double> input_function, double[] signal_sample_times)
        {
            this.algebra = algebra;
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

        IFunction<double, double> GetIRF(double[] signal)
        {

            AMatrix<MatrixType> solution = this.algebra.Create(new double[signal.Length]);
            AMatrix<MatrixType> solution_next = this.algebra.Create(new double[signal.Length]);
            for (int iteration = 0; iteration < 10; iteration++)
            {
                //AMatrix<MatrixType> sums = forward_matrix_flipped * this.algebra.DivideElements(signal, (forward_matrix * solution));
                //solution_next = this.algebra.MulplyElements(solution, sum);
                solution = solution_next;
            }

            return new FunctionWeigthed<double, double>(new AlgebraRealFloat64(), basis_function_list, solution.ToArray1DFloat64());
        }
    }
}



