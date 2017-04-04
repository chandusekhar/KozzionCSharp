namespace KozzionMathematics.Function.Implementation.Distance
{
    public class FunctionDistanceMSE :
        IFunctionDistance<double[], double>, 
        IFunctionDistance<float[], float>, 
        IFunctionDistance<int[], float>
    {
        public string FunctionType { get { return "FunctionDistanceMSE"; } }
        public static float ComputeStatic(int[] value_0, int[] value_1)
        {
            float distance = 0;
            for (int index = 0; index < value_0.Length; index++)
            {
                distance += (value_0[index] - value_1[index]) * (value_0[index] - value_1[index]);
            }
            return distance / value_0.Length;
        }

        public static float ComputeStatic(float[] value_0, float[] value_1)
        {
            float distance = 0;
            for (int index = 0; index < value_0.Length; index++)
            {
                distance += (value_0[index] - value_1[index]) * (value_0[index] - value_1[index]);
            }
            return distance / value_0.Length;
        }


        public static double ComputeStatic(double[] value_0, double[] value_1)
        {
            double distance = 0;
            for (int index = 0; index < value_0.Length; index++)
            {
                distance += (value_0[index] - value_1[index]) * (value_0[index] - value_1[index]);
            }
            return distance / value_0.Length;
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

        public double ComputeToRectangle(double[] valeu_0, double[] upper, double[] lower)
        {
            double sum = 0;
            for (int index = 0; index < valeu_0.Length; ++index)
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
            return (sum / valeu_0.Length) * (sum / valeu_0.Length);
        }

        public float ComputeToRectangle(int[] valeu_0, int[] upper, int[] lower)
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
            return (sum / valeu_0.Length) * (sum / valeu_0.Length);
        }

        public float ComputeToRectangle(float[] valeu_0, float[] upper, float[] lower)
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
            return (sum / valeu_0.Length) * (sum / valeu_0.Length);
        }
    }
}
