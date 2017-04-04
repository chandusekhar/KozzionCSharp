using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Convolution
{
    public interface IConvoluter<DomainType, RangeType>
    {
        IFunction<DomainType, RangeType> ConvolveFunction(IFunction<DomainType, RangeType> base_function, IFunction<DomainType, RangeType> kernel_function, DomainType domain_min, DomainType domain_max, DomainType kernel_min, DomainType kernel_max);
        RangeType[] ConvolveSampled(IList<RangeType> list_base, IList<RangeType> domain_base, IList<RangeType> list_kernel, IList<RangeType> domain_kernel);
        RangeType[] ConvolveUniform(IList<RangeType> list_base, IList<RangeType> list_kernel);
    }
}
