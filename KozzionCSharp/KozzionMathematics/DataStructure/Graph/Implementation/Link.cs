using System;
namespace KozzionMathematics.Datastructure.Graph.implementation
{
	public class Link<NodeType, LinkValueType> :
            IComparable<Link<NodeType, LinkValueType>>
				where LinkValueType : IComparable<LinkValueType>
	{  
        public NodeType Node_0 { get; private set; }
        public NodeType Node_1 { get; private set; }
        public LinkValueType Value { get; private set; } 

		public Link(
            NodeType index_element_0,
            NodeType index_element_1,
			LinkValueType link_value)
		{
            Node_0 = index_element_0;
            Node_1 = index_element_1;
			Value = link_value;
		}



		public int CompareTo(
            Link<NodeType, LinkValueType> other)
		{
            return Value.CompareTo(other.Value);
		}
	}
}