package com.kozzion.library.math.numeric.interpolation;

import com.kozzion.library.math.function.IFunctionFloat;

public class LagrangePolynomial implements IFunctionFloat
{
    int      point_count;
    float [] d_x_coordinates;
    float [] d_y_coordinates;

    public LagrangePolynomial(final float [] x_coordinates, final float [] y_coordinates)
    {
        if (x_coordinates.length != y_coordinates.length)
        {
            throw new RuntimeException("Array dimension mismatch");
        }
        point_count = x_coordinates.length;
        d_x_coordinates = x_coordinates;
        d_y_coordinates = y_coordinates;
    }

    @Override
    public float compute(final float input)
    {
        float value = 0.0f;
        for (int point_index = 0; point_index < point_count; point_index++)
        {
            value += d_y_coordinates[point_index] * lagrange_term(input, point_index);

        }
        return value;
    }

    private float lagrange_term(final float input, final int point_index)
    {
        float term = 1.0f;
        for (int term_index = 0; term_index < point_count; term_index++)
        {
            if (term_index != point_index)
            {
                term *= (input - d_x_coordinates[term_index]) / (d_x_coordinates[point_index] - d_x_coordinates[term_index]);
            }

        }
        return term;
    }

    @Override
    public Float compute(final Float input)
    {
        return compute(input.floatValue());
    }
}
