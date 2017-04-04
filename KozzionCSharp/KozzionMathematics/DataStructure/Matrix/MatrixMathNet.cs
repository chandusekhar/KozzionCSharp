using System;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using KozzionCore.Tools;

namespace KozzionMathematics.Algebra
{
    public class MatrixMathNet : AMatrix<Matrix<double>>
    {
        private int row_count;
        private int column_count;      

        public MatrixMathNet(Matrix<double> data)
            : base(new AlgebraLinearReal64MathNet(), data)
        {
            this.row_count = Data.RowCount;
            this.column_count = Data.ColumnCount;
        }


        public MatrixMathNet(int row_count, int column_count)
            : base(new AlgebraLinearReal64MathNet(), Create(row_count, column_count))
        {
            this.row_count = row_count;
            this.column_count = column_count;
        }

        private static Matrix<double> Create(int row_count, int column_count)
        {
            if ((row_count == 0) || (column_count == 0))
            {
                return null;
            }
            else
            {
                return new DenseMatrix(row_count, column_count);
            }
        }

        public override int RowCount { get { return row_count; } }

        public override int ColumnCount { get { return column_count; } }

        public override double GetElement(int index_row, int index_column)
        {
            return this.Data.Storage.At(index_row, index_column);
        }

        public override AMatrix<Matrix<double>> Invert()
        {
            throw new NotImplementedException();
        }

        public override void SetElement(int index_row, int index_column, double value)
        {
            this.Data[index_row, index_column] = value;
        }

        public override void SetSubMatrix(int offset_row, int offset_column, AMatrix<Matrix<double>> input)
        {
            if ((input.RowCount == 0) || (input.ColumnCount == 0))
            {
                return;
            }
            else
            {
                this.Data.SetSubMatrix(offset_row, offset_column, input.Data);
            }
        }

      

    

        public override AMatrix<Matrix<double>> Transform(IFunction<double, double> function)
        {
            throw new NotImplementedException();
        }

        public override AMatrix<Matrix<double>> Transpose()
        {
            return new MatrixMathNet(this.Data.Transpose());
        }

        public override double L2Norm()
        {
            return Data.L2Norm();
        }

        public override string ToString()
        {
            if (this.Data == null)
            {
                return "Matrix: " + this.RowCount + "x" + this.ColumnCount + " : {}";
            }
            else
            {
                return "Matrix: " + this.RowCount + "x" + this.ColumnCount + " : " + ToolsCollection.ToString(this.ToArray2DFloat64());
            }
        }

        public override AMatrix<Matrix<double>> GetSubMatrix(int row_index, int row_count, int column_index, int column_count)
        {
            if ((row_count == 0) || (column_count == 0))
            {
                return new MatrixMathNet(row_count, column_count);
            }
            else
            {
                return new MatrixMathNet(this.Data.SubMatrix(row_index, row_count, column_index, column_count));
            }
        }

        internal AMatrix<Matrix<double>> Power(int power)
        {
            if (power < 0)
            {
                //TODO invert and power
                throw new NotImplementedException();
            }
            if (power == 0)
            {
                //TODO return identity
                throw new NotImplementedException();
            }
            AMatrix<Matrix<double>> result = this;
            for (int power_index = 1; power_index < power; power_index++)
            {
                result = Algebra.Multiply(result, this);
            }
            return result;
        }

        public override double[] ToArray1DFloat64()
        {
            if (RowCount == 1)
            {
                return Data.ToArray().Select1DIndex0(0);
            }
            else if (ColumnCount == 1)
            {
                return Data.ToArray().Select1DIndex1(0);
            }
            else
            {
                throw new Exception("Matrix with more than 1 row and column does not have array form");
            }
        }

        public override double[,] ToArray2DFloat64()
        {
            return Data.ToArray();
        }

        public override float[] ToArray1DFloat32()
        {
            throw new NotImplementedException();
        }

        public override float[,] ToArray2DFloat32()
        {
            throw new NotImplementedException();
        }
    }
}