using KozzionMathematics.Algebra;
using System;
using KozzionMathematics.Function;
using KozzionGraphics.Image.Raster;
namespace KozzionGraphics.Image.Topology
{
    public class TopologyElementEdgeRaster1D :
        ATopologyElementRaster<IRaster1DInteger>,
        ITopologyElementEdgeRaster<IRaster1DInteger>
    {
        public override int ElementCount
        {
            get { return (raster_size * 2) - 1; }
        }

        private int raster_size;
        private int x_edge_offset;


        public TopologyElementEdgeRaster1D(IRaster1DInteger raster)
            : base(raster, 2)
        {
            this.raster_size = raster.ElementCount;
            this.x_edge_offset = raster.ElementCount;
        }


        public override ITopologyElementEdgeRaster<IRaster1DInteger> GetElementEdgeRasterTopology()
        {
            throw new NotImplementedException("Not Implemented");
        }

        public override ITopologyElementEdge GetTopologyElementEdge()
        {
            throw new NotImplementedException("Not Implemented");
        }

        public override void ElementNeighboursRBA(int element_index, int[] element_neigbour_array)
        {
            if (element_index < x_edge_offset)
            {
                if (element_index == 0)
                {
                    element_neigbour_array[0] = -1;
                }
                else
                {
                    element_neigbour_array[0] = element_index + x_edge_offset - 1;
                }

                if (element_index == x_edge_offset - 1)
                {
                    element_neigbour_array[1] = -1;
                }
                else
                {
                    element_neigbour_array[1] = element_index + x_edge_offset;
                }
            }
            else
            {
                element_neigbour_array[0] = element_index - x_edge_offset;
                element_neigbour_array[1] = element_index - x_edge_offset + 1;
            }

        }

        public Tuple<EdgeValueType[], EdgeValueType> CreateAlphaPartitionTreeElementArray<ElementValueType, EdgeValueType>(
            IAlgebraReal<EdgeValueType> algebra, 
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function, 
            ElementValueType[] element_values)
        {
            int element_count = this.ElementCount;
            EdgeValueType [] new_image = new EdgeValueType [element_count];
            EdgeValueType max_value = algebra.MinValue;

            // do x edges
            for (int element_index = x_edge_offset; element_index < element_count; element_index++)
            {
                new_image[element_index] = edge_function.Compute(
                    element_values[element_index - x_edge_offset],
                    element_values[element_index - x_edge_offset + 1]);
                max_value = algebra.Max(max_value, new_image[element_index]);
            }
        
            // set node values
            for (int element_index = 0; element_index < x_edge_offset; element_index++)
            {
                new_image[element_index] = max_value;
            }

            // flip x edges
            for (int element_index = x_edge_offset; element_index < element_count; element_index++)
            {
                new_image[element_index] = algebra.Subtract(max_value, new_image[element_index]);
            }
        
            return new Tuple<EdgeValueType[], EdgeValueType>(new_image, max_value);
        }
    }
}
