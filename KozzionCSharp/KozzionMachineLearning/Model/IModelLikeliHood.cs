using System;

namespace KozzionMachineLearning.Model
{
    public interface IModelLikelihood<DomainType, LabelType, LikelihoodType> : IModelDiscrete<DomainType, LabelType>
        where LikelihoodType :IComparable<LikelihoodType>
    {        
        LikelihoodType[] GetLikelihoods(DomainType [] instance_features);       
    }
}
