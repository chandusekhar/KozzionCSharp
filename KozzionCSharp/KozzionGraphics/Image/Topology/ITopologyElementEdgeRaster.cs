using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image.Topology
{
    public interface ITopologyElementEdgeRaster<RasterType>
        : ITopologyElementRaster<RasterType>,
          ITopologyElementEdge
        where RasterType : IRasterInteger
    {


    }
}