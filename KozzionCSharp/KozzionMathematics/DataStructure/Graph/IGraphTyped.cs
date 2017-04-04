namespace KozzionMathematics.Datastructure.Graph
{
    public interface IGraphTyped<GraphType, NodeType, EdgeType> : IGraphFlat<NodeType, EdgeType>
        where GraphType : IGraphTyped<GraphType, NodeType, EdgeType>
        where NodeType : INode<GraphType, NodeType, EdgeType>
        where EdgeType : IEdge<GraphType, NodeType, EdgeType>
    {
    }
}
