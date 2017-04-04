using KozzionCore.Tools;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using System;

namespace KozzionMathematics.Algebra
{
    public class MatrixKozzion<DataType> : AMatrix<DataType[,]>
    {

        private bool transpose;

        public MatrixKozzion(IAlgebraLinear<DataType [,]> algebra, DataType[,] data, bool transpose = false)
            : base(algebra, data)
        {
            this.transpose = transpose;
        }

        public override int RowCount
        {
            get
            {
                if (transpose)
                {
                    return this.Data.GetLength(0);
                }
                else
                {
                    return this.Data.GetLength(1);
                }
            }
        }

        public override int ColumnCount
        {
            get
            {
                if (transpose)
                {
                    return Data.GetLength(1);
                }
                else
                {
                    return Data.GetLength(0);
                }
            }
        }

     
        public static bool operator ==(MatrixKozzion<DataType> matrix_0, MatrixKozzion<DataType> matrix_1)
        {
            return matrix_0.Equals(matrix_1);
        }

        public static bool operator !=(MatrixKozzion<DataType> matrix_0, MatrixKozzion<DataType> matrix_1)
        {
            return !matrix_0.Equals(matrix_1);
        }

        public override int GetHashCode()
        {
            int hash_code = Algebra.GetHashCode();
            foreach (DataType value in Data)
            {
                hash_code += value.GetHashCode();
            }
            return hash_code;
        }

        public override bool Equals(object other)
        {
            if (other is MatrixKozzion<DataType>)
            {
                MatrixKozzion<DataType> other_typed = (MatrixKozzion<DataType>)other;
                return this == other_typed;//TODO
            }
            else
            {
                return false;
            }
        }


        public override void SetSubMatrix(int offset_0, int offset_1, AMatrix<DataType[,]> other)
        {
            for (int index_0 = 0; index_0 < other.RowCount; index_0++)
            {
                for (int index_1 = 0; index_1 < other.ColumnCount; index_1++)
                {
                    this.Data[index_0 + offset_0, index_1 + offset_1] = other.Data[index_0, index_1];
                }
            }
        }

        public override void SetElement(int index_0, int index_1, double value)
        {
            this.Algebra.SetElement(index_0, index_1);
        }


        public override double GetElement(int index_0, int index_1)
        {
            return this.Algebra.GetElement(index_0, index_1);
        }


        public override AMatrix<DataType[,]> Invert()
        {
            if (Data.GetLength(0) != Data.GetLength(1))
            {
                throw new Exception("Matrix not square");
            }
            throw new NotImplementedException();
        }




        public override double [] ToArray1DFloat64()
        {
            return this.Algebra.ToArray1D(this);
            //if ((Data.GetLength(0) != 1) && (Data.GetLength(1) != 1))
            //{
            //    throw new Exception("Matrix not a vector");
            //}

            //if (Data.GetLength(0) == 1)
            //{
            //    return Data.SelectRow(0);
            //}
            //else
            //{
            //    return Data.SelectColumn(0);
            //}           
        }

        public override double[,] ToArray2DFloat64()
        {
            return this.Algebra.ToArray2D(this);
            //throw new NotImplementedException();
        }


        public override AMatrix<DataType[,]> Transpose()
        {
            throw new NotImplementedException();
        }

        public override AMatrix<DataType[,]> Transform(IFunction<double, double> function)
        {
            throw new NotImplementedException();
        }

        public override double L2Norm()
        {
            throw new NotImplementedException();
        }

        public override AMatrix<DataType[,]> GetSubMatrix(int row_offset, int row_count, int column_offset, int column_count)
        {
            throw new NotImplementedException();
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
