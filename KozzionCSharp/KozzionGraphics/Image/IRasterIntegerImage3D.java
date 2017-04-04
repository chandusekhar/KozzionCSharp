package com.kozzion.library.graphics.image;

import com.kozzion.library.graphics.image.raster.IIntegerRaster3D;

public interface IIntegerRasterIntegerImage3D
    extends
        IIntegerRasterIntegerImage<IIntegerRaster3D>
{
    int get_size_x();

    int get_size_y();

    int get_size_z();

    int get_element_index(
        int index_x,
        int index_y,
        int index_z);

    int get_element_value(
        int index_x,
        int index_y,
        int index_z);

    void set_element_value(
        int index_x,
        int index_y,
        int index_z,
        int value);
}
