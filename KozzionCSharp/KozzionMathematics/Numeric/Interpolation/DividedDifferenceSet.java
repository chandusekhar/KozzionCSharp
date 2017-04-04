package com.kozzion.library.math.numeric.interpolation;

public class DividedDifferenceSet
{
    float [][] d_divided_differences;

    public DividedDifferenceSet(final float [] x_coordinates, final float [] y_coordinates)
    {
        if (x_coordinates.length != y_coordinates.length)
        {
            throw new RuntimeException("array dimensions do not match");
        }
        d_divided_differences = new float [x_coordinates.length] [x_coordinates.length];
        compute_divided_differences(x_coordinates, y_coordinates, d_divided_differences);

    }

    public static void compute_divided_differences(final float [] x_coordinates, final float [] y_coordinates,
        final float [][] divided_differences)
    {
        for (int term_index = 0; term_index < y_coordinates.length; term_index++)
        {
            divided_differences[0][term_index] = y_coordinates[term_index];
        }
        for (int order_index = 1; order_index < x_coordinates.length; order_index++)
        {
            for (int term_index = 0; term_index < (x_coordinates.length - order_index); term_index++)
            {
                final float lower_x = x_coordinates[term_index];
                final float upper_x = x_coordinates[term_index + order_index];
                final float lower_value = divided_differences[order_index - 1][term_index];
                final float upper_value = divided_differences[order_index - 1][term_index + 1];
                // System.out.println("order " + order_index);
                // System.out.println("lower_x     " + lower_x);
                // System.out.println("upper_x     " + upper_x);
                // System.out.println("lower_value " + lower_value);
                // System.out.println("upper_value " + upper_value);
                final float divided_difference_term = (upper_value - lower_value) / (upper_x - lower_x);
                // System.out.println(divided_difference_term);
                divided_differences[order_index][term_index] = divided_difference_term;
            }
        }
    }

    public float get_divided_differences(final int order, final int number)
    {
        if ((d_divided_differences.length - order) < number)
        {
            throw new RuntimeException("term of order: " + order + " and number: " + number + " does not exist");
        }
        else
        {
            return d_divided_differences[order][number];
        }
    }

}
