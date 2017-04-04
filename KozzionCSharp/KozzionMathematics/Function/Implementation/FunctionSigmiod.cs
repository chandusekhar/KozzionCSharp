
using System;

namespace KozzionMathematics.Function.Implementation
{
    public class FunctionSigmiodFloat :
        IFunctionDerivable<float, float>
    {
        public string FunctionType { get { return "FunctionSigmiodFloat"; } }
        public FunctionSigmiodFloat()
        {

        }

        public float Compute(float input)
        {
            return compute_static(input);
        }

        public IFunction<float, float> get_derivative()
        {
            return new FunctionSigmiodDerivativeFloat();
        }

        public static double compute_static(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        public static float compute_static(float input)
        {
            return 1 / (1 + (float)Math.Exp(-input));
        }

    }


    public class FunctionSigmiodDouble :
        IFunctionDerivable<double, double>
    {
        public string FunctionType { get { return "FunctionSigmiodDouble"; } }
        public FunctionSigmiodDouble()
        {

        }

        public double Compute(double input)
        {
            return compute_static(input);
        }

        public IFunction<double, double> get_derivative()
        {
            return new FunctionSigmiodDerivativeDouble();
        }


        public static double compute_static(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }



    }
}