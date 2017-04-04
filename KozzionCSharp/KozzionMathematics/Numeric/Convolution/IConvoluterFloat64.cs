using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Convolution
{
    public interface IConvoluterFloat64
    {
        IFunction<double, double> ConvolveFunction(IFunction<double, double> base_function, IFunction<double, double> kernel_function, double base_domain_min, double base_domain_max, double kernel_domain_min, double kernel_domain_max);
        double [] ConvolveSampled(IList<double> list_base, IList<double> domain_base, IList<double> list_kernel, IList<double> domain_kernel);
        double[] ConvolveUniform(IList<double> list_base, IList<double> list_kernel);
    }
}
