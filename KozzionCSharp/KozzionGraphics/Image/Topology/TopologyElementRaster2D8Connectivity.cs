using KozzionCore.Tools;
using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{

    public class TopologyElementRaster2D8Connectivity : ATopologyElementRaster<IRaster2DInteger>
    {
        private int [] raster_size;

        public override int ElementCount
        {
            get
            {
                return Raster.ElementCount; 
            }
        }

        public TopologyElementRaster2D8Connectivity(IRaster2DInteger raster)
            : base(raster, 8)
        {
            this.raster_size = ToolsCollection.Copy(raster.SizeArray);
        }

        public TopologyElementRaster2D8Connectivity(int [] raster_size)
            : this( new Raster2DInteger(raster_size))
        {
  
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            int index_0 = element_index % raster_size[0];
            int index_1 = element_index / raster_size[0];

            // Orthogonals
            if(index_0 == 0)
            {
                element_neigbour_array[0] = -1;
            }
            else
            {
                element_neigbour_array[0] = element_index - 1;            
            }        
        
            if(index_0 == raster_size[0] - 1)
            {
                element_neigbour_array[1] = -1;
            }
            else
            {
                element_neigbour_array[1] = element_index + 1;            
            }        
        
            if(index_1 == 0)
            {
                element_neigbour_array[2] = -1;
            }
            else
            {
                element_neigbour_array[2] = element_index - raster_size[0];            
            }        
        
            if(index_1 == raster_size[1] - 1)
            {
                element_neigbour_array[3] = -1;
            }
            else
            {
                element_neigbour_array[3] = element_index + raster_size[0];     
            }


            // Diagonals
            if ((index_0 == 0) || (index_1 == 0))
            {
                element_neigbour_array[4] = -1;
            }
            else
            {
                element_neigbour_array[4] = element_index - 1 - raster_size[0];
            }

            if ((index_0 == raster_size[0] - 1) || (index_1 == 0))
            {
                element_neigbour_array[5] = -1;
            }
            else
            {
                element_neigbour_array[5] = element_index + 1 - raster_size[0];
            }

            if ((index_0 == 0) || (index_1 == raster_size[1] - 1))
            {
                element_neigbour_array[6] = -1;
            }
            else
            {
                element_neigbour_array[6] = element_index - 1 + raster_size[0];
            }

            if ((index_0 == raster_size[0] - 1) || (index_1 == raster_size[1] - 1))
            {
                element_neigbour_array[7] = -1;
            }
            else
            {
                element_neigbour_array[7] = element_index + 1 + raster_size[0];
            }
        }

        public override ITopologyElementEdgeRaster<IRaster2DInteger> GetElementEdgeRasterTopology()
        {
            return new TopologyElementEdgeRaster2D4Connectivity(Raster);
        }

        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            return GetElementEdgeRasterTopology();
        }



    }
}