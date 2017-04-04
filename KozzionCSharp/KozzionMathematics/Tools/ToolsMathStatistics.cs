using KozzionCore.Collections;
using KozzionCore.Tools;
using KozzionMathematics.Algebra;
using KozzionMathematics.Numeric.Intergral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Tools
{
    public class ToolsMathStatistics
    {
        private static AlgebraRealFloat32 algebra_float_32 = new AlgebraRealFloat32();
        private static AlgebraRealFloat64 algebra_float_64 = new AlgebraRealFloat64();
        private static AlgebraRealBigDecimal algebra_big_decimal = new AlgebraRealBigDecimal();

        public static RealType Mean<RealType>(
            IAlgebraReal<RealType> algebra,
            IList<RealType> sample_0)
        {
            return algebra.Divide(ToolsMathCollection.Sum(algebra, sample_0), algebra.ToDomain((float)sample_0.Count));
        }

        public static double Mean(
            IList<double> sample_0)
        {
            return Mean(algebra_float_64, sample_0);
        }




        public static void Means1RBA<RealType>(
            IAlgebraReal<RealType> algebra,
             RealType[,] array_2d,
             IList<RealType> means)
        {
            for (int index_0 = 0; index_0 < array_2d.Length; index_0++)
            {
                for (int index_1 = 0; index_1 < means.Count; index_1++)
                {
                    means[index_1] = algebra.Add(means[index_1], array_2d[index_0, index_1]);
                }
            }
            for (int index_1 = 0; index_1 < means.Count; index_1++)
            {
                means[index_1] = algebra.Divide(means[index_1], algebra.ToDomain((float)array_2d.Length));
            }
        }

        public static double MedianAll(IList<IList<double>> list_list)
        {
            return Quantile(ToolsCollection.ConvertToArray1D(list_list), 0.5f);
        }

        internal static double[] Medians0(IList<IList<double>> list_list)
        {
            double[] medians = new double[list_list.Count]; 
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                medians[index_0] = Quantile(algebra_float_64, list_list[index_0], 0.5f);
            }
            return medians;
        }

        public static void Means1RBA<RealType>(
            IAlgebraReal<RealType> algebra,
            IList<IList<RealType>> list_list,
            IList<RealType> means)
        {
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                for (int index_1 = 0; index_1 < means.Count; index_1++)
                {
                    means[index_1] = algebra.Add(means[index_1], list_list[index_0][index_1]);
                }
            }

            for (int index_1 = 0; index_1 < means.Count; index_1++)
            {
                means[index_1] = algebra.Divide(means[index_1], algebra.ToDomain((float)list_list.Count));
            }

        }

        public static double SumSquaredAll(IList<IList<double>> list_list)
        {
            double sum_squared_all = 0;
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                for (int index_1 = 0; index_1 < list_list[index_0].Count; index_1++)
                {
                    sum_squared_all += ToolsMath.Sqr(list_list[index_0][index_1]);
                }
            }
            return sum_squared_all;
        }

        public static float[]  Means1(
            IList<IList<float>> list_list)
        {
            float[] means = new float[list_list[0].Count];
            Means1RBA(algebra_float_32, list_list, means);
            return means;
        }

        public static float[]  Means1(
             float[,] array_2d)
        {
            float[] means = new float[array_2d.GetLength(1)];
            Means1RBA(algebra_float_32, array_2d, means);
            return means;
        }


        public static double[] Means1(
            IList<IList<double>> list_list)
        {
            double [] means = new double[list_list[0].Count];
            Means1RBA(algebra_float_64, list_list, means);
            return means;
        }

        public static double[] Means1(
             double[,] array_2d)
        {
            double[] means = new double[array_2d.GetLength(1)];
            Means1RBA(algebra_float_64, array_2d, means);
            return means;
        }


        public static double[] Means0(
            IList<IList<double>> list_list)
        {
            double[] means = new double[list_list.Count];
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                means[index_0] = Mean(algebra_float_64, list_list[index_0]);
            }
            return means;
        }

        public static double[] Means0(
             double[,] array_2d)
        {
            double[] means = new double[array_2d.GetLength(0)];
            for (int index_0 = 0; index_0 < array_2d.GetLength(0); index_0++)
            {
                means[index_0] = Mean(algebra_float_64, array_2d.Select1DIndex0(index_0));
            }
            return means;
        }


        public static double[] Ranks(IList<double> list)
        {

            // compute ranks: equal ranks are averaged, ranks start at 1 (for most thest this is requiered)
            DictionaryCount<double> values_counts = new DictionaryCount<double>();
            foreach (double item in list)
            {
                values_counts.Increment(item);
            }

            Dictionary<double, double> ranks_lookup = new Dictionary<double, double>();
            List<double> keys = new List<double>(values_counts.Keys);
            keys.Sort();
            double rank = 1;
            foreach (double key in keys)
            {
                int count = values_counts[key];
                ranks_lookup[key] = (rank + rank + count - 1) / 2.0;
                rank += count;
            }

            double[] ranks = new double[list.Count];
            for (int index = 0; index < list.Count; index++)
            {
                ranks[index] = ranks_lookup[list[index]];
            } 
            return ranks;
        }

        public static double[,] Ranks0(IList<IList<double>> list_list)
        {
            double[,] ranks0 = new double[list_list.Count, list_list[0].Count];
            for (int index_0 = 0; index_0 < list_list.Count; index_0++)
            {
                ranks0.Set1DIndex0(index_0, Ranks(list_list[0]));
            }
            return ranks0;
        }

        public static double[,] Ranks1(IList<IList<double>> list_list)
        {
            double[,] ranks1 = new double[list_list.Count, list_list[0].Count];
            for (int index_1 = 0; index_1 < list_list[0].Count; index_1++)
            {
                double[] temp_1 = new double[list_list.Count];
                for (int index_0 = 0; index_0 < list_list.Count; index_0++)
                {
                    temp_1[index_0] = list_list[index_0][index_1];
                }
                ranks1.Set1DIndex1(index_1, Ranks(temp_1));
            }
            return ranks1;
        }

        public static RealType MeanAll<RealType>(IAlgebraReal<RealType> algebra, IList<IList<RealType>> samples)
        {
            RealType total_count = algebra.AddIdentity;
            RealType total_sum = algebra.AddIdentity;
            foreach (IList<RealType> sample in samples)
            {
                total_count = algebra.Add(total_count, algebra.ToDomain((float)sample.Count));
                total_sum = algebra.Add(total_sum, ToolsMathCollection.Sum(algebra, sample));
            }
            return algebra.Divide(total_sum, total_count);
        }

        public static double MeanAll(IList<IList<double>> samples)
        {
            return MeanAll(algebra_float_64, samples);
        }

        public static float MeanPooled(IList<IList<float>> samples)
        {
            return MeanAll(algebra_float_32, samples);
        }

        public static float Mean(
            IList<float> sample_0)
        {
            return Mean(algebra_float_32, sample_0);
        }

        public static double StandardDeviation(IList<double> sample)
        {
            return StandardDeviation(sample, Mean(sample));
        }

        public static double StandardDeviation(IList<double> sample, double sample_mean)
        {
            return Math.Sqrt(Variance(sample, sample_mean));
        }


        public static double ComputeROCAUCTrapeziod<RealType>(IList<bool> labels, IList<RealType> sample)
            where RealType : IComparable<RealType>
        {
            int postive_count = ToolsCollection.CountOccurance(labels, true);
            double true_positive_increment = 1.0 / postive_count;
            double false_positive_increment = 1.0 / (labels.Count -  postive_count);

            double[] true_positive_rates = new double[labels.Count + 1];
            double[] false_positive_rates = new double[labels.Count + 1];
            List <int> ordering = ToolsMathCollection.Ordering(sample);


            for (int index = 1; index < labels.Count + 1; index++)
            {
                if (labels[ordering[index - 1]])
                {
                    true_positive_rates[index] = true_positive_rates[index - 1] + true_positive_increment;
                    false_positive_rates[index] = false_positive_rates[index - 1];
                }
                else
                {
                    true_positive_rates[index] = true_positive_rates[index - 1];
                    false_positive_rates[index] = false_positive_rates[index - 1] + false_positive_increment;
                }                
            }
            return IntegralEvaluatorTrapezoid.EvaluateStaticValue(false_positive_rates, true_positive_rates);
        }


        public static RealType VariancePooled<RealType>(
            IAlgebraReal<RealType> algebra, 
            IList<IList<RealType>> samples)
        {
   
            RealType variance = algebra.AddIdentity;
            RealType degrees_of_freedom = algebra.AddIdentity;
            foreach (IList<RealType> sample in samples)
            {
                RealType mean_0 = Mean(algebra, sample);
                for (int index = 0; index < sample.Count; index++)
                {
                    variance = algebra.Add(variance, algebra.Sqr(algebra.Subtract(sample[index], mean_0)));
                  
                }
                degrees_of_freedom = algebra.Add(degrees_of_freedom, algebra.ToDomain((float)(sample.Count - 1)));
            }
            return algebra.Divide(variance, degrees_of_freedom);
        }

        public static double VariancePooled(
            IList<IList<double>> samples)
        {
            return VariancePooled(new AlgebraRealFloat64(), samples);
        }

        public static RealType VariancePooled<RealType>(
            IAlgebraReal<RealType> algebra,
            IList<RealType> sample_0,
            IList<RealType> sample_1)
        {
            RealType mean_0 = Mean(algebra, sample_0);
            RealType mean_1 = Mean(algebra, sample_1);
            RealType variance = algebra.AddIdentity;
            for (int index = 0; index < sample_0.Count; index++)
            {
                variance = algebra.Add(variance, algebra.Sqr(algebra.Subtract(sample_0[index], mean_0)));
            }
            for (int index = 0; index < sample_1.Count; index++)
            {
                variance = algebra.Add(variance, algebra.Sqr(algebra.Subtract(sample_1[index], mean_1)));
            }
            return algebra.Divide(variance, algebra.ToDomain((float)(sample_0.Count + sample_1.Count - 2)));
        }

        public static double VariancePooled(
             IList<double> sample_0,
             IList<double> sample_1)
        {
            return VariancePooled(new AlgebraRealFloat64(), sample_0, sample_1);
        }

        public static float VariancePooled(
             IList<float> sample_0,
             IList<float> sample_1)
        {
            return VariancePooled(new AlgebraRealFloat32(), sample_0, sample_1);
        }

        public static double Variance(
             IList<double> sample_0)
        {
            double mean = Mean(sample_0);

            double variance = 0.0f;
            for (int index = 0; index < sample_0.Count; index++)
            {
                variance += ToolsMath.Sqr(sample_0[index] - mean);
            }
            return variance / (sample_0.Count - 1.0f);
        }

        public static double Variance(IList<double> sample_0, double mean)
        {
            double variance = 0.0f;
            for (int index = 0; index < sample_0.Count; index++)
            {
                variance += ToolsMath.Sqr(sample_0[index] - mean);
            }
            return variance / (sample_0.Count - 1.0f);
        }


        public static double Skew(IList<double> sample_0)
        {
            double nominator = 0.0f;
            double denominator = 0.0f;
            double mean = Mean(sample_0);
            for (int index = 0; index < sample_0.Count; index++)
            {
                nominator += Math.Pow(sample_0[index] - mean, 3.0) / sample_0.Count;
                denominator += Math.Pow(sample_0[index] - mean, 2.0) / (sample_0.Count - 1);
            }
            denominator = Math.Pow(denominator, 1.5);
            return nominator / denominator;
        }

        public static double KurtosisPlain(IList<double> sample_0)
        {
            double nominator = 0.0f;
            double denominator = 0.0f;
            double mean = Mean(sample_0);
            for (int index = 0; index < sample_0.Count; index++)
            {
                nominator += Math.Pow(sample_0[index] - mean, 4.0) / sample_0.Count;
                denominator += Math.Pow(sample_0[index] - mean, 2.0) / sample_0.Count;
            }
            denominator = Math.Pow(denominator, 2.0);
            return nominator / denominator;
        }

        public static double KurtosisExcess(IList<double> sample_0)
        {
            //For normal distibutions
            return KurtosisPlain(sample_0) - 3;
        }

        public static double KurtosisMatlab(IList<double> sample_0)
        {
            if (sample_0.Count < 4)
            {
                throw new Exception("unable to correct curtosis");
            }
            //As used in matlab
            int n = sample_0.Count;
            double weight = (n - 1) / ((n - 2) * (n - 3));
            return (weight * (((n + 1) * KurtosisPlain(sample_0)) - (3 * (n - 1)))) + 3;
        }


        public static double Kurtosis(IList<double> sample_0)
        {
            if (sample_0.Count < 4)
            {
                return KurtosisExcess(sample_0);
            }
            else
            {
                return KurtosisMatlab(sample_0);
            }

        }

        public static RealType Quantile<RealType>(
             IAlgebraReal<RealType> algebra,
             IList<RealType> array,
             float quantile)
        {
            RealType[] copy = ToolsCollection.Copy(array);
            Array.Sort(copy, algebra);
            return QuantileSorted(algebra, array, quantile);
        }

        public static double Quantile(IList<double> list, float quantile)
        {
            return Quantile(algebra_float_64, list, quantile);
        }

        public static float Quantile(IList<float> list, float quantile)
        {
            return Quantile(algebra_float_32, list, quantile);
        }

        public static RealType QuantileIP<RealType>(
            IAlgebraReal<RealType> algebra,
            IList<RealType>  list,
            float quantile)
        {
            list = ToolsCollection.Sort(list, algebra);
            return QuantileSorted(algebra, list, quantile);
        }

        public static double QuantileIP(IList<double> list, float quantile)
        {
            return QuantileIP(algebra_float_64, list, quantile);
        }

        public static float QuantileIP(IList<float> lsit, float quantile)
        {
            return QuantileIP(algebra_float_32, lsit, quantile);
        }

        public static RealType QuantileSorted<RealType>(
            IAlgebraReal<RealType> algebra,
            IList<RealType> array_sorted,
            double quantile)
        {
            if ((quantile < 0.0f) || (1.0f < quantile))
            {
                throw new Exception("Out of range");
            }

            double index_real = array_sorted.Count * quantile;
            int index_low = (int)Math.Floor(index_real);
            if (index_low == array_sorted.Count)
            {
                return array_sorted[array_sorted.Count - 1];
            }
            else
            {
                RealType index_low_weight = algebra.ToDomain(index_real - (double)index_low);
                RealType index_high_weight = algebra.Subtract(algebra.ToDomain(index_low + 1), algebra.ToDomain(index_real));

                return algebra.Add(
                    algebra.Multiply(array_sorted[index_low], index_low_weight),
                    algebra.Multiply(array_sorted[index_low + 1], index_high_weight));
            }
        }

  

        public static double QuantileSorted(double[] array, double quantile)
        {
            return QuantileSorted(algebra_float_64, array, quantile);
        }

        public static float QuantileSorted(float[] array, float quantile)
        {
            return QuantileSorted(algebra_float_32, array, quantile);
        }

        public static float QuantileSorted(IList<float> array, float quantile)
        {
            return QuantileSorted(algebra_float_32, array, quantile);
        }

        public static RealType[] QuantileSorted<RealType>(
          IAlgebraReal<RealType> algebra,
          IList<RealType> list_sorted,
          float[] quantiles)
        {
            //TODO this can be done much faster
            RealType[] values = new RealType[quantiles.Length];
            for (int index = 0; index < quantiles.Length; index++)
            {
                values[index] = QuantileSorted(algebra, list_sorted, quantiles[index]);
            }    
            return values;
        }

        public static double[] QuantileSorted(double[] array, float [] quantile)
        {
            return QuantileSorted(algebra_float_64, array, quantile);
        }

        public static float[] QuantileSorted(IList<float> list, float [] quantile)
        {
            return QuantileSorted(algebra_float_32, list, quantile);
        }

        /***
		 * Lowest value has lowest rank
		 * Shared ranks are averaged
		 * 
		 * @param array
		 * @return
		 */
        public static double[][] ConvertToAccendingRanks(
            IList<IList<double>> list_list)
        {
            DictionaryCount<double> value_counts = new DictionaryCount<double>();
            foreach (IList<double> list in list_list)
            {
                foreach (double value in list)
                {
                    value_counts.Increment(value);
                }
            }

            Dictionary<double, double> rank_map = new Dictionary<double, double>();
            List<double> value_list = new List<double>(value_counts.Keys);
            value_list.Sort();

            double rank_level = 1.0f;
            for (int value_index = 0; value_index < value_list.Count; value_index++)
            {
                double value = value_list[value_index];
                double value_count = value_counts[value];
                double rank = (rank_level + rank_level + value_count - 1) / 2.0f;
                rank_map[value] = rank;
                rank_level = rank_level + value_count;
            }

            double[][] ranks = new double[list_list.Count][];
            for (int outer_index = 0; outer_index < list_list.Count; outer_index++)
            {
                ranks[outer_index] = new double[list_list[outer_index].Count];
                for (int inner_index = 0; inner_index < list_list[outer_index].Count; inner_index++)
                {
                    ranks[outer_index][inner_index] = rank_map[list_list[outer_index][inner_index]];
                }

            }
            return ranks;
        }

        /***
		 * Lowest value has lowest rank
		 * Shared ranks are averaged
		 * 
		 * @param array
		 * @return
		 */
        public static float[][] ConvertToAccendingRanks(
            float[][] array)
        {
            DictionaryCount<float> value_counts = new DictionaryCount<float>();
            foreach (float[] inner in array)
            {
                foreach (float value in inner)
                {
                    value_counts.Increment(value);
                }
            }

            Dictionary<float, float> rank_map = new Dictionary<float, float>();
            List<float> value_list = new List<float>(value_counts.Keys);
            value_list.Sort();

            float rank_level = 1.0f;
            for (int value_index = 0; value_index < value_list.Count; value_index++)
            {
                float value = value_list[value_index];
                float value_count = value_counts[value];
                float rank = (rank_level + rank_level + value_count - 1) / 2.0f;
                rank_map[value] = rank;
                rank_level = rank_level + value_count;
            }

            float[][] ranks = new float[array.Length][];
            for (int outer_index = 0; outer_index < array.Length; outer_index++)
            {
                ranks[outer_index] = new float[array[outer_index].Length];
                for (int inner_index = 0; inner_index < array[outer_index].Length; inner_index++)
                {
                    ranks[outer_index][inner_index] = rank_map[array[outer_index][inner_index]];
                }

            }

            return ranks;
        }
    }
}
