
using System.Collections.Generic;

namespace KozzionMachineLearning.Transform
{
    public interface ITemplateTransform<DomainType>
    {
        ITransform<DomainType, DomainType> Generate(List<DomainType> instances);

    }
}