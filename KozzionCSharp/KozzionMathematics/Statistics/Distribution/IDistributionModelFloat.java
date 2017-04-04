package com.kozzion.library.math.statistics.distribution.interfaces;

public interface IDistributionModelFloat<DomaintType>
{
    int get_parameter_count();
    
    public IDistributionFloat<DomaintType> get_distribution(
        DomaintType [] sample);
}
