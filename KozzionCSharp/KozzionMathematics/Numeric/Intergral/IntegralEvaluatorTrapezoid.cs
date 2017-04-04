using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
namespace KozzionMathematics.Numeric.Intergral
{
	public class IntegralEvaluatorTrapezoid : IIntegralEvaluator<double>
    {
		private int refinenment_scale;

		public IntegralEvaluatorTrapezoid(int refinenment_scale = 0)
		{
			this.refinenment_scale = refinenment_scale;
		}


		public int get_refinenment_scale()
		{
			return refinenment_scale;
		}

    
        public double[] refine_values(IFunction<double, double> function, double lower_bound, double upper_bound, double[] values)
		{

		    int new_interval_count = values.Length - (1 * refinenment_scale);
            double[] new_values = new double[new_interval_count + 1];
            double new_interval_width = (upper_bound - lower_bound) / new_interval_count;
			for (int value_index = 0; value_index < new_values.Length; value_index++)
			{
				if ((value_index % refinenment_scale) != 0)
				{
					values[value_index] = function.Compute(lower_bound + (new_interval_width * value_index));
				}
			}
			return new_values;
		}

		public double[] evaluate_multi_scale(IFunction<double, double> function, double lower_bound, double upper_bound, int evaluation_levels)
		{
			int total_interval_count = ToolsMathInteger.Pow(refinenment_scale, evaluation_levels - 1);
            double interval_width = (upper_bound - lower_bound) / total_interval_count;
            double[] values = new double[total_interval_count + 1];
			for (int value_index = 0; value_index < (total_interval_count + 1); value_index++)
			{
				values[value_index] = function.Compute(lower_bound + (interval_width * value_index));
			}

            double[] results = new double[evaluation_levels];
			int sparcety_factor = 1;
			for (int evaluation_level = 0; evaluation_level < evaluation_levels; evaluation_level++)
			{

				results[results.Length - evaluation_level - 1] = evaluate_sparce(lower_bound, upper_bound, values, sparcety_factor);
				sparcety_factor *= refinenment_scale;

			}
			return results;
		}

		private static double evaluate_sparce(double lower_bound, double upper_bound, double[] values, int sparcety_factor)
		{
			int interval_count = (values.Length - 1) / sparcety_factor;
            double interval_width = (upper_bound - lower_bound) / interval_count;
            double result = 0;
			for (int interval_index = 0; interval_index < interval_count; interval_index++)
			{
				result += (values[interval_index * sparcety_factor] + values[(interval_index + 1) * sparcety_factor]) * interval_width * 0.5f;

			}
			return result;
		}

        public static double EvaluateValue(IFunction<double, double> function, double lower_bound, double upper_bound, int interval_count)
		{
            double interval_width = (upper_bound - lower_bound) / interval_count;
            double[] values = new double[interval_count + 1];
			for (int value_index = 0; value_index < (interval_count + 1); value_index++)
			{
				values[value_index] = function.Compute(lower_bound + (interval_width * value_index));
			}

            double result = 0;
			for (int interval_index = 0; interval_index < interval_count; interval_index++)
			{
				result += (values[interval_index] + values[(interval_index + 1)]) * interval_width * 0.5f;

			}
			return result;
		}

		public static double EvaluateStaticValue(Tuple<double[], double[]> roc_curve)
		{
            double[] domain = roc_curve.Item1;
            double[] values = roc_curve.Item2;

            double result = 0;
			for (int interval_index = 0; interval_index < (domain.Length - 1); interval_index++)
			{
				result += (values[interval_index] + values[interval_index + 1]) * (domain[interval_index + 1] - domain[interval_index]) * 0.5f;
            }
			return result;

		}

        public double[] EvaluateSeries(double[] domain, double[] values)
        {
            double[] result = new double[domain.Length];
            EvaluateStaticSeriesRBA(domain, values, result);
            return result;
        }

		public void EvaluateSeriesRBA(double[] domain, double[] values, double[] result)
		{  
			EvaluateStaticSeriesRBA(domain, values, result);
		}

		//Ass in matlab
		public static void EvaluateStaticSeriesRBA(double[] domain, double[] values, double[] result)
		{  
			for (int interval_index = 1; interval_index < domain.Length; interval_index++)
			{
                double add = (values[interval_index - 1] + values[interval_index]) * (domain[interval_index] - domain[interval_index - 1]) * 0.5f;
				result[interval_index] = result[interval_index - 1]  + add;
			}    
		}
    
		public static double EvaluateStaticValue(double[] domain, double[] values)
		{
            double result = 0;        
			for (int interval_index = 1; interval_index < domain.Length; interval_index++)
			{
				result += (values[interval_index - 1] + values[interval_index]) * (domain[interval_index] - domain[interval_index - 1])
				* 0.5f;
			}  
			return result;
		}

 

        public double[] Resample(double[] sample_times, double[] aif_measured, double[] resampled_time)
        {
            throw new NotImplementedException();
        }

        public double EvaluateValue(double[] domain, double[] values, double domain_lower_bound, double domain_upper_bound)
        {
            throw new NotImplementedException();
        }
    }
}