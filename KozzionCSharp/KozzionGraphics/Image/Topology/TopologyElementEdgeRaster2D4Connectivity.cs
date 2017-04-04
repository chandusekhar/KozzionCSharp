using KozzionMathematics.Algebra;
using System;
using KozzionMathematics.Function;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image.Topology
{
    public class TopologyElementEdgeRaster2D4Connectivity :   
        ATopologyElementRaster<IRaster2DInteger>,
        ITopologyElementEdgeRaster<IRaster2DInteger>
    {

        private int [] raster_size;
        private int d_x_edge_offset;
        private int d_y_edge_offset;

        public TopologyElementEdgeRaster2D4Connectivity(
            IRaster2DInteger raster)
            : base(raster, 4)
        {
            this.raster_size = raster.SizeArray;
            this.d_x_edge_offset = raster_size[0] * raster_size[1];
            this.d_y_edge_offset = d_x_edge_offset + d_x_edge_offset;
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            if (element_index < d_x_edge_offset)
            {
                if (element_index % raster_size[0] == 0)
                {
                    element_neigbour_array[0] = -1;
                }
                else
                {
                    element_neigbour_array[0] = (element_index + d_x_edge_offset) - 1;
                }

                if (element_index % raster_size[0] == raster_size[0] - 1)
                {
                    element_neigbour_array[1] = -1;
                }
                else
                {
                    element_neigbour_array[1] = element_index + d_x_edge_offset;
                }

                if (element_index / raster_size[0] == 0)
                {
                    element_neigbour_array[2] = -1;
                }
                else
                {
                    element_neigbour_array[2] = (element_index + d_y_edge_offset) - raster_size[0];
                }

                if (element_index / raster_size[0] == raster_size[1] - 1)
                {
                    element_neigbour_array[3] = -1;
                }
                else
                {
                    element_neigbour_array[3] = element_index + d_y_edge_offset;
                }

            }
            else if (element_index < d_y_edge_offset)
            {
                element_neigbour_array[0] = element_index - d_x_edge_offset;
                element_neigbour_array[1] = element_index - d_x_edge_offset + 1;
                element_neigbour_array[2] = -1;
                element_neigbour_array[3] = -1;
            }
            else
            {
                element_neigbour_array[0] = element_index - d_y_edge_offset;
                element_neigbour_array[1] = element_index - d_y_edge_offset + raster_size[0];
                element_neigbour_array[2] = -1;
                element_neigbour_array[3] = -1;
            }

        }

        public override ITopologyElementEdgeRaster<IRaster2DInteger> GetElementEdgeRasterTopology()
        {
            throw new NotImplementedException();
        }

        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            throw new NotImplementedException();
        }

        public override int ElementCount
        {
            get
            {
                return d_x_edge_offset * 3;
            }
        }

        public Tuple<EdgeValueType[], EdgeValueType> CreateAlphaPartitionTreeElementArray<ElementValueType, EdgeValueType>(
            IAlgebraReal<EdgeValueType> algebra,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType[] element_values)
        {
            int element_count = ElementCount;
            EdgeValueType[] new_image = new EdgeValueType[element_count];
            EdgeValueType max_value = algebra.MinValue;

            // do x edges
            for (int element_index = d_x_edge_offset; element_index < d_y_edge_offset; element_index++)
            {
                if (element_index % raster_size[0] != raster_size[0] - 1)
                {
                    new_image[element_index] = edge_function.Compute(
                        element_values[element_index - d_x_edge_offset],
                        element_values[element_index - d_x_edge_offset + 1]);
                    max_value = algebra.Max(max_value, new_image[element_index]);
                }
            }

            // do y edges
            for (int element_index = d_y_edge_offset; element_index < (element_count - raster_size[0]); element_index++)
            {
                new_image[element_index] = edge_function.Compute(
                    element_values[element_index - d_y_edge_offset],
                    element_values[element_index - d_y_edge_offset + raster_size[0]]);
                max_value = algebra.Max(max_value, new_image[element_index]);

            }

            // set node values
            for (int element_index = 0; element_index < d_x_edge_offset; element_index++)
            {
                new_image[element_index] = max_value;
            }

            // flip all edges
            for (int element_index = d_x_edge_offset; element_index < element_count; element_index++)
            {
                new_image[element_index] = algebra.Subtract(max_value, new_image[element_index]);
            }

            return new Tuple<EdgeValueType[], EdgeValueType>(new_image, max_value);
        }


 
    }
}