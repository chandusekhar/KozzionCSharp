using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Function.Implementation.Distance
{
    public class FunctionDistanceHamming :
           IFunctionDistance<bool[], int>,
           IFunctionDistance<int[], int>
    {
        public string FunctionType { get { return "FunctionDistanceHamming"; } }
        public static int ComputeStaticBool(bool[] array_0, bool[] array_1)
        {
            int distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                if (array_0[index] != array_1[index])
                {
                    distance++;
                }
            }
            return distance;
        }

        public static int ComputeStaticInt32(int[] array_0, int[] array_1)
        {
            int distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                if (array_0[index] != array_1[index])
                {
                    distance++;
                }
            }
            return distance;
        }

        public int Compute(int[] domain_value_0, int[] domain_value_1)
        {
            return ComputeStaticInt32(domain_value_0, domain_value_1);
        }

        public int Compute(bool[] domain_value_0, bool[] domain_value_1)
        {
            return ComputeStaticBool(domain_value_0, domain_value_1);
        }

        public int ComputeToRectangle(int[] array_0, int[] upper, int[] lower)
        {
            int distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                if ((array_0[index] != upper[index]) && (array_0[index] != lower[index]))
                {
                    distance++;
                }
            }
            return distance;
        }

        public int ComputeToRectangle(bool[] array_0, bool[] upper, bool[] lower)
        {
            int distance = 0;
            for (int index = 0; index < array_0.Length; index++)
            {
                if ((array_0[index] != upper[index]) && (array_0[index] != lower[index]))
                {
                    distance++;
                }
            }
            return distance;
        }
    }
}