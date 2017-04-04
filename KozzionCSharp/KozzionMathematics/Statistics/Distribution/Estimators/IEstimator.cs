namespace KozzionMathematics.Statistics.Distribution.estimators
{
    public interface IEstimator<DomainType, DensityType>
    {
        IDistribution<DomainType, DensityType> Estimate(DomainType [] sample);
    }
}
