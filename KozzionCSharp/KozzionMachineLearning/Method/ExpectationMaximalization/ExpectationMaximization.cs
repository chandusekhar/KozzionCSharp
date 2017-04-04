using KozzionCore.Tools;
using KozzionMathematics.Function;
using System;

namespace KozzionMachineLearning.Method.expectation_maximalization
{
    public class ExpectationMaximization<ObeservationType, LabelType>
    {
        private IExpectationFunctionGenerator<ObeservationType, LabelType> d_generator;
        private int d_iteration_count;
        

        public ExpectationMaximization(IExpectationFunctionGenerator<ObeservationType, LabelType> generator, int iteration_count)
        {
            d_generator = generator;
            d_iteration_count = iteration_count;
        }

        public LabelType[] Maximize(ObeservationType [] observations, LabelType [] state)
        {
            LabelType [] new_state = new LabelType[state.Length];

            for (int index_iteration = 0; index_iteration < d_iteration_count; index_iteration++)
            {                 
                // expectation
                IFunction<Tuple<ObeservationType[], LabelType[], int>, LabelType> expectation_function = d_generator.generate(observations, state);
                         

                // maximazation    
                for (int index_element = 0; index_element < state.Length; index_element++)
                {
                    new_state[index_element] = expectation_function.Compute(new Tuple<ObeservationType[], LabelType[], int>(observations, state, index_element));
                }
                ToolsCollection.CopyRBA(state, new_state);
            }
       

            return state;
        }
    }
}
