using KozzionCore.Tools;
using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{

    public class TopologyElementRaster3D6Connectivity : ATopologyElementRaster<IRaster3DInteger>, ITopologyElementMetric
    {
        private int [] raster_size;
        private int size_xy;

        float[] neigbour_distance;

        public float[] NeigbourDistanceArray { get { return ToolsCollection.Copy(neigbour_distance); } }

        public override int ElementCount
        {
            get
            {
                return Raster.ElementCount;
            }
        }


        public TopologyElementRaster3D6Connectivity(IRaster3DInteger raster, float[] voxel_spacing)
            : base(raster, 6)
        {
            this.raster_size = ToolsCollection.Copy(raster.SizeArray);
            this.size_xy = raster_size[0] * raster_size[1];
            this.neigbour_distance = new float[] { voxel_spacing[0], voxel_spacing[0], voxel_spacing[1], voxel_spacing[1], voxel_spacing[2], voxel_spacing[2] };

        }

         public TopologyElementRaster3D6Connectivity(IRaster3DInteger raster)
            : this(raster, new float[]{1.0f, 1.0f, 1.0f})
        {

        }

        public TopologyElementRaster3D6Connectivity(int [] raster_size)
            : this(new Raster3DInteger(raster_size))
        {
  
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {    
            //TODO unsafe
            int index_x = element_index % raster_size[0];
            int index_y = (element_index % size_xy) / raster_size[0];
            int index_z = element_index /  size_xy;
        
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
        
        
            if(index_z == 0)
            {
                element_neigbour_array[4] = -1;
            }
            else
            {
                element_neigbour_array[4] = element_index - size_xy;            
            }        
        
            if(index_z == raster_size[2] - 1)
            {
                element_neigbour_array[5] = -1;
            }
            else
            {            
                element_neigbour_array[5] = element_index + size_xy;       
            }
        }

        public override ITopologyElementEdgeRaster<IRaster3DInteger> GetElementEdgeRasterTopology()
        {
            return new TopologyElementEdgeRaster3D6Connectivity(this.Raster);
        }


        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            return new TopologyElementEdgeRaster3D6Connectivity(this.Raster);
        }
    }
}