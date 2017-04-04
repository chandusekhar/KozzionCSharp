package com.kozzion.library.math.datastructure.histogram;

public interface IHistorgram<DomainType>
{

    DomainType get_smallest_value();

    DomainType get_largest_value();

    int get_bin_count();

    int get_bin_contends(
        int bin_index);

}
