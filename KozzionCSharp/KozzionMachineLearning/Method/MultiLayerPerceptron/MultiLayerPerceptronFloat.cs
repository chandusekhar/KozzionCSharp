
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function.Implementation;
using MathNet.Numerics.LinearAlgebra;

namespace KozzionMachineLearning.Method.multi_layer_perceptron
{
    public class MultiLayerPerceptronFloat64 : MultiLayerPerceptronNew<Matrix<double>>
    {
        public MultiLayerPerceptronFloat64(int [] setup) 
            : base(new AlgebraLinearReal64MathNet(), setup, new FunctionSigmiodDouble(), new FunctionSigmiodDerivativeDouble(), 0.1f, 0.1f)
        {
        }
    }
}
