package com.kozzion.library.math.statistics.distribution.interfaces;

import com.kozzion.library.math.function.IFunction;

public interface IProbabilityDensityFunctionFloat<DomainType> extends IFunction<DomainType, Float>
{
    public IDistributionFloat<DomainType> get_distribution();
}
