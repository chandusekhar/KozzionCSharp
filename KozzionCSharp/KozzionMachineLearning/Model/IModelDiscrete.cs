namespace KozzionMachineLearning.Model
{
    public interface IModelDiscrete<DomainType, LabelType> :
         IModelLabel<DomainType, LabelType>
    {
        int GetLabelIndex(DomainType [] domainType);
    }
}