package com.kozzion.library.math.datastructure.histogram;

import java.util.Arrays;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.math.function.IFunction;

public class HistrogramPercentileBin<DomainType>
    implements
        IHistorgram<DomainType>
{
    DomainType d_smallest_value;
    DomainType d_largest_value;
    
    int d_bin_count;
    public HistrogramPercentileBin(
        DomainType [] sample,
        int minimum_value_per_bin,
        IFunction<Tuple2<DomainType, DomainType>, DomainType> mean_function)
    {
        Arrays.sort(sample);
        d_bin_count = sample.length / minimum_value_per_bin; //last bin will be bigger
        
    }

    @Override
    public DomainType get_smallest_value()
    {
        return d_smallest_value;
    }

    @Override
    public DomainType get_largest_value()
    {
        return d_largest_value;
    }

    @Override
    public int get_bin_count()
    {
        return d_bin_count;
    }

    @Override
    public int get_bin_contends(
        int bin_index)
    {
        // TODO Auto-generated method stub
        return 0;
    }

    public DomainType get_bin_lower_bound(
        int bin_index)
    {
        return null;
    }

    public DomainType get_bin_upper_bound(
        int bin_index)
    {
        return null;
    }

}
