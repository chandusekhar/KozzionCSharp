package com.kozzion.library.math.statistics.distribution.interfaces;

public interface IDistributionFloatNumber<DomainType extends Number>
    extends
        IDistributionFloat<DomainType>
{
    public ICumulativeDistributionFunctionFloat<DomainType> get_culmative_density_function();

    public float get_cumulative_probability(
        DomainType input);
    
    public float get_mean();

    public float get_variance();



}
