package com.kozzion.library.math.function;

import com.kozzion.library.math.function.domain.IDomainExplicit;


public interface IFunctionDomainExplicit<DomainType, RangeType> extends IFunction<DomainType, RangeType>
{
    public IDomainExplicit<DomainType> get_explicit_domain();

}
