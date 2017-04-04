using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric.Deconvolution
{
    public class TemplateLancsos : ITemplateBasisFunction
    {
        private double resolution;

        public TemplateLancsos(double resolution, double width)
        {
            this.resolution = resolution;
        }

        public IFunction<double, double> Generate(IFunction<double, double> input_function, double[] signal_sample_times, int signal_index)
        {
            //Here we build a potentially asymetric windowing function  and convolve it with the input function.
            double window_lower_width = 0;
            double window_upper_width = 0;
            if (signal_index == 0)
            {
                window_lower_width = signal_sample_times[1] - signal_sample_times[0];
            }
            else
            {
                window_lower_width = signal_sample_times[signal_index] - signal_sample_times[signal_index - 1];
            }

            if (signal_index == signal_sample_times.Length - 1)
            {
                window_upper_width = signal_sample_times[signal_sample_times.Length - 1] - signal_sample_times[0];
            }
            else
            {
                window_upper_width = signal_sample_times[signal_index + 1] - signal_sample_times[signal_index];
            }

            int sample_count = (int)((window_lower_width + window_upper_width) / this.resolution) + 1;
            double[] shifts = new double[sample_count];
            //double[] weights,

            //return new FunctionWeigthedShiftedSum(input_function, weights, shifts)
            return null;
        }
    }
}
