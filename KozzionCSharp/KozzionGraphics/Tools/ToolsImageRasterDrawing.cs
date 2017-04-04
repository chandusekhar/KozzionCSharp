using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Algebra;

namespace KozzionGraphics.Tools
{
    public class ToolsImageRasterDrawing
    {
        public static void SetOverlay<RasterType, DomainType> (
            IImageRaster<RasterType, DomainType> destination,
            IImageRaster<RasterType, bool> overlay_mask,
            DomainType overlay_value)
              where RasterType : IRasterInteger
        {
            if (!destination.Raster.Equals(overlay_mask.Raster))
            {
                throw new Exception("Raster mismatch");
            }

            Parallel.For(0, overlay_mask.Raster.ElementCount, element_index =>
            {
                if (overlay_mask.GetElementValue(element_index))
                {
                    destination.SetElementValue(element_index, overlay_value);
                }
            });
            
        }


        public static void SetOverlay<RasterType, DomainType>(
            IImageRaster<RasterType, DomainType> destination, 
            IImageRaster<RasterType, bool> overlay_mask,
            IImageRaster<RasterType, DomainType> overlay_values)
                 where RasterType : IRasterInteger
        {
            if (!destination.Raster.Equals(overlay_mask.Raster))
            {
                throw new Exception("Raster mismatch");
            }

            if (!destination.Raster.Equals(overlay_values.Raster))
            {
                throw new Exception("Raster mismatch");
            }

            Parallel.For(0, overlay_mask.Raster.ElementCount, element_index =>
            {
                if (overlay_mask.GetElementValue(element_index))
                {
                    destination.SetElementValue(element_index, overlay_values.GetElementValue(element_index));
                }
            });
        }
    }
}
