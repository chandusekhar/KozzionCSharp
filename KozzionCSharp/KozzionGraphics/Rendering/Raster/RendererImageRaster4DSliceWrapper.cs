using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Rendering;

namespace KozzionGraphics.Renderer.Raster
{
    public class RendererImageRaster4DSliceWrapper<RangeType> : IRenderer2D<IImageRaster<IRaster4DInteger, RangeType>, RangeType>
    {
        private int index_2;
        private int index_3;

        public RendererImageRaster4DSliceWrapper(int index_2, int index_3)
        {

            this.index_2 = index_2;
            this.index_3 = index_3;
        }

        public IImageRaster<IRaster2DInteger, RangeType> Render(IImageRaster<IRaster4DInteger, RangeType> source_image)
        {
            return new ImageRaster2DWrapperSlice4D<RangeType>(source_image, this.index_2, this.index_3);
        }
    }
}
