using System.Security.Cryptography;
using KozzionMachineLearning.Method.GeneticAlgoritm;

namespace KozzionMachineLearning.HelperFunctions.Mutators
{
    public class MutatorPopulation<DomainType> : IMutatorPopulation<DomainType>
    {
        IMutatorPoint<DomainType> d_mutator_point;
        float d_chance_retain;
        float d_chance_mutate;

        public MutatorPopulation(IMutatorPoint<DomainType> mutator_point, float chance_retain, float chance_mutate)
        {
            d_mutator_point = mutator_point;
            d_chance_retain = chance_retain;
            d_chance_mutate = chance_mutate;
        }

        public void Repopulate(RandomNumberGenerator random, DomainType [,] population, int[] retain_indexes, int [] repopulate_indexes) 
        {
            foreach (int repopulate_index in repopulate_indexes)
            {
                int index_destination = repopulate_index;
                int index_source = retain_indexes[random.RandomInt32(retain_indexes.Length)];
                for (int element_index = 0; element_index < population.GetLength(1); element_index++)
                {
                  
                    if(random.Toss(d_chance_retain))
                    {
                        population[index_destination, element_index] = population[index_source, element_index];
                    }
                    else
                    {
                        if (random.Toss(d_chance_mutate))
                        {
                             d_mutator_point.MutatePoint(random, population, index_source, element_index);
                        }
                        else
                        {
                            //SEX
                            int index_partner = retain_indexes[random.RandomInt32(retain_indexes.Length)];
                            population[index_destination, element_index] = population[index_partner, element_index];
                        }
                    }
                }
            }
        }
    }
}
