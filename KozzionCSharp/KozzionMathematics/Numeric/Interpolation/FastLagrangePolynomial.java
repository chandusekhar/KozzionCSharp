package com.kozzion.library.math.numeric.interpolation;

import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.math.function.IFunctionFloat;

public class FastLagrangePolynomial
    implements
        IFunctionFloat
{
    int      point_count;
    float [] d_x_coordinates;
    float [] d_lagrange_terms;

    public FastLagrangePolynomial(
        final float [] x_coordinates,
        final float [] y_coordinates)
    {
        if (x_coordinates.length != y_coordinates.length)
        {
            throw new RuntimeException("Array dimension mismatch");
        }
        point_count = x_coordinates.length;
        d_x_coordinates = ArrayTools.copy(x_coordinates);
        d_lagrange_terms = new float [point_count];
        final DividedDifferenceSet divided_differences = new DividedDifferenceSet(x_coordinates, y_coordinates);
        for (int term_index = 0; term_index < point_count; term_index++)
        {
            d_lagrange_terms[term_index] = divided_differences.get_divided_differences(term_index, 0);
        }

    }

    @Override
    public float compute(
        final float input)
    {
        float value = d_lagrange_terms[0];
        float acumulator = 1;
        for (int term_index = 1; term_index < point_count; term_index++)
        {
            acumulator *= (input - d_x_coordinates[term_index - 1]);
            value += d_lagrange_terms[term_index] * acumulator;

        }
        return value;
    }

    @Override
    public Float compute(
        final Float input)
    {

        return compute(input.floatValue());
    }
}
