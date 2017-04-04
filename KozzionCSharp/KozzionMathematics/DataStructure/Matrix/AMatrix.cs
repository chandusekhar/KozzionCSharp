using System;
using KozzionMathematics.Function;
using KozzionMathematics.Algebra;
using KozzionMathematics.Tools;
using System.Collections.Generic;
using KozzionCore.Tools;

namespace KozzionMathematics.Datastructure.Matrix
{
    public abstract class AMatrix<DataType>
    {
        public IAlgebraLinear<DataType> Algebra { get; private set; }

        public DataType Data { get; set; }

        
        protected AMatrix(IAlgebraLinear<DataType> algebra, DataType data)
        {
            this.Algebra = algebra;
            this.Data = data;
        }

        public abstract int  RowCount { get; }

        public abstract int  ColumnCount { get; }

        public double GetElement()
        {
            if (RowCount != 1 || ColumnCount != 1)
            {
                throw new Exception("More then one element");
            }
            else
            {
                return GetElement(0, 0);
            }
        }

        public abstract double GetElement(int index_row, int index_column);

        public abstract double [] ToArray1DFloat64();

        public abstract float[] ToArray1DFloat32();

        public abstract double[,] ToArray2DFloat64();

        public abstract float[,] ToArray2DFloat32();

        public abstract void SetElement(int index_row, int index_column, double value);

        public abstract void SetSubMatrix(int offset_row, int offset_column, AMatrix<DataType> input);

        public abstract AMatrix<DataType> Invert();

        public abstract double L2Norm();

        public abstract AMatrix<DataType> Transpose();

        public abstract AMatrix<DataType> Transform(IFunction<double, double> function);

        public AMatrix<DataType> GetRow(int row_index)
        {
            return GetRows(row_index, 1);
        }

        public AMatrix<DataType> GetRows(int row_index, int row_count)
        {
            return this.GetSubMatrix(row_index, row_count, 0, this.ColumnCount);
        }

        public AMatrix<DataType> GetRowSection(int row_index, int column_index, int column_count)
        {
            return this.GetSubMatrix(row_index, this.RowCount, column_index, 1);
        }

        public AMatrix<DataType> GetColumn(int column_index)
        {
           return GetColumns(column_index, 1);
        }

    

        public AMatrix<DataType> GetColumns(int column_index, int column_count)
        {
            return this.GetSubMatrix(0, this.RowCount, column_index, column_count);
        }    

        public AMatrix<DataType> GetColumnSection(int row_index, int row_count, int column_index)
        {
            return this.GetSubMatrix(row_index, row_count, column_index, 1);
        }

        public abstract AMatrix<DataType> GetSubMatrix(int row_index, int row_count, int column_index, int column_count);
    

        public AMatrix<DataType> AppendRows(AMatrix<DataType> operant_0)
        {
            if (this.ColumnCount != operant_0.ColumnCount)
            {
                throw new Exception("Matrix size mismatch");
            }
            AMatrix < DataType >  result = this.Algebra.CreateZeros(this.RowCount + operant_0.RowCount, this.ColumnCount);
            if (this.Data != null)
            {
                result.SetSubMatrix(0, 0, this);
            }
            if (operant_0.Data != null)
            {
                result.SetSubMatrix(this.RowCount, 0, operant_0);
            }
            return result;
        }


        public AMatrix<DataType> AppendColumns(AMatrix<DataType> operant_0)
        {
            if (this.RowCount != operant_0.RowCount)
            {
                throw new Exception("Matrix size mismatch");
            }
            AMatrix<DataType> result = this.Algebra.CreateZeros(this.RowCount, this.ColumnCount + operant_0.ColumnCount);
            result.SetSubMatrix(0, 0, this);
            result.SetSubMatrix(0, this.ColumnCount, operant_0);
            return result;
        }

        public AMatrix<DataType> AppendRow(double operant_0)
        {
            return AppendRows(this.Algebra.Create(operant_0));
        }

        public AMatrix<DataType> AppendColumn(double operant_0)
        {
            return AppendColumns(this.Algebra.Create(operant_0));
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }



        public static AMatrix<DataType> operator +(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1)
        {
            return operant_0.Algebra.Add(operant_0, operant_1);
        }

        public static AMatrix<DataType> operator +(AMatrix<DataType> operant_0, double operant_1)
        {
            return operant_0.Algebra.Add(operant_0, operant_1);
        }

        public static AMatrix<DataType> operator +(AMatrix<DataType> operant_0, IList<double> operant_1)
        {
            return operant_0.Algebra.Add(operant_0, operant_0.Algebra.Create(operant_1));
        }

        public static AMatrix<DataType> operator -(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1)
        {
            return operant_0.Algebra.Subtract(operant_0, operant_1);
        }

        public static AMatrix<DataType> operator -(AMatrix<DataType> operant_0, double operant_1)
        {
            return operant_0.Algebra.Subtract(operant_0, operant_1);
        }

        public static AMatrix<DataType> operator -(IList<double> operant_0, AMatrix<DataType> operant_1)
        {
            return operant_1.Algebra.Subtract(operant_1.Algebra.Create(operant_0), operant_1);
        }



        public static AMatrix<DataType> operator *(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1)
        {
            return operant_0.Algebra.Multiply(operant_0, operant_1);
        }

        public static AMatrix<DataType> operator *(AMatrix<DataType> operant_0, double operant_1)
        {
            return operant_0.Algebra.Multiply(operant_0, operant_1);
        }

        public static AMatrix<DataType> operator *(AMatrix<DataType> operant_0, IList<double> operant_1)
        {
            return operant_0.Algebra.Multiply(operant_0, operant_0.Algebra.Create(operant_1));
        }

        public static AMatrix<DataType> operator *(IList<double> operant_0, AMatrix<DataType> operant_1)
        {
            return operant_1.Algebra.Multiply(operant_1.Algebra.Create(operant_0), operant_1);
        }

        public static AMatrix<DataType> operator /(AMatrix<DataType> operant_0, double operant_1)
        {
            return operant_0.Algebra.Divide(operant_0, operant_1);
        }

        

    }
}
