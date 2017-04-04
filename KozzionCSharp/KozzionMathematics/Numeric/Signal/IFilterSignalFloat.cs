namespace KozzionMathematics.Numeric.Signal
{
    public interface IFilterSignalFloat
    {
        float[] filter(float[] values, float[] time_points);

    }
}