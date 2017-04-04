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

namespace KozzionMachineLearning.Transform
{
    public class DimensionReductionPCA<MatrixType> : ITransform<double[], double[]>
    {
        public string FunctionType { get { throw new NotImplementedException(); } }

        public IDataContext DataContext { get; private set; }
        private IAlgebraLinear<MatrixType> algebra;
        private AMatrix<MatrixType> projection;

        public DimensionReductionPCA(IDataContext data_context, AMatrix<MatrixType> projection)
        {
            this.DataContext = data_context;
            this.algebra = projection.Algebra;
            this.projection = projection;
        }

        public double[] Compute(double[] instance_features)
        {
            AMatrix<MatrixType> vector = algebra.Create(instance_features, false);
            AMatrix<MatrixType> result = vector* projection;
            return result.ToArray1DFloat64();
        }

        public double[] ComputeInverse(double[] input)
        {
            throw new NotImplementedException();
        }

        public IFunctionBijective<double[], double[]> GetInverse()
        {
            throw new NotImplementedException();
        }
    }
}
