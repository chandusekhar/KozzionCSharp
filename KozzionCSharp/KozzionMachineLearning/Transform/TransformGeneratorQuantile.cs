using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
namespace KozzionMachineLearning.Transform
{
    public class TransformGeneratorQuantile : ITemplateTransform<float []>
	{
		float quantile;

		public TransformGeneratorQuantile(
			float quantile)
		{
			this.quantile = quantile;
		}

		public IFunctionBijective<float [], float []> Generate(
			IList<float []> instances)
		{
            float[,] array = ToolsCollection.ConvertToTable(instances);
            float[,] array_transposed = ToolsCollection.Transpose(array);
			float [] lower_bounds = new float [array_transposed.Length];
			float [] upper_bounds = new float [array_transposed.Length];
        
			for (int index = 0; index < array_transposed.Length; index++)
			{
				lower_bounds[index] = ToolsMathStatistics.QuantileSorted(array_transposed.Select1DIndex0(index), quantile);
				upper_bounds[index] = ToolsMathStatistics.QuantileSorted(array_transposed.Select1DIndex0(index), 1 - quantile);
			}
			return new TransformRescale(lower_bounds, upper_bounds);      
		}

        public IFunctionBijective<float[], float[]> Generate(List<float[]> instances)
        {
            throw new NotImplementedException();
        }

        ITransform<float[], float[]> ITemplateTransform<float[]>.Generate(List<float[]> instances)
        {
            throw new NotImplementedException();
        }
    }    
}