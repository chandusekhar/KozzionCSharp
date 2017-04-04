package com.kozzion.library.math.statistics.distribution.implementation;

import com.kozzion.library.math.statistics.distribution.interfaces.ICumulativeDistributionFunctionFloat;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionFloatNumber;

public class CumulativeDistributionFunctionFloat<DomainType extends Number> implements ICumulativeDistributionFunctionFloat<DomainType>
{
    IDistributionFloatNumber<DomainType> d_distribution;

    public CumulativeDistributionFunctionFloat(final IDistributionFloatNumber<DomainType> distribution)
    {
        d_distribution = distribution;
    }

    @Override
    public Float compute(final DomainType input)
    {
        return d_distribution.get_cumulative_probability(input);
    }

    @Override
    public IDistributionFloatNumber<DomainType> get_distribution()
    {
        return d_distribution;
    }

}
