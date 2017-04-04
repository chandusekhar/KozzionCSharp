using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{
    public interface ITopologyElementRaster<RasterType> : ITopologyElement
        where RasterType : IRasterInteger
    {
	
        RasterType Raster {get;}

        ITopologyElementEdgeRaster<RasterType> GetElementEdgeRasterTopology();
	}
}