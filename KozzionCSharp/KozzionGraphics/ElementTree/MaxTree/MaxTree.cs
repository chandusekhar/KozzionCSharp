using System;
using System.Collections.Generic;
using KozzionGraphics.ElementTree.MaxTree.Filter;

namespace KozzionGraphics.ElementTree.MaxTree
{
    [Serializable]
    public class MaxTree<ElementType> : IMaxTree<ElementType>
    {
        private IComparer<ElementType> element_value_comparer;
        private MaxTreeNode<ElementType>[] d_elements_to_nodes; // contains only node references
        private List<MaxTreeNode<ElementType>> node_list_bottom_to_top;
        public int FullElementCount {get; private set;} // includes virtual elements
        public int RealElementCount { get; private set; } // excluding virtual elements
        public int NodeCount { get { return node_list_bottom_to_top.Count; } }


        public IMaxTreeNode<ElementType> BottomLevelNode { get { return this.node_list_bottom_to_top[0]; } } //TODO remove


        //TODO make array of nodes bottom to upper at sequence of indexing, this will make iteration over them up or down simpler!!!

        public MaxTree(MaxTreeNode<ElementType> bottom_level_node, int full_element_count, int real_element_count, IComparer<ElementType> element_value_comparer)
        {
            this.element_value_comparer = element_value_comparer;
            this.FullElementCount = full_element_count; // includes virtual elements
            this.RealElementCount = real_element_count;

            this.d_elements_to_nodes = new MaxTreeNode<ElementType>[full_element_count];
            foreach (int element in bottom_level_node.GetElementIndexArrayNodeFull()) //!Note unused element in alfa trees
            {
                this.d_elements_to_nodes[element] = bottom_level_node;
            }
            bottom_level_node.ComputePrimitives(this);

            this.node_list_bottom_to_top = bottom_level_node.GetCulmativeChildrenBottomToTopInner();
            for (int node_index = 0; node_index < node_list_bottom_to_top.Count; node_index++)
            {
                MaxTreeNode<ElementType> node = node_list_bottom_to_top[node_index];
                node.NodeIndex = node_index;
                foreach (int element_index in node.GetElementIndexArrayNodeFull())
                {
                    this.d_elements_to_nodes[element_index] = node;
                }

            }
        }

        public ElementType GetDisplayValueElement(
            int element_index)
        {
            return this.d_elements_to_nodes[element_index].DisplayValue;
        }

        public ElementType[] GetDisplayValues()
        {
            ElementType[] display_values = new ElementType[this.RealElementCount];
            for (int element_index = 0; element_index < RealElementCount; element_index++)
            {
                display_values[element_index] = this.d_elements_to_nodes[element_index].DisplayValue;
            }
            return display_values;
        }


        //public void do_k_adsorption(
        //    float k_value,
        //    MaxTreeNode<ElementType>Filter filter)
        //{
        //    d_bottom_level_node.k_adsorption(k_value, filter);
        //}


        //public void do_k_substractive(
        //    float k_value,
        //    IMaxTreeNode<ElementType>Filter filter)
        //{
        //    d_bottom_level_node.k_substractive(k_value, filter);
        //}


        public int [] GetFullElementsIndexesOfElementLevelAndAbove(
            int element_index)
        {
            return this.d_elements_to_nodes[element_index].GetElementIndexArrayCulmativeFull();
        }


        public int [] GetRealElementsIndexesOfElementLevelAndAbove(
            int element_index)
        {
            return this.d_elements_to_nodes[element_index].GetElementIndexArrayCulmativeReal();
        }


        public int[] GetFullElementsIndexesOfElementLevelAndAbove(
            int element_index,
            ElementType level)
        {
            IMaxTreeNode<ElementType> node = this.d_elements_to_nodes[element_index];
            while ((node.Parent != null) && (this.element_value_comparer.Compare(level,node.Parent.Value) == -1))
            {
                node = node.Parent;
            }
            return node.GetElementIndexArrayCulmativeFull();
        }


        public int[] GetRealElementsIndexesOfElementLevelAndAbove(
            int element_index,
            ElementType level)
        {
            IMaxTreeNode<ElementType> node = this.d_elements_to_nodes[element_index];
            while ((node.Parent != null) && (this.element_value_comparer.Compare(level, node.Parent.Value) == -1))
            {
                node = node.Parent;
            }
            return node.GetElementIndexArrayCulmativeReal();
        }


        public Tuple<IList<ElementType>, IList<int>> get_real_culmative_size_series(
            int element_index)
        {
            List<ElementType> node_levels = new List<ElementType>();
            List<int> node_sizes = new List<int>();

            IMaxTreeNode<ElementType> node = this.d_elements_to_nodes[element_index];
            node_levels.Add(node.Value);
            node_sizes.Add(node.CulmativeRealSize);
            while (node.Parent != null)
            {
                node = node.Parent;
                node_levels.Add(node.Value);
                node_sizes.Add(node.CulmativeRealSize);
            }
            return new Tuple<IList<ElementType>, IList<int>>(node_levels, node_sizes);
        }
 

        //public void FilterAbsorbing(IFunction<IMaxTreeNode<ElementType>, bool> filter)
        //{
        //    node_list_bottom_to_top[0].FilterAbsorbing(filter);
        //}


        public void Filter(IMaxTreeFilter<ElementType> filter)
        {
            filter.Filter(node_list_bottom_to_top);
        }

        //public void generate_features(
        //    IFeatureGeneratorFloat<IElementNode> generator)
        //{
        //    d_features = new float [d_max_tree_nodes.size()] [generator.get_feature_count()];
        //    d_bottom_level_node.generate_features(generator);
        //}


        //public float [][] get_features()
        //{
        //    if (d_features == null)
        //    {
        //        throw new RuntimeException("Ne features yet");
        //    }
        //    else
        //    {
        //        return d_features;
        //    }
        //}


        //public void filter_element_values(
        //    IFilterElement<IElementNode> filter)
        //{
        //    d_bottom_level_node.filter_element_values(filter);
        
        //}    


        public IMaxTreeNode<ElementType> GetNode(int element_index)
        {
            return d_elements_to_nodes[element_index];
        }
    }
}
