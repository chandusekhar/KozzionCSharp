using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Deconvolution
{
    public class DeconvolutionFourier
    {
        public DeconvolutionFourier(IFunction<double, double> input_function, double[] signal_sample_times)
        {
            // Sample AIF
            // Do FFT
        }

        public IFunction<double, double> GetIRF(double[] signal)
        {
            // Do fft

            // Devide signal

            // Do IFFT

            return null;
        }

    }
}
