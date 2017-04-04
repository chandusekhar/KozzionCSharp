using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.Rendering.Raster
{
    public class RendererImageSpace4DSliceWrapper<RangeType> : IRenderer2D<IImageSpace<float, RangeType>, RangeType>
    {
        private int index_2;
        private int index_3;

        public RendererImageSpace4DSliceWrapper(int index_2, int index_3)
        {

            this.index_2 = index_2;
            this.index_3 = index_3;
        }

        public IImageRaster<IRaster2DInteger, RangeType> Render(IImageSpace<float, RangeType> source_image)
        {
            //return new ImageRaster2DWrapperImageSpace4D<RangeType>(source_image, this.index_2, this.index_3);
            throw new NotImplementedException();
        }
    }
}
