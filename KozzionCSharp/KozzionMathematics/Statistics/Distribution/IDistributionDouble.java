package com.kozzion.library.math.statistics.distribution.interfaces;

public interface IDistributionDouble<DomainType>
{
    public IProbabilityDensityFunctionDouble<DomainType> get_density_function();

    public Double get_probability_density(DomainType input);
}
