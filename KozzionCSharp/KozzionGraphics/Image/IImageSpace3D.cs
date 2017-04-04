namespace KozzionGraphics.Image
{
    public interface IImageSpace3D<CoordinateType, ValueType> : IImageSpace<CoordinateType, ValueType>
    {
        ValueType GetLocationValue(
            CoordinateType coordinate_0,
            CoordinateType coordinate_1,
            CoordinateType coordinate_2);
    }
}
