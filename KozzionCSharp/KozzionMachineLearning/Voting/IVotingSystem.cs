using System.Collections.Generic;

namespace KozzionMachineLearning.Voting
{
    public interface IVotingSystem<CantidateType>
    {
        CantidateType elect(IList<CantidateType> candidates);
    }
}
