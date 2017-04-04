using System;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;

namespace KozzionGraphics.Image.Topology
{
    public interface ITopologyElementEdge : ITopologyElement
    {
        int ElementCountReal { get; }

        Tuple<EdgeValueType[], EdgeValueType> CreateAlphaPartitionTreeElementArray<ElementValueType, EdgeValueType>(
            IAlgebraReal<EdgeValueType> algebra,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType[] element_values);
    }
}
