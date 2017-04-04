namespace KozzionMathematics.Statistics.Distribution.implementation
{
    public class ProbabilityDensityFunction<DomainType, DensityType> : IProbabilityDensityFunction<DomainType, DensityType>
    {
        public string FunctionType { get { return "ProbabilityDensityFunction"; } }
        public IDistribution<DomainType, DensityType> Distribution{get; private set;}

        public ProbabilityDensityFunction(IDistribution<DomainType, DensityType> distribution)
        {
            Distribution = distribution;
        }

        public DensityType Compute(DomainType value_domain)
        {
            return Distribution.ComputeProbabilityDensity(value_domain);
        }
    }
}
