using KozzionMathematics.Statistics.Distribution.implementation;

namespace KozzionMathematics.Statistics.Distribution
{
    public interface IDistribution<DomainType, DensityType>
    {
        DomainType[] Domain { get; }

        DomainType[] RandomSample(int sample_size);

        IProbabilityDensityFunction<DomainType, DensityType> GetProbabilityDensityFunction();

        ICulmativeDensityFunction<DomainType, DensityType> GetCulmativeDensityFunction();  

        DensityType ComputeProbabilityDensity(DomainType value_domain);

        DensityType ComputeCulmativeDensity(DomainType value_domain);
    } 
}
