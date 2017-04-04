using System.Security.Cryptography;

namespace KozzionMachineLearning.HelperFunctions.Mutators
{
    public class MutatorPointOneInt : IMutatorPoint<int>
    {
        public int MutatePoint(RandomNumberGenerator random, int[] new_state)
        {
            int index = random.RandomInt32(new_state.Length);
                
            if(random.Toss(0.5))
            {
                new_state[index]++;
            }
            else
            {
                new_state[index]--;
            }
            return index;
        }


        public void MutatePoint(RandomNumberGenerator random, int[,] population, int index_0, int index_1)
        {
            if(random.Toss(0.5))
            {
                population[index_0, index_1]++;
            }
            else
            {
                population[index_0, index_1]--;
            }
      
        }
    }
}
