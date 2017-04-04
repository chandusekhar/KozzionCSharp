ackage com.kozzion.library.math.numeric.methods.series.api;

public interface IFloatSeriesAccelerator
{
    public void iterate_all(float [] series, int start_index, IFloatSeriesIterator iterator);

    public int iterate_until(float [] series, int start_index, IFloatSeriesIterator iterator, IFloatHaltingCriterion halting_criterion);

}
