using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image
{
    public interface IImageRaster4D<ElementType> : IImageRaster<IRaster4DInteger, ElementType>
    {
        ElementType GetElementValue(
            int coordinate_x,
            int coordinate_y,
            int coordinate_z,
            int coordinate_t);

        void SetElementValue(
            int coordinate_x,
            int coordinate_y,
            int coordinate_z,
            int coordinate_t,
            ElementType value);

    }
}
