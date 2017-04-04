using KozzionGraphics.ColorFunction;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Tools;
using KozzionMathematics.Function;
using System.Windows.Media.Imaging;

namespace KozzionGraphics.Rendering
{
    public class RendererBitmapSource<RenderSourceType, ElementValueType> : IRendererBitmapSource<RenderSourceType>
    {
        IRenderer2D<RenderSourceType, ElementValueType> inner_renderer;
        IFunction<ElementValueType, System.Drawing.Color> converter;
        public RendererBitmapSource(IRenderer2D<RenderSourceType, ElementValueType> inner_renderer, IFunction<ElementValueType, System.Drawing.Color> converter)
        {
            this.inner_renderer = inner_renderer;
            this.converter = converter;
        }

        public BitmapSource Render(RenderSourceType render_source)
        {
            IImageRaster<IRaster2DInteger, ElementValueType> rendered_image = inner_renderer.Render(render_source);
            BitmapFast bitmap_fast = new BitmapFast(rendered_image.Raster.Size0, rendered_image.Raster.Size1);
            bitmap_fast.Lock();
            for (int y_index = 0; y_index < rendered_image.Raster.Size1; y_index++)
			{
			    for (int x_index = 0; x_index < rendered_image.Raster.Size0; x_index++)
			    {
                    bitmap_fast.SetPixel(x_index, y_index, converter.Compute(rendered_image.GetElementValue(rendered_image.Raster.GetElementIndex(x_index, y_index))));
			    }
			}
            bitmap_fast.Unlock();
            return ToolsRendering.CreateBitmapSourceFromBitmap(bitmap_fast.Bitmap);
        }


    }
}
