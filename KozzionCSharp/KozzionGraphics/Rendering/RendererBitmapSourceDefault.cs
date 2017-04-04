using KozzionMathematics.Function;
using System.Drawing;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image;

namespace KozzionGraphics.Rendering
{
    public class RendererBitmapSourceDefault<ElementValueType> : RendererBitmapSource<IImageRaster<IRaster2DInteger, ElementValueType>, ElementValueType>, IRendererBitmapSource<IImageRaster<IRaster2DInteger, ElementValueType>> 
    {
        public RendererBitmapSourceDefault(IFunction<ElementValueType, Color> converter)
            : base(new Renderer2DIdentity<ElementValueType>(), converter)
        {
        }      
    }
}
