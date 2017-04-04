using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Voting
{
    public class VotingSystemMeanDouble : IVotingSystem<Tuple<double[], double, double>>
    {
        public Tuple<double[], double, double> elect(IList<Tuple<double[], double, double>> candidates)
        {
            double value = 0;
            foreach (Tuple<double[], double, double> candidate in candidates)
            {
                value += candidate.Item2;
            }
            return new Tuple<double[], double, double>(null, value / candidates.Count, 0);
        }
    }
}
