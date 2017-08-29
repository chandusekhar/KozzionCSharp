using KozzionCore.Tools;
using KozzionMachineLearning.Transform;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using KozzionMachineLearning.DataSet;
using System.Collections.Generic;

namespace KozzionMachineLearning.Transform
{
    //This is Cholesky Whitening
    //TODO mahalanobis or ZCA whitening
    //TODO (PCA whitening)
    public class TransformWhiteningCholesky : ITransform
    {
        public string FunctionType { get { return "TransformWhiteningCholesky"; } }

        IDataContext DataContext { get; }
        Vector<double> _means;
        Matrix<double> _matrixForward;
        Matrix<double> _matrixBackward;

        public TransformWhiteningCholesky(IDataContext dataContext, Vector<double> means, Matrix<double> matrixForward, Matrix<double> matrixBackward)
        {
            DataContext = dataContext;
            _means = means;
            _matrixForward = matrixForward;
            _matrixBackward = matrixBackward;
        }

        public double[] TransformForward(double[] input)
		{
            return ((new DenseVector(input) - _means) * _matrixForward).ToArray();
		}

		public double[] TransformBackward(double[] input)
		{
			return ((new DenseVector(input) * _matrixBackward) + _means).ToArray(); 
        }

        public IDataSet TransformForward(IDataSet source)
        {
            List<double[]> target = new List<double[]>();
            foreach (var item in source.InstanceList)
            {
                target.Add(TransformForward(item.FeatureData));
            }
            return new DataSetDefault(target);
          
        }

        public IDataSet TransformBackward(IDataSet source)
        {
            List<double[]> target = new List<double[]>();
            foreach (var item in source.InstanceList)
            {
                target.Add(TransformBackward(item.FeatureData));
            }
            return new DataSetDefault(DataContext, target);
        }
    }
}