using KozzionMathematics.Datastructure.Matrix;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using KozzionCore.Tools;
using KozzionMathematics.Numeric.linear_solver;

namespace KozzionMathematics.Algebra
{
    public class AlgebraLinearReal64Naitive :IAlgebraLinear<double[,]>
    {   
        public double[,] Add(double[,] array_0, double[,] array_1)
        {
            if ((array_0.GetLength(0) != array_1.GetLength(0)) && (array_0.GetLength(1) != array_1.GetLength(1)))
            {
                throw new Exception("Dimension mismatch");
            }

            double[,] result = new double[array_0.GetLength(0), array_0.GetLength(1)];
            for (int index_0 = 0; index_0 < array_0.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array_0.GetLength(1); index_1++)
                {
                    result[index_0, index_1] = array_0[index_0, index_1] + array_1[index_0, index_1];
                }
            }
            return result;           
        }

        public double[,] Add(double[,] array_0, double value)
        {

            double[,] result = new double[array_0.GetLength(0), array_0.GetLength(1)];
            for (int index_0 = 0; index_0 < array_0.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array_0.GetLength(1); index_1++)
                {
                    result[index_0, index_1] = array_0[index_0, index_1] + value;
                }
            }
            return result;
        }

        public double[,] Subtract(double[,] array_0, double[,] array_1)
        {
            if ((array_0.GetLength(0) != array_1.GetLength(0)) && (array_0.GetLength(1) != array_1.GetLength(1)))
            {
                throw new Exception("Dimension mismatch");
            }

            double[,] result = new double[array_0.GetLength(0), array_0.GetLength(1)];
            for (int index_0 = 0; index_0 < array_0.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array_0.GetLength(1); index_1++)
                {
                    result[index_0, index_1] = array_0[index_0, index_1] - array_1[index_0, index_1];
                }
            }
            return result;
        }

        public double[,] Subtract(double[,] array_0, double value)
        {

            double[,] result = new double[array_0.GetLength(0), array_0.GetLength(1)];
            for (int index_0 = 0; index_0 < array_0.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array_0.GetLength(1); index_1++)
                {
                    result[index_0, index_1] = array_0[index_0, index_1] - value;
                }
            }
            return result;
        }

        public double[,] Multiply(double[,] array_0, double[,] array_1)
        {
            if (array_0.GetLength(1) != array_1.GetLength(0))
            {
                throw new Exception("Dimension mismatch");
            }

            double[,] result = new double[array_0.GetLength(0), array_1.GetLength(1)];
            for (int index_0 = 0; index_0 < array_0.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array_1.GetLength(1); index_1++)
                {
                    for (int index_inner = 0; index_inner < array_0.GetLength(1); index_inner++)
                    {
                        result[index_0, index_1] += array_0[index_0, index_inner] * array_1[index_inner, index_1];
                    }
                }
            }
            return result;
        }

        public double[,] Multiply(double[,] array_0, double value)
        {

            double[,] result = new double[array_0.GetLength(0), array_0.GetLength(1)];
            for (int index_0 = 0; index_0 < array_0.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array_0.GetLength(1); index_1++)
                {
                    result[index_0, index_1] = array_0[index_0, index_1] * value;
                }
            }
            return result;
        }

        public AMatrix<double[,]> Add(AMatrix<double[,]> element_0, AMatrix<double[,]> element_1)
        {
            return new MatrixKozzion<double>(this, Add(element_0.Data, element_1.Data));
        }

        public AMatrix<double[,]> Add(AMatrix<double[,]> element_0, double other)
        {
            return new MatrixKozzion<double>(this, Add(element_0.Data, other));
        }

        public AMatrix<double[,]> Subtract(AMatrix<double[,]> element_0, AMatrix<double[,]> element_1)
        {
            return new MatrixKozzion<double>(this, Subtract(element_0.Data, element_1.Data));
        }

        public AMatrix<double[,]> Subtract(AMatrix<double[,]> element_0, double other)
        {
            return new MatrixKozzion<double>(this, Subtract(element_0.Data, other));
        }

        public AMatrix<double[,]> Multiply(AMatrix<double[,]> element_0, AMatrix<double[,]> element_1)
        {
            return new MatrixKozzion<double>(this, Multiply(element_0.Data, element_1.Data));
        }

        public AMatrix<double[,]> Multiply(AMatrix<double[,]> element_0, double other)
        {
            return new MatrixKozzion<double>(this, Multiply(element_0.Data, other));
        }

        public AMatrix<double[,]> Create(int size_0, int size_1 = 1)
        {
            return new MatrixKozzion<double>(this, new double[size_0, size_1]);
        }

        public AMatrix<double[,]> Create(IList<double> array, bool transpose = false)
        {
            return new MatrixKozzion<double>(this, ToolsCollection.ConvertToArray2D(array), transpose);
        }

        public AMatrix<double[,]> Create(float element)
        {
            return Create((double)element);
        }

        public AMatrix<double[,]> Create(float[,] operant_0)
        {
            return Create(ToolsCollection.ConvertToDoubleArray(operant_0));
        }

        //public AMatrix<double[,]> Create(IList<float> operant_0, bool transpose = false)
        //{

        //    return Create(ToolsCollection.ConvertToDoubleArray(operant_0), transpose);
        //}

        public double ElementUnit
        {
            get { return 1.0f; }
        }

        public double ElementZero
        {
            get { return 0.0f; }
        }

        public double ElementRandom(RandomNumberGenerator random)
        {
            return (random.RandomFloat64Unit() - 0.5f) * 2.0f;
        }



        public AMatrix<double[,]> Multiply(IReadOnlyList<double> operant_0, AMatrix<double[,]> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> CreateZeros(int row_count, int column_count)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> CreateVector(IReadOnlyList<double> vector, bool transpose = true)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> CreateRandomUnit(int row_count, int column_count, RandomNumberGenerator random)
        {
            throw new NotImplementedException();
        }

        public void SetElement(int index_0, int index_1)
        {
            throw new NotImplementedException();
        }

        public double GetElement(int index_0, int index_1)
        {
            throw new NotImplementedException();
        }

       

        AMatrix<double[,]> IAlgebraLinear<double[,]>.CreateZeros(int row_count, int column_count)
        {
            throw new NotImplementedException();
        }

    

        AMatrix<double[,]> IAlgebraLinear<double[,]>.CreateRandomUnit(int row_count, int column_count, RandomNumberGenerator random)
        {
            throw new NotImplementedException();
        }

        public double[] ToArray1D<DataType>(MatrixKozzion<DataType> matrixKozzion)
        {
            throw new NotImplementedException();
        }

        public double[,] ToArray2D<DataType>(MatrixKozzion<DataType> matrixKozzion)
        {
            throw new NotImplementedException();
        }

        public AMatrix<float[,]> ComputeRoot(AMatrix<float[,]> covariance_matrix)
        {
            throw new NotImplementedException();
        }

        public double[] ToArray1D(AMatrix<double[,]> operant_0)
        {
            throw new NotImplementedException();
        }

        public double[,] ToArray2D(AMatrix<double[,]> operant_0)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> ComputeRoot(AMatrix<double[,]> operant_0)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Create(double[,] featureData)
        {
            throw new NotImplementedException();
        }

        public SVD<MatrixType> ComputeSVD<MatrixType>(AMatrix<MatrixType> data)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> SelectRows(AMatrix<double[,]> operant_0, IReadOnlyList<int> row_indexes)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> SelectColumns(AMatrix<double[,]> operant_0, IReadOnlyList<int> column_indexes)
        {
            throw new NotImplementedException();
        }

        public SVD<double[,]> ComputeSVD(AMatrix<double[,]> data)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> SelectRows(AMatrix<double[,]> operant_0, IList<int> row_indexes)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> SelectColumns(AMatrix<double[,]> operant_0, IList<int> column_indexes)
        {
            throw new NotImplementedException();
        }

   

        public AMatrix<double[,]> Add(AMatrix<double[,]> operant_0, IList<double> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Subtract(AMatrix<double[,]> operant_0, IList<double> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Multiply(AMatrix<double[,]> operant_0, IList<double> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Multiply(IList<double> operant_0, AMatrix<double[,]> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Divide(AMatrix<double[,]> operant_0, double operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> CreateRandomUnit(int row_count, int column_count)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Create(double element)
        {
            throw new NotImplementedException();
        }

        public ISolverLinear<double[,]> GetSimpleSolver()
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> MultiplyElements(AMatrix<double[,]> operant_0, AMatrix<double[,]> operant_1)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Create(IList<float> vector, bool transpose = true)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Create(double[][] elements)
        {
            throw new NotImplementedException();
        }

        public AMatrix<double[,]> Create(float[][] elements)
        {
            throw new NotImplementedException();
        }
    }
}
