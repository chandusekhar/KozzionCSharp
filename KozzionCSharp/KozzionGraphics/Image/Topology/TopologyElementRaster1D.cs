using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{
    public class TopologyElementRaster1D :
        ATopologyElementRaster<IRaster1DInteger>,
            ITopologyElement
    {
        private int size_x;

        public override int ElementCount
        {
            get
            {
                return Raster.ElementCount;
            }
        }


        public TopologyElementRaster1D(
            IRaster1DInteger raster)
            : base(raster, 2)
        {
            size_x = raster.ElementCount;
        }

        public override ITopologyElementEdgeRaster<IRaster1DInteger> GetElementEdgeRasterTopology()
        {
            return new TopologyElementEdgeRaster1D(Raster);
        }

        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            return GetElementEdgeRasterTopology();
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            element_neigbour_array[0] = element_index - 1;
            element_neigbour_array[1] = element_index + 1;

            if (element_index == 0)
            {
                element_neigbour_array[0] = -1;
            }

            if (element_index == size_x - 1)
            {
                element_neigbour_array[1] = -1;
            }
        }
    }
}
