using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
namespace KozzionMachineLearning.Clustering
{

    public class ModelClusteringCentroids<DomainType, LabelType, ControidType, DistanceType, LikelihoodType> : AModelLikelihood<DomainType, LabelType, LikelihoodType>
        where LikelihoodType : IComparable<LikelihoodType>
    {
        private IClusteringCentroid<DomainType, DistanceType> clustering;
        private LikelihoodType[,] cluster_likelyhoods;

        public ModelClusteringCentroids(IDataContext data_context, IClusteringCentroid<DomainType, DistanceType> clustering, LikelihoodType[,] cluster_likelyhoods)
            : base(data_context, "ModelClusteringCentroids")
        {
            this.clustering = clustering;
            this.cluster_likelyhoods = cluster_likelyhoods;
        }

        public override LikelihoodType[] GetLikelihoods(DomainType[] instance_features)
        {
            return cluster_likelyhoods.Select1DIndex0(clustering.GetClusterIndex(instance_features));
        }
    }
}
