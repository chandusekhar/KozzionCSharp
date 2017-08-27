using System;

namespace KozzionMachineLearning.Model
{
    public interface IModelLikelihood<DomainType, LabelType> : IModelDiscrete<DomainType, LabelType>
    {        
        double[] GetLikelihoods(DomainType [] instance_features);       
    }
}
