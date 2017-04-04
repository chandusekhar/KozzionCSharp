using KozzionCore.Tools;
using KozzionGraphics.Image.Raster;
using System;

namespace KozzionGraphics.Image.Topology
{

    public class TopologyElementRaster4D8Connectivity : ATopologyElementRaster<IRaster4DInteger>, ITopologyElement
    {
        private int [] raster_size;
        private int size_01;
        private int size_012;

        public override int ElementCount
        {
            get
            {
                return Raster.ElementCount;
            }
        }


        public TopologyElementRaster4D8Connectivity(IRaster4DInteger raster)
            : base(raster, 8)
        {
            this.raster_size = ToolsCollection.Copy(raster.SizeArray);
            this.size_01 = raster_size[0] * raster_size[1];
            this.size_012 = size_01 * raster_size[2];
        }

      

        public TopologyElementRaster4D8Connectivity(int [] raster_size)
            : this(new Raster4DInteger(raster_size))
        {
  
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            //TODO unsafe check bounds properly
            int index_0 = element_index % raster_size[0];
            int index_1 = (element_index % (size_01)) / raster_size[0];
            int index_2 = (element_index % (size_012)) / (size_01);
            int index_3 = element_index / (size_012);

            if (index_0 == 0)
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


            if (index_2 == 0)
            {
                element_neigbour_array[4] = -1;
            }
            else
            {
                element_neigbour_array[4] = element_index - size_01;
            }

            if (index_2 == raster_size[2] - 1)
            {
                element_neigbour_array[5] = -1;
            }
            else
            {
                element_neigbour_array[5] = element_index + size_01;
            }


            if (index_3 == 0)
            {
                element_neigbour_array[6] = -1;
            }
            else
            {
                element_neigbour_array[6] = element_index - size_012;
            }

            if (index_3 == raster_size[3] - 1)
            {
                element_neigbour_array[7] = -1;
            }
            else
            {
                element_neigbour_array[7] = element_index + size_012;
            }
        }

        public override ITopologyElementEdgeRaster<IRaster4DInteger> GetElementEdgeRasterTopology()
        {
            throw new NotImplementedException();
            //return new TopologyElementEdgeRaster3D6Connectivity(this.Raster);
        }


        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            throw new NotImplementedException();
            //return new TopologyElementEdgeRaster3D6Connectivity(this.Raster);
        }
    }
}