package com.kozzion.library.math.statistics.distribution.interfaces;

import com.kozzion.library.math.function.IFunctionFloatArrayToFloat;

public interface IMultivariateDistributionFloat
{
    public IFunctionFloatArrayToFloat get_density_function();
    public IFunctionFloatArrayToFloat get_culmative_function();

    public float get_probability_density(final float input);
    public float get_cumulative_probability(final float input);
}
