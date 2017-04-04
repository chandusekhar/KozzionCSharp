using KozzionMathematics.Function;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
	public class FunctionBoolToColor : IFunction<bool, Color>
	{
        public string FunctionType { get { return "FunctionBoolToColor"; } }
        private Color d_true_color;
		private Color d_false_color;


		public FunctionBoolToColor()
		{
			d_true_color = Color.White;
			d_false_color = Color.Black;
		}

        public FunctionBoolToColor(Color true_color, Color false_color)
        {
            d_true_color = true_color;
            d_false_color = false_color;
        }

		public Color convert_to_color(bool value)
		{
			if (value)
			{
				return d_true_color;
			}
			else
			{
				return d_false_color;
			}
		}

        public Color Compute(bool value)
        {
            return convert_to_color(value);
        }
    }
}
