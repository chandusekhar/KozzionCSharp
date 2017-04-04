package com.kozzion.library.math.statistics.distribution.interfaces;

import com.kozzion.library.math.function.IFunction;

public interface ICumulativeDistributionFunctionDouble<DomainType extends Number> extends IFunction<DomainType, Double>
{
    public IDistributionDoubleNumber<DomainType> get_distribution();

}
