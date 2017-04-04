using System;

namespace KozzionMathematics.Numeric.Minimizer
{
    public class HaltingCriterionIterationsSimplex : IMinimizerHaltingCriterion<Simplex>
	{
		private int maximum_iteration_count;

        public HaltingCriterionIterationsSimplex(int maximum_iteration_count)
		{
			this.maximum_iteration_count = maximum_iteration_count;
		}

        public int MaximumIterationCount
        {
            get
            {
                return maximum_iteration_count;
            }
        }

		public bool CheckHalt(Simplex simplex)
		{
            double mean = 0;
            for (int j = 0; j < simplex.VertexCount; j++)
            {
                mean += simplex.vertex_values[j];
            }
            mean /= simplex.VertexCount;

            double error = 0;
            for (int j = 0; j < simplex.VertexCount; j++)
            {
                error += (double)Math.Sqrt(Math.Abs(simplex.vertex_values[j] - mean));
            }

            error /= simplex.VertexCount;
		    return false;
		}
	}
}