using KozzionMathematics.Datastructure.Matrix;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using KozzionCore.Tools;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using KozzionMathematics.Numeric.linear_solver;
using KozzionMathematics.Numeric.Solver.LinearSolver;

namespace KozzionMathematics.Algebra
{
    public class AlgebraLinearReal64MathNet : IAlgebraLinear<Matrix<double>>
    {
        private DenseMatrix CreateDenseMatrix(AMatrix<Matrix<double>> operant_0, IList<double> operant_1)
        {
            return new DenseMatrix(operant_0.Data.RowCount, operant_0.Data.ColumnCount, ToolsCollection.ConvertToDoubleArray(operant_1));
        }

        public AMatrix<Matrix<double>> Add(AMatrix<Matrix<double>> operant_0, double operant_1)
        {
            return new MatrixMathNet(operant_0.Data + operant_1);
        }

        public AMatrix<Matrix<double>> Add(AMatrix<Matrix<double>> operant_0, IList<double> operant_1)
        {
            return new MatrixMathNet(operant_0.Data.Add(CreateDenseMatrix(operant_0, operant_1)));
        }

        public AMatrix<Matrix<double>> Add(AMatrix<Matrix<double>> operant_0, AMatrix<Matrix<double>> operant_1)
        {
            return new MatrixMathNet(operant_0.Data + operant_1.Data);
        }

        public AMatrix<Matrix<double>> ComputeRoot(AMatrix<Matrix<double>> operant_0)
        {
            throw new NotImplementedException();
        }

        public SVD<Matrix<double>> ComputeSVD(AMatrix<Matrix<double>> operant_0)
        {
            Svd<double> svd = operant_0.Data.Svd();
            return new SVD<Matrix<double>>(new MatrixMathNet(svd.U), CreateDiagonal(svd.S.ToArray()), new MatrixMathNet(svd.VT));
        }

        public AMatrix<Matrix<double>> CreateDiagonal(double[] operant_0)
        {
            return new MatrixMathNet(new DiagonalMatrix(operant_0.Length, operant_0.Length, operant_0));
        }

        public AMatrix<Matrix<double>> Create(double element)
        {
            return new MatrixMathNet(new DenseMatrix(1, 1, new double[] { element }));
        }

        public AMatrix<Matrix<double>> Create(double[,] operant_0)
        {
            return new MatrixMathNet(new DenseMatrix(operant_0.GetLength(0), operant_0.GetLength(1), ToolsCollection.ConvertToArray1D(operant_0, true)));
        }

        //public AMatrix<Matrix<double>> Create(double[,] elements)
        //{
        //    AMatrix<Matrix<double>> matrix = new MatrixMathNet(elements.GetLength(0), elements.GetLength(1));
        //    for (int index_0 = 0; index_0 < elements.GetLength(0); index_0++)
        //    {
        //        for (int index_1 = 0; index_1 < elements.GetLength(1); index_1++)
        //        {
        //            matrix.SetElement(index_0, index_1, elements[index_0, index_1]);
        //        }
        //    }

        //    return matrix;
        //}

        public AMatrix<Matrix<double>> Create(float[][] elements)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Create(IList<double> operant_0, bool transpose = false)
        {

            if (transpose)
            {
                return new MatrixMathNet(new DenseMatrix(operant_0.Count, 1, ToolsCollection.ConvertToArray1D(operant_0)));
            }
            else
            {         
                return new MatrixMathNet(new DenseMatrix(1, operant_0.Count, ToolsCollection.ConvertToArray1D(operant_0)));
            }
        }


        public AMatrix<Matrix<double>> Create(float element)
        {
            return Create((double)element);
        }

        public AMatrix<Matrix<double>> Create(float[,] operant_0)
        {
            return Create(ToolsCollection.ConvertToDoubleArray(operant_0));
        }

        public AMatrix<Matrix<double>> Create(IList<float> operant_0, bool transpose = false)
        {

            return Create(ToolsCollection.ConvertToDoubleArray(operant_0), transpose);
        }

        public AMatrix<Matrix<double>> CreateZeros(int row_count, int column_count)
        {
            if ((row_count == 0) || (column_count == 0))
            {
                return new MatrixMathNet(row_count, column_count);
            }
            else
            {
                return new MatrixMathNet(new DenseMatrix(row_count, column_count));
            }
        }


        public AMatrix<Matrix<double>> CreateRandomUnit(int row_count, int column_count, RandomNumberGenerator random)
        {
            throw new NotImplementedException();
        }


        public double GetElement(int index_0, int index_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Multiply(AMatrix<Matrix<double>> operant_0, IReadOnlyList<double> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Multiply(IReadOnlyList<double> operant_0, AMatrix<Matrix<double>> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Multiply(AMatrix<Matrix<double>> operant_0, double operant_1)
        {
            return new MatrixMathNet(operant_0.Data * operant_1);
        }

        public AMatrix<Matrix<double>> Multiply(AMatrix<Matrix<double>> operant_0, AMatrix<Matrix<double>> operant_1)
        {
            return new MatrixMathNet(operant_0.Data * operant_1.Data);
        }

        public AMatrix<Matrix<double>> SelectColumns(AMatrix<Matrix<double>> operant_0, IList<int> column_indexes)
        {
            Matrix<double> destination = new DenseMatrix(operant_0.RowCount, column_indexes.Count);
            for (int column_index = 0; column_index < column_indexes.Count; column_index++)
            {
                destination.SetColumn(column_index, operant_0.Data.Column(column_indexes[column_index]));
            }
            return new MatrixMathNet(destination);
        }

        public AMatrix<Matrix<double>> SelectRows(AMatrix<Matrix<double>> operant_0, IList<int> row_indexes)
        {
            Matrix<double> destination = new DenseMatrix(row_indexes.Count, operant_0.RowCount);
            for (int row_index = 0; row_index < row_indexes.Count; row_index++)
            {
                destination.SetRow(row_index, operant_0.Data.Row(row_indexes[row_index]));
            }
            return new MatrixMathNet(destination);
        }

        public void SetElement(int index_0, int index_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Subtract(AMatrix<Matrix<double>> operant_0, IList<double> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Subtract(AMatrix<Matrix<double>> operant_0, double operant_1)
        {
            return new MatrixMathNet(operant_0.Data - operant_1);
        }

        public AMatrix<Matrix<double>> Subtract(AMatrix<Matrix<double>> operant_0, AMatrix<Matrix<double>> operant_1)
        {
            return new MatrixMathNet(operant_0.Data - operant_1.Data);
        }

        public double[] ToArray1D(AMatrix<Matrix<double>> operant_0)
        {
            throw new NotImplementedException();
        }

        public double[,] ToArray2D(AMatrix<Matrix<double>> operant_0)
        {
            throw new NotImplementedException();
        }
  

        public AMatrix<Matrix<double>> Multiply(AMatrix<Matrix<double>> operant_0, IList<double> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Multiply(IList<double> operant_0, AMatrix<Matrix<double>> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<Matrix<double>> Divide(AMatrix<Matrix<double>> operant_0, double operant_1)
        {
            return new MatrixMathNet(operant_0.Data / operant_1);
        }

        public AMatrix<Matrix<double>> CreateRandomUnit(int row_count, int column_count)
        {
            throw new NotImplementedException();
        }

        public ISolverLinear<Matrix<double>> GetSimpleSolver()
        {
            return new SolverLinearMathNetQR();
        }

        public AMatrix<Matrix<double>> MultiplyElements(AMatrix<Matrix<double>> operant_0, AMatrix<Matrix<double>> operant_1)
        {
            throw new NotImplementedException();
        }  

   
    }
}
