using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace KozzionCore.Tools
{
    public class ToolsCollection
	{
        public static ArrayType[] Append<ArrayType>(
            ArrayType[] array_0,
            ArrayType[] array_1)
        {
            ArrayType[] result = new ArrayType[array_0.Length + array_1.Length];
            AppendFill(array_0, array_1, result);
            return result;
        }

        public static void AppendFill<ArrayType>(
            ArrayType[] array_0,
            ArrayType[] array_1,
            ArrayType[] result)
        {
            Array.Copy(array_0, 0, result, 0, array_0.Length);
            Array.Copy(array_1, 0, result, array_0.Length, array_1.Length);
        }
        public static string[][] Append(
            string[] toappend,
            string[][] toappendto)
        {
            int maxrowLength = Math.Max(toappend.Length, toappendto[0].Length);
            string[][] toreturn = new string[toappendto.Length + 1][];
            //  copy_into(toappendto, toreturn, 0, 0);
            //  copy_fill(toappend, toreturn, toappendto.Length, 0);
            return toreturn;
        }

        public static ArrayType[] Copy<ArrayType>(
            ArrayType[] source_array)
        {
            ArrayType[] array_destination = new ArrayType[source_array.Length];
            Array.Copy(source_array, array_destination, source_array.Length);
            return array_destination;
        }

        public static int CountOccurance<DataType>(IList<DataType> list, DataType value_to_count)
        {
            int count = 0;
            foreach (DataType value in list)
            {
                if (value.Equals(value_to_count))
                {
                    count++;
                }
            }
            return count;
        }

        public static DataType[] CreateArray<DataType>(int size, DataType value)
        {
            DataType[] array = new DataType[size];
            Parallel.For(0, size, index =>
            {
                array[index] = value;
            });
            return array;
        }

        public static bool IsStaggered<DataType>(IList<IList<DataType>> array)
        {
            int length = array[0].Count;
            for (int index = 0; index < array.Count; index++)
            {
                if (array[index].Count != length)
                {
                    return true;
                }
            }
            return false;

        }

        public static bool AreEqualSize<DataType0, DataType1>(IList<DataType0 []> list_0, IList<DataType1 []> list_1)
        {
            if (list_0.Count != list_1.Count)
            {
                return false;
            }

            if (list_0.Count == 0)
            {
                return true;
            }

            for (int index_0 = 0; index_0 < list_0.Count; index_0++)
            {
                if (list_0[index_0].Length != list_1[index_0].Length)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsStaggered<DataType>(IList<DataType []> list)
        {
            if (list.Count == 0)
            {
                return false;
            }

            int length = list[0].Length;
            for (int index = 0; index < list.Count; index++)
            {
                if (list[index].Length != length)
                {
                    return true;
                }
            }
            return false;
        }



        public static ArrayType[] Copy<ArrayType>(
             IList<ArrayType> source)
        {
            ArrayType[] destination = new ArrayType[source.Count];
            for (int index = 0; index < source.Count; index++)
            {
                destination[index] = source[index];
            }
            return destination;
        }


        public static ArrayType[,] Copy<ArrayType>(
         ArrayType[,] source_array)
        {
            ArrayType[,] array_destination = new ArrayType[source_array.GetLength(0), source_array.GetLength(1)];
            Array.Copy(source_array, array_destination, source_array.Length);
            return array_destination;
        }

        public static ArrayType[][] Copy<ArrayType>(
            ArrayType[][] array_source)
        {
            ArrayType[][] array_destination = new ArrayType[array_source.Length][];
            for (long index_row = 0; index_row < array_source.Length; index_row++)
            {
                array_destination[index_row] = Copy(array_source[index_row]);
            }
            return array_destination;
        }

        public static void CopyRBA<ArrayType>(
           IList<ArrayType> list_source,
           ArrayType[] array_target)
        {
            Parallel.For(0, list_source.Count, index =>
            {
                array_target[index] = list_source[index];
            });
        }

        public static void CopyRBA<ArrayType>(
          IList<ArrayType> list_source,
          int source_offset,
          ArrayType[] array_target,
          int array_offset)
        {
            Parallel.For(0, list_source.Count, index =>
            {
                array_target[index + array_offset] = list_source[index + source_offset];
            });
        }

        public static void CopyRBA<ArrayType>(
              IList<ArrayType> list_source,
              int source_offset,
              ArrayType[] array_target,
              int array_offset,
              int count)
        {
            Parallel.For(0, count, index =>
            {
                array_target[index + array_offset] = list_source[index + source_offset];
            });
        }

        public static void CopyRBA<ArrayType>(
            ArrayType[] array_source,
            ArrayType[] array_destination)
        {
            Array.Copy(array_source, array_destination, array_source.Length);
        }

        public static void CopyRBA<ArrayType>(
            ArrayType[,,] array_source,
            ArrayType[,,] array_destination)
        {
            Array.Copy(array_source, array_destination, array_source.Length);
        }

        public static void CopyRBA<ArrayType>(
            ArrayType[][] array_source,
            ArrayType[][] array_destination)
        {
            for (long row_index = 0; row_index < array_source.Length; row_index++)
            {
                CopyRBA(array_source[row_index], array_destination[row_index]);
            }
        }

        public static void CopyBlockRBA<ArrayType>(
            ArrayType[][] array_source,
            ArrayType[][] array_destination,
            int row_index,
            int column_index)
        {
            for (int index = row_index; index < array_source.Length; index++)
            {
                Array.Copy(array_source, 0, array_destination, column_index, array_source.Length);
            }
        }

        public static void CopyRBA<ArrayType>(
            ArrayType[] source,
            int source_index,
            ArrayType[] destination,
            int destination_index,
            int count)
        {
            Array.Copy(source, source_index, destination, destination_index, count);
        }


        public static void CopyRowRBA<ArrayType>(IList<ArrayType> source, ArrayType[,] destination, int row_index)
        {
            for (int column_index = 0; column_index < source.Count; column_index++)
            {
                destination[row_index, column_index] = source[column_index];
            }
        }

        public static void CopyRowRBA<ArrayType>(
             ArrayType[,] source,
             int source_row_index,
             ArrayType[,] destination,
             int destination_row_index)
        {
            for (int column_index = 0; column_index < source.GetLength(1); column_index++)
            {
                destination[source_row_index, column_index] = source[source_row_index, column_index];
            }
        }

        public static void CopyRowsRBA<ArrayType>(
            ArrayType[,] source,
            int source_row_index,
            ArrayType[,] destination,
            int destination_row_index,
            int count)
        {
            for (int row_index = 0; row_index < count; row_index++)
            {
                CopyRowRBA(source, source_row_index + row_index, destination, destination_row_index + row_index);
            }
        }

        public static void CopyRowsRBA<ArrayType>(
            ArrayType[][] source,
            int source_row_index,
            ArrayType[,] destination,
            int destination_row_index,
            int count)
        {
            for (int row_index = 0; row_index < count; row_index++)
            {
                CopyRowRBA(source[source_row_index + row_index], destination, destination_row_index + row_index);
            }
        }

        public static void CopyRowsRBA<ArrayType>(
           ArrayType[][] source,
           int source_row_index,
           ArrayType[][] destination,
           int destination_row_index,
           int count)
        {
            for (int row_index = 0; row_index < count; row_index++)
            {
                Array.Copy(source[source_row_index + row_index], destination[destination_row_index + row_index], source[source_row_index + row_index].Length);
            }
        }

        //public static ArrayTypeTarget[] Cast<ArrayTypeSource, ArrayTypeTarget>(IList<ArrayTypeSource> list)
        //    where ArrayTypeSource : ArrayTypeTarget
        //{
        //    ArrayTypeTarget[] target = new ArrayTypeTarget[list.Count];
        //    for (int index = 0; index < list.Count; index++)
        //    {
        //        target = (ArrayTypeTarget)list[index];
        //    }
        //    return target;
        //}

        public static IReadOnlyList<ArrayType> Select<ArrayType>(
            IReadOnlyList<ArrayType> source,
            int offset,
            int count)
        {
            ArrayType[] target = new ArrayType[count];
            Parallel.For(0, count, index =>
            {
                target[index] = source[index + offset];
            });
            return target;
        }

        public static ArrayType[] Select<ArrayType>(
            ArrayType[] array_source,
            int offset,
            int count)
        {
            ArrayType[] copy = new ArrayType[count];
            Array.Copy(array_source, offset, copy, 0, count);
            return copy;
        }

        public static ArrayType[] Select<ArrayType>(
             IList<ArrayType> source,
             int offset,
             int count)
        {
            ArrayType[] destination = new ArrayType[count];
            SelectRBA(source, destination, offset, count);
            return destination;
        }

        public static void SelectRBA<ArrayType>(
             IList<ArrayType> source,
             IList<ArrayType> destination,
             int offset,
             int count)
        {
            for (int index = 0; index < count; index++)
            {
                destination[index] = source[index + offset];
            }
        }

        public static ArrayType[] Select<ArrayType>(
            ArrayType[] array,
            HashSet<int> index_selection_set)
        {
            List<int> list = new List<int>(index_selection_set);
            list.Sort();
            return Select(array, list);
        }

        public static ArrayType[] Select<ArrayType>(
            IList<ArrayType> array,
            IList<int> selection_indexes)
        {
            ArrayType[] copy = new ArrayType[selection_indexes.Count];
            for (int index = 0; index < selection_indexes.Count; index++)
            {
                copy[index] = array[selection_indexes[index]];
            }
            return copy;
        }


        public static void SelectElementsRBA<ArrayType>(
            ArrayType[] to_fill,
            ArrayType[] array,
            int[] index_selection_array)
        {
            for (int index = 0; index < index_selection_array.Length; index++)
            {
                to_fill[index] = array[index_selection_array[index]];
            }
        }

        public static DataType[] SelectWithSelectorValue<DataType, SelectorType>(IList<DataType> base_list, IList<SelectorType> selector_list, SelectorType selector_value)
        {
            if (base_list.Count != selector_list.Count)
            {
                throw new Exception("base and selector list are not of same lenght");
            }

            int count = CountOccurance(selector_list, selector_value);
            DataType[] selection = new DataType[count];
            int selection_index = 0;
            for (int base_index = 0; base_index < base_list.Count; base_index++)
            {
                if (selector_list[base_index].Equals(selector_value))
                {
                    selection[selection_index] = base_list[base_index];
                    selection_index++;
                }
            }
            return selection;

        }

        public static ArrayType[][] SelectRows<ArrayType>(
            ArrayType[][] array_source,
            int[] index_selection_array)
        {
            ArrayType[][] array_destination = new ArrayType[index_selection_array.Length][];
            for (int index = 0; index < index_selection_array.Length; index++)
            {
                array_destination[index] = Copy(array_source[index_selection_array[index]]);
            }
            return array_destination;
        }

        public static ArrayType[] ConvertToArray1D<ArrayType>(ArrayType[,] array_source, bool column_first = false)
        {
            ArrayType[] array_destination = new ArrayType[array_source.GetLength(0) * array_source.GetLength(1)];
            int destination_index = 0;
            if (column_first)
            {
                for (int column_index = 0; column_index < array_source.GetLength(1); column_index++)
                {
                    for (int row_index = 0; row_index < array_source.GetLength(0); row_index++)
                    {

                        array_destination[destination_index] = array_source[row_index, column_index];
                        destination_index++;
                    }
                }
            }
            else
            {
                for (int row_index = 0; row_index < array_source.GetLength(0); row_index++)
                {
                    for (int column_index = 0; column_index < array_source.GetLength(1); column_index++)
                    {
                        array_destination[destination_index] = array_source[row_index, column_index];
                        destination_index++;
                    }
                }

            }
            return array_destination;
        }







        public static ArrayType[,] ConvertToArray2D<ArrayType>(IList<ArrayType> list, bool transpose = true)
        {
            ArrayType[,] array_destination = null;
            if (transpose)
            {
                array_destination = new ArrayType[list.Count, 1];
            }
            else
            {
                array_destination = new ArrayType[1, list.Count];
            }
            for (int index = 0; index < list.Count; index++)
            {
                if (transpose)
                {
                    array_destination[index, 0] = list[index];
                }
                else
                {
                    array_destination[0, index] = list[index];
                }
            }
            return array_destination;
        }

        public static ArrayType[,] ConvertToArray2D<ArrayType>(ArrayType[][] source, bool transpose = true)
        {
            ArrayType[,] destination = new ArrayType[source.Length, source[0].Length];
            for (int row_index = 0; row_index < destination.GetLength(0); row_index++)
            {
                for (int column_index = 0; column_index < destination.GetLength(1); column_index++)
                {
                    destination[row_index, column_index] = source[row_index][column_index];
                }
            }
            return destination;
        }


        public static ArrayType[,] ConvertToArray2D<ArrayType>(IList<ArrayType[]> source)
        {
            ArrayType[,] destination = new ArrayType[source.Count, source[0].Length];
            for (int row_index = 0; row_index < destination.GetLength(0); row_index++)
            {
                for (int column_index = 0; column_index < destination.GetLength(1); column_index++)
                {
                    destination[row_index, column_index] = source[row_index][column_index];
                }
            }
            return destination;
        }


        public static ArrayType[][] ConvertToArrayArray<ArrayType>(ArrayType[,] array_source)
        {
            ArrayType[][] array_destination = new ArrayType[array_source.GetLength(0)][];
            for (int row_index = 0; row_index < array_source.GetLength(0); row_index++)
            {
                array_destination[row_index] = array_source.Select1DIndex0(row_index);
            }
            return array_destination;
        }

        public static ArrayType[][] ConvertToArrayArray<ArrayType>(IList<ArrayType[]> array_source)
        {
            ArrayType[][] array_destination = new ArrayType[array_source.Count][];
            for (int row_index = 0; row_index < array_source.Count; row_index++)
            {
                array_destination[row_index] = Copy(array_source[row_index]);
            }
            return array_destination;
        }


        public static ArrayType[] SelectColumn<ArrayType>(
            ArrayType[][] array_source,
            long index_selected_column)
        {
            ArrayType[] array_destination = new ArrayType[array_source.Length];
            for (int index_row = 0; index_row < array_source.Length; index_row++)
            {
                array_destination[index_row] = array_source[index_row][index_selected_column];
            }
            return array_destination;
        }



        public static ArrayType[][] SelectColumns<ArrayType>(
            ArrayType[][] array,
            IList<int> index_selection_array)
        {
            ArrayType[][] copy = new ArrayType[array.Length][];
            for (int index_row = 0; index_row < index_selection_array.Count; index_row++)
            {
                copy[index_row] = new ArrayType[index_selection_array.Count];
                for (int index_column = 0; index_column < index_selection_array.Count; index_column++)
                {
                    copy[index_row][index_column] = array[index_row][index_selection_array[index_column]];
                }
            }
            return copy;
        }

        public static ArrayType[] select_indexes_with_values<ArrayType>(ArrayType[] array_0, ArrayType value)
        {
            int selected_index_count = 0;
            int[] selected_indexes = new int[array_0.Length];
            for (int index = 0; index < array_0.Length; index++)
            {
                if (array_0[index].Equals(value))
                {
                    selected_indexes[selected_index_count] = index;
                    selected_index_count++;
                }
            }
            return Select(array_0, 0, selected_index_count);
        }

        public static void set_column<ArrayType>(
            ArrayType[][] array,
            int index_column,
            ArrayType[] column)
        {
            for (int index_row = 0; index_row < array.Length; index_row++)
            {
                array[index_row][index_column] = column[index_row];
            }
        }

        public static ArrayType[][] ConvertToArrayArray<ArrayType>(
            ArrayType[] array,
            int count_row,
            int count_column)
        {
            ArrayType[][] result = new ArrayType[count_row][];
            for (int index_row = 0; index_row < count_row; index_row++)
            {
                result[index_row] = new ArrayType[count_column];
                for (int index_column = 0; index_column < count_column; index_column++)
                {
                    result[index_row][index_column] = array[(index_row * count_column) + index_column];
                }
            }
            return result;
        }



        public static ArrayType[,] Transpose<ArrayType>(
            ArrayType[,] array)
        {
            int array_count_row = array.GetLength(0);
            int array_count_column = array.GetLength(1);
            ArrayType[,] transpose = new ArrayType[array_count_row, array_count_column];
            Parallel.For(0, array_count_row, index_row =>
            {
                for (int index_column = 0; index_column < array_count_column; index_column++)
                {
                    transpose[index_column, index_row] = array[index_row, index_column];
                }
            });
            return transpose;
        }

        public static ArrayType[][] Transpose<ArrayType>(
            ArrayType[][] array)
        {
            int count_row = array.Length;
            int count_column = array[0].Length;
            ArrayType[][] transpose = new ArrayType[count_column][];
            for (int index_column = 0; index_column < count_column; index_column++)
            {
                transpose[index_column] = new ArrayType[count_row];
            }
            for (int index_row = 0; index_row < count_row; index_row++)
            {

                for (int index_column = 0; index_column < count_column; index_column++)
                {
                    transpose[index_column][index_row] = array[index_row][index_column];
                }
            }
            return transpose;
        }

        public static bool EqualsArray<ArrayType>(
            IList<ArrayType> array_0,
            IList<ArrayType> array_1)
        {
            if (array_0.Count != array_1.Count)
            {
                return false;
            }
            for (int index = 0; index < array_0.Count; index++)
            {
                if (!array_0[index].Equals(array_1[index]))
                {
                    return false;
                }
            }
            return true;
        }

        public static void ReplaceRBA<ArrayType>(
            ArrayType[] array,
            ArrayType value_to_replace,
            ArrayType value_to_with)
        {
            for (int index = 0; index < array.Length; index++)
            {
                if (array[index].Equals(value_to_replace))
                {
                    array[index] = value_to_with;
                }
            }

        }


        public static string[,] ConvertToStringArray<DataType>(DataType[,] array)
        {
            string[,] copy = new string[array.GetLength(0), array.GetLength(1)];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    copy[index_0, index_1] = array[index_0, index_1].ToString();
                }
            }
            return copy;
        }



        public static string[] ConvertToStringArray<DataType>(IList<DataType> list)
        {
            string[] copy = new string[list.Count];
            for (int index = 0; index < list.Count; index++)
            {
                copy[index] = list[index].ToString();
            }
            return copy;
        }

        public static float[] ConvertToFloatArray(
            int[] array)
        {
            float[] copy = new float[array.Length];
            for (int index = 0; index < array.Length; index++)
            {
                copy[index] = (float)array[index];
            }
            return copy;
        }


        public static float[] ConvertToFloatArray(
            double[] array)
        {
            float[] copy = new float[array.Length];
            for (int index = 0; index < array.Length; index++)
            {
                copy[index] = (float)array[index];
            }
            return copy;
        }

        public static float[] ConvertToFloatArray(
            List<float> list)
        {
            float[] copy = new float[list.Count];
            for (int index = 0; index < list.Count; index++)
            {
                copy[index] = list[index];
            }
            return copy;
        }

        public static long[] ConvertToLongArray(
            List<long> list)
        {
            long[] copy = new long[list.Count];
            for (int index = 0; index < list.Count; index++)
            {
                copy[index] = list[index];
            }
            return copy;
        }

        public static float[][] ConvertToFloatArray(
            String[][] array)
        {
            float[][] copy = new float[array.Length][];
            for (int index_0 = 0; index_0 < array.Length; index_0++)
            {
                for (int index_1 = 0; index_1 < array[0].Length; index_1++)
                {
                    copy[index_0][index_1] = float.Parse(array[index_0][index_1]);
                }
            }
            return copy;
        }

        public static double[] ConvertToDoubleArray(IList<int> list_0)
        {
            double[] copy = new double[list_0.Count];
            for (int index = 0; index < list_0.Count; index++)
            {
                copy[index] = list_0[index];
            }
            return copy;
        }


        public static double[] ConvertToDoubleArray(IList<float> list_0)
        {
            double[] copy = new double[list_0.Count];
            for (int index = 0; index < list_0.Count; index++)
            {
                copy[index] = list_0[index];
            }
            return copy;
        }

        public static double[] ConvertToDoubleArray(IList<double> list_0)
        {
            double[] copy = new double[list_0.Count];
            for (int index = 0; index < list_0.Count; index++)
            {
                copy[index] = list_0[index];
            }
            return copy;
        }

        public static double[][] ConvertToDoubleArray(
            float[][] array)
        {
            double[][] copy = new double[array.Length][];
            for (int index_0 = 0; index_0 < array.Length; index_0++)
            {
                copy[index_0] = new double[array[index_0].Length];
                for (int index_1 = 0; index_1 < array[0].Length; index_1++)
                {
                    copy[index_0][index_1] = array[index_0][index_1];
                }
            }
            return copy;
        }

        public static double[,] ConvertToDoubleArray(float[,] array)
        {
            double[,] copy = new double[array.GetLength(0), array.GetLength(1)];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    copy[index_0, index_1] = array[index_0, index_1];
                }
            }
            return copy;
        }

        public static bool ContainsIndex(
            int[][] array,
            int index_0,
            int index_1)
        {
            if ((index_0 < 0) || (index_1 < 0) || (array.Length <= index_0) || (array[index_1].Length <= index_0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static ArrayType[] Crop<ArrayType>(
            IList<ArrayType> array,
            int crop_size)
        {
            ArrayType[] cropped_array = new ArrayType[crop_size];
            for (int index = 0; index < crop_size; index++)
            {
                cropped_array[index] = array[index];
            }
            return cropped_array;
        }

        public static ArrayType[] Crop<ArrayType>(
            ArrayType[] array,
            int crop_size)
        {
            ArrayType[] cropped_array = new ArrayType[crop_size];
            Array.Copy(array, 0, cropped_array, 0, crop_size);
            return cropped_array;
        }

        public static ArrayType[][] Crop<ArrayType>(
            ArrayType[][] array,
            int crop_size_0,
            int crop_size_1)
        {
            ArrayType[][] cropped_array = new ArrayType[crop_size_0][];
            for (int index_0 = 0; index_0 < crop_size_0; index_0++)
            {
                Array.Copy(array[index_0], 0, cropped_array[index_0], 0, crop_size_1);
            }
            return cropped_array;
        }

        public static ArrayType[,] Crop<ArrayType>(
            ArrayType[,] array,
            int crop_size_0,
            int crop_size_1)
        {
            ArrayType[,] cropped_array = new ArrayType[crop_size_0, crop_size_1];
            for (int index_0 = 0; index_0 < crop_size_0; index_0++)
            {
                for (int index_1 = 0; index_1 < crop_size_1; index_1++)
                {
                    cropped_array[index_0, index_1] = array[index_0, index_1];
                }
            }
            return cropped_array;
        }

        public static int[] delete_index(
            int[] array,
            int delete_index)
        {
            int[] copy = new int[array.Length - 1];
            Array.Copy(array, 0, copy, 0, delete_index);
            Array.Copy(array, delete_index + 1, copy, delete_index, array.Length - (delete_index + 1));
            return copy;
        }



        public static byte[] shift(
            byte[] array,
            int shift_right_amount)
        {
            byte[] result = new byte[array.Length];
            for (int index = 0; index < array.Length; index++)
            {
                array[index] = array[(index - shift_right_amount + array.Length) % array.Length];
            }
            return result;
        }

        public static void shift_inplace(
            byte[] array,
            int shift_right_amount)
        {
            byte temp_value = array[0];
            int target_index = 0;
            int source_index = (target_index - shift_right_amount + array.Length) % array.Length;
            for (int index = 1; index < array.Length; index++)
            {
                array[target_index] = array[source_index];
                target_index = source_index;
                source_index = (target_index - shift_right_amount + array.Length) % array.Length;
            }

            array[target_index] = temp_value;
        }

        public static void xor(
            byte[] array_0,
            byte[] array_1,
            byte[] result)
        {
            for (int index = 0; index < array_0.Length; index++)
            {
                result[index] = ToolsBinary.xor(array_0[index], array_1[index]);
            }

        }

        public static string ToString<ArrayType>(IList<ArrayType> list, bool transpose = false)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            if (0 < list.Count)
            {
                builder.Append(list[0].ToString());
                for (int index = 1; index < list.Count; index++)
                {
                    if (transpose)
                    {
                        builder.AppendLine(", " + list[index]);
                    }
                    else
                    {
                        builder.Append(", " + list[index]);
                    }
                }


            }
            builder.Append("}");
            return builder.ToString();
        }

        public static string ToStringArray<ArrayType>(IList<ArrayType[]> list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            if (0 < list.Count)
            {
                builder.Append(ToString(list[0]));
                for (int index = 1; index < list.Count; index++)
                {
                    builder.Append(", " + ToString(list[index]));        
                }
            }
            builder.Append("}");
            return builder.ToString();
        }


        public static string ToString<ArrayType>(ArrayType[,] array)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{");
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                builder.Append("{");
                builder.Append(array[index_0, 0]);
                for (int index_1 = 1; index_1 < array.GetLength(1); index_1++)
                {
                    builder.Append(", " + array[index_0, index_1]);
                }
                builder.AppendLine("}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static void print<ArrayType>(ArrayType[] array)
        {
            foreach (ArrayType element in array)
            {
                System.Diagnostics.Debug.WriteLine(element);
            }
        }

 



        public static DataType[][] CreateArrayArray<DataType>(int size_0, int size_1)
        {
            DataType[][] copy = new DataType[size_0][];
            for (int index = 0; index < size_0; index++)
            {
                copy[index] = new DataType[size_1];
            }
            return copy;
        }



        public static ArrayType[,] SelectRowsAndColumns<ArrayType>(ArrayType[,] data, IList<int> row_indexes, IList<int> column_indexes)
        {
            ArrayType[,] selected = new ArrayType[row_indexes.Count, column_indexes.Count];
            for (int row_index = 0; row_index < row_indexes.Count; row_index++)
            {
                for (int column_index = 0; column_index < column_indexes.Count; column_index++)
                {
                    selected[row_index, column_index] = data[row_indexes[row_index], column_indexes[column_index]];
                }
            }
            return selected;
        }



        public static ArrayType[][] SelectRowsAndColumns<ArrayType>(ArrayType[][] source, IList<int> row_indexes, IList<int> column_indexes)
        {
            ArrayType[][] destination = new ArrayType[row_indexes.Count][];
            for (int row_index = 0; row_index < row_indexes.Count; row_index++)
            {
                destination[row_index] = Select(source[row_indexes[row_index]], column_indexes);
            }
            return destination;
        }

        public static IList<ArrayType>[] SelectRowsAndColumns<ArrayType>(IList<ArrayType>[] source, IList<int> row_indexes, IList<int> column_indexes)
        {
            ArrayType[][] destination = new ArrayType[row_indexes.Count][];
            for (int row_index = 0; row_index < row_indexes.Count; row_index++)
            {
                destination[row_index] = Select(source[row_indexes[row_index]], column_indexes);
            }
            return destination;
        }

        public static string[][] ConvertDictionaryToStringArray<Type1, Type2>(IDictionary<Type1, Type2> data)
		{
			return data.Select(x => new string[]{x.Key.ToString(), x.Value.ToString()}).ToArray();
		}

        public static ArrayType[] ConvertToArray1D<ArrayType>(IList<ArrayType> array_source)
        {
            ArrayType[] array_destination = new ArrayType[array_source.Count];
            for (int index = 0; index < array_source.Count; index++)
            {
                array_destination[index] = array_source[index];
            }
            return array_destination;
        }

        public static ArrayType[] ConvertToArray1D<ArrayType>(
            IList<IList<ArrayType>> list_list)
        {
            return ConvertToArray1D(list_list, CountElements(list_list));
        }

        public static ArrayType[] ConvertToArray1D<ArrayType>(
           IList<IList<ArrayType>> list_list,
           long size)
        {
            ArrayType[] result = new ArrayType[size];
            int index_result = 0;
            for (int index_row = 0; index_row < list_list.Count; index_row++)
            {
                for (int index_column = 0; index_column < list_list[index_row].Count; index_column++)
                {
                    result[index_result] = list_list[index_row][index_column];
                    index_result++;
                }
            }
            return result;
        }

        public static ArrayType[] ConvertToArray1D<ArrayType>(
            ArrayType[][] array)
        {
            return ConvertToArray1D(array, CountElements(array));
        }

        public static ArrayType[] ConvertToArray1D<ArrayType>(
            ArrayType[][] array,
            long size)
        {
            ArrayType[] result = new ArrayType[size];
            int index_result = 0;
            for (int index_row = 0; index_row < array.Length; index_row++)
            {
                for (int index_column = 0; index_column < array[index_row].Length; index_column++)
                {
                    result[index_result] = array[index_row][index_column];
                    index_result++;
                }
            }
            return result;
        }

        public static string[][] ConvertToStringArray(IList<IList<string>> data)
		{
			return data.Select(x => x.ToArray()).ToArray();
		}

        public static TableType[,] ConvertToTable<TableType>(IList<TableType[]> data)
        {

            int count_columns = data[0].Length;
            TableType[,] table = new TableType[data.Count, count_columns];
            Parallel.For(0, data.Count, index_row =>
            {
                for (int index_column = 0; index_column < data[0].Length; index_column++)
                {
                    table[index_row, index_column] = data[index_row][index_column];
                }
            });
            return table;
        }


        public static TableType[,] ConvertToTable<TableType>(IList<IList<TableType>> data)
        {
            
			int count_columns = data[0].Count;
			TableType[,] table = new TableType[data.Count, count_columns];
            Parallel.For(0, data.Count, index_row =>
            {
                for (int index_column = 0; index_column < data[0].Count; index_column++)
                {
                    table[index_row, index_column] = data[index_row][index_column];
                }
            });
            return table;
        }

        public static List<ElementType> Crop<ElementType>(IList<ElementType> source, int from, int to)
        {
            List<ElementType> destination = new List<ElementType>();
            for (int index = from; index < to; index++)
            {
                destination.Add(source[index]);
            }
            return destination;
        }



        public static ValueType[] GetValueArray<KeyType, ValueType>(IDictionary<KeyType, ValueType> dictionary, KeyType[] keys)
        {
            ValueType[] values = new ValueType[keys.Length];
            for (int index = 0; index < keys.Length; index++)
            {
                values[index] = dictionary[keys[index]];          
            }
            return values;
        }

        public static KeyType [] ConvertToArray1D<KeyType>(IDictionary<KeyType, int> dictionary)
        {
            int max_index = 0;
            foreach (int index in dictionary.Values)
            {
                max_index = Math.Max(max_index, index);

            }
            KeyType[] array = new KeyType[max_index + 1];
            foreach (KeyType key in dictionary.Keys)
            {
                array[dictionary[key]] = key;
            }
            return array;
        }


        public static long CountElements<ArrayType>(
            IList<IList<ArrayType>> array)
        {
            int count = 0;
            for (int index_row = 0; index_row < array.Count; index_row++)
            {
                count += array[index_row].Count;
            }
            return count;
        }

        public static IList<RealType> Sort<RealType>(IList<RealType> list, IComparer<RealType> comparer)
        {
            IEnumerable<RealType> sortedEnum = list.OrderBy(f => f, comparer);
            return sortedEnum.ToList();
        }

        public static IList<RealType> Sort<RealType>(IList<RealType> list)
        {
            IEnumerable<RealType> sortedEnum = list.OrderBy(f => f);
            return sortedEnum.ToList();
        }

        public static Dictionary<ListType, int> ConvertToDictionary<ListType>(IList<ListType> list)
        {
            Dictionary<ListType, int> dictionary = new Dictionary<ListType, int>();

            for (int index = 0; index < list.Count; index++)
            {
                if (dictionary.ContainsKey(list[index]))
                {
                    throw new Exception("Duplicate list entry: " + list[index] + " at index " + index);
                }
                dictionary[list[index]] = index;
            }
            return dictionary;
        }

        /*
public static String [,] convert_to_table(List<String []> splitlines, int maxsize)
{
    String [, ] toreturn = new String [splitlines.Count, maxsize];
    for (int rowindex = 0; rowindex < splitlines.Count; rowindex++)
    {
        for (int collumindex = 0; collumindex < maxsize; collumindex++)
        {
            if (splitlines.get(rowindex).length > collumindex)
            {
                toreturn[rowindex, collumindex] = splitlines.get(rowindex)[collumindex];
            }
        }
    }
    return toreturn;
}

public static Dictionary<String, String> convert_to_string_string_Dictionary(String [][] array)
{
    // assumes 2 values per row
    Dictionary<String, String> Dictionary = new HashDictionary<String, String>();
    for (String [] element : array)
    {
        Dictionary.put(element[0], element[1]);
    }
    return Dictionary;
}

public static Dictionary<string, int> convert_to_string_integer_Dictionary(string [][] array)
{
    // assumes 2 values per row
    Dictionary<String, int> Dictionary = new Dictionary<String, int>();
    for (String [] element : array)
    {
        Dictionary.put(element[0], Integer.parseInt(element[1]));
    }
    return Dictionary;
}



public static <Type> List<Type> convert_to_list(Type [] array)
{
    List<Type> list = new List<Type>();
    for (Type element : array)
    {
        list.add(element);
    }
    return list;
}

public static List<String> convert_to_list(String [] array)
{
    List<String> list = new List<String>();
    for (String element : array)
    {
        list.add(element);
    }
    return list;
}

public static List<Integer> convert_to_list(int [] array)
{
    List<Integer> list = new List<Integer>();
    for (int element : array)
    {
        list.add(element);
    }
    return list;
}

public static List<Float> convert_to_list(float [] array)
{
    List<Float> list = new List<Float>();
    for (float element : array)
    {
        list.add(element);
    }
    return list;
}

public static <Type> Set<Type> convert_to_set(Type [] array)
{
    Set<Type> set = new HashSet<>();
    for (Type object : array)
    {
        set.add(object);
    }
    return set;
}

public static <Type1, Type2> Tuple2<Type1 [], Type2 []> convert_to_pair_of_arrays(Collection<Tuple2<Type1, Type2>> collection)
{
    Tuple2<List<Type1>, List<Type2>> pair_of_lists = convert_to_pair_of_lists(collection);
    return new Tuple2<Type1 [], Type2 []>(convert_to_array(pair_of_lists.get_object1()), convert_to_array(pair_of_lists.get_object2()));
}

public static <Type1, Type2> Tuple2<List<Type1>, List<Type2>> convert_to_pair_of_lists(Collection<Tuple2<Type1, Type2>> collection)
{
    List<Type1> list1 = new List<>();
    List<Type2> list2 = new List<>();
    for (Tuple2<Type1, Type2> tuple : collection)
    {
        list1.add(tuple.get_object1());
        list2.add(tuple.get_object2());
    }
    return new Tuple2<List<Type1>, List<Type2>>(list1, list2);
}

public static <Type1, Type2> Tuple2<Type1 [], Type2 []> convert_to_pair_of_arrays(Dictionary<Type1, Type2> Dictionary)
{
    Tuple2<List<Type1>, List<Type2>> pair_of_lists = convert_to_pair_of_lists(Dictionary);
    return new Tuple2<Type1 [], Type2 []>(convert_to_array(pair_of_lists.get_object1()), convert_to_array(pair_of_lists.get_object2()));
}

public static <Type1, Type2> Tuple2<List<Type1>, List<Type2>> convert_to_pair_of_lists(Dictionary<Type1, Type2> Dictionary)
{
    List<Type1> key_list = new List<Type1>(Dictionary.keySet());
    List<Type2> value_list = new List<Type2>();
    for (Type1 key : key_list)
    {
        value_list.add(Dictionary.get(key));
    }
    return new Tuple2<List<Type1>, List<Type2>>(key_list, value_list);

}

public static <Key, Value> List<Value> convert_values_to_list(Dictionary<Key, Value> Dictionary)
{
    List<Value> values = new List<Value>();
    List<Key> keys = convert_keys_to_list(Dictionary);
    for (Key key : keys)
    {
        values.add(Dictionary.get(key));
    }
    return values;
}

public static <Key, Value> List<Key> convert_keys_to_list(Dictionary<Key, Value> Dictionary)
{
    return convert_to_list(Dictionary.keySet());
}

public static <Type> List<Type> select(List<Type> list, int [] selected_indexes)
{
    Arrays.sort(selected_indexes);
    List<Type> selected_list = new List<>();
    for (int selected_index : selected_indexes)
    {
        selected_list.add(list.get(selected_index));
    }
    return selected_list;
}

public static void print(Dictionary<String, String> Dictionary)
{
    print(convert_Dictionary_string_string_to_string_array(Dictionary));
}

public static <Type extends Object> void print(Iterable<Type> iterable)
{
    System.out.print("{");
    int size = size(iterable);
    int index = 0;
    for (Type element : iterable)
    {
        System.out.print(element.toString());
        index++;
        if (index < size)
        {
            System.out.print(", ");
        }
    }
    System.out.println("}");

}

public static <Type extends Object> void print(Type [] array)
{
    for (Type element : array)
    {
        System.out.println(element.toString());
    }
}

public static <Type extends Object> void print(Type [][] array)
{
    for (Type [] row : array)
    {
        for (Type element : row)
        {
            System.out.print(element.toString() + "   ");
        }
        System.out.println();
    }
}


public static <Type> List<Type> select(List<Type> source_list, Set<Integer> index_selection_set)
{
    List<Integer> sorted_unique_index_selection_list = new List<Integer>(index_selection_set);
    Collections.sort(sorted_unique_index_selection_list);
    return select(source_list, sorted_unique_index_selection_list);
}

public static <Type> List<Type> select(List<Type> source_list, List<Integer> sorted_unique_index_selection_list)
{
    List<Type> selection_list = new List<Type>();
    for (int selection_index = 0; selection_index < sorted_unique_index_selection_list.size(); selection_index++)
    {
        selection_list.add(source_list.get(sorted_unique_index_selection_list.get(selection_index)));
    }
    return selection_list;
}

private static int get_max_size(List<List<String>> lines)
{
    int max_size = 0;
    for (List<String> list : lines)
    {
        max_size = Math.max(max_size, list.size());
    }
    return max_size;
}

public static String [][] filterOnCollum(String [][] data, int collumindex, String filter)
{
    if (data.length == 0)
    {
        return data;
    }
    else
    {
        int size = data[0].length;
        List<String []> listdata = convert_to_list(data);

        int index = 0;
        while (index < listdata.size())
        {
            if (listdata.get(index)[collumindex] == null)
            {
                listdata.remove(index);
            }
            else
            {
                if (listdata.get(index)[collumindex].equals(filter))
                {
                    index++;
                }
                else
                {
                    listdata.remove(index);
                }
            }
        }
        return convert_to_table(listdata, size);
    }
}

public static String [] selectCollum(int collumindex, String [][] array)
{
    String [] collum = new String [array.length];
    for (int index = 0; index < array.length; index++)
    {
        collum[index] = array[index][collumindex];
    }
    return collum;
}

public static int compare(byte [] array1, byte [] array2)
{
    if (array1.length < array2.length)
    {
        return 1;
    }
    if (array1.length > array2.length)
    {
        return -1;
    }

    for (int index = 0; index < array1.length; index++)
    {
        if (array1[index] < array2[index])
        {
            return 1;
        }
        if (array1[index] > array2[index])
        {
            return -1;
        }
    }
    return 0;
}

public static boolean equals(byte [] array1, byte [] array2)
{
    if (array1.length != array2.length)
    {
        return false;
    }

    for (int index = 0; index < array1.length; index++)
    {
        if (array1[index] != array2[index])
        {
            return false;
        }
    }
    return true;
}

public static <Type extends Object> int size(Iterable<Type> iterable)
{
    int size = 0;
    for (@SuppressWarnings("unused")
    Type element : iterable)
    {
        size++;
    }
    return size;

}

public static List<byte []> split(byte [] data, int blocksize)
{
    List<byte []> toreturn = new List<byte []>();
    ByteBuffer buffer = ByteBuffer.allocate(data.length);
    buffer.put(data);
    buffer.rewind();
    while (buffer.remaining() >= blocksize)
    {
        byte [] newarray = new byte [blocksize];
        buffer.get(newarray);
        toreturn.add(newarray);
    }
    if (buffer.remaining() == 0)
    {
        return toreturn;
    }
    else
    {
        byte [] newarray = new byte [buffer.remaining()];
        buffer.get(newarray);
        toreturn.add(newarray);
        return toreturn;
    }
}

public static byte [] slice(byte [] array, int i, int j)
{
    byte [] slice = new byte [j - i];
    for (int index = 0; index < (j - i); index++)
    {
        slice[index] = array[index + i];
    }
    return slice;
}

public static Tuple2<List<Double>, List<Double>> split(List<Tuple2<Double, Double>> unsplit)
{
    Tuple2<List<Double>, List<Double>> split = new Tuple2<List<Double>, List<Double>>(new List<Double>(),
        new List<Double>());
    for (Tuple2<Double, Double> unsplit_pair : unsplit)
    {
        split.get_object1().add(unsplit_pair.get_object1());
        split.get_object2().add(unsplit_pair.get_object2());
    }
    return split;
}

public static byte [] merge(List<byte []> list_of_arrays)
{
    int totallength = 0;
    for (int index = 0; index < list_of_arrays.size(); index++)
    {
        totallength += list_of_arrays.get(index).length;
    }
    ByteBuffer buffer = ByteBuffer.allocate(totallength);
    for (int index = 0; index < list_of_arrays.size(); index++)
    {
        buffer.put(list_of_arrays.get(index));
    }
    buffer.rewind();
    return buffer.array();
}

public static List<String> remove_duplicates(List<String> list)
{
    return convert_to_list(new HashSet<String>(list));
}

public static <Type> List<List<Type>> invert_lists(List<List<Type>> list)
{
    List<List<Type>> inverted_list = new List<List<Type>>();

    for (int inner_index = 0; inner_index < list.get(0).size(); inner_index++)
    {
        inverted_list.add(new List<Type>());
    }

    for (int outer_index = 0; outer_index < list.size(); outer_index++)
    {
        for (int inner_index = 0; inner_index < list.get(0).size(); inner_index++)
        {
            inverted_list.get(inner_index).add(list.get(outer_index).get(inner_index));
        }
    }

    return inverted_list;
}

public static <KeyType, ValuesType> Dictionary<KeyType, ValuesType> merge_Dictionarys(Dictionary<KeyType, ValuesType> Dictionary_1, Dictionary<KeyType, ValuesType> Dictionary_2,
    ValuesType conflict_value)
{
    Dictionary<KeyType, ValuesType> merged_Dictionary = new HashDictionary<>(Dictionary_1);
    for (KeyType key : Dictionary_2.keySet())
    {
        if (!merged_Dictionary.containsKey(key))
        {
            merged_Dictionary.put(key, Dictionary_2.get(key));
        }
        else
        {
            if (merged_Dictionary.get(key) == null)
            {
                merged_Dictionary.put(key, Dictionary_2.get(key));
            }
            else
            {
                if (!merged_Dictionary.get(key).equals(Dictionary_2.get(key)))
                    ;
                {
                    merged_Dictionary.put(key, conflict_value);
                }
            }
        }
    }
    return merged_Dictionary;
}

public static <Type> List<List<Type>> group(List<Type> instances, int [] group_assignment, int group_count)
{
    List<List<Type>> grouped_instances = new List<>();
    for (int group_index = 0; group_index < group_count; group_index++)
    {
        grouped_instances.add(new List<Type>());

    }
    for (int instance_index = 0; instance_index < instances.size(); instance_index++)
    {
        int group_index = group_assignment[instance_index];
        grouped_instances.get(group_index).add(instances.get(instance_index));
    }
    return grouped_instances;
}
*/

        public static IList<ElementType> ConvertToList<ElementType>(ElementType element)
        {
            List<ElementType> list = new List<ElementType>();
            list.Add(element);
            return list;
        }

        public static IList<ElementType> ConvertToSortedList<ElementType>(ISet<ElementType> element)
        {
            List<ElementType> list = element.ToList();
            list.Sort();
            return list;
        }


        public static List<ElementType> SelectList<ElementType>(IList<ElementType> list, IList<int> index_selection_list)
        {
            List<ElementType> selected = new List<ElementType>();    
            for (int index = 0; index < index_selection_list.Count; index++)
            {
                selected.Add( list[index_selection_list[index]]);;
            }
            return selected;
        }

        public static List<ElementType> Select<ElementType>(IList<ElementType> list, ISet<int> index_selection_set)
        {
            List<int> index_selection_list = new List<int>(index_selection_set);
            index_selection_list.Sort();
            return SelectList(list, index_selection_list);
        }


        public static void SetValue<ElementType>(IList<ElementType> list, ElementType value)
        {
            Parallel.For(0, list.Count, index => 
            {
                list[index] = value;
            });

        }

  

        public static void SetValue<ElementType>(ElementType[,] array, ElementType value)
        {
            throw new NotImplementedException();
        }

        public static void SetValue<ElementType>(ElementType[,,] array, ElementType value)
        {
            throw new NotImplementedException();
        }

        public static void SetValues<ElementType>(ElementType[,,,] array, ElementType value)
        {
            throw new NotImplementedException();
        }

        public static void SetValue<ElementType>(ElementType[,,,,] array, ElementType value)
        {
            throw new NotImplementedException();
        }
    }
}