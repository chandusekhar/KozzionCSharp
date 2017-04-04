
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KozzionCore.Tools;
using KozzionMathematics.Algebra;

namespace KozzionMathematics.Tools
{
    public class ToolsMathInterpolation
    {
        public static double Interpolation1DQuadratic(double fraction_0, double value_0, double value_1)
        {
            double weight_0 = fraction_0 * fraction_0;
            double weight_1 = (1 - fraction_0) * (1 - fraction_0);
            return ((value_0 * weight_0) +
                   (value_1 * weight_1)) / (weight_0 + weight_1);
        }

        public static RealType Interpolation1DLinear<RealType>(IAlgebraReal<RealType> algebra, RealType fraction_0, RealType value_0, RealType value_1)
        {
            return algebra.Add(algebra.Multiply(value_0, fraction_0),
                               algebra.Multiply(value_1, algebra.Subtract(algebra.MultiplyIdentity, fraction_0)));
        }

        public static double Interpolation1DLinear(double fraction_0, double value_0, double value_1)
        {
            return (value_0 * (1 - fraction_0)) +
                   (value_1 * fraction_0);
        }

        public static float Interpolation1DLinear(float fraction_0, float value_0, float value_1)
        {
            return (value_0 * (1 - fraction_0)) +
                   (value_1 * fraction_0);
        }


        public static RealType Interpolation1DLinear<RealType>(IAlgebraReal<RealType> algebra, RealType domain_0, RealType value_0, RealType domain_1, RealType value_1, RealType domain_new)
        {
            return Interpolation1DLinear(algebra, algebra.Divide(algebra.Subtract(domain_new, domain_0), algebra.Subtract(domain_1,domain_0)), value_0, value_1);
        }

        public static double Interpolation1DLinear(double domain_0, double value_0, double domain_1, double value_1, double domain_new)
        {
            return Interpolation1DLinear((domain_new - domain_0) / (domain_1 - domain_0), value_0, value_1);
        }

        public static float Interpolation1DLinear(float domain_0, float value_0, float domain_1, float value_1, float domain_new)
        {
            return Interpolation1DLinear((domain_new - domain_0) / (domain_1 - domain_0), value_0, value_1);
        }

    

        public static double Interpolation2DLinear(double fraction_0, double fraction_1, double value_00, double value_01, double value_10, double value_11)
        {
            return (value_00 *      fraction_0  *      fraction_1) + 
				   (value_01 * (1 - fraction_0) *      fraction_1) +  
				   (value_10 *      fraction_0  * (1 - fraction_1)) + 
				   (value_11 * (1 - fraction_0) * (1 - fraction_1));
        }


        //should be analogeus to matlab interp1 with method 'linear'
        public static RealType[] Resample<RealType>(IAlgebraReal<RealType> algebra, RealType[] old_sample_times, RealType[] old_values, RealType[] new_sample_times)
        {
            RealType[] new_values = new RealType[new_sample_times.Length];
            ResampleRBA(algebra, old_sample_times, old_values, new_sample_times, new_values);
            return new_values;
        }


        //should be analogeus to matlab interp1 with method 'linear'
        public static float[] Resample(float[] old_sample_times, float[] old_values, float[] new_sample_times)
        {
            float[] new_values = new float[new_sample_times.Length];
            ResampleRBA(old_sample_times, old_values, new_sample_times, new_values);
            return new_values;
        }


        //should be analogeus to matlab interp1 with method 'linear'
        public static double[] Resample(double[] old_sample_times, double[] old_values, double[] new_sample_times)
        {
            double[] new_values = new double[new_sample_times.Length];
            ResampleRBA(old_sample_times, old_values, new_sample_times, new_values);
            return new_values;
        }

        //should be analogeus to matlab interp1 with method 'linear'
        public static void ResampleRBA<RealType>(IAlgebraReal<RealType> algebra, RealType[] old_sample_times, RealType[] old_values, RealType[] new_sample_times, RealType[] new_values)
        {
            int old_sample_count = old_sample_times.Length;
            int new_sample_count = new_sample_times.Length;

            int old_sample_index = 0;
            for (int new_sample_index = 0; new_sample_index < new_sample_count; new_sample_index++)
            {
                if (algebra.Compare(new_sample_times[new_sample_index],old_sample_times[0]) != 1)
                {
                    //extrapolate_before
                    new_values[new_sample_index] = old_values[0];
                }
                else
                {
                    if (algebra.Compare(old_sample_times[old_sample_count - 1], new_sample_times[new_sample_index]) != 1)
                    {
                        //extrapolate_before after
                        new_values[new_sample_index] = old_values[old_sample_count - 1];
                    }
                    else
                    {
                        //We are stricktly between samples
                        while (algebra.Compare(old_sample_times[old_sample_index],new_sample_times[new_sample_index])  == -1)
                        {
                            old_sample_index++;
                        }

                        new_values[new_sample_index] = Interpolation1DLinear(algebra, old_sample_times[old_sample_index - 1], old_values[old_sample_index - 1], old_sample_times[old_sample_index], old_values[old_sample_index], new_sample_times[new_sample_index]);

                    }
                }

            }
        }


        //should be analogeus to matlab interp1 with method 'linear'
        public static void ResampleRBA(float[] old_sample_times, float[] old_values, float[] new_sample_times, float[] new_values)
        {
            int old_sample_count = old_sample_times.Length;
            int new_sample_count = new_sample_times.Length;

            int old_sample_index = 0;
            for (int new_sample_index = 0; new_sample_index < new_sample_count; new_sample_index++)
            {
                if (new_sample_times[new_sample_index] <= old_sample_times[0])
                {
                    //extrapolate_before
                    new_values[new_sample_index] = old_values[0];
                }
                else
                {
                    if (old_sample_times[old_sample_count - 1] <= new_sample_times[new_sample_index])
                    {
                        //extrapolate_before after
                        new_values[new_sample_index] = old_values[old_sample_count - 1];
                    }
                    else
                    {
                        //We are stricktly between samples
                        while (old_sample_times[old_sample_index] < new_sample_times[new_sample_index])
                        {
                            old_sample_index++;
                        }

                        new_values[new_sample_index] = Interpolation1DLinear(old_sample_times[old_sample_index - 1], old_values[old_sample_index - 1], old_sample_times[old_sample_index], old_values[old_sample_index], new_sample_times[new_sample_index]);

                    }
                }

            }
        }


        //should be analogeus to matlab interp1 with method 'linear'
        public static void ResampleRBA(double[] old_sample_times, double[] old_values, double[] new_sample_times, double[] new_values)
        {
            int old_sample_count = old_sample_times.Length;
            int new_sample_count = new_sample_times.Length;

            int old_sample_index = 0;
            for (int new_sample_index = 0; new_sample_index < new_sample_count; new_sample_index++)
            {
                if (new_sample_times[new_sample_index] <= old_sample_times[0])
                {
                    //extrapolate_before
                    new_values[new_sample_index] = old_values[0];
                }
                else
                {
                    if (old_sample_times[old_sample_count - 1] <= new_sample_times[new_sample_index])
                    {
                        //extrapolate_before after
                        new_values[new_sample_index] = old_values[old_sample_count - 1];
                    }
                    else
                    {
                        //We are stricktly between samples
                        while (old_sample_times[old_sample_index] < new_sample_times[new_sample_index])
                        {
                            old_sample_index++;
                        }

                        new_values[new_sample_index] = Interpolation1DLinear(old_sample_times[old_sample_index - 1], old_values[old_sample_index - 1], old_sample_times[old_sample_index], old_values[old_sample_index], new_sample_times[new_sample_index]);

                    }
                }

            }
        }

        public static void ResampleFiniteDifferenceRBA(double[] old_sample_times, double[] old_values, double step_size, double[] new_values)
        {
            double a = 0.0f;
            double b = 0.0f;      

            int source_index = 1;
            for (int output_index = 0; output_index < new_values.Length; output_index++)
            {
                /* Find the first time poinputt (j) after i*dt. */
                for (; source_index < (old_values.Length - 1); source_index++)
                {
                    if (old_sample_times[source_index] > (output_index * step_size))
                    {
                        break;
                    }
                }

                /*
                 * Calculate the first order derivatives at next time using finite difference.
                 * The derivatives are scaled by (time[j] - time[j - 1])
                 */
                if (source_index == 1)
                {
                    /* source_index - 1 is an end-point. Calculate one-sided difference. */
                    a = old_values[source_index] - old_values[source_index - 1];
                }
                else
                {
                    a =    (0.5f * (old_values[source_index    ] - old_values[source_index - 1]))
                        + ((0.5f * (old_values[source_index - 1] - old_values[source_index - 2]) 
                        *          (old_sample_times[source_index    ] - old_sample_times[source_index - 1])) /
                                   (old_sample_times[source_index - 1] - old_sample_times[source_index - 2]));
                }

                if (source_index == (old_values.Length - 1))
                {
                    /* source_index is an end-point. Calculate one-sided difference. */
                    b = old_values[source_index] - old_values[source_index - 1];
                }
                else
                {
                    b = ((0.5f * (old_values[source_index + 1] - old_values[source_index]) *
                                (old_sample_times[source_index    ] - old_sample_times[source_index - 1])) / 
                                (old_sample_times[source_index + 1] - old_sample_times[source_index])) +
                        (0.5f * (old_values[source_index] - old_values[source_index - 1]));
                }

                /* interpolate at 'i*dt'. */
                double t1 = ((output_index* step_size) - old_sample_times[source_index - 1]) / (old_sample_times[source_index] - old_sample_times[source_index - 1]);
                double t2 = t1* t1;
                double t3 = t1* t2;
                new_values[output_index] = ((((2 * t3) - (3 * t2)) + 1) * old_values[source_index - 1]) + (((t3 - (2 * t2)) + t1) * a) + (((-2 * t3) + (3 * t2)) * old_values[source_index]) + ((t3 - t2) * b);
            }
        }


    }
}