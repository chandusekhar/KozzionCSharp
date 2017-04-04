using System;
using KozzionGraphics.ElementTree.Feature;
namespace KozzionGraphics.ElementTree
{
	public interface IElementTree<ValueType>
		where ValueType : IComparable<ValueType>
	{
		int [] get_alfa_limited_top_level_node_elements(
			int element_index,
			ValueType max_alfa);

		int [] get_alfa_and_size_limited_top_level_node_elements(
			int element_index,
			ValueType max_alfa,
			int max_element);

		Tuple<ValueType [], int []> get_size_series(
			int element_index);

		void generate_features(
            IFeatureGeneratorFloat<IElementTreeNode<ValueType>> generator);

		float [][] get_features();
	}
}