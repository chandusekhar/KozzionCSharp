using System;

namespace KozzionMathematics.Numeric.Signal.Windowing
{
    public class BartlettWindow : AWindowFloat
    {

        public BartlettWindow(float filter_width)
            :  base(filter_width)
        {
          
        }

        protected override float ComputeInside(float input)
        {
            return 1 - (Math.Abs(input) / (FilterWidth / 2.0f));
        }
    }
}
