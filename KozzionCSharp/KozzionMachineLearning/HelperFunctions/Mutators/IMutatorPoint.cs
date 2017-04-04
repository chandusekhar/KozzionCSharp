using System.Security.Cryptography;

namespace KozzionMachineLearning.HelperFunctions.Mutators
{
    public interface IMutatorPoint<DomainType>
    {
        int MutatePoint(RandomNumberGenerator random, DomainType[] new_state);

        void MutatePoint(RandomNumberGenerator random, DomainType[,] population, int index_1, int index_2);
    }
}
