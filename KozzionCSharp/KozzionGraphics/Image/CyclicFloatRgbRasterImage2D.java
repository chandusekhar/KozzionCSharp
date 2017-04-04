package com.kozzion.library.graphics.image;

import java.awt.image.BufferedImage;

public class CyclicFloatRgbRasterImage2D
    extends
        IntegerRasterFloatRgbImage2D
{

    /**
     * 
     */
    private static final long serialVersionUID = 1L;

    public CyclicFloatRgbRasterImage2D(
        final BufferedImage image)
    {
        super(image);
    }

    public CyclicFloatRgbRasterImage2D(
        final int width,
        final int height)
    {
        super(width, height);
    }

    public int positice_modula(
        final int value,
        final int modula)
    {
        return (((value % modula) + modula) % modula);
    }

    @Override
    public float get_r(
        int coordinate_x,
        int coordinate_y)
    {
        coordinate_x = positice_modula(coordinate_x, get_width());
        coordinate_y = positice_modula(coordinate_y, get_height());
        return get_r(coordinate_x, coordinate_y);
    }

    @Override
    public float get_g(
        int coordinate_x,
        int coordinate_y)
    {
        coordinate_x = positice_modula(coordinate_x, get_width());
        coordinate_y = positice_modula(coordinate_y, get_height());
        return get_g(coordinate_x, coordinate_y);
    }

    @Override
    public float get_b(
        int coordinate_x,
        int coordinate_y)
    {
        coordinate_x = positice_modula(coordinate_x, get_width());
        coordinate_y = positice_modula(coordinate_y, get_height());
        return get_b(coordinate_x, coordinate_y);
    }

    @Override
    public void set_rgb(
        int coordinate_x,
        int coordinate_y,
        final float r,
        final float g,
        final float b)
    {
        set_rgb(coordinate_x % get_width(), coordinate_y % get_height(), r, g, b);
    }
}
