using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Tools;
using MathNet.Numerics.LinearAlgebra.Double;
using KozzionCore.Tools;

namespace KozzionMachineLearning.Transform
{
    public class TemplateTransformWhiteningCholesky : ITemplateTransform
    {
        public ITransform Generate(IDataSet dataset)
        {
            var data_matrix = dataset.GetFeatureDataAsMatrix(); 
            var matrixCovariance = ToolsMathLinear.CovarianceMatrix(data_matrix);
            var means = ToolsMathLinear.Means0(data_matrix);
            var matrixForward = matrixCovariance.Inverse().Cholesky().Factor;
            var matrixBackward = matrixForward.Inverse();
            return new TransformWhiteningCholesky(dataset.DataContext, means, matrixForward, matrixBackward);
        }
    }
}
