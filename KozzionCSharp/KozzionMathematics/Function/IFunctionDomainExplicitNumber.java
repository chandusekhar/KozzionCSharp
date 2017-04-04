package com.kozzion.library.math.function;

import com.kozzion.library.math.function.domain.IDomainExplicitNumber;


public interface IFunctionDomainExplicitNumber<NumberType extends Number, RangeType> extends IFunction<NumberType, RangeType>
{
    public IDomainExplicitNumber<NumberType> get_explicit_number_domain();

}
