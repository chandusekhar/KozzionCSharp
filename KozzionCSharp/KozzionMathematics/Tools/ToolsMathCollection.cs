using System;
using System.Collections.Generic;
using KozzionMathematics.Algebra;
using System.Linq;
using KozzionMathematics.Function;
using KozzionCore.Tools;
using System.Threading.Tasks;

namespace KozzionMathematics.Tools
{
    public static class ToolsMathCollection
    {

        private static AlgebraRealFloat32 algebra_float_32 = new AlgebraRealFloat32();
        private static AlgebraRealFloat64 algebra_float_64 = new AlgebraRealFloat64();
        private static AlgebraRealBigDecimal algebra_big_decimal = new AlgebraRealBigDecimal();

        public static List<Tuple<int, int>> FindPeakIntervals(IFunction<int, double> function, int min_index, int max_index, double peak_rate)
        {
            return FindPeakIntervals(new AlgebraRealFloat64(), function, min_index, max_index, peak_rate);
        }

        public static List<Tuple<int, int>> FindPeakIntervals<ValueType>(IAlgebraReal<ValueType> algebra, IFunction<int, ValueType> function, int min_index, int max_index, ValueType peak_rate)
        {

            int temp_index = min_index - 1;
            int start_index = min_index - 1;
            int index = min_index - 1;
            List<Tuple<int, int>> intervals = new List<Tuple<int, int>>();

            while (index < max_index)
            {
                if ((algebra.Compare(function.Compute(index + 1), algebra.Multiply(peak_rate, function.Compute(start_index))) == -1) &&
                    (algebra.Compare(function.Compute(temp_index), (algebra.Multiply(peak_rate, function.Compute(start_index)))) == 1))
                {
                    int peak_index = start_index;
                    while (algebra.Compare(function.Compute(peak_index - 1), (algebra.Multiply(peak_rate, function.Compute(start_index)))) != -1)
                    {
                        peak_index = peak_index - 1;
                    }
                    intervals.Add(new Tuple<int, int>(peak_index, index));
                }

                if ((algebra.Compare(function.Compute(index + 1), function.Compute(temp_index)) == -1) ||
                    (algebra.Compare(function.Compute(index + 1), algebra.Multiply(peak_rate, function.Compute(start_index))) == -1))
                {
                    temp_index = index + 1;
                    start_index = index + 1;
                    index = index + 1;
                }
                else if (algebra.Compare(function.Compute(index + 1), function.Compute(start_index)) == -1)
                {
                    start_index = index + 1;
                    index = index + 1;
                }
                else
                {
                    index = index + 1;
                }

            }

            return intervals;

        }

        public static RealType[] AbsoluteDifference<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_0, IList<RealType> list_1)
        {
            if (list_0.Count != list_1.Count)
            {
                throw new Exception("Sizes do not match");
            }
            RealType[] result = new RealType[list_0.Count];
            Parallel.For(0, result.Length, index =>
            {
                result[index] = algebra.Abs(algebra.Subtract(list_0[index], list_1[index]));        
            });
            return result;
        }

        public static double[] AbsoluteDifference(IList<double> list_0, IList<double> list_1)
        {
            return AbsoluteDifference(algebra_float_64, list_0, list_1);
        }

  

        public static RealType[] DivideElements<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> divided, IList<RealType> divisors)
        {
            if (divided.Count != divisors.Count)
            {
                throw new Exception("Size mismatch: " + divided.Count + " does not match with " + divisors.Count);
            }
            RealType[] result = new RealType[divided.Count];
            Parallel.For(0, result.Length, index =>
            {
                result[index] = algebra.Divide(divided[index], divisors[index]);
            });
            return result;
        }



        public static void DivideElementsRBA<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> divided, RealType divisor, IList<RealType> result)
        {
            Parallel.For(0, result.Count, index =>
            {
                result[index] = algebra.Divide(divided[index], divisor);
            });
        }


        public static void DivideElementsRBA(IList<float> divided, float divisor, IList<float> result)
        {
            DivideElementsRBA(algebra_float_32, divided, divisor, result);
        }

        public static RealType[] DivideElements<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> divided, RealType divisor)
        {      
            RealType[] result = new RealType[divided.Count];
            DivideElementsRBA(algebra, divided, divisor, result);
            return result;
        }

        public static float[] DivideList(IList<float> devided, float divisor)
        {
            return DivideElements(algebra_float_32, devided, divisor);
        }

        public static double[] DivideList(IList<double> devideList, IList<double> divisorList)
        {
            return DivideElements(algebra_float_64, devideList, divisorList);
        }


        public static void DivideElementsIP(IList<double> devided, double divisor)
        {
            Parallel.For(0, devided.Count, index =>
            {
                devided[index] = devided[index] / divisor;
            });
        }

        public static RealType[] Abs<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_0)
        {
            RealType[] result = new RealType[list_0.Count];
            Parallel.For(0, result.Length, index =>
            {
                result[index] = algebra.Abs(list_0[index]);
            });
            return result;
        }

        public static RealType[] Subtract<RealType>(IAlgebraReal<RealType> algebra, RealType value, IList<RealType> to_subtract)
        {
            RealType[] result = new RealType[to_subtract.Count];
            for (int index = 0; index < to_subtract.Count; index++)
            {
                result[index] = algebra.Subtract(value, to_subtract[index]);
            }
            return result;
        }

        public static void NormalizeIP(IList<double> list)
        {
            DivideElementsIP(list, Sum(list));
        }

        public static RealType[] SubtractElements<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> list_0, IList<RealType> list_1)
        {
            if (list_0.Count != list_1.Count)
            {
                throw new Exception("Size mismatch: " + list_0.Count + " does not match with " + list_1.Count);
            }
            RealType[] result = new RealType[list_0.Count];
            Parallel.For(0, result.Length, index =>
            {
                result[index] = algebra.Subtract(list_0[index], list_1[index]);
            });
            return result;
        }

        public static double[] SubtractElements(IList<double> list_0, IList<double> list_1)
        {
            return SubtractElements(algebra_float_64, list_0, list_1);
        }

        public static RealType[] Sums0<RealType>(IAlgebraReal<RealType> algebra, RealType[,] array2d)
        {
            RealType[] sums_0 = new RealType[array2d.GetLength(0)];
            for (int index_0 = 0; index_0 < array2d.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array2d.GetLength(1); index_1++)
                {
                    sums_0[index_0] = algebra.Add(sums_0[index_0], array2d[index_0, index_1]);
                }
            }
            return sums_0;
        }


        public static RealType[] Sums1<RealType>(IAlgebraReal<RealType> algebra, RealType[,] array2d)
        {
            RealType[] sums_1 = new RealType[array2d.GetLength(1)];
            for (int index_0 = 0; index_0 < array2d.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array2d.GetLength(1); index_1++)
                {
                    sums_1[index_1] = algebra.Add(sums_1[index_1], array2d[index_0, index_1]);
                }
            }
            return sums_1;
        }


        public static RealType[] Sums0<RealType>(IAlgebraReal<RealType> algebra, IList<IList<RealType>> list_list)
        {
            RealType[] sums_0 = new RealType[list_list.Count];
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                for (int index_1 = 0; index_1 < list_list[0].Count; index_1++)
                {
                    sums_0[index_0] = algebra.Add(sums_0[index_0], list_list[index_0][index_1]);
                }
            }
            return sums_0;
        }


        public static RealType[] Sums1<RealType>(IAlgebraReal<RealType> algebra, IList<IList<RealType>> list_list)
        {
            RealType[] sums_1 = new RealType[list_list[0].Count];
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                for (int index_1 = 0; index_1 < list_list[0].Count; index_1++)
                {
                    sums_1[index_1] = algebra.Add(sums_1[index_1], list_list[index_0][index_1]);
                }
            }
            return sums_1;
        }


        public static double[] Sums0(double[,] array2d)
        {
            return Sums0(algebra_float_64, array2d);
        }

        public static double[] Sums1(double[,] array2d)
        {
            return Sums1(algebra_float_64, array2d);
        }

        public static float[] Sums0(float[,] array2d)
        {
            return Sums0(algebra_float_32, array2d);
        }

        public static float[] Sums1(float[,] array2d)
        {
            return Sums1(algebra_float_32, array2d);
        }

        public static double[] Sums0(IList<IList<double>> array2d)
        {
            return Sums0(algebra_float_64, array2d);
        }

        public static double[] Sums1(IList<IList<double>> array2d)
        {
            return Sums1(algebra_float_64, array2d);
        }

        public static float[] Sums0(IList<IList<float>> array2d)
        {
            return Sums0(algebra_float_32, array2d);
        }

        public static float[] Sums1(IList<IList<float>> array2d)
        {
            return Sums1(algebra_float_32, array2d);
        }

        public static int FirstLargerIndex<DomainType>(DomainType[] sorted_array, DomainType value) 
            where DomainType : IComparable<DomainType>
        {
            throw new NotImplementedException();
        }

        public static int FirstEqualOrLargerIndex<DataType>(List<DataType> sorted_list, DataType value)
            where DataType : IComparable<DataType>
        {
            int index = sorted_list.BinarySearch(value);
            if (index < 0)
            {
                index = ~index - 1;
            }
            if (index == sorted_list.Count)
            {
                return -1;
            }
            else
            {
                return index;
            }
        }

        public static int FirstEqualOrLargerIndex<DataType>(DataType [] sorted_array, DataType value)
           where DataType : IComparable<DataType>
        {
            int index = Array.BinarySearch(sorted_array, value);
            if (index < 0)
            {
                index = ~index - 1;
            }
            if (index == sorted_array.Length)
            {
                return -1;
            }
            else
            {
                return index;
            }
        }

        public static RealType MaxValue<RealType>(IList<RealType> array)
       where RealType : IComparable<RealType>
        {
            RealType max_value = array[0];
            for (int index = 1; index < array.Count; index++)
            {
                if (array[index].CompareTo(max_value) == 1)
                {
                    max_value = array[index];
                }
            }
            return max_value;
        }


        public static double Sum(
             IList<double> array_0)
        {
            double sum = 0;
            foreach (double element in array_0)
            {
                sum += element;
            }
            return sum;
        }

        public static float Sum(
            IList<float> array_0)
        {
            float sum = 0;
            foreach (float element in array_0)
            {
                sum += element;
            }
            return sum;
        }

        public static float Sum(
             float [,,] array_0) //TODO!!! check if this works
        {
            float sum = 0;
            foreach (float element in array_0)
            {
                sum += element;
            }
            return sum;
        }


        public static RealType MinValue<RealType>(IList<RealType> array)
            where RealType : IComparable<RealType>
        {
            RealType min_value = array[0];
            for (int index = 1; index < array.Count; index++)
            {
                if (array[index].CompareTo(min_value) == -1)
                {
                    min_value = array[index];
                }
            }
            return min_value; ;
        }

        public static int MaxIndex<RealType>(IList<RealType> array)
            where RealType : IComparable<RealType>
        {
            int max_index = 0;
            RealType max_value = array[0];
            for (int index = 1; index < array.Count; index++)
            {
                if (array[index].CompareTo(max_value) == 1)
                {
                    max_index = index;
                    max_value = array[index];
                }
            }
            return max_index;
        }

        public static List<int> Ordering<RealType>(IList<RealType> list)
            where RealType : IComparable<RealType>
        {
            //TODO check this kendrall gave errors using this
            return list.Select((x, i) => new KeyValuePair<RealType, int>(x, i))
                .OrderBy(x => x.Key)
                .ToList().Select(x => x.Value).ToList();
        }

        public static int MaxIndex<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> array)
        {
            int max_index = 0;
            RealType max_value = array[0];
            for (int index = 1; index < array.Count; index++)
            {
                if (algebra.CompareTo(array[index], max_value) == 1)
                {
                    max_index = index;
                    max_value = array[index];
                }
            }
            return max_index;
        }


        public static RealType Sum<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> array)
        {
            RealType sum = algebra.AddIdentity;
            for (int index = 0; index < array.Count; index++)
            {
                sum = algebra.Add(sum, array[index]);
            }
            return sum;
        }

        public static void ShuffleIP<ElementType>(IList<ElementType> list)
        {   
            Random rng = new Random();//TODO this is bad should be a better random source
            int destination = list.Count;
            while (destination > 1)
            {
                destination--;
                int source = rng.Next(destination + 1);
                ElementType temp = list[source];
                list[source] = list[destination];
                list[destination] = temp;
            }
        }


        public static ElementType [] Shuffle<ElementType>(IList<ElementType> source)
        {
            ElementType[] target = ToolsCollection.Copy(source);
            ShuffleIP(target);
            return target;
        }

        public static int CountOccurance<RealType>(IList<RealType> list, RealType value)
        {
            int count = 0;
            for (int index = 0; index < list.Count; index++)
            {
                if (list[index].Equals(value))
                {
                    count++;
                }
            }
            return count;
        }

        public static double [] Add(IList<double> array_0, double value)
        {
            double[] target = new double[array_0.Count];
            for (int index = 0; index < array_0.Count; index++)
            {
                target[index] = array_0[index] + value;
            }
            return target;
        }

        public static void AddRBA<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> array_0, IList<RealType> array_1, IList<RealType> array_destination)
        {
            for (int index = 0; index < array_0.Count; index++)
            {
                array_destination[index] = algebra.Add(array_0[index], array_1[index]);
            }
        }

        public static RealType WeightedMean<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> values, IList<RealType> weights)
        {
            RealType value = algebra.AddIdentity;
            for (int index = 0; index < values.Count; index++)
            {
                value = algebra.Add(value, algebra.Multiply(values[index], weights[index]));
            }
            return algebra.Divide(value, Sum(algebra, weights));
        }

        public static RealType[] AddMultiple<RealType>(IAlgebraReal<RealType> algebra, IList<RealType> values_source, IList<RealType> values_add, RealType multiple)
        {
            RealType[] result = new RealType[values_source.Count];
            for (int index = 0; index < values_source.Count; index++)
            {
                result[index] = algebra.Add(values_source[index], algebra.Multiply(values_add[index], multiple));
            }
            return result;
        }

        public static void AddIP(float[,,] to_add, float[,,] add_to)
        {
            Parallel.For(0, add_to.GetLength(2), index_2 =>
           {
               for (int index_1 = 0; index_1 < add_to.GetLength(1); index_1++)
               {
                   for (int index_0 = 0; index_0 < add_to.GetLength(0); index_0++)
                   {
                       add_to[index_0, index_1, index_2] += to_add[index_0, index_1, index_2];
                   }
               }
           });
        }

        public static void AddIP(float[,] to_add, float[,] add_to)
        {
            Parallel.For(0, add_to.GetLength(1), index_1 =>
            {
                for (int index_0 = 0; index_0 < add_to.GetLength(0); index_0++)
                {
                    add_to[index_0, index_1] += to_add[index_0, index_1];
                }
            });
        }

        public static float[] Select(float[] values, float lower_bound, float upper_bound)
        {
            List<float> list = new List<float>();
            for (int index = 0; index < values.Length; index++)
            {
                if ((lower_bound <= values[index]) && (values[index] <= upper_bound))
                {
                    list.Add(values[index]);
                }
            }
            return list.ToArray();
        }

        public static double[] Select(double[] values, double lower_bound, double upper_bound)
        {
            List<double> list = new List<double>();
            for (int index = 0; index < values.Length; index++)
            {
                if ((lower_bound <= values[index]) && (values[index] <= upper_bound))
                {
                    list.Add(values[index]);
                }
            }
            return list.ToArray();
        }
    }
}
