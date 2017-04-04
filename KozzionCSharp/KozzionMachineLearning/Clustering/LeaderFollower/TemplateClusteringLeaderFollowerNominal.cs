using System;
using System.Collections.Generic;
using KozzionMathematics.Function;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Clustering.KMeans;

namespace KozzionMachineLearning.Clustering.LeaderFollower
{
    public class TemplateClusteringLeaderFollowerNominal : TemplateClusteringLeaderFollower<int, double, IDataSet<int>>
    {

        public TemplateClusteringLeaderFollowerNominal(double critical_distance)
            : base(new TemplateCentroidCalculatorNominal(), critical_distance)
        {

        }
    }
}