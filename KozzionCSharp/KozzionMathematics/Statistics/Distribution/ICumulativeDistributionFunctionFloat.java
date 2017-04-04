package com.kozzion.library.math.statistics.distribution.interfaces;

import com.kozzion.library.math.function.IFunction;

public interface ICumulativeDistributionFunctionFloat<DomainType extends Number> extends IFunction<DomainType, Float>
{
    public IDistributionFloatNumber<DomainType> get_distribution();

}
