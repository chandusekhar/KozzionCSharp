package com.kozzion.library.math.numeric.interpolation;

import com.kozzion.library.math.function.IFunctionFloat;

public class CubicHermiteFuction implements IFunctionFloat
{

    @Override
    public float compute(final float input)
    {
        // find cubic which hermite to use

        // us it
        return 0;
    }

    @Override
    public Float compute(final Float input)
    {
        return compute(input.floatValue());
    }

}
