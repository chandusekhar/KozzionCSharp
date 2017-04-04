namespace KozzionMathematics.Function.Implementation
{
    public class FunctionHimmelblau: IFunction<double [], double>
	{
        public string FunctionType { get { return "FunctionHimmelblau"; } }
        // http://en.wikipedia.org/wiki/Test_functions_for_optimization TODO do others

        public double Compute(double[] input)
		{
            double term_0 = (input[0] * input[0]) +  input[1]             - 11;
            double term_1 =  input[0]             + (input[1] * input[1]) - 7;

			return (term_0 * term_0) + (term_1 * term_1);
		}
	}
}