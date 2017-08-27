using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
namespace KozzionMachineLearning.Clustering
{

    public class ModelClusteringCentroids<DomainType, LabelType, ControidType> : AModelLikelihood<DomainType, LabelType>
    {
        private IClusteringCentroid<DomainType> clustering;
        private double[,] cluster_likelyhoods;

        public ModelClusteringCentroids(IDataContext data_context, IClusteringCentroid<DomainType> clustering, double[,] cluster_likelyhoods)
            : base(data_context, "ModelClusteringCentroids")
        {
            this.clustering = clustering;
            this.cluster_likelyhoods = cluster_likelyhoods;
        }

        public override double[] GetLikelihoods(DomainType[] instance_features)
        {
            return cluster_likelyhoods.Select1DIndex0(clustering.GetClusterIndex(instance_features));
        }
    }
}
