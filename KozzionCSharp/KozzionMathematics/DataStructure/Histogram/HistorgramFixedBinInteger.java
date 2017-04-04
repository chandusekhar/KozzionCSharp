package com.kozzion.library.math.datastructure.histogram;

import com.kozzion.library.math.tools.MathToolsInteger;
import com.kozzion.library.math.tools.MathToolsIntegerArray;

public class HistorgramFixedBinInteger implements IHistorgram<Integer>
{
    private int[] d_bin_contends;
    private int   d_lower_bound;
    private int   d_upper_bound;
    private int   d_bin_width;

    public HistorgramFixedBinInteger(int[] data, int bin_count)
    {
        d_bin_contends = new int[bin_count];
        d_lower_bound = MathToolsIntegerArray.min_value(data);
        d_upper_bound = MathToolsIntegerArray.max_value(data);
        int width = d_upper_bound - d_lower_bound;
        d_bin_width = MathToolsInteger.compute_first_mulptiple_equal_or_larger(width, bin_count);
        for (int value : data)
        {
            int bin_index = get_bin_index(value);
            d_bin_contends[bin_index] = d_bin_contends[bin_index] + 1;
        }
    }

    public HistorgramFixedBinInteger()
    {
        d_bin_contends = new int[0];
        d_lower_bound = 0;
        d_upper_bound = 0;
        d_bin_width = 0;
    }

    private int get_bin_index(int value)
    {
        return (value - d_lower_bound) / d_bin_width;
    }

    @Override
    public Integer get_smallest_value()
    {
        return d_lower_bound;
    }

    @Override
    public Integer get_largest_value()
    {
        return d_lower_bound;
    }

    @Override
    public int get_bin_count()
    {
        return d_bin_contends.length;
    }

    @Override
    public int get_bin_contends(int bin_index)
    {
        return d_bin_contends[bin_index];
    }



}
