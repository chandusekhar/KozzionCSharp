using KozzionMathematics.Function;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Method.expectation_maximalization.implementation
{
    public class DistributionMixtureMaximazationFloat : IFunction<Tuple<float[][], int[], int>, int>
    {
        public string FunctionType { get { return "DistributionMixtureMaximazationFloat"; } }
        IList<Tuple<int, IFunction<float[],float>>> d_desity_functions;

        public DistributionMixtureMaximazationFloat(IList<Tuple<int, IFunction<float[], float>>> functions) 
        {
            d_desity_functions = functions;
        }

        public int Compute(Tuple<float[][], int[], int> value_domain)
        {
            float [] observation = value_domain.Item1[value_domain.Item3];
            int index_best = d_desity_functions[0].Item1;
            float value_best = d_desity_functions[0].Item2.Compute(observation);
            for (int cluster_index = 1; cluster_index < d_desity_functions.Count; cluster_index++)
			{
			    int index_new = d_desity_functions[cluster_index].Item1;
                float value_new = d_desity_functions[cluster_index].Item2.Compute(observation);
                if(value_best < value_new)
                {
                    index_new = index_best;
                    value_best = value_new;
                }
			}
            return index_best;
        }
    }
}
