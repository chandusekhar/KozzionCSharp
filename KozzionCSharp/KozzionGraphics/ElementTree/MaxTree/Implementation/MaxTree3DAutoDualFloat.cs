using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Algebra;
using KozzionCore.DataStructure;

namespace KozzionGraphics.ElementTree.MaxTree.Implementation
{
    public class MaxTree3DAutoDualFloat : MaxTreeAutoDual<float>
    {
        IRaster3DInteger raster;

        public MaxTree3DAutoDualFloat(IImageRaster<IRaster3DInteger, float> image, IProgressReporter reporter) 
          : base(new AlgebraRealFloat32(), new MaxTreeBuilderSingleQueue<float>(), new TopologyElementRaster3D6Connectivity(image.Raster), image.GetElementValues(true), reporter)
        {
           this.raster = image.Raster;
        }

         public MaxTree3DAutoDualFloat(IImageRaster<IRaster3DInteger, float> image) 
          : base(new AlgebraRealFloat32(), new MaxTreeBuilderSingleQueue<float>(), new TopologyElementRaster3D6Connectivity(image.Raster), image.GetElementValues(true), null)
        {
           this.raster = image.Raster;
        }
    
        public ImageRaster3D<float> GetFilteredImage()
        {
            return new ImageRaster3D<float>(raster, GetDisplayValues(), false);
        }
    }
}
