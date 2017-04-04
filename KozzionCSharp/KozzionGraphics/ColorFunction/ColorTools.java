package com.kozzion.library.graphics.color;

public class ColorTools
{

    public static void integer_to_integer_array(
        final int argb_integer,
        final int [] argb_array)
    {
        argb_array[0] = (argb_integer >> 24) & 0xff; // alfa
        argb_array[1] = (argb_integer >> 16) & 0xff; // red
        argb_array[2] = (argb_integer >> 8) & 0xff; // green
        argb_array[3] = (argb_integer) & 0xff; // blue
    }

    public static void integer_to_float_array(
        int argb_integer,
        float [] argb_array)
    {
        argb_array[0] = (float)((argb_integer >> 24) & 0xff); // alfa
        argb_array[1] = (float)((argb_integer >> 16) & 0xff); // red
        argb_array[2] = (float)((argb_integer >> 8) & 0xff); // green
        argb_array[3] = (float)((argb_integer) & 0xff); // blue      
    }

    public static void integer_to_byte_array(
        final int argb_integer,
        final byte [] argb_array)
    {
        argb_array[0] = (byte) ((argb_integer >> 24) & 0xff); // alfa
        argb_array[1] = (byte) ((argb_integer >> 16) & 0xff); // red
        argb_array[2] = (byte) ((argb_integer >> 8) & 0xff); // green
        argb_array[3] = (byte) ((argb_integer) & 0xff); // blue
    }

    public static int gray_integer_to_argb_integer(
        final int gray_integer)
    {
        return argb_integer(255, gray_integer, gray_integer, gray_integer);
    }

    public static int argb_integer(
        final int alfa,
        final int red,
        final int green,
        final int blue)
    {
        return ((((((alfa * 256) + red) * 256) + green) * 256) + blue);
    }

    public static int get_absolute_color_error(
        final int rgba1,
        final int rgba2)
    {
        final int [] rgba_array1 = new int [4];
        integer_to_integer_array(rgba1, rgba_array1);
        final int [] rgba_array2 = new int [4];
        integer_to_integer_array(rgba2, rgba_array2);
        int error = 0;
        error += Math.abs(rgba_array1[0] - rgba_array2[0]);
        error += Math.abs(rgba_array1[1] - rgba_array2[1]);
        error += Math.abs(rgba_array1[2] - rgba_array2[2]);
        error += Math.abs(rgba_array1[3] - rgba_array2[3]);
        return error;
    }

    /*
     * Conversts floats from the range [0,1)
     */
    public static int floats_to_argb_integer(
        final float float_a,
        final float float_r,
        final float float_g,
        final float float_b)
    {
        final int int_a = (int) Math.floor(float_a * 255f);
        final int int_r = (int) Math.floor(float_r * 255f);
        final int int_g = (int) Math.floor(float_g * 255f);
        final int int_b = (int) Math.floor(float_b * 255f);
        return argb_integer(int_a, int_r, int_g, int_b);
    }

}
