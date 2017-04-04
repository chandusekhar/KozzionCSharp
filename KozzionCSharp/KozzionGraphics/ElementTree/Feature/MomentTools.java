package com.kozzion.library.graphics.elementtree.feature;

import com.kozzion.library.core.datastructure.Tuple2;

public class MomentTools
{
    public static int compute_momemt_count(final int moment_order)
    {
        return ((moment_order * moment_order) + (3 * moment_order) + 3) / 2;
    }

    public static int compute_momemt_index(final int moment_order, final int power_x, final int power_y)
    {
        if (moment_order < (power_x + power_y))
        {
            throw new IllegalArgumentException("Moment powers are to big for order");
        }
        return (((-power_y * power_y) + (2 * power_y * (moment_order + 1)) + power_y) / 2) + power_x;
    }

    public static Tuple2<Integer, Integer> compute_momemt_type(final int moment_order, final int moment_index)
    {
        if (compute_momemt_count(moment_order) <= moment_index)
        {
            throw new IllegalArgumentException("Moment index is to big for order");
        }

        // return (((-power_y * power_y) + (2 * power_y * (moment_order +1)) +
        // power_y) / 2) + power_x;
        return null;
    }
}
