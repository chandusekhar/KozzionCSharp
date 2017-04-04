using KozzionCore.Tools;
using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{

    public class TopologyElementRaster2D4Connectivity : ATopologyElementRaster<IRaster2DInteger>
    {
        private int [] raster_size;

        public override int ElementCount
        {
            get
            {
                return Raster.ElementCount; 
            }
        }

        public TopologyElementRaster2D4Connectivity(IRaster2DInteger raster)
            : base(raster, 4)
        {
            this.raster_size = ToolsCollection.Copy(raster.SizeArray);
        }

        public TopologyElementRaster2D4Connectivity(int [] raster_size)
            : this( new Raster2DInteger(raster_size))
        {
  
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            int index_x = element_index % raster_size[0];
            int index_y = element_index / raster_size[0];

        
            if(index_x == 0)
            {
                element_neigbour_array[0] = -1;
            }
            else
            {
                element_neigbour_array[0] = element_index - 1;            
            }        
        
            if(index_x == raster_size[0] - 1)
            {
                element_neigbour_array[1] = -1;
            }
            else
            {
                element_neigbour_array[1] = element_index + 1;            
            }        
        
            if(index_y == 0)
            {
                element_neigbour_array[2] = -1;
            }
            else
            {
                element_neigbour_array[2] = element_index - raster_size[0];            
            }        
        
            if(index_y == raster_size[1] - 1)
            {
                element_neigbour_array[3] = -1;
            }
            else
            {
                element_neigbour_array[3] = element_index + raster_size[0];     
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