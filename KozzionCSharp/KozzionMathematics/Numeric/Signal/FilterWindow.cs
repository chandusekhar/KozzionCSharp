

using KozzionMathematics.Function;

using System;
using KozzionMathematics.Algebra;

namespace KozzionMathematics.Numeric.Signal
{
    public class FilterWindow<RealType> : IFunction<RealType[], RealType[]>
	{
        public string FunctionType { get { return "WindowFilter"; } }
        IAlgebraReal<RealType> algebra;
		IFilterWindow<RealType> window;

        public FilterWindow(IAlgebraReal<RealType> algebra, IFilterWindow<RealType> window)
		{
            this.algebra = algebra;
			this.window = window;
            //d_time_offset_limit = this.algebra.FloorInt(window.HalfWindowWidth);
		}

        public RealType[] Compute(RealType[] sample_times, RealType[] input)
		{
            RealType[] result = new RealType[input.Length];
            ComputeRBA(sample_times, input, result);
			return result;
		}

        public RealType[] Compute(RealType[] value_domain)
        {
            throw new NotImplementedException();
        }

		/**
		 * Variable time filter
		 * 
		 * @param input
		 *            samples
		 * @param sample_times
		 *            samples
		 * @param result
		 */
        public void ComputeRBA(RealType[] sample_times, RealType[] input, RealType[] result)
		{
			int time_index_lower = 0;
			for (int sample_index = 0; sample_index < result.Length; sample_index++)
			{
                RealType total_contribution = this.algebra.AddIdentity;
                RealType sample_time = sample_times[sample_index];
				for (int time_index = time_index_lower; time_index < result.Length; time_index++)
				{
                    RealType time_offset = this.algebra.Subtract(sample_times[time_index], sample_time);
                    if (this.algebra.CompareTo(this.window.WindowLowerBound, time_offset) == 1)
					{
						time_index_lower++;
					}
					else
					{
                        if (this.algebra.CompareTo(this.window.WindowUpperBound, time_offset) == -1)
						{
							break;
						}
						else
						{
                            RealType contribution = this.window.Compute(time_offset);
                            result[sample_index] = this.algebra.Add(result[sample_index], this.algebra.Multiply(contribution, input[time_index]));
							total_contribution = this.algebra.Add(total_contribution, contribution);
						}
					}
				}
				result[sample_index] = this.algebra.Divide(result[sample_index], total_contribution);
			}

		}

     
    }
}