using System;
using System.Collections.Generic;

namespace KozzionMathematics.Datastructure.Graph.implementation
{
    public class GraphTypedImplicit<NodeType, EdgeType> : IGraphTyped<GraphTypedImplicit<NodeType, EdgeType>, NodeType, EdgeType>
        where NodeType : INode<GraphTypedImplicit<NodeType, EdgeType>, NodeType, EdgeType>
        where EdgeType : IEdge<GraphTypedImplicit<NodeType, EdgeType>, NodeType, EdgeType>
    {
        HashSet<NodeType> d_nodes;
        HashSet<EdgeType> d_edges;
        public GraphTypedImplicit() 
        {
            d_nodes = new HashSet<NodeType>();
            d_edges = new HashSet<EdgeType>();
        }


        public void AddNode(NodeType node)
        {
            d_nodes.Add(node);
            node.Graph = this;
        }

        public void AddEdge(NodeType node_0, NodeType node_1, EdgeType edge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NodeType> Nodes()
        {
            return d_nodes;
        }

        public Tuple<NodeType, NodeType> Nodes(EdgeType edge)
        {
            return edge.Nodes;
        }

        public IEnumerable<EdgeType> Edges()
        {
            return d_edges;
        }

        public IEnumerable<EdgeType> Edges(NodeType node)
        {
            return node.Edges;
        }
    }
}
