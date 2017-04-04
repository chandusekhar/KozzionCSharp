package com.kozzion.library.math.numeric.interpolation;

import com.kozzion.library.math.function.IFunctionFloat;

public interface IDerivableFloatFunctionInterpolator
{
    IFunctionFloat interpolate(float [] x_coordinates, float [] y_value, float [] derivative_values);

}
