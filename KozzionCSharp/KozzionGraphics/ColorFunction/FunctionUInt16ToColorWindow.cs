using KozzionMathematics.Function;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionUInt16ToColorWindow : IFunction<ushort, System.Drawing.Color>
    {
        public string FunctionType { get { return "FunctionUInt16ToColorWindow"; } }
        int window_lower_bound;
        int window_upper_bound;

        public FunctionUInt16ToColorWindow(
            int window_lower_bound,
            int window_upper_bound)
        {
  
            this.window_lower_bound = window_lower_bound;
            this.window_upper_bound = window_upper_bound;
        }

        public Color Compute(ushort value)
        {
            int converted = ConvertValue(value);
            return Color.FromArgb(255, (int)converted, (int)converted, (int)converted);
        }

        private int ConvertValue(ushort value)
        {
            int converted = ((value - window_lower_bound) * (255)) / (window_upper_bound - window_lower_bound);

            if (255 < converted)
            {
                converted = 255;
            }

            if (converted < 0)
            {
                converted = 0;
            }
            return converted;
        }
    }
}