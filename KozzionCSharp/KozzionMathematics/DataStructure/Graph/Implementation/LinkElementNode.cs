using System.Collections.Generic;

namespace KozzionMathematics.Datastructure.Graph.implementation
{
	public class NodeTreeLink<ValueType>
	{
		private ValueType                        link_value;
		private int []                           element_indexes;
		private List<NodeTreeLink<ValueType>> childeren_nodes;

		private  NodeTreeLink<ValueType>       parent_node;

		public NodeTreeLink(
			ValueType value,
			int index_element_0,
			int index_element_1)
		{
			link_value = value;
			element_indexes = new int [] {index_element_0, index_element_1};
            childeren_nodes = new List<NodeTreeLink<ValueType>>();
		}

		public NodeTreeLink(
			ValueType value,
			int index_element_0,
			NodeTreeLink<ValueType> node_0)
		{
			link_value = value;
			element_indexes = new int [] {index_element_0};
            childeren_nodes = new List<NodeTreeLink<ValueType>>();
			childeren_nodes.Add(node_0);
			node_0.set_parent(this);
		}

		public NodeTreeLink(
			ValueType value,
			NodeTreeLink<ValueType> node_0,
			NodeTreeLink<ValueType> node_1)
		{
			link_value = value;
			element_indexes = new int [] {};
            childeren_nodes = new List<NodeTreeLink<ValueType>>();
			childeren_nodes.Add(node_0);
			childeren_nodes.Add(node_1);
			node_0.set_parent(this);
			node_1.set_parent(this);
		}

		private void set_parent(
			NodeTreeLink<ValueType> parent)
		{
			parent_node = parent;
		}

		public NodeTreeLink<ValueType> get_parent()
		{
			return parent_node;
		}

		public int [] get_element_indexes()
		{
			return element_indexes;
		}

		public List<NodeTreeLink<ValueType>> get_child_nodes()
		{
			return childeren_nodes;
		}

		public ValueType get_value()
		{
			return link_value;
		}

	}
}