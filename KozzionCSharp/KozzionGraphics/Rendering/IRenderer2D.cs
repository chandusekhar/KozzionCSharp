using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Rendering
{
    public interface IRenderer2D<RenderSourceType, ElementValueType>
    {
        IImageRaster<IRaster2DInteger, ElementValueType> Render(RenderSourceType render_source);
    }
}
