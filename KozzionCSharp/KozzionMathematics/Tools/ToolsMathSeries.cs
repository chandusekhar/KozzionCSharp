using KozzionMathematics.Algebra;
using System;
using System.Collections.Generic;

namespace KozzionMathematics.Tools
{
    public static class ToolsMathSeries
    {

        //Richardson Extrapolation http://en.wikipedia.org/wiki/Richardson_extrapolation
        // 
        // @param value  *            original value
        // @param refined_value     *            with base times smaller intervals
        // @param base     *            refinement factor of value
        // @param power     *            order of scale
        //@return
        public static RealType RichardsonExtrapolation<RealType>(IAlgebraReal<RealType> algebra, RealType value, RealType refined_value, RealType base_value, RealType power)
        {
            RealType scale = algebra.Pow(base_value, power);
            return algebra.Divide(algebra.Subtract(algebra.Multiply(scale,refined_value),value),algebra.Subtract(scale, algebra.MultiplyIdentity));
        }

        public static double RichardsonExtrapolation(double value, double refined_value, int base_value, int power)
        {
            float scale = (float)Math.Pow(base_value, power);
            return ((scale * refined_value) - value) / (scale - 1.0f);
        }

        public static int[] RangeInt32(int offset, int count)
        {
            int[] range = new int[count];
            for (int index = 0; index < count; index++)
            {
                range[index] = index + offset;
            }
            return range;
        }

        public static int [] RangeInt32(int count)
        { 
            return RangeInt32(0, count);
        }

        public static int[] RangeInt32Without(int count, ISet<int> without)
        {
            int[] range = new int[count - without.Count];
            int range_index = 0;
            for (int index = 0; index < count; index++)
            {
                if (!without.Contains(index))
                {
                    range[range_index] = index;
                    range_index++;
                }
            }
            return range;
        }

        public static int[] RangeInt32Without(int count, int without)
        {
            return RangeInt32Without(count, new HashSet<int>(new int[] { without }));
        }
        

        public static float[] RangeFloat32(int count)
        {
            float[] range = new float[count];
            for (int index = 0; index < count; index++)
            {
                range[index] = index;
            }
            return range;
        }

        public static double[] RangeFloat64(int count)
        {
            double[] range = new double[count];
            for (int index = 0; index < count; index++)
            {
                range[index] = index;
            }
            return range;
        }


     

      

      
    }
}
