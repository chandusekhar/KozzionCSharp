using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.DataStructure.BigDecimal;
using KozzionMathematics.Function;
using KozzionCore.DataStructure.Collections;
using KozzionMathematics.Algebra;

namespace KozzionMathematics.Tools
{
    public class ToolsMathConvolution
    {


        // equivalent to matlab conv(a,b, 'full')
        public static double[] ConvolveUniformFull(IList<double> list_base, IList<double> list_kernel)
        {
            if ((list_base.Count == 0) || (list_kernel.Count == 0))
            {
                throw new Exception("list_base and list_kernel must at least be of size 1");
            }
		
            double[] result = new double[list_base.Count + list_kernel.Count - 1];

            for (int result_index = 0; result_index < result.Length; result_index++)
            {
                for (int kernel_index = 0; kernel_index < list_kernel.Count; kernel_index++)
                {
                    if ((0 <= result_index - kernel_index) && (result_index - kernel_index < list_base.Count))
                    {
                        result[result_index] += list_base[result_index - kernel_index] * list_kernel[kernel_index];
                    }
                }
            }
            return result;
        }

        public static double[] ConvolveUniform(IList<double> list_base, IList<double> list_kernel, bool corrected, int offset, int lenght)
        {
            if ((list_base.Count == 0) || (list_kernel.Count == 0))
            {
                throw new Exception("list_base and list_kernel must at least be of size 1");
            }
            double kernel_total = 0;
            if (corrected)
            {
                for (int kernel_index = 0; kernel_index < list_kernel.Count; kernel_index++)
                {
                    kernel_total += list_kernel[kernel_index];
                }
            }

            double[] result = new double[lenght];
            for (int result_index = 0; result_index < result.Length; result_index++)
            {
                double kernel_part = 0;
                for (int kernel_index = 0; kernel_index < list_kernel.Count; kernel_index++)
                {
                    if ((0 <= result_index + offset - kernel_index) && (result_index + offset - kernel_index < list_base.Count))
                    {
                        result[result_index] += list_base[result_index + offset - kernel_index] * list_kernel[kernel_index];
                        kernel_part += list_kernel[kernel_index];
                    }
                }
                if (corrected)
                {
                    result[result_index] *= (kernel_total / kernel_part);
                }
            }
            return result;
        }


        public static RealType[] ConvolveUniform<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_base, IList<RealType> list_kernel, bool corrected, int offset, int lenght)
        {
            if ((list_base.Count == 0) || (list_kernel.Count == 0))
            {
                throw new Exception("list_base and list_kernel must at least be of size 1");
            }
            RealType kernel_total = algebra.AddIdentity;
            if (corrected)
            {
                for (int kernel_index = 0; kernel_index < list_kernel.Count; kernel_index++)
                {
                    kernel_total = algebra.Add(kernel_total, list_kernel[kernel_index]);
                }
            }

            RealType[] result = new RealType[lenght];
            for (int result_index = 0; result_index < result.Length; result_index++)
            {
                RealType kernel_part = algebra.AddIdentity;
                for (int kernel_index = 0; kernel_index < list_kernel.Count; kernel_index++)
                {
                    if ((0 <= result_index + offset - kernel_index) && (result_index + offset - kernel_index < list_base.Count))
                    {
                        result[result_index] = algebra.Add(result[result_index], algebra.Multiply(list_base[result_index + offset - kernel_index], list_kernel[kernel_index]));
                        kernel_part = algebra.Add(kernel_part, list_kernel[kernel_index]);
                    }
                }
                if (corrected)
                {
                    result[result_index] = algebra.Multiply(result[result_index],(algebra.Divide(kernel_total, kernel_part)));
                }
            }
            return result;
        }


        // equivalent to matlab c = conv(a,b, 'full'); c(1:numel(a))
        public static double[] ConvolveUniformSameUncorrectedUnoffsetted(IList<double> list_base, IList<double> list_kernel)
        {
            int offset = 0;
            int lenght = list_base.Count;
            return ConvolveUniform(list_base, list_kernel, false, offset, lenght);
        }

        // equivalent to matlab c = conv(a,b, 'full'); c(1:numel(a))
        public static RealType[] ConvolveUniformSameUncorrectedUnoffsetted<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_base, IList<RealType> list_kernel)
        {
            int offset = 0;
            int lenght = list_base.Count;
            return ConvolveUniform(algebra, list_base, list_kernel, false, offset, lenght);
        }

        // equivalent to matlab conv(a,b, 'same')
        public static double[] ConvolveUniformSameUncorrectedOffsetted(IList<double> list_base, IList<double> list_kernel)
        {
            int offset = list_kernel.Count / 2;
            int lenght = list_base.Count;
            return ConvolveUniform(list_base, list_kernel, false, offset, lenght);
        }

        // equivalent to matlab conv(a,b, 'same')
        public static RealType[] ConvolveUniformSameUncorrectedOffseted<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_base, IList<RealType> list_kernel)
        {
            int offset = list_kernel.Count / 2;
            int lenght = list_base.Count;
            return ConvolveUniform(algebra, list_base, list_kernel, false, offset, lenght);
        }

        // close to matlab conv(a,b, 'same') but not really
        public static double[] ConvolveUniformSameCorrectedUnoffsetted(IList<double> list_base, IList<double> list_kernel)
        {
            int offset = 0;
            int lenght = list_base.Count;
            return ConvolveUniform(list_base, list_kernel, true, offset, lenght);
        }

        // close to matlab conv(a,b, 'same') but not really
        public static RealType[] ConvolveUniformSameCorrectedUnoffseted<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_base, IList<RealType> list_kernel)
        {
            int offset = 0;
            int lenght = list_base.Count;
            return ConvolveUniform(algebra, list_base, list_kernel, true, offset, lenght);
        }

        // close to matlab conv(a,b, 'same') but not really
        public static double[] ConvolveUniformSameCorrectedOffsetted(IList<double> list_base, IList<double> list_kernel)
        {

            int offset = list_kernel.Count / 2;
            int lenght = list_base.Count;
            return ConvolveUniform(list_base, list_kernel, true, offset, lenght);
        }

        // close to matlab conv(a,b, 'same') but not really
        public static RealType[] ConvolveUniformSameCorrectedOffsetted<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_base, IList<RealType> list_kernel)
        {

            int offset = list_kernel.Count / 2;
            int lenght = list_base.Count;
            return ConvolveUniform(algebra, list_base, list_kernel, true, offset, lenght);
        }



        // Convolution returns only space where the tot kernel fits in the base equivalent to matlab conv(a,b, 'valid')
        // Correction is irrelevant as only the portion with no need for correction is returned
        public static double[] ConvolveUniformValid(IList<double> list_base, IList<double> list_kernel)
        {
            if (list_base.Count < list_kernel.Count)
            {
                throw new Exception("list_base must be must at least be of size of list_kernel");
            }

            int offset = list_kernel.Count - 1;
            int lenght = list_base.Count - (list_kernel.Count - 1);  
            return ConvolveUniform(list_base, list_kernel, true, offset, lenght); 
        }

        // Convolution returns only space where the tot kernel fits in the base equivalent to matlab conv(a,b, 'valid')
        // Correction is irrelevant as only the portion with no need for correction is returned
        public static RealType[] ConvolveUniformValid<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_base, IList<RealType> list_kernel)
        {
            if (list_base.Count < list_kernel.Count)
            {
                throw new Exception("list_base must be must at least be of size of list_kernel");
            }

            int offset = list_kernel.Count - 1;
            int lenght = list_base.Count - (list_kernel.Count - 1);
            return ConvolveUniform(algebra, list_base, list_kernel, false, offset, lenght);
        }



        private static double[] ConvolveUniformCycleLoop(IList<double> list_base, IList<double> list_kernel)
        {
            if ((list_base.Count == 0) || (list_kernel.Count == 0))
            {
                throw new Exception("list_base and list_kernel must at least be of size 1");
            }

            if (list_base.Count != list_kernel.Count)
            {
                throw new Exception("list_base and list_kernel must be of equal size");
            }
            double[] result = new double[list_base.Count];
 

            for (int result_index = 0; result_index < list_base.Count; result_index++)
            {
                for (int kernel_index = 0; kernel_index < list_base.Count; kernel_index++)
                {
                    result[result_index] += list_kernel[kernel_index] * list_base[(kernel_index + result_index) % list_base.Count];
                }
            }
            return result;
        }

        private static double[] ConvolveUniformCycleFFT( double[] array0,  double[] array1)
        {
            // This was in the transform library
            //         DoubleFFT_1D fft_operator = new DoubleFFT_1D(array0.length);
            //
            //        fft_operator.realForward(array0);
            //        fft_operator.realForward(array1);
            //         double [] result = ArrayTools.multiply_copy(array0, array1);
            //        fft_operator.realInverse(result, true);
            //        return result;
            return null;
        }

  


        public static double[] convolute_by_cycle( double[] array0,  double[] array1)
        {
            //double[] array0_padded = resize_array(array0, array0.length * 2, array0.length, 0);
            //double[] array1_padded = resize_array(array1, array0.length * 2, 0, 0);
            //double[] result_padded = ConvolveCycle(array0_padded, array1_padded);
            //double[] reversed = reverse_array(result_padded);
            //return reversed;
            return null;
        }

        public static double[] convolute_by_fft( double[] array0,  double[] array1)
        {

            // double[] array0_padded = resize_array(array0, array0.length * 2, array0.length, 0);
            // double[] array1_padded = resize_array(array1, array0.length * 2, 0, 0);
            // double[] result_padded = do_convolute_by_fft_operation(array0_padded, array1_padded);
            // double[] result = resize_array(result_padded, array0.length, 0, array0.length / 2);
            // double[] reversed = reverse_array(result);
            //return reversed;
            return null;
        }

   
        public static IFunction<double, double> convolute_by_fft(IFunction<double, double> function_0,
             IFunction<double, double> function_1, ISamplingStrategy1D<double> sampling_strategy, IList<double> sample_points)
        {
            //          double[] array0 = sampling_strategy.convert_to_array(function_0);
            //          double[] array1 = sampling_strategy.convert_to_array(function_1);
            //          double[] result = convolute_by_fft(array0, array1);
            //return sampling_strategy.convert_to_function(result);
            return null;

        }

        public static IFunction<double, double> ConvolveByCycleFunction(
			IFunction<double, double> function_0, IFunction<double, double> function_1,
             ISamplingStrategy1D<double> sampling_strategy, IList<double> sample_points)
        {
            // double[] array0 = sampling_strategy.convert_to_array(function_0, sample_points);
            // double[] array1 = sampling_strategy.convert_to_array(function_1, sample_points);

            // double[] response_array = convolute_by_cycle(array0, array1);
            // double[] result = new double[array0.length];

            //for (int index = 0; index < array0.length; index++)
            //{
            //    for (int offset = -array0.length; offset < array0.length; offset++)
            //    {
            //        if ((0 < (index + offset)) && ((index + offset) < array0.length))
            //        {
            //            result[index] += array0[index + offset] * response_array[offset + array0.length];
            //        }
            //    }
            //}

            //return domain.convert_to_function(result);
            return null;
        }



        private static double[] resize_array(double[] array, int new_length, int offset_new, int offset_old)
        {
            double[] resized = new double[new_length];
            int copy_size = Math.Min(array.Length, new_length);
            for (int index = 0; index < copy_size; index++)
            {
                resized[index + offset_new] = array[index + offset_old];
            }
            return resized;
        }

    }
}