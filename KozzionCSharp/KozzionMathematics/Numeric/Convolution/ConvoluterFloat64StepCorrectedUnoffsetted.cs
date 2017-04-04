using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;
using KozzionMathematics.Tools;

namespace KozzionMathematics.Numeric.Convolution
{
    public class ConvoluterFloat64StepCorrectedUnoffsetted : IConvoluterFloat64
    {
        double sample_time;
        public ConvoluterFloat64StepCorrectedUnoffsetted(double sample_time)
        {
            this.sample_time = sample_time;
        }

        public ConvoluterFloat64StepCorrectedUnoffsetted()
          : this(1.0)
        {
        }

        public IFunction<double, double> ConvolveFunction(IFunction<double, double> base_function, IFunction<double, double> kernel_function, double base_domain_min, double base_domain_max, double kernel_domain_min, double kernel_domain_max)
        {
            int base_sample_count = ((int)((base_domain_max - base_domain_min) / sample_time)) + 1;

            double[] base_sample_times = new double[base_sample_count];
            double[] list_base = new double[base_sample_count];
            base_sample_times[0] = base_domain_min;
            list_base[0] = base_function.Compute(base_sample_times[0]);
            for (int sample_index = 1; sample_index < base_sample_count; sample_index++)
            {
                base_sample_times[sample_index] = base_sample_times[sample_index - 1] + this.sample_time;
                list_base[sample_index] = base_function.Compute(base_sample_times[sample_index]);
            }

            int kernel_sample_count = ((int)((kernel_domain_max - kernel_domain_min) / sample_time)) + 1;
            double[] list_kernel = new double[kernel_sample_count];
            double kernel_sample_time = kernel_domain_min;
            for (int sample_index = 0; sample_index < kernel_sample_count; sample_index++)
            {
                list_kernel[sample_index] = kernel_function.Compute(kernel_sample_time);
                kernel_sample_time += sample_time;
            }

            double[] result = ConvolveUniform(list_base, list_kernel);

            if (sample_time != 1.0)
            {
                for (int sample_index = 0; sample_index < base_sample_count; sample_index++)
                {
                    result[sample_index] *= this.sample_time;
                }
            }
            return new FunctionMultiStep<double, double>(base_sample_times, result);
        }

        public double[] ConvolveSampled(IList<double> list_base, IList<double> domain_base, IList<double> list_kernel, IList<double> domain_kernel)
        {
            throw new NotImplementedException();
        }

        public double[] ConvolveUniform(IList<double> list_base, IList<double> list_kernel)
        {
            return ToolsMathConvolution.ConvolveUniformSameCorrectedUnoffsetted(list_base, list_kernel);
        }
    }
}
