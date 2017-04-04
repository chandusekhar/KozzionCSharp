using KozzionGraphics.ElementTree.AlphaPartition;
using KozzionMathematics.Algebra;
using System;

namespace KozzionGraphics.Filter
{
    public class FilterAlfaPartitionMean<ElementType>
        where ElementType :IComparable<ElementType>
    {
        public IAlgebraReal<ElementType> algebra;

        public FilterAlfaPartitionMean(IAlgebraReal<ElementType> algebra)
        {
            this.algebra = algebra;
        }

        public ElementType[] Filter(IAlphaPartitionTree<ElementType> tree, ElementType [] values, int max_size, ElementType max_alpha)
        {
            ElementType[] filtered = new ElementType[tree.RealElementCount];
            bool[] done = new bool [tree.RealElementCount];
            for (int element_index = 0; element_index < tree.RealElementCount; element_index++)
            {
                if (!done[element_index])
                {
                    int [] element_group = tree.GetRealElementsIndexesWithMaxAlfaMaxSize(element_index, max_size, max_alpha);
                    ElementType mean = this.algebra.AddIdentity;                
                    foreach (int group_element_index in element_group)
                    {
                        mean = this.algebra.Add(mean, values[group_element_index]);
                        done[group_element_index] = true;
                    }

                    mean = this.algebra.Divide(mean, this.algebra.ToDomain((float)element_group.Length));

                    foreach (int group_element_index in element_group)
                    {
                        filtered[group_element_index] = mean;
                        done[group_element_index] = true;
                    }
                }
            }
            return filtered;
        }
    }
}
