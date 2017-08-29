
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Tools
{
    public static class ToolsMathLinear
    {
        public static Matrix<double> CovarianceMatrix(Matrix<double> input)
        {

            Matrix<double> ones = new DenseMatrix( input.RowCount, 1, Enumerable.Repeat(1.0, input.RowCount).ToArray()) ;
            Matrix<double> mean_a = ones * Means0(input).ToRowMatrix(); ;
            Matrix<double> temp = input - mean_a;
            return (temp.Transpose() * temp) / (input.RowCount - 1);
        }



        public static Vector<double> Means0(Matrix<double> input) //mean(input,1)
        {
            return (input.ColumnSums() / input.RowCount);
        }

        public static Vector<double> Means1(Matrix<double> input) //mean(input,2)
        {
            return (input.RowSums() / input.ColumnCount);
        }
    }
}
