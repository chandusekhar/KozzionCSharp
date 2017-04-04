using System;
using System.Drawing;
using KozzionMathematics.Function;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionFloat32ToColorASIST: IFunction<float, Color>
	{
        public string FunctionType { get { return "FunctionFloat32ToColorASIST"; } }
        private float  window_lower_bound;
		private float  window_upper_bound;
		private Color [] d_color_lookup;

        public FunctionFloat32ToColorASIST(float window_lower_bound, float window_upper_bound)
		{
			if (window_upper_bound <= window_lower_bound)
			{
				throw new Exception("Lower bound should be lower than upper bound");
			}
            this.window_lower_bound = window_lower_bound;
            this.window_upper_bound = window_upper_bound;
			d_color_lookup = new Color [256];
			for (int color_index = 0; color_index < d_color_lookup.Length; color_index++)
			{
				d_color_lookup[color_index] = ColorLookup(color_index);

			}

		}

		/**
		 * @param color_index
		 * @return
		 */
		private Color ColorLookup(int color_index)
		{
			int r = 0;
			if (color_index < 20)
			{
				r = color_index * 4;
			}
			else if ((20 <= color_index) && (color_index < 100))
			{
				r = 100 - color_index;
			}
			else if ((100 <= color_index) && (color_index < 128))
			{
				r = 0;
			}
			else if ((128 <= color_index) && (color_index < 192))
			{
				r = (color_index - 128) * 4;
			}
			else if ((192 <= color_index))
			{
				r = 255;
			}

			int g = 0;
			if ((45 <= color_index) && (color_index < 130))
			{
				g = (color_index - 45) * 3;
			}
			else if ((130 <= color_index) && (color_index < 192))
			{
				g = 255;
			}
			else if (192 <= color_index)
			{
				g = 256 + ((color_index - 191) * -4);
			}

			int b = 0;
			if ((1 <= color_index) && (color_index < 87))
			{
				b = (color_index - 1) * 3;
			}
			else if ((87 <= color_index) && (color_index < 138))
			{
				b = ((color_index - 87) * -5) + 250;
			}

			return Color.FromArgb(255, r, g, b); 
		}


		public Color Compute(float value)
		{

            float rescaled_value = ((value - this.window_lower_bound) * 255f) / (this.window_upper_bound - this.window_lower_bound);
            int int_value = (int)rescaled_value;
            if (255 < int_value)
            {
                int_value = 255;
            }
            if (int_value < 0)
            {
                int_value = 0;
            }

            return d_color_lookup[int_value];
		}
	}
}
