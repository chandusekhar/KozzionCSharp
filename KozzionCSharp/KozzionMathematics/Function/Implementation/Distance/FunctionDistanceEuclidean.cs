using System;

namespace KozzionMathematics.Function.Implementation.Distance
{
    public class FunctionDistanceEuclidean :
        IFunctionDistance<double[], double>, 
        IFunctionDistance<float[], float>,
        IFunctionDistance<int[], float>
    {
        public string FunctionType { get { return "FunctionDistanceEuclidean"; } }
        public static int ComputeStaticInt32(int[] array_0, int[] array_1)
        {
            int distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                distance += (array_0[index] - array_1[index]) * (array_0[index] - array_1[index]);
            }
            return (int)Math.Sqrt(distance);
        }

        public static float ComputeStatic(int[] array_0, int[] array_1)
        {
            float distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                distance += (array_0[index] - array_1[index]) * (array_0[index] - array_1[index]);
            }
            return (float)Math.Sqrt(distance);
        }

        public static float ComputeStatic(float[] array_0, float[] array_1)
        {
            float distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                distance += (array_0[index] - array_1[index]) * (array_0[index] - array_1[index]);
            }
            return (float)Math.Sqrt(distance);
        }


        public static double ComputeStatic(double[] value_0, double[] value_1)
        {
            double distance = 0;
            for (int index = 0; index < value_0.Length; index++)
            {
                distance += (value_0[index] - value_1[index]) * (value_0[index] - value_1[index]);
            }
            return distance;
        }

        public double Compute(double[] value_0, double[] value_1)
        {
            return ComputeStatic(value_0, value_1);

        }
        public float Compute(float[] value_0, float[] value_1)
        {
            return ComputeStatic(value_0, value_1);

        }

        public float Compute(int[] value_0, int[] value_1)
        {
            return ComputeStatic(value_0, value_1);
        }

 

        /// <summary>
        /// Find the shortest distance from a point to an axis aligned rectangle in n-dimensional space.
        /// </summary>
        /// <param name="valeu_0">The point of interest.</param>
        /// <param name="lower">The minimum coordinate of the rectangle.</param>
        /// <param name="upper">The maximum coorindate of the rectangle.</param>
        /// <returns>The shortest squared n-dimensional squared distance between the point and rectangle.</returns>
        public double ComputeToRectangle(double[] valeu_0, double[] lower, double[] upper)
        {
            double sum = 0;
            for (int index= 0; index < valeu_0.Length; ++index)
            {
                double difference = 0;
                if (valeu_0[index] > upper[index])
                {
                    difference = (valeu_0[index] - upper[index]);
                }
                else if (valeu_0[index] < lower[index])
                {
                    difference = (valeu_0[index] - lower[index]);
                }
                sum += difference * difference;
            }
            return sum;
        }


        public float ComputeToRectangle(float[] valeu_0, float[] lower, float[] upper)
        {
            float sum = 0;
            for (int index = 0; index < valeu_0.Length; ++index)
            {
                float difference = 0;
                if (valeu_0[index] > upper[index])
                {
                    difference = (valeu_0[index] - upper[index]);
                }
                else if (valeu_0[index] < lower[index])
                {
                    difference = (valeu_0[index] - lower[index]);
                }
                sum += difference * difference;
            }
            return sum;
        }

        public float ComputeToRectangle(int[] valeu_0, int[] lower, int[] upper)
        {
            int sum = 0;
            for (int index = 0; index < valeu_0.Length; ++index)
            {
                int difference = 0;
                if (valeu_0[index] > upper[index])
                {
                    difference = (valeu_0[index] - upper[index]);
                }
                else if (valeu_0[index] < lower[index])
                {
                    difference = (valeu_0[index] - lower[index]);
                }
                sum += difference * difference;
            }
            return sum;
        }       
    }
}
