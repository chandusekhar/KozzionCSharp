using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Convolution
{
    public class ConvoluterGenericStepCorrectedUnoffsetted<RealType> : IConvoluter<RealType, RealType>
             where RealType : IComparable<RealType>
    {
        private RealType sample_time;
        private IAlgebraReal<RealType> algebra;

        public ConvoluterGenericStepCorrectedUnoffsetted(IAlgebraReal<RealType> algebra, RealType sample_time)
        {
            this.algebra = algebra;
            this.sample_time = sample_time;
        }

        public IFunction<RealType, RealType> ConvolveFunction(IFunction<RealType, RealType> base_function, IFunction<RealType, RealType> kernel_function, RealType base_domain_min, RealType base_domain_max, RealType kernel_domain_min, RealType kernel_domain_max)
        {
            int base_sample_count = this.algebra.FloorInt(this.algebra.Divide(this.algebra.Subtract(base_domain_max,base_domain_min),sample_time)) + 1;
            RealType[] base_sample_times = new RealType[base_sample_count];
            RealType[] list_base = new RealType[base_sample_count];
            base_sample_times[0] = base_domain_min;
            list_base[0] = base_function.Compute(base_sample_times[0]);
            for (int sample_index = 1; sample_index < base_sample_count; sample_index++)
            {
                base_sample_times[sample_index] = this.algebra.Add(base_sample_times[sample_index - 1],this.sample_time);
                list_base[sample_index] = base_function.Compute(base_sample_times[sample_index]);
            }

            int kernel_sample_count = this.algebra.FloorInt(this.algebra.Divide(this.algebra.Subtract(kernel_domain_max,kernel_domain_min), sample_time)) + 1;
            RealType[] list_kernel = new RealType[kernel_sample_count];
            RealType kernel_sample_time = kernel_domain_min;
            for (int sample_index = 0; sample_index < kernel_sample_count; sample_index++)
            {
                list_kernel[sample_index] = kernel_function.Compute(kernel_sample_time);
                kernel_sample_time = this.algebra.Add(kernel_sample_time, sample_time);
            }

            RealType[] result = ConvolveUniform(list_base, list_kernel);        
            for (int sample_index = 0; sample_index < base_sample_count; sample_index++)
            {
                result[sample_index] = this.algebra.Multiply(result[sample_index], this.sample_time);
            }
            return new FunctionMultiStep<RealType, RealType>(base_sample_times, result);
        }

        public RealType[] ConvolveSampled(IList<RealType> list_base, IList<RealType> domain_base, IList<RealType> list_kernel, IList<RealType> domain_kernel)
        {
            throw new NotImplementedException();
        }

        public RealType[] ConvolveUniform(IList<RealType> list_base, IList<RealType> list_kernel)
        {
            return ToolsMathConvolution.ConvolveUniformSameCorrectedUnoffseted(this.algebra, list_base, list_kernel);
        }
    }
}
