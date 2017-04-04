using KozzionCore.Tools;
using System;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function.Implementation.Distance;
namespace KozzionGraphics.Image.Topology
{
    public class TopologyElementRaster3D26Connectivity
        :
            ATopologyElementRaster<IRaster3DInteger>,
            ITopologyElementMetric
    {
        private int [] raster_size;
        private int size_xy;
        private int d_size_xyz;
        private int x_edge_offset;
        private int y_edge_offset;
        private int z_edge_offset;

        float[] neigbour_distance;
        
        public float[] NeigbourDistanceArray { get { return ToolsCollection.Copy(neigbour_distance); } }

        public override int ElementCount { get { return Raster.ElementCount; } }
     
        public TopologyElementRaster3D26Connectivity(IRaster3DInteger raster, float [] voxel_spacing)
            : base(raster, 26)
        {
            this.raster_size = raster.SizeArray;
            this.size_xy = raster_size[0] * raster_size[1];
            this.d_size_xyz = raster_size[0] * raster_size[1] * raster_size[2];
      
            this.x_edge_offset = d_size_xyz;
            this.y_edge_offset = d_size_xyz + d_size_xyz;
            this.z_edge_offset = d_size_xyz + d_size_xyz + d_size_xyz;

            neigbour_distance = new float [26];
            int neigbour_index = 0;
            for (int x_index = -1; x_index < 2; x_index++)
			{
			    for (int y_index = -1; y_index < 2; y_index++)
			    {
			        for (int z_index = -1; z_index < 2; z_index++)
			        {
			            if ((x_index != 0) || (y_index != 0) || (z_index == 0))
                        {
                            neigbour_distance[neigbour_index] = FunctionDistanceEuclidean.ComputeStatic(new int [] {0,0,0}, new int [] {x_index, y_index, z_index});
                            neigbour_index++;
                        }
			        }
			    }
			}
        }

        public TopologyElementRaster3D26Connectivity(IRaster3DInteger raster)
            : this(raster, new float[] { 1f,1f,1f})
        {
        }


        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            int x_index = element_index % this.Raster.Size0;
            int y_index = (element_index % size_xy) / this.Raster.Size0;
            int z_index = element_index / size_xy;

            int neigbour_index_index = 0;
            for (int z_offset = -1; z_offset < 2; z_offset++)
            {               
                for (int y_offset = -1; y_offset < 2; y_offset++)
                {
                    for (int x_offset = -1; x_offset < 2; x_offset++)
                    {
                        if ((x_offset != 0) || (y_offset != 0) || (z_offset != 0))
                        {
                            int x_index_neigbour = x_index + x_offset;
                            int y_index_neigbour = y_index + y_offset;
                            int z_index_neigbour = z_index + z_offset;
                            if (Raster.ContainsCoordinates(x_index_neigbour, y_index_neigbour, z_index_neigbour))
                            {
                                element_neigbour_array[neigbour_index_index] = Raster.GetElementIndex(x_index_neigbour, y_index_neigbour, z_index_neigbour);
                            }
                            else
                            {
                                element_neigbour_array[neigbour_index_index] = -1;
                            }
                            neigbour_index_index++;
                        }
                    }
                }
            }
        }

        public override ITopologyElementEdgeRaster<IRaster3DInteger> GetElementEdgeRasterTopology()
        {
            throw new NotImplementedException();
        }

        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            throw new NotImplementedException();
        }
       
    }

}