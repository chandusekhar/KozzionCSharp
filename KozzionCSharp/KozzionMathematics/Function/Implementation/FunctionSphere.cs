namespace KozzionMathematics.Function.Implementation
{
    public class FunctionSphere  : IFunction<float [], float>
	{
        public string FunctionType { get { return "FunctionSphere"; } }
        // http://en.wikipedia.org/wiki/Test_functions_for_optimization
        public float Compute( float [] input)
		{
			float result = 0;
			foreach (float element in input)
			{
				result += element * element;
			}
			return result;
		}

	}
}