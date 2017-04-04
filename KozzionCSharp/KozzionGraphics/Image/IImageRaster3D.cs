using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image
{
    public interface IImageRaster3D<ElementType> : IImageRaster<IRaster3DInteger, ElementType>
    {
        ElementType GetElementValue(
            int coordinate_x,
            int coordinate_y,
            int coordinate_z);

        void SetElementValue(
            int coordinate_x,
            int coordinate_y,
            int coordinate_z,
            ElementType value);

    }
}
