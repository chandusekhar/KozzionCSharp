namespace KozzionMathematics.Numeric.solver.linear_solver
{
    public static class ToolsMathLinearRegression
    {
        public static void LinearRegesion2D(double[] array_x, double[] array_y, out double slope, out double intercept)
        {
            /* declarations */
            double sx  = 0;  /* Sum of domain */
            double sy  = 0;  /* Sum ov value */
            double sxx = 0; /* Sum of domain squared */
            double syy = 0; /* Sum of value s squared */
            double sxy = 0; /* Sum of domain times values */
            double n   = array_x.Length;
        
            /* Apply the linearised fit */
            for (int j = 0; j < n; j++) 
            {
                sx += array_x[j];
                sy += array_y[j];
                sxx += array_x[j] * array_x[j];
                syy += array_y[j] * array_y[j];
                sxy += array_x[j] * array_y[j];
            }

            /* Get the slope and intercept of the linear fit. */
            slope = (sxy - sx * sy / n) / (sxx - sx * sx / n);
            intercept = ((sy - slope * sx) / n); 
        }

        public static double LinearRegesion2DFixedIntercept(double[] array_x, double[] array_y, double fixed_intercept)
        {
            //TODO check this
            /* declarations */
            double sxx = 0; /* Sum of domain squared */
            double sxy = 0; /* Sum of domain times values */
            double n   = array_x.Length;
        
            /* Apply the linearised fit */
            for (int j = 0; j < n; j++) 
            {
                sxx += array_x[j] * array_x[j];
                sxy += array_x[j] * (array_y[j] - fixed_intercept);
            }

            /* Get the slope of the linear fit. */
            return sxy / sxx;
        }

        public static double LinearRegesion2DFixedSlope(double[] array_x, double[] array_y, double fixed_slope)
        {
            /* declarations */
            double sx  = 0;  /* Sum of domain */
            double sy  = 0;  /* Sum ov value */
            double n   = array_x.Length; 
        
            for (int j = 0; j < n; j++) 
            {
                sx += array_x[j];
                sy += array_y[j];
            }

            /* Get the intercept of the linear fit. */  
            return  ((sy - fixed_slope * sx) / n); 
        }
    }
}
