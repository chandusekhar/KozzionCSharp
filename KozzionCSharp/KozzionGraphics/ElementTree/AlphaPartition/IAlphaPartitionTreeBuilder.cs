using System;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Function;
using KozzionCore.DataStructure;

namespace KozzionGraphics.ElementTree.AlphaPartition
{
    public interface IAlphaPartitionTreeBuilder<ElementValueType, EdgeValueType>
        where EdgeValueType : IComparable<EdgeValueType>
    {
        IAlphaPartitionTree<EdgeValueType> BuildAlphaPartitionTree(
            ITopologyElementEdge element_topology,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType[] element_values);

        IAlphaPartitionTree<EdgeValueType> BuildAlphaPartitionTree(
            ITopologyElementEdge element_topology,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType[] element_values,
            IProgressReporter reporter);

        IAlphaPartitionTree<EdgeValueType> BuildAlphaPartitionTree(
            ITopologyElementEdge element_topology,
            EdgeValueType[] element_and_egde_values,
            EdgeValueType max_edge_value, 
            IProgressReporter reporter);


    }
}