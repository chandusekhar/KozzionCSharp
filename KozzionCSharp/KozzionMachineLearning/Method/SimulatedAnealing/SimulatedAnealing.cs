using KozzionCore.Tools;
using System;
using System.Security.Cryptography;
using KozzionMathematics.Function;
using KozzionMachineLearning.HelperFunctions.Mutators;

namespace KozzionMachineLearning.Method.SimulatedAnealing
{
    public class SimulatedAnealing<DomainType, EvaluationType>
        where EvaluationType :IComparable<EvaluationType>
    {
        private RandomNumberGenerator d_random;
        private IMutatorPoint<DomainType> d_mutator;
        private IFunction<int, float> d_accept_chance;
        public int IterationCount{get; private set;}

        public SimulatedAnealing(IMutatorPoint<DomainType> mutator, IFunction<int, float> accept_chance, int iteration_count):
            this(new RNGCryptoServiceProvider(), mutator, accept_chance, iteration_count)
        {
        }

        public SimulatedAnealing(RandomNumberGenerator random, IMutatorPoint<DomainType> mutator, IFunction<int, float> accept_chance, int iteration_count)
        {
            d_random = random;
            d_mutator = mutator;
            d_accept_chance = accept_chance;
            IterationCount = iteration_count;
        }


        public DomainType[] Minimize(DomainType[] state, IFunction<DomainType[], EvaluationType> evaluation_function)
        {
            DomainType[] new_state = ToolsCollection.Copy(state);
            EvaluationType evaluation = evaluation_function.Compute(state);
            for (int iteration_index = 0; iteration_index < IterationCount; iteration_index++)
            {
                int mutated_element = d_mutator.MutatePoint(d_random, new_state);
                EvaluationType new_evaluation = evaluation_function.Compute(new_state);

                // if new state is higher
                if (new_evaluation.CompareTo(evaluation) == 1)
                {
                    //accept it with a chance
                    if (d_random.Toss(d_accept_chance.Compute(iteration_index)))
                    {
                         state[mutated_element] = new_state[mutated_element];
                         evaluation = new_evaluation;
                    }
                    else
                    {
                        //reject it
                         new_state[mutated_element]= state[mutated_element];
                    }
                }
                else   // if new state is lower or equal
                {
                    //accept it
                    state[mutated_element] = new_state[mutated_element];
                    evaluation = new_evaluation;
                }
            }
            return state;
        }
        
    }
}
