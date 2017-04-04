namespace KozzionMathematics.Function.Implementation
{
    public class FunctionSigmiodDerivativeFloat :
        IFunction<float, float>
    {
        public string FunctionType { get { return "FunctionSigmiodDerivativeFloat"; } }
        public float Compute(float input)
        {
            return FunctionSigmiodFloat.compute_static(input) * (1 - FunctionSigmiodFloat.compute_static(input));
        }

    }

    public class FunctionSigmiodDerivativeDouble :
      IFunction<double, double>
    {
        public string FunctionType { get { return "FunctionSigmiodDerivativeDouble"; } }
        public double Compute(double input)
        {
            return FunctionSigmiodDouble.compute_static(input) * (1 - FunctionSigmiodDouble.compute_static(input));
        }


    }
}
