using System;
using System.Collections.Generic;

namespace KozzionMathematics.Datastructure.Graph
{
    public interface IGraphFlat<NodeType, EdgeType>
    {
        void AddNode(NodeType node);
        void AddEdge(NodeType node_0, NodeType node_1, EdgeType edge);

        IEnumerable<NodeType> Nodes();
        Tuple<NodeType, NodeType> Nodes(EdgeType edge);

        IEnumerable<EdgeType> Edges();
        IEnumerable<EdgeType> Edges(NodeType node);
    }
}
