package com.kozzion.library.math.statistics.distribution.implementation;

import com.kozzion.library.math.statistics.distribution.interfaces.ICumulativeDistributionFunctionDouble;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionDoubleNumber;

public class CumulativeDistributionFunctionDouble<DomainType extends Number> implements ICumulativeDistributionFunctionDouble<DomainType>
{
    IDistributionDoubleNumber<DomainType> d_distribution;

    public CumulativeDistributionFunctionDouble(final IDistributionDoubleNumber<DomainType> distribution)
    {
        d_distribution = distribution;
    }

    @Override
    public Double compute(final DomainType input)
    {
        return d_distribution.get_cumulative_probability(input);
    }

    @Override
    public IDistributionDoubleNumber<DomainType> get_distribution()
    {
        return d_distribution;
    }

}
