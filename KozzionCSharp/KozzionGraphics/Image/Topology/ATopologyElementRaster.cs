using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{
    public abstract class ATopologyElementRaster<RasterType> : ITopologyElementRaster<RasterType>
        where RasterType : IRasterInteger
    {

        public RasterType Raster {get; private set;}

        public int ElementCountReal { get { return Raster.ElementCount; } }

        public int MaximumConnectivity {get; private set;}

        protected ATopologyElementRaster(RasterType raster, int maximum_connectivity)
        {
            this.Raster = raster;
            this.MaximumConnectivity = maximum_connectivity;
        }

        public abstract ITopologyElementEdgeRaster<RasterType> GetElementEdgeRasterTopology();

        public abstract ITopologyElementEdge GetTopologyElementEdge();

        public abstract void ElementNeighboursRBA(int element_index, int[] element_neigbour_array);

        public abstract int ElementCount { get; }
    }
}