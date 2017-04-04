

using KozzionMathematics.Function;

using System;

namespace KozzionMathematics.Numeric.Signal
{
    public class WindowFilterFloat : IFunction<float [], float []>
	{
        public string FunctionType { get { return "WindowFilterFloat"; } }

        IFilterWindow<float> window;
		int                time_offset_limit;

		public WindowFilterFloat(IFilterWindow<float> window)
		{
			this.window = window;
			time_offset_limit = (int) Math.Floor(window.FilterWidth / 2.0f);
		}

		public float [] Compute(float [] input)
		{
			float [] result = new float [input.Length];
			ComputeRBA(input, result);
			return result;
		}

		/**
		 * Uniform time filter
		 */
		public void ComputeRBA(float [] input, float [] result)
		{
			for (int sample_index = 0; sample_index < result.Length; sample_index++)
			{
				float total_contribution = 0;
				for ( int time_offset = -time_offset_limit; sample_index <= time_offset_limit; sample_index++)
				{
					if ((time_offset < 0) && (time_offset < result.Length))
					{
						 float contribution = window.Compute(time_offset);
						result[sample_index] += contribution * input[sample_index + time_offset];
						total_contribution += contribution;
					}
				}
				result[sample_index] /= total_contribution;
			}
		}

        public float[] ComputeTime(float[] input, float[] time)
		{
			float [] result = new float [input.Length];
			ComputeTimeRBA(input, time, result);
			return result;
		}

		/**
		 * Variable time filter
		 * 
		 * @param input
		 *            samples
		 * @param sample_times
		 *            samples
		 * @param result
		 */
		public void ComputeTimeRBA( float [] input,  float [] sample_times,  float [] result)
		{
			int time_index_lower = 0;
			for (int sample_index = 0; sample_index < result.Length; sample_index++)
			{
				float total_contribution = 0;
				 float sample_time = sample_times[sample_index];
				for (int time_index = time_index_lower; time_index < result.Length; time_index++)
				{
					 float time_offset = sample_times[time_index] - sample_time;
                     if (time_offset < (window.FilterWidth / -2.0))
					{
						time_index_lower++;
					}
					else
					{
                        if ((window.FilterWidth / 2.0) < time_offset)
						{
							break;
						}
						else
						{
							float contribution = window.Compute(time_offset);                 
							result[sample_index] += contribution * input[time_index];
							total_contribution += contribution;
						}
					}
				}
				result[sample_index] /= total_contribution;
			}

		}
	}
}