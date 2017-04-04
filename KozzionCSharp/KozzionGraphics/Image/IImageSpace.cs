namespace KozzionGraphics.Image
{
    public interface IImageSpace<CoordinateType, ValueType>
    {
        int DimensionCount { get; }

        ValueType GetLocationValue(CoordinateType[] location);
    }
}
