package com.kozzion.library.math.numeric.interpolation;

import com.kozzion.library.math.function.IFunctionFloat;

public class LagrangeInterpolatorFloat implements IFloatFunctionInterpolator
{
    @Override
    public IFunctionFloat interpolate(final float [] x_coordinates, final float [] y_coordinates)
    {
        return new LagrangePolynomial(x_coordinates, y_coordinates);
    }

}
