package com.kozzion.library.math.numeric.methods.series;

import com.kozzion.library.math.numeric.methods.series.api.IFloatHaltingCriterion;

public class NeverHaltingCriterion implements IFloatHaltingCriterion
{

    @Override
    public boolean halt(final float [] series, final int last_entry)
    {
        return false;
    }

}
