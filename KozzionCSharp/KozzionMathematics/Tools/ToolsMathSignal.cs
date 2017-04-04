using KozzionMathematics.Algebra;
using KozzionMathematics.Numeric.Signal;
using KozzionMathematics.Numeric.Signal.Windowing;
using System;
using System.Collections.Generic;

namespace KozzionMathematics.Tools
{
    public class ToolsMathSignal
    {
		public float [] FilterBartlett(float[] sample_times, float [] input,  float filter_width)
		{
			float [] result = new float [input.Length];
            FilterBartlettRBA(sample_times, input, filter_width, result);
			return result;
		}

		public void FilterBartlettRBA(float[] sample_times,  float [] input, float filter_width, float[] result)
		{
            FilterWindow<float> filter = new FilterWindow<float>(new AlgebraRealFloat32(), new BartlettWindow(filter_width));
			filter.ComputeRBA(sample_times, input, result);
		}

        public static double[] SampleInterval(double lower_bound, double upper_bound, double sample_interval)
        {
            int sample_count = (int)(Math.Floor((upper_bound - lower_bound) / sample_interval)) + 1;
            double[] samples = new double[sample_count];
            for (int index = 0; index < sample_count; index++)
            {
                samples[index] = lower_bound + (index * sample_interval);
            }
            return samples;
        }


        public static float[] SampleInterval(float lower_bound, float upper_bound, float sample_interval)
        {
            int sample_count = (int)(Math.Floor((upper_bound - lower_bound) / sample_interval)) + 1;
            float[] samples = new float[sample_count];
            for (int index = 0; index < sample_count; index++)
            {
                samples[index] = lower_bound + (index * sample_interval);
            }
            return samples;
        }

        public static int[] SampleInterval(int lower_bound, int upper_bound, int sample_interval)
        {
            int sample_count = ((upper_bound - lower_bound) / sample_interval) + 1;
            int[] samples = new int[sample_count];
            for (int index = 0; index < sample_count; index++)
            {
                samples[index] = lower_bound + (index * sample_interval);
            }
            return samples;
        }
    }
}


//void bandlimit( float [] in,  float [] out,  float [] t,  float w)
//{

//    for (int i = 0; i < in.Length; i++)
//    {
//        out[i] = w * in[i];

//        float sum = w;/* Total contribution. */

//        /* Add values left of i. */
//        for (int j = i - 1; j >= 0; j--)
//        {
//             float f = (t[j] - t[i]) + w;
//            if (f <= 0)
//            {
//                break;
//            }
//            out[i] += f * in[j];
//            sum += f;
//        }

//        /* Add values right of i. */
//        for (int j = i + 1; j < in.Length; j++)
//        {
//             float f = (t[i] - t[j]) + w;
//            if (f <= 0)
//            {
//                break;
//            }
//            out[i] += f * in[j];
//            sum += f;
//        }

//        /* Normalize the result. */
//        out[i] /= sum;
//    }
