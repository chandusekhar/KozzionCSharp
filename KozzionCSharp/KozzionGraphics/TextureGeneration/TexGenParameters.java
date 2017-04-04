package com.kozzion.library.graphics.texturegeneration;

public class TexGenParameters
{
    public int d_local_x;
    public int d_local_y;
    public int d_width_in;
    public int d_height_in;
    public int d_width_out;
    public int d_height_out;

    public TexGenParameters(final int local_x, final int localy, final int width_in, final int height_in, final int width_out,
        final int height_out)
    {
        d_local_x = local_x;
        d_local_y = localy;
        d_width_in = width_in;
        d_height_in = height_in;
        d_width_out = width_out;
        d_height_out = height_out;

    }
}
