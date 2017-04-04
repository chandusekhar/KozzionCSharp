using KozzionMathematics.Datastructure.Matrix;
using System.Collections.Generic;
using System.Security.Cryptography;
using KozzionMathematics.Numeric.linear_solver;

namespace KozzionMathematics.Algebra
{
    public interface IAlgebraLinear<DataType>
    {
        ISolverLinear<DataType> GetSimpleSolver();

        void SetElement(int index_0, int index_1);
        double GetElement(int index_0, int index_1);

        AMatrix<DataType> SelectRows(AMatrix<DataType> operant_0, IList<int> row_indexes);
        AMatrix<DataType> SelectColumns(AMatrix<DataType> operant_0, IList<int> column_indexes);

        AMatrix<DataType> CreateZeros(int row_count, int column_count);
        AMatrix<DataType> Create(double element);
        AMatrix<DataType> Create(IList<double> vector, bool transpose = true);
        AMatrix<DataType> Create(double[,] elements);

        AMatrix<DataType> Create(float element);
        AMatrix<DataType> Create(IList<float> vector, bool transpose = true);
        AMatrix<DataType> Create(float[,] elements);

        AMatrix<DataType> CreateRandomUnit(int row_count, int column_count, RandomNumberGenerator random);
        AMatrix<DataType> CreateRandomUnit(int row_count, int column_count);


        AMatrix<DataType> Add(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1);
        AMatrix<DataType> Add(AMatrix<DataType> operant_0, double operant_1);

        AMatrix<DataType> Subtract(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1);
        AMatrix<DataType> Subtract(AMatrix<DataType> operant_0, double operant_1);


        AMatrix<DataType> MultiplyElements(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1);
        AMatrix<DataType> Multiply(AMatrix<DataType> operant_0, AMatrix<DataType> operant_1);
        AMatrix<DataType> Multiply(AMatrix<DataType> operant_0, double operant_1);

        AMatrix<DataType> Multiply(IList<double> operant_0 , AMatrix<DataType> operant_1);

        AMatrix<DataType> Divide(AMatrix<DataType> operant_0, double operant_1);

        SVD<DataType> ComputeSVD(AMatrix<DataType> data);
        AMatrix<DataType> ComputeRoot(AMatrix<DataType> operant_0);


        double[] ToArray1D(AMatrix<DataType> operant_0);
        double[,] ToArray2D(AMatrix<DataType> operant_0);
      
    }
}
