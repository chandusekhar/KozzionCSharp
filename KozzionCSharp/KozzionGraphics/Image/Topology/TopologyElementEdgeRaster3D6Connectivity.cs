using KozzionMathematics.Algebra;
using System;
using KozzionMathematics.Function;
using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{
    public class TopologyElementEdgeRaster3D6Connectivity
        :
            ATopologyElementRaster<IRaster3DInteger>,
            ITopologyElementEdgeRaster<IRaster3DInteger>
    {
        private int [] raster_size;
        private int size_x;
        private int size_xy;
        private int size_xyz;
        private int x_edge_offset;
        private int y_edge_offset;
        private int z_edge_offset;

        public override int ElementCount { get { return x_edge_offset * 4; } }

        public TopologyElementEdgeRaster3D6Connectivity(IRaster3DInteger raster)
            : base(raster, 6)
        {
            this.raster_size = raster.SizeArray;
            this.size_x = raster_size[0];
            this.size_xy = raster_size[0] * raster_size[1];
            this.size_xyz = raster_size[0] * raster_size[1] * raster_size[2];
      
            this.x_edge_offset = 1 * size_xyz;
            this.y_edge_offset = 2 * size_xyz;
            this.z_edge_offset = 3 * size_xyz;
        }



        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            if (element_index < 0)
            {
                throw new Exception("no such element: " + element_index);
            }
            if ((size_xyz * 4) <= element_index)
            {
                throw new Exception("no such element: " + element_index);
            }
        
            if (element_index < x_edge_offset)
            {
                //to x edges
                if (element_index % raster_size[0] == 0)
                {
                    element_neigbour_array[0] = -1;
                }
                else
                {
                    element_neigbour_array[0] = (element_index + x_edge_offset) - 1;
                }

                if (element_index % raster_size[0] == raster_size[0] - 1)
                {
                    element_neigbour_array[1] = -1;
                }
                else
                {
                    element_neigbour_array[1] = element_index + x_edge_offset;
                }

            
                //to y edges
                if ((element_index % size_xy)/ raster_size[0] == 0)
                {
                    element_neigbour_array[2] = -1;
                }
                else
                {
                    element_neigbour_array[2] = (element_index + y_edge_offset) - raster_size[0];
                }

                if ((element_index % size_xy) / raster_size[0] == raster_size[1] - 1)
                {
                    element_neigbour_array[3] = -1;
                }
                else
                {
                    element_neigbour_array[3] = element_index + y_edge_offset;
                }

                //to z edges
                if (element_index / size_xy == 0)
                {
                    element_neigbour_array[4] = -1;
                }
                else
                {
                    element_neigbour_array[4] = (element_index + z_edge_offset) - size_xy;
                }

                if (element_index / size_xy == raster_size[2] - 1)
                {
                    element_neigbour_array[5] = -1;
                }
                else
                {
                    element_neigbour_array[5] = element_index + z_edge_offset;
                }

            }
            else if (element_index < y_edge_offset) // x edge
            {
                element_neigbour_array[0] = element_index - x_edge_offset;
                element_neigbour_array[1] = element_index - x_edge_offset + 1;
                element_neigbour_array[2] = -1;
                element_neigbour_array[3] = -1;
                element_neigbour_array[4] = -1;
                element_neigbour_array[5] = -1;
            }
            else if (element_index < z_edge_offset) // y edge
            {
                element_neigbour_array[0] = element_index - y_edge_offset;
                element_neigbour_array[1] = element_index - y_edge_offset + size_x;
                element_neigbour_array[2] = -1;
                element_neigbour_array[3] = -1;
                element_neigbour_array[4] = -1;
                element_neigbour_array[5] = -1;
            }
            else // z edge        
            {
                element_neigbour_array[0] = element_index - z_edge_offset;
                element_neigbour_array[1] = element_index - z_edge_offset + size_xy;
                element_neigbour_array[2] = -1;
                element_neigbour_array[3] = -1;
                element_neigbour_array[4] = -1;
                element_neigbour_array[5] = -1;
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



        public Tuple<EdgeValueType[], EdgeValueType> CreateAlphaPartitionTreeElementArray<ElementValueType, EdgeValueType>(
            IAlgebraReal<EdgeValueType> algebra,     
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType [] element_values)
        {
            int element_count = this.ElementCount;
            EdgeValueType[] element_and_edge_values = new EdgeValueType[element_count];
            
            EdgeValueType max_value = algebra.MinValue;

            // do x edges
            for (int element_index = x_edge_offset; element_index < y_edge_offset; element_index++)
            {
                if (element_index % raster_size[0] != raster_size[0] - 1)
                {
                    element_and_edge_values[element_index] = edge_function.Compute(
                        element_values[element_index - x_edge_offset],
                        element_values[element_index - x_edge_offset + 1]);
                    max_value = algebra.Max(max_value, element_and_edge_values[element_index]);
                }
            }

            // do y edges
            for (int element_index = y_edge_offset; element_index < z_edge_offset; element_index++)
            {
                if ((element_index % size_xy) / raster_size[0] != raster_size[1] - 1)
                {
                    element_and_edge_values[element_index] = edge_function.Compute(
                        element_values[element_index - y_edge_offset], 
                        element_values[element_index - y_edge_offset + this.size_x]);
                    max_value = algebra.Max(max_value, element_and_edge_values[element_index]);
                }
            }

            // do z edges
            for (int element_index = z_edge_offset; element_index < (element_and_edge_values.Length - size_xy); element_index++)
            {
                    element_and_edge_values[element_index] = edge_function.Compute(
                        element_values[element_index - z_edge_offset],
                        element_values[element_index - z_edge_offset + this.size_xy]);
                    max_value = algebra.Max(max_value, element_and_edge_values[element_index]);  
            }

            // set real node values to max value
            for (int element_index = 0; element_index < x_edge_offset; element_index++)
            {
                element_and_edge_values[element_index] = max_value;
            }

            // flip all edges
            for (int element_index = x_edge_offset; element_index < element_count; element_index++)
            {
                element_and_edge_values[element_index] = algebra.Subtract(max_value, element_and_edge_values[element_index]);
            }

            return new Tuple<EdgeValueType[], EdgeValueType>(element_and_edge_values, max_value);
        }






   
    }

}