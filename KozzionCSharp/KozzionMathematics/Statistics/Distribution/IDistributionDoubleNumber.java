package com.kozzion.library.math.statistics.distribution.interfaces;

public interface IDistributionDoubleNumber<DomainType extends Number> extends IDistributionDouble<DomainType>
{
    public ICumulativeDistributionFunctionDouble<DomainType> get_culmative_function();

    public double get_mean();

    public double get_variance();

    public double get_cumulative_probability(DomainType input);

}
