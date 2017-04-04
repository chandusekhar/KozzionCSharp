using System;
using System.Collections.Generic;
using KozzionCore.Collections;
using KozzionCore.DataStructure;
using KozzionGraphics.Image.Topology;
namespace KozzionGraphics.ElementTree.MaxTree
{
    public class MaxTreeBuilderSingleQueue<ElementType> : IMaxTreeBuilder<ElementType>
    {
        // initialized values temporary, but made members for access purposes
        private ElementType [] d_element_array;
        private ITopologyElement d_topology;
        private IComparer<int> element_index_comparer;
        private IComparer<ElementType> element_value_comparer;
        private bool[] d_elements_to_queued;
        private PriorityQueueC5<int> d_fringe;
        private Stack<Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>> d_component_stack;
        private int[] d_neigbor_element_array;
        private int d_done;
        private IProgressReporter d_reporter;

        public MaxTreeBuilderSingleQueue()
        {
           
        }

        //public IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<int> element_comparer, ITopologyElement topology)
        //{
        //    d_element_array = element_array;
        //    d_topology = topology;
        //    d_elements_to_queued = new bool[d_element_array.Length];
        //    d_done = 0;
        //    d_neigbor_element_array = new int[d_topology.MaximumConnectivity];
        //    d_fringe = new PriorityQueueS1<int>(element_comparer);
        //    d_component_stack = new Stack<Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>>();
        //    d_bottom_level_node = null;
        //    this.element_comparer = element_comparer;
        //    Process(0);
        //    // build tree
        //    while (d_fringe.Count != 0)
        //    {
        //        Process(d_fringe.Dequeue());
        //    }
        //    return new MaxTree<ElementType>(d_bottom_level_node, d_element_array.Length, d_element_array.Length);
        //}
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
            this.d_reporter = reporter;

            this.d_done = 0;
            this.d_elements_to_queued = new bool[d_element_array.Length];
            this.d_neigbor_element_array = new int[d_topology.MaximumConnectivity];
            this.d_fringe = new PriorityQueueC5<int>(element_index_comparer);
            this.d_component_stack = new Stack<Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>>();
    
         
           

            d_fringe.Enqueue(0);
            d_elements_to_queued[0] = true;
            // Build tree
            while (d_fringe.Count != 0)
            {
                Process(d_fringe.PeekLast());
            }
            Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>> component = d_component_stack.Pop();
            MaxTreeNode<ElementType> bottom_level_node = new MaxTreeNode<ElementType>(component.Item1, component.Item2, component.Item3, element_value_comparer);
            return new MaxTree<ElementType>(bottom_level_node, d_element_array.Length, real_element_count, element_value_comparer);
        }

        private void Process(int element_index)
        {
            // Console.WriteLine("Process" + element_index);
            // If we have inserted all its neigbours without finding one that is higher the magic begins:
            // Note that we can still be sure that the front elemenent in the queue is the same 
            // (The priorety queue should guantee this)
   
            // If we are not working on anything 
            if (d_component_stack.Count == 0)
            {
                //Console.WriteLine("Increase process" + element_index);
                // Increase process level add leave it in the queue
                d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(d_element_array[element_index], new List<int>(), new List<MaxTreeNode<ElementType>>()));
            }
            else
            {
                ElementType process_level = d_component_stack.Peek().Item1;

                // If we are higher than the current processing level
                if (this.element_value_comparer.Compare(process_level, d_element_array[element_index]) == -1)
                {
                    //Console.WriteLine("Increase process" + element_index);
                    // Do NOT remove the element from the fringe
                    // Increase process level add it the the new component
                    d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(d_element_array[element_index], new List<int>(), new List<MaxTreeNode<ElementType>>()));
                }


                // If element level is below the process level
                if (this.element_value_comparer.Compare(process_level, d_element_array[element_index]) == 1)                 
                {
                    //Console.WriteLine("DecreaseProcessLevel" + element_index);
                    // Do NOT remove the element from the fringe
                    // Decrease process level (this pops the node)
                    DecreaseProcessLevel(element_index);
                }

                // If element level is the same as process level
                if (this.element_value_comparer.Compare(process_level, d_element_array[element_index]) == 0)
                {
                    
                    // Explore node, check if we can insert all its neigbours
                    // Remove the element from the fringe

                    if (!CanAddNeigborsToFringe(element_index))
                    {
                        //Console.WriteLine("Neigbors break" + element_index);
                        // If CANNOT; Do NOT remove the element from the fringe
                        AddNeigborsToFringe(element_index);
                        // Add this neigtbour
                        // Retry later with new fringe
                        return;
                    }
                    else 
                    {
                        d_fringe.DequeueLast();
                        AddNeigborsToFringe(element_index);
                        //Console.WriteLine("Add" + element_index);                     
                       
                        // Add the Element to the new component
                        d_component_stack.Peek().Item2.Add(element_index);

                        d_done++;
                        if (d_reporter != null)
                        {
                            d_reporter.Report(d_done, d_elements_to_queued.Length);
                        }
                    }
      
                }
            }
        }



        private void DecreaseProcessLevel(int element_index)
        {
            //Pop the top of the stack
            Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>> component = d_component_stack.Pop();
            MaxTreeNode<ElementType> popped_node = new MaxTreeNode<ElementType>(component.Item1, component.Item2, component.Item3, element_value_comparer);
          

   
            if (d_component_stack.Count == 0)
            {
                d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(d_element_array[element_index], new List<int>(), new List<MaxTreeNode<ElementType>>()));
            }
            else
            {
                if (!d_component_stack.Peek().Item1.Equals(d_element_array[element_index]))
                {
       
                    d_component_stack.Push(new Tuple<ElementType, IList<int>, IList<MaxTreeNode<ElementType>>>(d_element_array[element_index], new List<int>(), new List<MaxTreeNode<ElementType>>()));
                }                
            }
            // And add the new tree element node to there
            d_component_stack.Peek().Item3.Add(popped_node);

        }

        private bool CanAddNeigborsToFringe(int element_index)
        {
            d_topology.ElementNeighboursRBA(element_index, d_neigbor_element_array);
            foreach (int neigbor_element_index in d_neigbor_element_array)
            {
                if (neigbor_element_index != -1)
                {
                    if (!d_elements_to_queued[neigbor_element_index])
                    {
                        if (this.element_index_comparer.Compare(element_index, neigbor_element_index) == -1)// HOOK
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void AddNeigborsToFringe(int element_index)
        {
            d_topology.ElementNeighboursRBA(element_index, d_neigbor_element_array);
            foreach ( int neigbor_element_index in d_neigbor_element_array)
            {
                if (neigbor_element_index != -1)
                {
                    if (!d_elements_to_queued[neigbor_element_index])
                    {
                        //Console.WriteLine("Enqueue" + neigbor_element_index);
                        d_fringe.Enqueue(neigbor_element_index);
                        d_elements_to_queued[neigbor_element_index] = true;
                        if (this.element_index_comparer.Compare(element_index, neigbor_element_index) == -1)// HOOK
                        {
                            return;
                        }
                    }
                }
            }
        }

   
    }
}