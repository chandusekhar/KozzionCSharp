
using KozzionMachineLearning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using KozzionCore.Tools;

namespace KozzionMachineLearning.Transform
{
    public class TemplateDimensionReductionPCA<MatrixType, DataSetType>
    {
        private IAlgebraLinear<MatrixType> algebra;
        private int destination_dimension_count;

        public TemplateDimensionReductionPCA(IAlgebraLinear<MatrixType> algebra, int destination_dimension_count)
        {
            this.algebra = algebra;
            this.destination_dimension_count = destination_dimension_count;
        }

        public ITransform<double[], double[]> GenerateTransform(IDataSet<double> data_set)
        {
            AMatrix<MatrixType> data = algebra.Create(ToolsCollection.ConvertToArray2D(data_set.FeatureData));
            SVD<MatrixType> svd = algebra.ComputeSVD(data);
            AMatrix<MatrixType> projection = svd.VT.Transpose().GetColumns(0, destination_dimension_count);
            return new DimensionReductionPCA<MatrixType>(data_set.DataContext, projection);
        }
    }
}
