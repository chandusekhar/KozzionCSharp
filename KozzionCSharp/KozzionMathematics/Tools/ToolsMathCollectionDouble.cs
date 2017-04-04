
using System;
using System.Collections.Generic;

namespace KozzionMathematics.Tools
{
    public class ToolsMathCollectionDouble
    {
 
        public static double[] Multply(
             IList<double> array_0,
             IList<double> array_1)
        {
            double[] copy = new double[array_0.Count];
            for (int index = 0; index < array_0.Count; index++)
            {
                copy[index] = array_0[index] * array_1[index];
            }
            return copy;
        }

        public static double[] Multply(
              IList<double> array_0,
              double value_0)
        {
            double[] copy = new double[array_0.Count];
            for (int index = 0; index < array_0.Count; index++)
            {
                copy[index] = array_0[index] * value_0;
            }
            return copy;
        }

        public static double[] LogE(IList<double> list_0)
        {
            double[] copy = new double[list_0.Count];
            for (int index = 0; index < list_0.Count; index++)
            {
                copy[index] = Math.Log(list_0[index]);
            }
            return copy;
        }

        public static void AbsRBA(IList<double> list_0)
        {
            for (int index = 0; index < list_0.Count; index++)
            {
                list_0[index] = Math.Abs(list_0[index]);
            }
        }

        public static double[] Add(
             IList<double> array_0,
             IList<double> array_1)
        {
            double[] copy = new double[array_0.Count];
            for (int index = 0; index < array_0.Count; index++)
            {
                copy[index] = array_0[index] + array_1[index];
            }
            return copy;
        }




        public static double[] Subtract(
             IList<double> array_0,
             IList<double> array_1)
        {
            double[] result = new double[array_0.Count];
            SubtractRBA(array_0, array_1, result);
            return result;
        }

        public static double[] Subtract(
               IList<double> array_0,
               double value_0)
        {
            double[] result = new double[array_0.Count];
            SubtractRBA(array_0, value_0, result);
            return result;
        }

        public static void SubtractRBA(
             IList<double> array_0,
             IList<double> array_1,
             IList<double> result)
        {
            for (int index = 0; index < array_0.Count; index++)
            {
                result[index] = array_0[index] - array_1[index];
            }
        }


        public static void SubtractRBA(
            IList<double> array_0,
            double value_0,
            IList<double> result)
        {
            for (int index = 0; index < array_0.Count; index++)
            {
                result[index] = array_0[index] - value_0;
            }
        }
        public static double[] Exp(IList<double> list_0)
        {
            double[] copy = new double[list_0.Count];
            for (int index = 0; index < list_0.Count; index++)
            {
                copy[index] = Math.Exp(list_0[index]);
            }
            return copy;
        }

        public static double SumSquaredDifference(
             IList<double> array_0,
             IList<double> array_1)
        {
            double squared_difference = 0.0;
            double[] differences = ToolsMathCollectionDouble.Subtract(array_0, array_1);
            foreach (double difference in differences)
            {
                squared_difference += difference * difference;
            }
            return squared_difference;
        }

    

        

       
    }
}