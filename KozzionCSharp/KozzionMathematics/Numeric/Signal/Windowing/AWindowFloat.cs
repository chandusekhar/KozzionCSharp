using System;
using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric.Signal.Windowing
{
    public abstract class AWindowFloat :  IFilterWindow<float>
    {
        public string FunctionType { get { return "AFilterWindowFloat"; } }

        public float WindowUpperBound { get; private set; }
        public float WindowLowerBound { get; private set; }

        public float FilterWidth {get; private set;}

        protected AWindowFloat(float filter_width)
        {
            FilterWidth = filter_width;
        }

        public float Compute(float input)
        {
            if (Math.Abs(input) < (FilterWidth / 2.0f))
            {
                return ComputeInside(input);
            }
            else
            {
                return 0;
            }
        }

        protected abstract float ComputeInside(float input);
    }
}