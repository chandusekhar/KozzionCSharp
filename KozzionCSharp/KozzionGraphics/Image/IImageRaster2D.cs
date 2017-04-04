using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image
{
    public interface IImageRaster2D<ElementType> : IImageRaster<IRaster2DInteger, ElementType>
    {
        ElementType GetElementValue(
            int coordinate_0,
            int coordinate_1);

        void SetElementValue(
            int coordinate_0,
            int coordinate_1,
            ElementType value);

    }
}
