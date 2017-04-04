package com.kozzion.library.math.numeric.interpolation;

import com.kozzion.library.math.function.IFunctionFloat;

public interface IFloatFunctionInterpolator
{
    IFunctionFloat interpolate(float [] x_coordinates, float [] y_coordinates);

}
