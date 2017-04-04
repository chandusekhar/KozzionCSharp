package com.kozzion.library.math.signal.filterwindow;

import com.kozzion.library.math.function.IFunctionDouble;
import com.kozzion.library.math.signal.IFilterWindowDouble;

public abstract class AFilterWindowDouble implements IFilterWindowDouble
{
    private final double d_filter_width;

    protected AFilterWindowDouble(final double filter_width)
    {
        d_filter_width = filter_width;
    }

    @Override
    public double get_filter_width()
    {
        return d_filter_width;
    }

    @Override
    public Double compute(final Double input)
    {
        return compute(input.doubleValue());
    }

    @Override
    public double compute(final double input)
    {
        if ((input / 2) < get_filter_width())
        {
            return compute_inside(input);
        }
        else
        {
            return 0;
        }
    }

    protected abstract double compute_inside(double input);

    @Override
    public IFunctionDouble get_fourier_transform()
    {
        return null;
    }

}
