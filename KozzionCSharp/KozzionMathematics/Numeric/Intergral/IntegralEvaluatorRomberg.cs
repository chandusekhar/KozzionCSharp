using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
namespace KozzionMathematics.Numeric.Intergral
{
    public class IntegralEvaluatorRomberg : IIntegralEvaluator<double>
    {
        private IntegralEvaluatorTrapezoid interal_evaluator;
        private int evaluation_level;
        public IntegralEvaluatorRomberg( int refinenment_scale, int evaluation_level)
        {
            this.interal_evaluator = new IntegralEvaluatorTrapezoid(refinenment_scale);
            this.evaluation_level = evaluation_level;
        }

        public double EvaluateValue( IFunction<double, double> function, double lower_bound, double upper_bound)
        {
            double[,] romberg_matrix = CreateRombergMatrix(function, lower_bound, upper_bound, evaluation_level);
            return romberg_matrix[evaluation_level - 1, evaluation_level - 1];
        }

        public double[] EvaluateSeries(double[] domain, double[] values)
        {
            throw new NotImplementedException();
        }

        public void EvaluateSeriesRBA(double[] domain, double[] values, double[] result)
        {
            throw new NotImplementedException();
        }

        public double[] Resample(double[] sample_times, double[] aif_measured, double[] resampled_time)
        {
            throw new NotImplementedException();
        }

        private double[,] CreateRombergMatrix( IFunction<double, double> function, double lower_bound, double upper_bound,
             int evaluation_levels)
        {
            double[] trapezoid_estimates = interal_evaluator.evaluate_multi_scale(function, lower_bound, upper_bound,
                evaluation_levels);
            double[,] romberg_matrix = new double[evaluation_levels, evaluation_levels];
            for (int row_index = 0; row_index < evaluation_levels; row_index++)
            {
                romberg_matrix[row_index,0] = trapezoid_estimates[row_index];
                for (int col_index = 0; col_index < row_index; col_index++)
                {
                    romberg_matrix[row_index,col_index + 1] = ToolsMathSeries.RichardsonExtrapolation(romberg_matrix[row_index - 1, col_index],
                        romberg_matrix[row_index,col_index], interal_evaluator.get_refinenment_scale(),
                        interal_evaluator.get_refinenment_scale() * (col_index + 1));

                }
            }

            return romberg_matrix;
        }

   
        public double EvaluateValue(double[] domain, double[] values, double domain_lower_bound, double domain_upper_bound)
        {
            throw new NotImplementedException();
        }
    }
}