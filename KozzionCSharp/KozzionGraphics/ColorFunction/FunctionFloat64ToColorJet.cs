using KozzionMathematics.Function;
using System;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionFloat64ToColorJet : IFunction<double, Color>
	{
        public string FunctionType { get { return "FunctionFloat32ToColorJet"; } }
        //TODO add hot and cold: http://stackoverflow.com/questions/7706339/grayscale-to-red-green-blue-matlab-jet-color-scale
        private double  window_lower_bound;
		private double window_upper_bound;
		private Color [] color_lookup;

		public FunctionFloat64ToColorJet(double window_lower_bound, double window_upper_bound)
		{
			if (window_upper_bound <= window_lower_bound)
			{
				throw new Exception("Lower bound should be lower than upper bound");
			}
            this.window_lower_bound = window_lower_bound;
            this.window_upper_bound = window_upper_bound;
			this.color_lookup = new Color [256];
			for (int color_index = 0; color_index < color_lookup.Length; color_index++)
			{
                this.color_lookup[color_index] = ColorLookup(color_index);
			}

		}

		/**
		 * @param color_index
		 * @return
		 */
        private Color ColorLookup(int color_index)
        {
            // int_value is guaranteed to run from 0 to 255
            int red   = ColorRamp(color_index - 96);
            int green = ColorRamp(color_index - 32);
            int blue  = ColorRamp(color_index + 32);         

			return Color.FromArgb(255, red, green, blue); 
		}


        private int ColorRamp(int color_index)
        {
            if ((0 <= color_index) && (color_index < 64))
            {
               return color_index * 4;
            }
            else if ((64 <= color_index) && (color_index < 128))
            {
                return 255;
            }
            else if ((128 <= color_index) && (color_index < 192))
            {
                return (color_index * -4) + 767;
            }
            return 0;
        }

		public Color Compute(double value)
		{
            double rescaled_value = ((value - this.window_lower_bound) * 255.0) / (this.window_upper_bound - this.window_lower_bound);
            int int_value = (int)rescaled_value;
            if (255 < int_value)
            {
                int_value = 255;
            }
            if (int_value < 0)
            {
                int_value = 0;
            }

            return this.color_lookup[int_value];
		}
	}
}