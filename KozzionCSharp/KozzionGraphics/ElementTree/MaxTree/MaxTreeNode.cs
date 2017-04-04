using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KozzionGraphics.ElementTree.MaxTree
{
    [Serializable]
    public class MaxTreeNode<ElementType> : IMaxTreeNode<ElementType>
    {
        public IMaxTree<ElementType> MaxTree { get; private set; }   // The maxtree this node is part of

        public IComparer<ElementType> element_value_comparer;

        public ElementType Value { get; private set; }
        public ElementType DisplayValue { get; set; }
        public ElementType PeakComponentValue { get; private set; }

        public IList<int> NodeElementIndexes { get; private set; }   // Element indexes contained in this node
        private IList<MaxTreeNode<ElementType>> children;

        public int NodeFullSize { get; private set; }
        public int NodeRealSize { get; private set; }

        public int CulmativeFullSize { get; private set; }
        public int CulmativeRealSize { get; private set; }
        public int NodeIndex { get; set; }

        private MaxTreeNode<ElementType> parent;
        public IMaxTreeNode<ElementType> Parent { get {return parent;} } // A node that evaluates lower

        public MaxTreeNode(ElementType node_value, IList<int> elements_indexes, IList<MaxTreeNode<ElementType>> children, IComparer<ElementType> element_value_comparer)
        {
            this.element_value_comparer = element_value_comparer;
            this.Value = node_value;
            this.DisplayValue = node_value;
            this.NodeElementIndexes = elements_indexes;
            this.children = children;
            foreach ( MaxTreeNode<ElementType>  child in children)
            {
                child.parent = this;
            }
        }

        public int[] GetElementIndexArrayNodeReal()
        {
            int[] node_full_element_index_array = new int[NodeElementIndexes.Count];
            GetElementIndexArrayNodeRealRBA(node_full_element_index_array, 0);
            return node_full_element_index_array;
        }

        public void GetElementIndexArrayNodeRealRBA(int[] node_real_element_index_array, int array_offset)
        {
            int array_index = array_offset;
            foreach (int element_index in NodeElementIndexes)
            {
                if(element_index < MaxTree.RealElementCount)
                {
                    node_real_element_index_array[array_index] =  element_index;
                    array_index++;
                }
            }
        }

        public int[] GetElementIndexArrayNodeFull()
        {
            int [] node_full_element_array = new int [NodeElementIndexes.Count];
            get_node_full_element_index_array_fill(node_full_element_array, 0);
            return node_full_element_array;
        }

        public void get_node_full_element_index_array_fill(int[] node_full_element_index_array, int array_offset)
        {
            Array.Copy(NodeElementIndexes.ToArray(), 0, node_full_element_index_array, array_offset, NodeElementIndexes.Count);
        }

        public int [] GetElementIndexArrayCulmativeFull()
        {

            int [] elements = new int [CulmativeFullSize];
            int element_index = 0;
            // Depth first for low memory
            Stack<Tuple<MaxTreeNode<ElementType>, int>> node_stack = new Stack<Tuple<MaxTreeNode<ElementType>, int>>();
            node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(this, 0));
            while (node_stack.Count != 0)
            {
                Tuple<MaxTreeNode<ElementType>, int> node_index_pair = node_stack.Pop();
                MaxTreeNode<ElementType> node = node_index_pair.Item1;
                int node_index = node_index_pair.Item2;
                if (node_index < node.children.Count)
                {
                    node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(node, node_index + 1));
                    node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(node.children[node_index], 0));
                }
                else
                {
                    node.GetElementIndexArrayCulmativeRealRBA(elements, element_index);
                    element_index = element_index + node.NodeElementIndexes.Count;
                }
            }
            return elements;
        }

        public int [] GetElementIndexArrayCulmativeReal()
        {
            int [] full_element_array = GetElementIndexArrayCulmativeFull();
            int [] real_element_array = new int [CulmativeRealSize];

            int real_element_index = 0;
            int tree_real_element_count = this.MaxTree.RealElementCount;
            for (int full_element_index = 0; full_element_index < full_element_array.Length; full_element_index++)
            {
                if (full_element_array[full_element_index] < tree_real_element_count)
                {
                    real_element_array[real_element_index] = full_element_array[full_element_index];
                    real_element_index++;
                }
            }
            return real_element_array;
        }

        private void GetElementIndexArrayCulmativeRealRBA(
            int[] array,
            int offset)
        {
            Array.Copy(NodeElementIndexes.ToArray(), 0, array, offset, NodeElementIndexes.Count);
        }

    //public void generate_features(
    //    IFeatureGeneratorFloat<IElementNode> generator)
    //{
    //    // Depth first for low memory use and the generator seems to prefer it
    //    Stack<Tuple<MaxTreeNode<ElementType>, int>> node_stack = new Stack<Tuple<MaxTreeNode<ElementType>, int>>();
    //    node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(this, 0));
    //    while (node_stack.Count != 0)
    //    {
    //        Tuple<MaxTreeNode<ElementType>, int> node_index_pair = node_stack.pop();
    //        MaxTreeNode<ElementType> node = node_index_pair.get_object1();
    //        int node_index = node_index_pair.get_object2();
    //        if (node_index < node.d_children.length)
    //        {
    //            node_stack.Push(new Tuple<MaxTreeNode<ElementType>, Integer>(node, node_index + 1));
    //            node_stack.Push(new Tuple<MaxTreeNode<ElementType>, Integer>(node.d_children[node_index], 0));
    //        }
    //        else
    //        {
    //            generator.generate_features(node, node.get_feature_array());
    //        }
    //    }

    //}


    //public int [] get_local_real_elements()
    //{
    //    return NodeElementIndexes.ToArray();
    //}


    //public void set_feature_array(
    //    float [] feature_array)
    //{

    //}

    //public int [] get_element_array()
    //{
    //    return NodeElementIndexes.ToArray();
    //}

    //public IList<int> get_element_list()
    //{
    //    return NodeElementIndexes;
    //}

    //private IList<int> get_real_element_list()
    //{
    //     IList<int> real_elements = new List<int>();
    //    foreach ( int element in NodeElementIndexes)
    //    {
    //        if (element < MaxTree.RealElementCount)
    //        {
    //            real_elements.Add(element);
    //        }
    //    }
    //    return real_elements;
    //}


    public IList<IMaxTreeNode<ElementType>> GetNodeChildren()
    {
        return new List<IMaxTreeNode<ElementType>>(children);
    }

    public IList<IMaxTreeNode<ElementType>> GetCulmativeChildrenBottomToTop()
    {
        return new List<IMaxTreeNode<ElementType>>(GetCulmativeChildrenBottomToTopInner());
    }

    public List<MaxTreeNode<ElementType>> GetCulmativeChildrenBottomToTopInner()
    {
        List<MaxTreeNode<ElementType>> node_list_bottom_to_top = new List<MaxTreeNode<ElementType>>();
        // Bread first, list should be bottom to top
        Queue<MaxTreeNode<ElementType>> queue = new Queue<MaxTreeNode<ElementType>>();
        queue.Enqueue(this);
        while (queue.Count != 0)
        {
            MaxTreeNode<ElementType> node = queue.Dequeue();
            node_list_bottom_to_top.Add(node);
            foreach (MaxTreeNode<ElementType> child in node.children)
            {
                queue.Enqueue(child);
            }
        }
        return node_list_bottom_to_top;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Node value: " + Value);
        builder.AppendLine("Elements:");
        foreach (int element in NodeElementIndexes)
        {
            builder.AppendLine(element.ToString());
        }
        builder.AppendLine("Children Count: " + this.children.Count);
        return builder.ToString();
    }

    public void ComputePrimitives(
        IMaxTree<ElementType> max_tree)
    {
        int real_image_size = max_tree.RealElementCount;
        // Depth first for low memory and
        Stack<Tuple<MaxTreeNode<ElementType>, int>> node_stack = new Stack<Tuple<MaxTreeNode<ElementType>, int>>();
        node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(this, 0));
        while (node_stack.Count != 0)
        {
            Tuple<MaxTreeNode<ElementType>, int> node_index_pair = node_stack.Pop();
            MaxTreeNode<ElementType> node = node_index_pair.Item1;
            int index = node_index_pair.Item2;
            if (index < node.children.Count)
            {
                node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(node, index + 1));
                node_stack.Push(new Tuple<MaxTreeNode<ElementType>, int>(node.children[index], 0));
            }
            else
            {
                node.MaxTree = max_tree;
                node.PeakComponentValue = node.Value;
                node.CulmativeFullSize = 0;

                foreach (int element in node.NodeElementIndexes)
                {
                    node.CulmativeFullSize++;
                    if (element < real_image_size)
                    {
                        node.CulmativeRealSize++;
                    }
                }
                foreach ( MaxTreeNode<ElementType> child in node.children)
                {
                    if (element_value_comparer.Compare(PeakComponentValue, child.PeakComponentValue) == -1)
                    {
                        node.PeakComponentValue = child.PeakComponentValue;
                    }
                    node.CulmativeFullSize += child.CulmativeFullSize;
                    node.CulmativeRealSize += child.CulmativeRealSize;
                }
            }
        }
    }

    //public void k_substractive(
    //     float k_value,
    //     IMaxTreeNode<ElementType>Filter filter)
    //{
    //    // TODO this is breath first: high memory cost
    //    Queue<Tuple<MaxTreeNode<ElementType>, Float>> queue = new LinkedList<>();
    //    queue.add(new Tuple<MaxTreeNode<ElementType>, Float>(this, 0.0f));
    //    while (!queue.isEmpty())
    //    {
    //        Tuple<MaxTreeNode<ElementType>, Float> node_index_pair = queue.poll();
    //        MaxTreeNode<ElementType> node = node_index_pair.get_object1();
    //        float propagation_value = node_index_pair.get_object2();

    //        float parent_value = 0;
    //        float parent_display_value = 0;

    //        if (node.d_parent != null)
    //        {
    //            parent_value = node.d_parent.d_value;
    //            parent_display_value = node.d_parent.d_display_value;
    //        }

    //         float difference_level = node.d_value - parent_value;

    //        if (((node.d_peak_component_value - parent_value) > k_value) && (filter.accept_node(this)))
    //        {
    //            // Preserve

    //            node.d_display_value = parent_display_value + difference_level;
    //            propagation_value = k_value;
    //        }
    //        else
    //        {
    //            // Filter

    //            if (difference_level > propagation_value)
    //            {
    //                node.d_display_value = parent_display_value + propagation_value;
    //                propagation_value = 0;
    //            }
    //            else
    //            {
    //                node.d_display_value = parent_display_value + difference_level;
    //                propagation_value -= difference_level;
    //            }
    //        }

    //        for ( MaxTreeNode<ElementType> child : node.d_children)
    //        {
    //            queue.add(new Tuple<MaxTreeNode<ElementType>, Float>(child, propagation_value));
    //        }
    //    }
    //}
    //public void FilterAbsorbing(
    //    IFunction<IMaxTreeNode<ElementType>, bool> filter)
    //{
    //    // TODO this is breath first: high memory cost
    //    Queue<MaxTreeNode<ElementType>> queue = new Queue<MaxTreeNode<ElementType>>();
    //    queue.Enqueue(this);
    //    while (queue.Count != 0)
    //    {
    //        MaxTreeNode<ElementType> node = queue.Dequeue();
    //        if (filter.Compute(node))
    //        {
    //            if(node.parent != null)
    //            {
    //                DisplayValue = node.Parent.DisplayValue;
    //            }
    //        }          
     
    //        foreach ( MaxTreeNode<ElementType> child in node.children)
    //        {
    //            queue.Enqueue(child);
    //        }
    //    }

    //}
    //public void k_adsorption(
    //     float k_value,
    //     IMaxTreeNode<ElementType>Filter filter)
    //{
    //    // TODO this is breath first: high memory cost
    //    Queue<Tuple<MaxTreeNode<ElementType>, Float>> queue = new LinkedList<>();
    //    queue.add(new Tuple<MaxTreeNode<ElementType>, Float>(this, 0.0f));
    //    while (!queue.isEmpty())
    //    {
    //        Tuple<MaxTreeNode<ElementType>, Float> node_index_pair = queue.poll();
    //        MaxTreeNode<ElementType> node = node_index_pair.get_object1();
    //        float propagation_value = node_index_pair.get_object2();

    //        float parent_value = 0;
    //        float parent_display_value = 0;

    //        if (node.d_parent != null)
    //        {
    //            parent_value = node.d_parent.d_value;
    //            parent_display_value = node.d_parent.d_display_value;
    //        }

    //         float difference_level = d_value - parent_value;
    //        if ((node.d_peak_component_value - parent_value) > k_value)
    //        {
    //            if (filter.accept_node(this))
    //            {
    //                if (propagation_value >= 0)
    //                {
    //                    node.d_display_value = parent_display_value + difference_level;
    //                    propagation_value = k_value;
    //                }
    //                else
    //                {
    //                    node.d_display_value = parent_display_value;
    //                    propagation_value += difference_level;
    //                    if (propagation_value > 0)
    //                    {
    //                        node.d_display_value += propagation_value + k_value;
    //                        propagation_value = k_value;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                node.d_display_value = parent_display_value;
    //                propagation_value = -k_value;
    //            }
    //        }
    //        else
    //        {
    //            if (difference_level > propagation_value)
    //            {
    //                node.d_display_value = parent_display_value + Math.max(propagation_value, 0);
    //                propagation_value = 0;
    //            }
    //            else
    //            {
    //                node.d_display_value = parent_display_value + difference_level;
    //                propagation_value -= difference_level;
    //            }
    //        }

    //        for ( MaxTreeNode<ElementType> child : node.d_children)
    //        {
    //            queue.add(new Tuple<MaxTreeNode<ElementType>, Float>(child, propagation_value));
    //        }
    //    }

    //}

    //public int [] get_first_satifiers(
    //    IFilterMaxTreeNode filter)
    //{
    //    // TODO Auto-generated method stub
    //    return null;
    //}


    ///**
    // * Filter on display values, for each failing node subtract their value difference from the display value
    // * @param filter
    // */
    //public void filter_element_values(
    //    IFilterElement<IElementNode> filter)
    //{
    //    // Depth first for low memory and
    //    Stack<Tuple<MaxTreeNode<ElementType>, Float>> node_stack = new Stack<>();
    //    if(filter.remove(this))
    //    {
    //        node_stack.Push(new Tuple<MaxTreeNode<ElementType>, Float>(this, d_value));
    //    }
    //    else
    //    {
    //        node_stack.Push(new Tuple<MaxTreeNode<ElementType>, Float>(this, 0.0f));
    //    }
    //    while (!node_stack.empty())
    //    {
    //        //Get current case
    //        Tuple<MaxTreeNode<ElementType>, Float> node_subtraction_tuple = node_stack.pop();
    //        MaxTreeNode<ElementType> parent = node_subtraction_tuple.get_object1();
    //        float subtraction = node_subtraction_tuple.get_object2();
        
    //        //Do current case
    //        parent.d_display_value = parent.d_value - subtraction;
                
    //        //Queue children
    //        for (MaxTreeNode<ElementType> child : parent.d_children)
    //        {
    //            float difference = child.get_value() - parent.get_value();
    //            if(filter.remove(child))
    //            {
    //                node_stack.Push(new Tuple<MaxTreeNode<ElementType>, Float>(child, subtraction + difference));
    //            }
    //            else
    //            {
    //                node_stack.Push(new Tuple<MaxTreeNode<ElementType>, Float>(child, subtraction));
    //            }
    //        }
            
    //    }
        
    //}



    }
}
