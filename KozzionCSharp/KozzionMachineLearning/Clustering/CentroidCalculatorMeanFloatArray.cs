using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System.Collections.Generic;
using KozzionMathematics.Function.Implementation.Distance;
using System;

namespace KozzionMachineLearning.Clustering
{
	public sealed class CentroidCalculatorMeanFloatArray :  IFunction<IList<float []>, ICentroidDistance<float, float>>
	{
        public string FunctionType { get { return "CentroidCalculatorMeanFloatArray"; } }

        private IFunctionDistance<float[], float> distance_function;


        public CentroidCalculatorMeanFloatArray(IFunctionDistance<float[], float> distance_function)
        {
            this.distance_function = distance_function;
        }

        public ICentroidDistance<float, float> Compute(IList<float []> instance_feature_list)
		{
            int feature_count = instance_feature_list[0].Length;
            float[] location = new float[feature_count];
            for (int instance_index = 0; instance_index < instance_feature_list.Count; instance_index++)
            {
                for (int feature_index = 0; feature_index < feature_count; feature_index++)
                {
                    location[feature_index] += instance_feature_list[instance_index][feature_index];
                }
            }

            for (int feature_index = 0; feature_index < feature_count; feature_index++)
            {
                location[feature_index] /= feature_count;
            }

            return new CentroidFloat32(distance_function, instance_feature_list, location);
		}
	}
}