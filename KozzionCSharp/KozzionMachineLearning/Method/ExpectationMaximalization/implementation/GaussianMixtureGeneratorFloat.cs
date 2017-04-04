using KozzionMathematics.Function;
using System;

namespace KozzionMachineLearning.Method.expectation_maximalization.implementation
{
    public class GaussianMixtureGeneratorFloat : IExpectationFunctionGenerator<float[], int>
    {

        public IFunction<Tuple<float[][], int[], int>, int> generate(float[][] observations, int[] state)
        {
            throw new NotImplementedException();
        }
    }
}
