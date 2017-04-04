using KozzionMathematics.Function;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionUInt32ToColorWindow : IFunction<uint, System.Drawing.Color>
    {
        public string FunctionType { get { return "FunctionUInt32ToColorWindow"; } }
        uint window_lower_bound;
        uint window_upper_bound;

        public FunctionUInt32ToColorWindow(
            uint window_lower_bound,
            uint window_upper_bound)
        {
  
            this.window_lower_bound = window_lower_bound;
            this.window_upper_bound = window_upper_bound;
        }

        public Color Compute(uint value)
        {
            value = ConvertValue(value);
            return Color.FromArgb(255, (int)value, (int)value, (int)value);
        }

        private uint ConvertValue(uint value)
        {
            value = ((value - window_lower_bound) * 255) / (window_upper_bound - window_lower_bound);

            if (255 < value)
            {
                value = 255;
            }

            if (value < 0)
            {
                value = 0;
            }
            return value;
        }
    }
}