using System.Collections.Generic;

namespace KozzionMathematics.Datastructure.Graph
{
    public interface INode<GraphType, NodeType, EdgeType>
        where GraphType : IGraphTyped<GraphType, NodeType, EdgeType>
        where NodeType : INode<GraphType, NodeType, EdgeType>
        where EdgeType : IEdge<GraphType, NodeType, EdgeType>
    {
        GraphType Graph { get; set; }
        IEnumerable<EdgeType> Edges { get;}
        void AddEdge(EdgeType edge);
        void RemoveEdge(EdgeType edge);
    }
}
