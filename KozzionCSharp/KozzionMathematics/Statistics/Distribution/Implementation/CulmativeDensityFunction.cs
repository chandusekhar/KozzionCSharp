using System;

namespace KozzionMathematics.Statistics.Distribution.implementation
{
    public class CulmativeDensityFunction<DomainType, DensityType> : 
        ICulmativeDensityFunction<DomainType, DensityType>
    {
        public string FunctionType { get { throw new NotImplementedException(); } }
        public IDistribution<DomainType, DensityType> Distribution { get; private set; }

       

        public CulmativeDensityFunction(IDistribution<DomainType , DensityType> distribution)
        {
            Distribution = distribution;
        }

        public DensityType Compute(DomainType value_domain)
        {
            return Distribution.ComputeCulmativeDensity(value_domain);
        }
    }
}
