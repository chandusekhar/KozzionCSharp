package com.kozzion.library.math.statistics.distribution.implementation;

import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionDouble;
import com.kozzion.library.math.statistics.distribution.interfaces.IProbabilityDensityFunctionDouble;

public class ProbabilityDensityFunctionDouble<DomainType> implements IProbabilityDensityFunctionDouble<DomainType>
{
    IDistributionDouble<DomainType> d_distribution;

    public ProbabilityDensityFunctionDouble(final IDistributionDouble<DomainType> distribution)
    {
        d_distribution = distribution;
    }

    @Override
    public Double compute(final DomainType input)
    {
        return d_distribution.get_probability_density(input);
    }

    @Override
    public IDistributionDouble<DomainType> get_distribution()
    {
        return d_distribution;
    }

}
