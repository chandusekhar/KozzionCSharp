using System;

namespace KozzionMathematics.Numeric.Signal.Windowing
{
    public class FilterWindowLancsosWindowFloat : AWindowFloat
	{
		public FilterWindowLancsosWindowFloat(float filter_width)
			: base(filter_width)
		{
			
		}

        protected override float ComputeInside(float input)
		{
			if (input == 0.0f)
			{
				return 0.0f;
			}
			else
			{
				float a = FilterWidth/ 2.0f;
				return (float) (a * Math.Sin(Math.PI  * input) *  Math.Sin((Math.PI  * input) / a)          
				/ (Math.PI * Math.PI * input * input));
			} 
		}

        public static float ComputeFloat(float domain_value, float window_centre, float window_width)
        {
            
            if (domain_value == 0.0f) //TODO??
            {
                return 0.0f;
            }
            else
            {
                float a = window_width / 2.0f;
                return (float)(a * Math.Sin(Math.PI * domain_value) * Math.Sin((Math.PI * domain_value) / a)
                / (Math.PI * Math.PI * domain_value * domain_value));
            }
        }
    }
   
}