using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.SamplingStrategy
{
    public interface ISamplingStrategy<RasterType, RangeType>
              where RasterType : IRasterInteger
    {
        RangeType[][] Sample(IList<IImageRaster<RasterType, RangeType>> images, IImageRaster<RasterType, bool> mask);
  
    }
}
