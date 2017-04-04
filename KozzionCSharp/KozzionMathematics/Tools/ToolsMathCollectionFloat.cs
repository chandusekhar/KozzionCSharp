using KozzionCore.Collections;
using KozzionCore.Tools;
using System;
using System.Collections.Generic;
namespace KozzionMathematics.Tools
{
	public class ToolsMathCollectionFloat
	{
        public static float[] Multply(
           IList<float> array_0,
           IList<float> array_1)
        {
            float[] copy = new float[array_0.Count];
            for (int index = 0; index < array_0.Count; index++)
            {
                copy[index] = array_0[index] * array_1[index];
            }
            return copy;
        }

        public static float[] Multply(
              IList<float> array_0,
              float value_0)
        {
            float[] copy = new float[array_0.Count];
            for (int index = 0; index < array_0.Count; index++)
            {
                copy[index] = array_0[index] * value_0;
            }
            return copy;
        }

        public static void AbsRBA(
			float [] array)
		{
			for (int index_row = 0; index_row < array.Length; index_row++)
			{
				array[index_row] = Math.Abs(array[index_row]);
			}
		}

		public static float [] Add(
			float [] array_0,
			float [] array_1)
		{
			float [] result = new float [array_0.Length];
			Add(array_0, array_1, result);
			return result;
		}

		public static void Add(
			float [] array_0,
			float [] array_1,
			float [] result)
		{
			for (int index = 0; index < result.Length; index++)
			{
				result[index] = array_0[index] + array_1[index];
			}
		}

		

		// //cov(xi, xj) = mean(xi.*xj) - mean(xi)*mean(xj);
		public static float covariance(
			float [] array_0,
			float [] array_1)
		{
            float mean_0 = ToolsMathStatistics.Mean(array_0);
            float mean_1 = ToolsMathStatistics.Mean(array_1);
			float mean_product = MeanProduct(array_0, array_1);
			return mean_product - (mean_0 * mean_1);

		}

		public static float covariance(
			float [] array_0,
			float [] array_1,
			float mean_0,
			float mean_1)
		{
            return MeanProduct(array_0, array_1) - (mean_0 * mean_1);
		}

		public static float [,] covariance(
			float [][] data)
		{
			data = ToolsCollection.Transpose(data);
			int column_count = data.Length;
			float [,] covariance_array = new float [column_count, column_count];
			float [] means = new float [column_count];

			for (int index_column = 0; index_column < column_count; index_column++)
			{
                means[index_column] = ToolsMathStatistics.Mean(data[index_column]);
			}

			for (int index_column = 0; index_column < column_count; index_column++)
			{
				covariance_array[index_column, index_column] = ToolsMathCollectionFloat
					.covariance(data[index_column], data[index_column], means[index_column], means[index_column]);
			}

			for (int index_row = 0; index_row < column_count; index_row++)
			{
				for (int index_column = index_row + 1; index_column < column_count; index_column++)
				{
					float covariance = ToolsMathCollectionFloat
						.covariance(data[index_row], data[index_column], means[index_row], means[index_column]);
					covariance_array[index_row, index_column] = covariance;
					covariance_array[index_column, index_row] = covariance;
				}

			}
			return covariance_array;
		}

		public static void divide(
			float [] array_0,
			float [] result,
			float value)
		{
			for (int index = 0; index < result.Length; index++)
			{
				result[index] = array_0[index] / value;
			}
		}

		public static void divide_in_place(
			float [] array_0,
			float value)
		{
			for (int index = 0; index < array_0.Length; index++)
			{
				array_0[index] = array_0[index] / value;
			}
		}

		private static float [] flatten(
			float [][] array)
		{
			float [] flat = new float [value_count(array)];
			int index = 0;
			foreach (float [] inner in array)
			{
                foreach (float value in inner)
				{
					flat[index] = value;
					index++;
				}
			}
			return flat;
		}

		public static int get_index_first_value_larger(
			float [] array,
			float value)
		{
			for (int index = 0; index < array.Length; index++)
			{
				if (value < array[index])
				{
					return index;
				}
			}
			return -1;
		}

		public static int get_index_last_value_smaller(
			float [] array,
			float value)
		{
			for (int index = 0; index < array.Length; index++)
			{
				if (value < array[index])
				{
					return index - 1;
				}
			}
			return array.Length - 1;
		}

		public static float get_minimal_absolute_difference(
			float [] array)
		{
			float minimal_difference = Single.MaxValue;
			for (int index = 0; index < (array.Length - 1); index++)
			{
				float difference = Math.Abs(array[index] - array[index + 1]);
				if (difference < minimal_difference)
				{
					minimal_difference = difference;
				}
			}
			return minimal_difference;
		}

		public static float intergrate_by_trapezoid(
			 float [] sample_times,
			float [] values)
		{

			float result = 0;
			for (int interval_index = 0; interval_index < (sample_times.Length - 1); interval_index++)
			{
				result += (values[interval_index] + values[interval_index + 1])
					* (sample_times[interval_index + 1] - sample_times[interval_index]) * 0.5f;

			}
			return result;

		}


	
    
        private static float MeanProduct(
			float [] array_0,
			float [] array_1)
		{
			float mean = 0.0f;
			for (int index = 0; index < array_0.Length; index++)
			{
				mean += array_0[index] * array_1[index];
			}
			return mean / array_0.Length;
		}

		public static float [] means_columns(
			float [][] array)
		{
			float [] means = new float [array[0].Length];
			for (int index_row = 0; index_row < array.Length; index_row++)
			{
				for (int index_columns = 0; index_columns < means.Length; index_columns++)
				{
					means[index_columns] += array[index_row][index_columns];
				}
			}

			for (int index_columns = 0; index_columns < means.Length; index_columns++)
			{
				means[index_columns] /= array.Length;
			}

			return means;
		}

        public static float[] means_columns(
        float[,] array)
        {
            float[] means = new float[array.GetLength(1)];
            for (int index_row = 0; index_row < array.Length; index_row++)
            {
                for (int index_columns = 0; index_columns < means.Length; index_columns++)
                {
                    means[index_columns] += array[index_row, index_columns];
                }
            }

            for (int index_columns = 0; index_columns < means.Length; index_columns++)
            {
                means[index_columns] /= array.Length;
            }

            return means;
        }



		public static float [] means_row(
			float [][] array)
		{
			float [] means = new float [array.Length];
			for (int index_row = 0; index_row < array.Length; index_row++)
			{
                means[index_row] = ToolsMathStatistics.Mean(array[index_row]);
			}
			return means;
		}

		public static float [] range(
			float min_value,
			float max_value,
			int value_count)
		{
			if (value_count == 1)
			{
				throw new Exception("Illegal range sizein 1");
			}

			float [] array = new float [value_count];
			float increment = (max_value - min_value) / (float) (value_count - 1);

			for (int index = 1; index < array.Length; index++)
			{
				array[index] = array[index - 1] + increment;
			}
			return array;
		}

		public static float [] subtract(
			float [] array_0,
			float [] array_1)
		{
			float [] result = new float [array_0.Length];
			subtract(array_0, array_1, result);
			return result;
		}

		public static void subtract(
			float [] array_0,
			float [] array_1,
			float [] result)
		{
			for (int index = 0; index < result.Length; index++)
			{
				result[index] = array_0[index] - array_1[index];
			}
		}

		public static void subtract_fill(
			float [] array_0,
			float value,
			float [] result)
		{
			for (int index = 0; index < result.Length; index++)
			{
				result[index] = array_0[index] - value;
			}

		}

		public static float sum(
			 float [] array)
		{
			float sum = 0;
			foreach ( float element in array)
			{
				sum += element;
			}
			return sum;
		}

		public static float [] sum_columns(
			float [][] array)
		{
			float [] sums = new float [array[0].Length];
			for (int index_row = 0; index_row < array.Length; index_row++)
			{
				for (int index_columns = 0; index_columns < sums.Length; index_columns++)
				{
					sums[index_columns] += array[index_row][index_columns];
				}
			}
			return sums;
		}

		public static float [] sum_rows(
			float [][] array)
		{
			float [] sums = new float [array.Length];
			for (int index_row = 0; index_row < array.Length; index_row++)
			{
				sums[index_row] = sum(array[index_row]);
			}
			return sums;
		}

		public static int value_count(
			float [][] array)
		{
			int total_count = 0;
            foreach (float[] inner in array)
			{
				total_count += inner.Length;
			}
			return total_count;
		}



       

      
    }
}