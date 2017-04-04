using System;
using System.Security.Cryptography;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;

namespace KozzionMachineLearning.Method.GeneticAlgoritm
{
    public class GeneticAlgoritm<DomainType, EvaluationType>
        where EvaluationType :IComparable<EvaluationType>
    {
        private RandomNumberGenerator d_random;
        private IMutatorPopulation<DomainType> d_mutator;
        private IFunction<DomainType[,], float[]> d_fitness_function;

        public int IterationCount{get; private set;}
        public int PopulationCount{get; private set;}
        public int RetentionCount{get; private set;}

        public GeneticAlgoritm(
            IMutatorPopulation<DomainType> mutator,
            IFunction<DomainType[,], float[]> fitness_function, 
            int iteration_count,
            int population_count,
            int retention_count):
            this(new RNGCryptoServiceProvider(), mutator, fitness_function, iteration_count, population_count, retention_count)
        {
        }

        public GeneticAlgoritm(
            RandomNumberGenerator random,      
            IMutatorPopulation<DomainType> mutator,
            IFunction<DomainType[,], float[]> fitness_function, 
            int iteration_count,
            int population_count,
            int retention_count)
        {
            d_random = random;
            d_mutator = mutator;
            d_fitness_function = fitness_function;
            IterationCount = iteration_count;
            PopulationCount = population_count;
            RetentionCount = retention_count;
        }


        public DomainType[] Minimize(DomainType[] initial_state)
        {
            DomainType[,] population = new DomainType[PopulationCount, initial_state.Length];
            float[] fitness_values = new float[PopulationCount];
            for (int iteration_index = 0; iteration_index < IterationCount; iteration_index++)
            {

                d_mutator.Repopulate(null, null, null, null);
       
            }
            return population.Select1DIndex0(ToolsMathCollection.MaxIndex(fitness_values));
        }
        
    }
}
