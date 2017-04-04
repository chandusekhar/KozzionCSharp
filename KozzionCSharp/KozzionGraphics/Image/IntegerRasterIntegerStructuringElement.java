package com.kozzion.library.graphics.image;

public abstract class IntegerRasterIntegerStructuringElement
{
    public IntegerRasterIntegerImage2D do_dilation(final IIntegerRasterIntegerImage2D to_dilate)
    {
        final IntegerRasterIntegerImage2D dilated = new IntegerRasterIntegerImage2D(to_dilate.get_size_x(), to_dilate.get_size_y());
        final int image_size = to_dilate.get_element_count();
        for (int source_element = 0; source_element < image_size; source_element++)
        {
            final int [] members = get_members(source_element, to_dilate.get_size_x(), to_dilate.get_size_y());
            final int max_value = to_dilate.get_max_value(members);
            dilated.set_element_value(source_element, max_value);
        }
        return dilated;
    }

    public abstract int [] get_members(int source_element, int width, int height);

}
