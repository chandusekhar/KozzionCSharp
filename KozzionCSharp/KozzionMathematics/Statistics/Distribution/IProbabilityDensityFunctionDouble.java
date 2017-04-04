package com.kozzion.library.math.statistics.distribution.interfaces;

import com.kozzion.library.math.function.IFunction;

public interface IProbabilityDensityFunctionDouble<DomainType> extends IFunction<DomainType, Double>
{
    public IDistributionDouble<DomainType> get_distribution();
}
