
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KozzionCore.Tools;


namespace KozzionMathematics.Tools
{
    public static class ToolsMathCollectionInteger
    {

        public static int product( int [] array)
        {
            int product = 1;
            foreach ( int value in array)
            {
                product *= value;
            }
            return product;
        }

        public static int [] subtract(int [] array, int value)
        {
            int [] result = new int [array.Length];
            for (int index = 0; index < array.Length; index++)
            {
                result[index] = array[index] - value;
            }
            return result;
        }

        public static int Sum(int[] array)
        {
            int sum = 0;
            foreach (int element in array)
            {
                sum += element;
            }
            return sum;
        }

        public static uint Sum(uint[] array)
        {
            uint sum = 0;
            foreach (uint element in array)
            {
                sum += element;
            }
            return sum;
        }

        public static ushort Sum(ushort[] array)
        {
            ushort sum = 0;
            foreach (ushort element in array)
            {
                sum += element;
            }
            return sum;
        }

        public static int min_value( int [] array)
        {
            int min_value = array[0];
            foreach ( int value in array)
            {
                if (value < min_value)
                {
                    min_value = value;
                }
            }
            return min_value;
        }

        public static int max_value( int [] array)
        {
            int max_value = array[0];
            foreach ( int value in array)
            {
                if (max_value < value)
                {
                    max_value = value;
                }
            }
            return max_value;
        }

        private static int [] min_max_value(int [] array)
        {
            int min_value = array[0];
            int max_value = array[0];
            foreach ( int value in array)
            {
                if (max_value < value)
                {
                    max_value = value;
                }
                if (value < min_value)
                {
                    min_value = value;
                }
            }
            return new int [] {min_value, max_value};
        }

        public static bool is_unique_max_value( int max_value,  int [] all_values)
        {
            bool seen = false;
            foreach ( int value in all_values)
            {
                if (max_value < value)
                {
                    return false;
                }
                else
                {
                    if (max_value == value)
                    {
                        if (seen)
                        {
                            return false;
                        }
                        else
                        {
                            seen = true;
                        }
                    }
                }
            }
            if (seen == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool is_max_value( int max_value,  int [] all_values)
        {
            bool seen = false;
            foreach ( int value in all_values)
            {
                if (max_value < value)
                {
                    return false;
                }
                else
                {
                    if (max_value == value)
                    {
                        seen = true;
                    }
                }
            }
            if (seen == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static float mean(int [] array)
        {
            float sum = Sum(array);
            return sum / ((float) array.Length);
        }

        public static float [] normalize(int [] array)
        {
            float sum = Sum(array);
            float [] normalized = new float [array.Length];
            for (int index = 0; index < normalized.Length; index++)
            {
                normalized[index] = array[index] / sum;
            }
            return normalized;
        }

        public static int [] histogram_positive(int [] array)
        {
            int [] histogram = new int [max_value(array) + 1];
            for (int index = 0; index < array.Length; index++)
            {
                histogram[array[index]]++;
            }
            return histogram;
        }

        public static int [] histogram_positive_safe(int [] array)
        {
            int [] min_max_values = min_max_value(array);
            if (min_max_values[0] < 0)
            {
                throw new Exception("This function does not allow for negative values");
            }
            int [] histogram = new int [min_max_values[1]];
            for (int index = 0; index < array.Length; index++)
            {
                histogram[array[index]]++;
            }
            return histogram;
        }

        public static Tuple<int [], int []> acummulate_histogram_with_next(int [] histogram, int minimal_bin_value)
        {
            int accumulated_value = 0;
            int acummulating_bin_index = 0;
            int [] bin_mapping = new int [histogram.Length];

            for (int index = 0; index < histogram.Length; index++)
            {

                accumulated_value += histogram[index];
                bin_mapping[index] = acummulating_bin_index;

                if (minimal_bin_value <= accumulated_value)
                {
                    accumulated_value = 0;
                    bin_mapping[index] = acummulating_bin_index;
                    acummulating_bin_index++;
                }

            }
            if (accumulated_value != 0)
            {
                ToolsCollection.ReplaceRBA(bin_mapping, acummulating_bin_index, acummulating_bin_index - 1);

            }

            int [] acummulated_histogram = new int [acummulating_bin_index];
            for (int index = 0; index < histogram.Length; index++)
            {
                acummulated_histogram[bin_mapping[index]] += histogram[index];
            }
            return new Tuple<int [], int []>(acummulated_histogram, bin_mapping);
        }

        public static int [] cumulative_sum(int [] array)
        {
            int [] cumulative_sum = new int [array.Length];
            cumulative_sum[0] = array[0];
            for (int index = 1; index < array.Length; index++)
            {
                cumulative_sum[index] = array[index] + cumulative_sum[index - 1];
            }
            return cumulative_sum;
        }

        public static int [] CropValues(int [] values, int value_at_least, int value_less_than)
        {
            int allowed_value_count = 0;
            foreach (int value in values)
            {
                if ((value_at_least <= value) && (value < value_less_than))
                {
                    allowed_value_count++;
                }
            }
            int [] result = new int [allowed_value_count];
            int index_result = 0;
            for (int index_input = 0; index_input < values.Length; index_input++)
            {
                if ((value_at_least <= values[index_input]) && (values[index_input] < value_less_than))
                {
                    result[index_result] = values[index_input];
                    index_result++;
                }
            }
            return result;
        }

        public static int [] range(int value_first, int value_count)
        {
            int [] result = new int [value_count];
            for (int index = 0; index < value_count; index++)
            {
                result[index] = index + value_first;
            }
            return result;

        }



        public static int [] select_index_of_value(int [] array, int value)
        {
            int count = ToolsCollection.CountOccurance(array, value);
            int [] indexes_with_value = new int [count];
            int index_indexes_with_value = 0;
            for (int index = 0; index < array.Length; index++)
            {

                if (array[index] == value)
                {
                    indexes_with_value[index_indexes_with_value] = index;
                    index_indexes_with_value++;
                }

            }
            return indexes_with_value;
        }

        public static void means_columns_fill(List<int []> list, int [] means)
        {
            for (int index_columns = 0; index_columns < means.Length; index_columns++)
            {
                means[index_columns] = 0;
            }
        
            for (int index_row = 0; index_row < list.Count; index_row++)
            {
                for (int index_columns = 0; index_columns < means.Length; index_columns++)
                {
                    means[index_columns] += list[index_row][index_columns];
                }
            }

            for (int index_columns = 0; index_columns < means.Length; index_columns++)
            {
                means[index_columns] /= list.Count;
            }

        }


        public static int[] CropZerosEnd(int[] array)
        {
            int index_last_non_zero = -1;
            for (int index = 0; index < array.Length; index++)
            {
                if (array[index] != 0)
                {
				    index_last_non_zero = index;
                }
            }
            return ToolsCollection.Select(array, 0, index_last_non_zero + 1);
        }

        public static ValueType[] CropValuesEnd<ValueType>(ValueType[] array, ValueType crop_value)
        {
            int index_last_non_value = -1;
            for (int index = 0; index < array.Length; index++)
            {
                if (!array[index].Equals(crop_value))
                {
                    index_last_non_value = index;
                }
            }
            return ToolsCollection.Select(array, 0, index_last_non_value + 1);
        }

        public static int[] Range(int min_inclusive, int max_exclusive)
        {
            int lenght = max_exclusive - min_inclusive;
            int [] range = new int [lenght];
            Parallel.For(0, lenght, index =>
            {
                range[index] = index + min_inclusive;
            });
            return range;
        }

        public static int[] Range(int[] sorted_orginal, int[] sorted_remove)
        {
            //TODO could be faster
            ISet<int> orginal = new HashSet<int>(sorted_orginal);
            orginal.ExceptWith(sorted_remove);
            List<int> list = new List<int>(orginal);
            list.Sort();
            return list.ToArray();
        }





        public static int[] Multiply(int[] array, int factor)
        {
            int[] result = new int[array.Length];
            for (int index = 0; index < array.Length; index++)
            {
                result[index] = array[index] * factor;
            }
            return result;
        }

        public static void AddFill(int[] array_0, int[] array_1, int[] array_destination)
        {
            System.Diagnostics.Debug.Assert(array_0.Length == array_1.Length);
            System.Diagnostics.Debug.Assert(array_0.Length == array_destination.Length);
            for (int index = 0; index < array_0.Length; index++)
            {
                array_destination[index] = array_0[index] + array_1[index];
            }
        }

        public static void DivideFill(int[] array_0, int[] array_1, int[] array_destination)
        {
            System.Diagnostics.Debug.Assert(array_0.Length == array_1.Length);
            System.Diagnostics.Debug.Assert(array_0.Length == array_destination.Length);
            for (int index = 0; index < array_0.Length; index++)
            {
                array_destination[index] = array_0[index] / array_1[index];
            }
        }
    }
}