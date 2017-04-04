using System;

namespace KozzionMathematics.Datastructure.Graph
{
    public interface IEdge<GraphType, NodeType, EdgeType>
        where GraphType : IGraphTyped<GraphType, NodeType, EdgeType>
        where NodeType : INode<GraphType, NodeType, EdgeType>
        where EdgeType : IEdge<GraphType, NodeType, EdgeType>
    {
        GraphType Graph { get; }
        Tuple<NodeType, NodeType> Nodes { get; }
        NodeType Node_0 { get; }
        NodeType Node_1 { get; }
    }
}
