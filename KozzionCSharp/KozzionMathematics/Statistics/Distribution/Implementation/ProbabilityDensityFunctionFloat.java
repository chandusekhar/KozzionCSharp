package com.kozzion.library.math.statistics.distribution.implementation;

import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionFloat;
import com.kozzion.library.math.statistics.distribution.interfaces.IProbabilityDensityFunctionFloat;

public class ProbabilityDensityFunctionFloat<DomainType> implements IProbabilityDensityFunctionFloat<DomainType>
{
    IDistributionFloat<DomainType> d_distribution;

    public ProbabilityDensityFunctionFloat(final IDistributionFloat<DomainType> distribution)
    {
        d_distribution = distribution;
    }

    @Override
    public Float compute(final DomainType input)
    {
        return d_distribution.get_probability_density(input);
    }

    @Override
    public IDistributionFloat<DomainType> get_distribution()
    {
        return d_distribution;
    }

}
