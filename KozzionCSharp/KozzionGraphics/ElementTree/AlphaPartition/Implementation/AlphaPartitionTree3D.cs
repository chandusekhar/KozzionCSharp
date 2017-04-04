using System;
using System.Collections.Generic;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Function;
namespace KozzionGraphics.ElementTree.AlphaPartition.Implementation
{
    public class AlphaPartitionTree3D<ElementValueType, EdgeValueType>
        where EdgeValueType : IComparable<EdgeValueType>
    {
        private IAlphaPartitionTree<EdgeValueType> tree;

        public AlphaPartitionTree3D(
            IAlphaPartitionTreeBuilder<ElementValueType, EdgeValueType> builder,
            ITopologyElementEdgeRaster<IRaster3DInteger> topology,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            IImageRaster<IRaster3DInteger, ElementValueType> image)
        {
            //TODO make sure topology matches image
            ElementValueType[] element_values = image.GetElementValues(false);
            tree = builder.BuildAlphaPartitionTree(topology, edge_function, element_values);
        }

        public void ComputeFeatures()
        {
            throw new NotImplementedException();
        }


        public IList<IElementTreeNode<float>> GetFirstSatisfiers(IFunction<IElementTreeNode<float>, bool> node_detector)
        {

            return null;
        }

        public IList<IElementTreeNode<float>> GetSmallestSatisfiers(IFunction<IElementTreeNode<float>, bool> node_detector)
        {
            IList<IElementTreeNode<float>> smallestt_satisfiers = new List<IElementTreeNode<float>>();
            return smallestt_satisfiers;
        }
    }
}