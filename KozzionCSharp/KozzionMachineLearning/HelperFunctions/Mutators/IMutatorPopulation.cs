using System.Security.Cryptography;

namespace KozzionMachineLearning.Method.GeneticAlgoritm
{
    public interface IMutatorPopulation<DomainType>
    {
        void Repopulate(RandomNumberGenerator random, DomainType[,] population, int[] retain_indexes, int[] repopulate_indexes);
    }
}
