using KozzionCore.Tools;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;

namespace KozzionMathematics.Datastructure.Matrix
{
    public class TransformWhiteningOld<MatrixType> :
			IFunctionBijective<float [], float []>
	{
        public string FunctionType { get { return "TransformWhitening"; } }

        AMatrix<MatrixType> means;
        AMatrix<MatrixType> matrix_forward;
        AMatrix<MatrixType> matrix_backward;

		public TransformWhiteningOld(IAlgebraLinear<MatrixType> algebra,
			float [,] data)
		{
            AMatrix<MatrixType> data_matrix = algebra.Create(data);
            means = algebra.Create(ToolsMathStatistics.Means1(data));
            AMatrix<MatrixType> covariance_matrix = data_matrix * data_matrix.Transpose();
			matrix_backward = covariance_matrix.Algebra.ComputeRoot(covariance_matrix);
			matrix_forward = matrix_backward.Invert();
		}

		public float [] Compute(float [] input)
		{
			return (matrix_forward * (ToolsCollection.ConvertToDoubleArray( input) - means)).ToArray1DFloat32();      
		}

		public float [] ComputeInverse(float [] input)
		{
			return ((matrix_backward * ToolsCollection.ConvertToDoubleArray(input)) + means).ToArray1DFloat32();
        }

        public IFunctionBijective<float[], float[]> GetInverse()
        {
            throw new NotImplementedException();         
        }
    }
}