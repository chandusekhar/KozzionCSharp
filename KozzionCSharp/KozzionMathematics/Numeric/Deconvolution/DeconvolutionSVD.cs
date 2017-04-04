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
    public class DeconvolutionSVD<MatrixType>
    {
        public DeconvolutionSVD(IAlgebraLinear<MatrixType> algebra, IFunction<double, double> input_function, double[] signal_sample_times)
        {
            // build AIF matrix

            AMatrix<MatrixType> forward_matrix = null;
            SVD<MatrixType> svd = algebra.ComputeSVD(forward_matrix);
        }

        public IFunction<double, double> GetIRF(double[] signal)
        {

            return null;
        }
    }
}
