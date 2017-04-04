
namespace KozzionGraphics.Image
{
    public interface IImageSpace4D<CoordinateType, ValueType> : IImageSpace<CoordinateType, ValueType>
	{
        ValueType GetLocationValue(
            CoordinateType coordinate_x, 
            CoordinateType coordinate_y, 
            CoordinateType coordinate_z, 
            CoordinateType coordinate_t);

	}
}