package com.kozzion.library.math.function;

import com.kozzion.library.math.function.domain.implementation.ValueInterval;

public interface IFunctionDomainNumberLimited<DomainType extends Number, RangeType> extends IFunction<DomainType, RangeType>
{

    public ValueInterval<DomainType> get_domain();

}
