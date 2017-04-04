using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Voting
{
    public class SingleVoteSystem<CantidateType> : IVotingSystem<CantidateType>
    {
        public CantidateType elect(IList<CantidateType> candidates)
        {
            if (candidates.Count != 1)
            {
                throw new Exception("More than one option to vote on");
            }
            else
            {
                return candidates[0];
            }
        }
    }
}
