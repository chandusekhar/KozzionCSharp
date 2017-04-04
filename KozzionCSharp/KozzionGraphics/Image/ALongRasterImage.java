package com.kozzion.library.graphics.image;

import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.math.tools.MathToolsIntegerArray;
import com.kozzion.library.math.tools.MathToolsLongArray;

public class ALongRasterImage
{
    private static final long serialVersionUID = 1L;

    private long []           d_size;

    public ALongRasterImage(final long [] size)
    {
        d_size = ArrayTools.copy(size);
    }

    public ALongRasterImage(ALongRasterImage other)
    {
        d_size = ArrayTools.copy(other.d_size);
    }

    public ALongRasterImage(long dimension_0, long dimension_1)
    {
        d_size = new long [] {dimension_0, dimension_1};
    }

    public ALongRasterImage(long dimension_0, long dimension_1, long dimension_2)
    {
        d_size = new long [] {dimension_0, dimension_1, dimension_2};
    }

    public ALongRasterImage(int dimension_0, int dimension_1, int dimension_2, int dimension_3)
    {
        d_size = new long [] {dimension_0, dimension_1, dimension_2, dimension_3};
    }

    public long get_size(int dimension_index)
    {
        return d_size[dimension_index];
    }

    public int get_dimension_count()
    {
        return d_size.length;
    }

    public long get_element_count()
    {
        return MathToolsLongArray.product(d_size);
    }

    public int get_element_index(int [] indexes)
    {
        int element_index = 0;
        int multyplier = 1;
        for (int index = 0; index < indexes.length; index++)
        {
            element_index += indexes[index] * multyplier;
            multyplier *= d_size[index];
        }
        return element_index;
    }

    public boolean equals(final Object other)
    {
        if (other instanceof ALongRasterImage)
        {
            final ALongRasterImage typed = (ALongRasterImage) other;
            for (int index = 0; index < d_size.length; index++)
            {
                if (d_size[index] != typed.d_size[index])
                {
                    return false;
                }
            }
        }

        return true;
    }

    @Override
    public int hashCode()
    {
        int sum = 0;
        for (int index = 0; index < d_size.length; index++)
        {
            sum += d_size[index];
        }
        return sum;
    }
}
