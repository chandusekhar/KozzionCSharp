using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.ColorFunction
{
    class FunctionFloat32ToColorGrayInt : IFunction<float, int>
    {
        public string FunctionType { get { return "FunctionFloat32ToColorGrayInt"; } }
        private float window_lower_bound;
        private float window_upper_bound;

        public FunctionFloat32ToColorGrayInt(float window_lower_bound, float window_upper_bound)
        {
            if (window_upper_bound <= window_lower_bound)
            {
                throw new Exception("Lower bound should be lower than upper bound");
            }
            this.window_lower_bound = window_lower_bound;
            this.window_upper_bound = window_upper_bound;
        }

        public int Compute(float value)
        {
            int converted = ConvertValue(value);
            return Color. FromArgb(255, converted, converted, converted).ToArgb(); ;
        }


        public int ConvertValue(float value)
        {
            float rescaled_value = ((value - window_lower_bound) * 255f) / (window_upper_bound - window_lower_bound);
            int int_value = (int)rescaled_value;
            if (255 < int_value)
            {
                int_value = 255;
            }
            if (int_value < 0)
            {
                int_value = 0;
            }
            return int_value;
        }

    }
}

