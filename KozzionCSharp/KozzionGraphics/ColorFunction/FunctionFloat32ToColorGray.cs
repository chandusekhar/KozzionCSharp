using KozzionMathematics.Function;
using System;
using System.Drawing;
using DrawColor = System.Drawing.Color;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionFloat32ToColorGray : IFunction<float, DrawColor>
	{
        public string FunctionType { get { return "FunctionFloat32ToColorGray"; } }
        private float window_lower_bound;
		private float window_upper_bound;
    
		public FunctionFloat32ToColorGray(float window_lower_bound, float window_upper_bound)
		{
			if (window_upper_bound <= window_lower_bound)
			{
				throw new Exception("Lower bound should be lower than upper bound");
			}
			this.window_lower_bound = window_lower_bound;
			this.window_upper_bound = window_upper_bound;
		}

        public Color Compute(float value)
        {
            int converted = ConvertValue(value);
            return Color.FromArgb(255, converted, converted, converted);
        }


		public int ConvertValue(float value)
		{
			float rescaled_value = ((value - window_lower_bound) * 255f) / (window_upper_bound - window_lower_bound);        
			int int_value = (int) rescaled_value;
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