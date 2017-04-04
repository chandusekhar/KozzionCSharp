using System.Drawing;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function;

namespace KozzionGraphics.Renderer
{
    public class RendererImageRaster3DToBitmapSlice<ElementType>
    {
        private IFunction<ElementType, Color> converter;
        private int index_z;

        public RendererImageRaster3DToBitmapSlice(IFunction<ElementType, Color> converter, int index_z)
        {
            this.converter = converter;
            this.index_z = index_z;
        }

        public Bitmap Render(IImageRaster<IRaster3DInteger, ElementType> source_image)
        {
            IRaster3DInteger raster = source_image.Raster;
            Bitmap destination_image = new Bitmap(raster.Size0, raster.Size1);
            for (int index_y = 0; index_y < raster.Size1; index_y++)
            {
                for (int index_x = 0; index_x < raster.Size0; index_x++)
                {  
                    ElementType value = source_image.GetElementValue(raster.GetElementIndex(index_x, index_y, index_z));
                    destination_image.SetPixel(index_x, index_y, this.converter.Compute(value)); 
                }
            }
            return destination_image;
        }
    }
}
