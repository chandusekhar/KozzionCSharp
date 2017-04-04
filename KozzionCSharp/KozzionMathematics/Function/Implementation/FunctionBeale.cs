using System;

namespace KozzionMathematics.Function.Implementation
{
    public class FunctionBeale : IFunction<double [], double>
	{
        public string FunctionType { get { return "FunctionBeale"; } }
        // http://en.wikipedia.org/wiki/Test_functions_for_optimization TODO do others


        public double Compute(double[] input)
		{
        
            double term_0 = 1.5f   - input[0] + input[0] * input[1];
            double term_1 = 2.25f  - input[0] + input[0] * input[1] * input[1];
            double term_2 = 2.625f - input[0] + input[0] * input[1] * input[1] * input[1];
            Console.WriteLine("eval " + input[0] + " " + input[1]);
            Console.WriteLine("eval " + ((term_0 * term_0) + (term_1 * term_1) + (term_2 * term_2)));
            return (term_0 * term_0) + (term_1 * term_1) + (term_2 * term_2);
		}
	}
}
