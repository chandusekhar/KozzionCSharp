using System;
using System.Collections.Generic;
using KozzionGraphics.Image.Topology;
using Wintellect.PowerCollections;
using KozzionCore.DataStructure;
namespace KozzionGraphics.ElementTree.MaxTree
{
    public class MaxTreeBuilderMultiQueue<ElementType> : IMaxTreeBuilder<ElementType>
    {
        // initialized values temporary, but made members for acces purposes
        private ElementType [] d_element_array;
        private IComparer<ElementType> element_value_comparer;
        private IComparer<int> element_index_comparer;
        private ITopologyElement d_topology;
        private bool[] d_elements_to_processed;
        private Stack<OrderedBag<int>> d_fringe;
        private Stack<Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>> d_component_stack;
        private MaxTreeNode<ElementType> d_bottom_level_node;
        int[] d_neigbor_element_array;

        public MaxTreeBuilderMultiQueue()
        {
           
        }



        public IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<ElementType> element_value_comparer, ITopologyElement topology, int real_element_count)
        {
            return BuildMaxTree(element_array, element_value_comparer, topology, real_element_count, null);
        }



        public IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<ElementType> element_value_comparer, ITopologyElement topology, int real_element_count, IProgressReporter reporter)
        {
            this.d_element_array = element_array;
            this.element_value_comparer = element_value_comparer;
            this.element_index_comparer = new ComparerArrayIndex<ElementType>(element_value_comparer, element_array);
            this.d_topology = topology;
            this.d_elements_to_processed = new bool[d_element_array.Length];
            this.d_neigbor_element_array = new int[d_topology.MaximumConnectivity];
            this.d_fringe = new Stack<OrderedBag<int>>();
            this.d_component_stack = new Stack<Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>>();
            this.d_bottom_level_node = null;

            process(0);
            // build tree
            while (d_fringe.Count != 0)
            {

                if (d_fringe.Peek().Count == 0)
                {
                    decrease_process_level(); // continue on a lower level
                }
                else
                {
                    // otherwise keep on processing the fringe
                    process(d_fringe.Peek().RemoveFirst());
                }
            }
            return new MaxTree<ElementType>(d_bottom_level_node, d_element_array.Length, real_element_count, element_value_comparer);
        }
        private void process( int element_index)
        {
            if (d_elements_to_processed[element_index])
            {
                return; // do nothing
                // check if higher than working level
            }


            if (d_component_stack.Count == 0)
            {
                // if we are not working on anything increase process level
                increase_process_level(element_index); 
            }
            else
            {
                ElementType process_level = d_component_stack.Peek().Item1;         

                if (element_value_comparer.Compare(process_level, d_element_array[element_index]) == -1)
                {
                    // increase the process level and add the node to that fringe (leaves element unprocessed)
                    increase_process_level(element_index); 
                }

                if (element_value_comparer.Compare(process_level, d_element_array[element_index]) == 0)               
                {
                    // if element level is the same as process level add element to current node
                    d_component_stack.Peek().Item2.Add(element_index); 
                    // add its neighbors to the current fringe
                    add_neigbors_to_fringe(element_index);
                    // and mark it as processed
                    d_elements_to_processed[element_index] = true; 
                }

                if (element_value_comparer.Compare(process_level, d_element_array[element_index]) == 1)                 
                {
                    // if element is lower than same as process level push element back into fringe
                    // (leaves element unprocessed)
                    d_fringe.Peek().Add(element_index); 
                    decrease_process_level(); // and continue on a lower level
                }
            }
        }

        private void decrease_process_level()
        {
            OrderedBag<int> fringe_top = d_fringe.Pop();
            MaxTreeNode<ElementType> popped_node = pop_component_stack();
            if (fringe_top.Count == 0) // if the popped fringe is empty
            {
                if (d_component_stack.Count == 0)
                {
                    d_bottom_level_node = popped_node; // we are done.
                }
                else
                {
                    // and add the new node to there
                    d_component_stack.Peek().Item3.Add(popped_node);
                  
                }
            }
            else
            // if the popped fringe is not empty
            {
                if (d_component_stack.Count == 0)
                {
                    // find where tocontinue
                    ElementType next_process_level = d_element_array[fringe_top.GetFirst()]; 
                    d_fringe.Push(fringe_top); // push the fringe back in
                    d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(next_process_level, new List<int>(),
                        new List<MaxTreeNode<ElementType>>()));
                    // add the popped node as child
                    d_component_stack.Peek().Item3.Add(popped_node); 
                }
                else
                {
                    ElementType fringe_level = d_element_array[fringe_top.GetFirst()]; 
                    ElementType component_stack_level = d_component_stack.Peek().Item1;
                    if (element_value_comparer.Compare(component_stack_level, fringe_level) == -1) //TODO
                    {

                        d_fringe.Push(fringe_top); // push the fringe back in
                        d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(fringe_level, new List<int>(),
                            new List<MaxTreeNode<ElementType>>()));
                        // add the popped node as child
                        d_component_stack.Peek().Item3.Add(popped_node); 
                    }
                    else
                    {
                        while(0 < fringe_top.Count)
                        {
                            d_fringe.Peek().Add(fringe_top.RemoveFirst());
                        }
                        d_component_stack.Peek().Item3.Add(popped_node);
                    }
                }
            }
        }

        private MaxTreeNode<ElementType> pop_component_stack()
        {
            Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>> component = d_component_stack.Pop();
            return new MaxTreeNode<ElementType>(component.Item1, component.Item2, component.Item3, element_value_comparer);
        }

        private void increase_process_level( int element_index)
        {
            // find new  process level
            ElementType new_process_level = d_element_array[element_index];

            // build up the components stack
            d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(new_process_level, new List<int>(), new List<MaxTreeNode<ElementType>>()));

            d_fringe.Push(new OrderedBag<int>(element_index_comparer));
            d_fringe.Peek().Add(element_index);
        }

        private void add_neigbors_to_fringe( int element_index)
        {
            d_topology.ElementNeighboursRBA(element_index, d_neigbor_element_array);
            foreach ( int neigbor_element_index in d_neigbor_element_array)
            {
                if (neigbor_element_index != -1)
                {

                    d_fringe.Peek().Add(neigbor_element_index);
                }
            }
        }




    }
}