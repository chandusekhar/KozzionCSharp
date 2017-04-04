using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Rendering
{
    public class Renderer2DIdentity<ElementValueType> : IRenderer2D<IImageRaster<IRaster2DInteger, ElementValueType>, ElementValueType>
    {
        public IImageRaster<IRaster2DInteger, ElementValueType> Render(IImageRaster<IRaster2DInteger, ElementValueType> render_source)
        {
            return render_source;
        }
    }
}
