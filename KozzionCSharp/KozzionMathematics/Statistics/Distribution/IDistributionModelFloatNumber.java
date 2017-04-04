package com.kozzion.library.math.statistics.distribution.interfaces;

public interface IDistributionModelFloatNumber<DomainType extends Number> extends IDistributionModelFloat<DomainType>
{

    public IDistributionFloatNumber<DomainType> get_number_distribution(
        DomainType [] sample);
}
