using System.Drawing;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionGraphics.Rendering;

namespace KozzionGraphics.Renderer
{
    public class RendererImageRaster3DToBitmapZMean<ElementType> : IRendererBitmap<IImageRaster<IRaster3DInteger, ElementType>>
    {

        private IFunction<ElementType, Color> converter;
        private IAlgebraReal<ElementType> algebra;

        public RendererImageRaster3DToBitmapZMean(IFunction<ElementType, Color> converter, IAlgebraReal<ElementType> algebra)
        {
            this.converter = converter;
            this.algebra = algebra;
        }

        public Bitmap Render(IImageRaster<IRaster3DInteger, ElementType> source_image)
        {
            IRaster3DInteger raster = source_image.Raster;
            Bitmap destination_image = new Bitmap(raster.Size0, raster.Size1);
            for (int index_y = 0; index_y < raster.Size1; index_y++)
            {
                for (int index_x = 0; index_x < raster.Size0; index_x++)
                {
                    ElementType mean_value = source_image.GetElementValue(raster.GetElementIndex(index_x, index_y, 0));
                    for (int index_z = 1; index_z < raster.Size2; index_z++)
                    {
                        mean_value = this.algebra.Add(mean_value, source_image.GetElementValue(raster.GetElementIndex(index_x, index_y, index_z)));
                    }
                    mean_value = algebra.Divide(mean_value, this.algebra.ToDomain(raster.Size2));
                    destination_image.SetPixel(index_x, index_y, this.converter.Compute(mean_value));
 
                }
            }
            return destination_image;
        }
    }
}
