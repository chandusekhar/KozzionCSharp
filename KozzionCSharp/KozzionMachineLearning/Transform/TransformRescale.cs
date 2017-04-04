using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
namespace KozzionMachineLearning.Transform
{
	public class TransformRescale :	IFunctionBijective<float [], float []>
	{
        public string FunctionType { get { return "TransformRescale"; } }

        float [] lower_bounds;
		float [] upper_bounds;
		float [] window_sizes;

		public TransformRescale(
			float [] lower_bounds,
			float [] upper_bounds)
		{
			this.lower_bounds = ToolsCollection.Copy(lower_bounds);
			this.upper_bounds = ToolsCollection.Copy(upper_bounds);
			window_sizes = ToolsMathCollectionFloat.subtract(upper_bounds, lower_bounds);
		}

		public float [] Compute(
			float [] input)
		{
			float [] result = new float [input.Length];
			for (int index = 0; index < input.Length; index++)
			{
				result[index] = (input[index] - lower_bounds[index]) / window_sizes[index];
			}
			return result;
		}

		public float [] ComputeInverse(
			float [] input)
		{
			float [] result = new float [input.Length];
			for (int index = 0; index < input.Length; index++)
			{
				result[index] = (input[index] * window_sizes[index]) + lower_bounds[index];
			}
			return result;
		}

        public IFunctionBijective<float[], float[]> GetInverse()
        {
            throw new System.NotImplementedException();
        }
    }
}