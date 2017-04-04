using System.Collections.Generic;
using System.Drawing;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Rendering;
using KozzionMathematics.Function;

namespace KozzionGraphics.Renderer
{
    public class RendererImageRaster3DToBitmapZMIP<ElementType> : IRendererBitmap<IImageRaster<IRaster3DInteger, ElementType>>
    {
        private IFunction<ElementType, Color> converter;
        private IComparer<ElementType> comparer;

        public RendererImageRaster3DToBitmapZMIP(IFunction<ElementType, Color> converter, IComparer<ElementType> comparer)
        {
            this.converter = converter;
            this.comparer = comparer;
        }

        public Bitmap Render(IImageRaster<IRaster3DInteger, ElementType> source_image)
        {
            IRaster3DInteger raster = source_image.Raster;
            Bitmap destination_image = new Bitmap(raster.Size0, raster.Size1);
            for (int index_y = 0; index_y < raster.Size1; index_y++)
            {
                for (int index_x = 0; index_x < raster.Size0; index_x++)
                {
                    ElementType max_value = source_image.GetElementValue(raster.GetElementIndex(index_x, index_y, 0));
                    for (int index_z = 1; index_z < raster.Size2; index_z++)
                    {
                        ElementType value = source_image.GetElementValue(raster.GetElementIndex(index_x, index_y, index_z));
                        if (this.comparer.Compare(max_value, value) == -1)
                        {
                            max_value = value;
                        }
                    }
                    destination_image.SetPixel(index_x, index_y, this.converter.Compute(max_value));
 
                }
            }
            return destination_image;
        }

    
    }
}
