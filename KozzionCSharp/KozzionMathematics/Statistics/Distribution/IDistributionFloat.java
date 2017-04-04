package com.kozzion.library.math.statistics.distribution.interfaces;

public interface IDistributionFloat<DomainType>
{
    public IProbabilityDensityFunctionFloat<DomainType> get_probability_density_function();

    public float get_probability_density(
        DomainType input);


}
