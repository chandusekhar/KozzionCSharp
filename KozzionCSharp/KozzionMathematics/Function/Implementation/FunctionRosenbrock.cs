namespace KozzionMathematics.Function.Implementation
{
    public class FunctionRosenbrock : IFunction<double [], double>
    {
        public string FunctionType { get { return "FunctionRosenbrock"; } }
        // http://en.wikipedia.org/wiki/Test_functions_for_optimization TODO do others
        private double parameter;

		public FunctionRosenbrock()
		{
			parameter = 100;
		}

		public FunctionRosenbrock(double parameter)
		{
			this.parameter = parameter;
		}

	
		public double Compute(double[] input)
		{
            double result = 0;
			for (int index = 0; index < (input.Length - 1); index++)
			{
				result += 
					(parameter * (input[index + 1] - (input[index] * input[index])) * (input[index + 1] - (input[index] * input[index]))) + 
					((input[index] - 1) * (input[index] - 1));
			}        
			return result;
		}
	}
}