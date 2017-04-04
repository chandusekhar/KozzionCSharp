using System;

namespace KozzionMathematics.Function.Implementation.Distance
{
    public class FunctionDistanceAbsoluteDifference : IFunctionDistance<float, float>
    {
        public string FunctionType { get { return "FunctionDistanceAbsoluteDifference"; } }
        public float Compute(float value_0, float value_1)
        {
            return Math.Abs(value_0 - value_1);
        }

        public float ComputeToRectangle(float valeu_0, float upper, float lower)
        {
            throw new NotImplementedException();
        }
    }
}
