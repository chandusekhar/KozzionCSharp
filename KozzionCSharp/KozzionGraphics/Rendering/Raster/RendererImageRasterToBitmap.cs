using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using System.Drawing;
using KozzionMathematics.Function;

namespace KozzionGraphics.Renderer
{
    public class RendererImageRasterToBitmap<ElementType>
    {
        private IFunction<ElementType, Color> converter;

        public RendererImageRasterToBitmap(IFunction<ElementType, Color> converter)
        {
            this.converter = converter;
        }

        public Bitmap Render(IImageRaster<IRaster2DInteger, ElementType> source_image) 
        {
            IRaster2DInteger raster =  source_image.Raster;
            Bitmap destination_image = new Bitmap(raster.Size0, raster.Size1);
            for (int index_y = 0; index_y < raster.Size1; index_y++)
            {
                for (int index_x = 0; index_x < raster.Size0; index_x++)
                {
                    destination_image.SetPixel(index_x, index_y, converter.Compute(source_image.GetElementValue(raster.GetElementIndex(index_x, index_y))));
                }
            }
            return destination_image;
        }
    }
}
