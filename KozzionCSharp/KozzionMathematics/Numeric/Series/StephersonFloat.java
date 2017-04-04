package com.kozzion.library.math.numeric.methods.series;

import com.kozzion.library.math.numeric.methods.series.api.IFloatHaltingCriterion;
import com.kozzion.library.math.numeric.methods.series.api.IFloatSeriesAccelerator;
import com.kozzion.library.math.numeric.methods.series.api.IFloatSeriesIterator;

public class StephersonFloat implements IFloatSeriesAccelerator
{

    @Override
    public void iterate_all(final float [] series, final int start_index, final IFloatSeriesIterator iterator)
    {
        iterate_until(series, start_index, iterator, new NeverHaltingCriterion());
    }

    /**
     * @return the index past the last number completed before halting -1 of halting criterion was never true.
     */
    @Override
    public int iterate_until(final float [] series, final int start_index, final IFloatSeriesIterator iterator,
        final IFloatHaltingCriterion halting_criterion)
    {
        int entry_count = start_index;

        for (int index = start_index; index < series.length; index++)
        {

            if (2 < entry_count)
            {
                series[index] = compute_aitkens_term(series[index - 1], series[index - 2], series[index - 3]);
                entry_count = 1;
            }
            else
            {
                iterator.iterate_once(series, index);
                entry_count++;
            }

            if (halting_criterion.halt(series, index))
            {
                return index + 1;
            }
        }

        return -1;
    }

    /**
     * @return the next aitkens term
     */
    private float compute_aitkens_term(final float value_0, final float value_1, final float value_2)
    {
        return value_0 - (((value_1 - value_0) * (value_1 - value_0)) / ((value_2 - (2 * value_1)) + value_0));
    }

}
