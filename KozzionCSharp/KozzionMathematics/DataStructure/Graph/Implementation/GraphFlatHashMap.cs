using System;
using System.Collections.Generic;

namespace KozzionMathematics.Datastructure.Graph.implementation
{
    public class GraphHashMap<NodeType, EdgeType> : IGraphFlat<NodeType, EdgeType>
    {
        Dictionary<NodeType, HashSet<EdgeType>> d_nodes;
        Dictionary<EdgeType, Tuple<NodeType, NodeType>> d_edges;

        public GraphHashMap() 
        {
            d_nodes = new Dictionary<NodeType, HashSet<EdgeType>>();
            d_edges = new Dictionary<EdgeType, Tuple<NodeType, NodeType>>();
        }

        public void AddNode(NodeType node)
        {
            if(d_nodes.ContainsKey(node))
            {
                throw new Exception("Duplicate node: " + node);
            }
            d_nodes[node] = new HashSet<EdgeType>();
        }

        public void AddEdge(NodeType node_0, NodeType node_1, EdgeType edge)
        {
            if (!d_nodes.ContainsKey(node_0))
            {
                throw new Exception("Missing node: " + node_0);
            }

            if (!d_nodes.ContainsKey(node_1))
            {
                throw new Exception("Missing node: " + node_1);
            }

            if (d_edges.ContainsKey(edge))
            {
                throw new Exception("Duplicate edge: " + edge);
            }
            d_edges[edge] = new Tuple<NodeType, NodeType>(node_0, node_1);
            d_nodes[node_0].Add(edge);
            d_nodes[node_1].Add(edge);
        }

        public IEnumerable<NodeType> Nodes()
        {
            return d_nodes.Keys;
        }

        public Tuple<NodeType, NodeType> Nodes(EdgeType edge)
        {
            return d_edges[edge];
        }

        public IEnumerable<EdgeType> Edges()
        {
            return d_edges.Keys;
        }

        public IEnumerable<EdgeType> Edges(NodeType node)
        {
            return d_nodes[node];
        }
    }
}
