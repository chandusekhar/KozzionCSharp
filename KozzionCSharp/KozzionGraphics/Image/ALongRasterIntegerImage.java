package com.kozzion.library.graphics.image;

import java.util.List;

import com.kozzion.library.core.datastructure.array.LongArrayInteger;
import com.kozzion.library.core.datastructure.array.LongArrayTools;

public class ALongRasterIntegerImage extends ALongRasterImage
{
    private static final long serialVersionUID = 1L;

    private LongArrayInteger  d_image;

    public ALongRasterIntegerImage(long [] size)
    {
        super(size);
        d_image = new LongArrayInteger(get_element_count());
    }

    public ALongRasterIntegerImage(long [] size, int [] image)
    {
        super(size);
        d_image = LongArrayTools.convert(image);
    }

    public ALongRasterIntegerImage(int width, int height)
    {
        super(width, height);
        d_image = new LongArrayInteger(get_element_count());
    }

    public ALongRasterIntegerImage(int width, int height, int [] image)
    {
        super(width, height);
        d_image = LongArrayTools.convert(image);
    }

    public ALongRasterIntegerImage(int width, int height, int depth, int time_count)
    {

        super(width, height, depth, time_count);

        d_image = new LongArrayInteger(get_element_count());
    }

    public ALongRasterIntegerImage(int width, int height, int depth, int time_count, int [] image)
    {
        super(width, height, depth, time_count);
        d_image = LongArrayTools.convert(image);
    }

    public ALongRasterIntegerImage(ALongRasterIntegerImage other)
    {
        super(other);
        d_image = LongArrayTools.copy(other.d_image);
    }

    public int get_element_value(int element_index)
    {
        return d_image.get_value(element_index);
    }

    public int get_element_value(int [] indexes)
    {
        return get_element_value(get_element_index(indexes));
    }

    public void set_element_value(int element_index, int value)
    {
        d_image.set(element_index, value);
    }

    public void invert(final int max_value)
    {
        for (long element_index = 0; element_index < d_image.length(); element_index++)
        {
            d_image.set(element_index, max_value - d_image.get_value(element_index));
        }

    }

    public void paint(final List<Long> elements, final int value)
    {
        for (final Long element_index : elements)
        {
            d_image.set(element_index, value);
        }

    }

    public void threshold(final int threshold, final int max_value)
    {
        for (int element_index = 0; element_index < d_image.length(); element_index++)
        {
            if (threshold < d_image.get_value(element_index))
            {
                d_image.set(element_index, max_value);
            }
            else
            {
                d_image.set(element_index, 0);
            }
        }
    }

    public boolean equals(final ALongRasterIntegerImage other)
    {

        if (!super.equals(other))
        {
            return false;
        }

        for (long element_index = 0; element_index < d_image.length(); element_index++)
        {
            if (d_image.get_value(element_index) != other.d_image.get_value(element_index))
            {
                return false;        
            }
        }
        return true;
    }

    public int get_max_value(final int [] element_indexes)
    {
        int max_value = Integer.MIN_VALUE;
        for (final int element_index : element_indexes)
        {
            max_value = Math.max(max_value, d_image.get_value(element_index));
        }
        return max_value;
    }
}
