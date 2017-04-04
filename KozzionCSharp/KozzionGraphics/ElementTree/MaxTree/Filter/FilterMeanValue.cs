using System.Collections.Generic;
using System.Linq;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.ElementTree.MaxTree.Filter;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;

namespace KozzionGraphics.ElementTree.AlphaPartition.Filter
{
    public class MaxTreeFilterMean<ElementType> : IMaxTreeFilter<ElementType>
    {
        private IFunction<IMaxTreeNode<ElementType>, bool> filter_function;
        IAlgebraReal<ElementType> algebra;
        private ElementType[] source_values;

        public MaxTreeFilterMean(IFunction<IMaxTreeNode<ElementType>, bool> filter_function, IAlgebraReal<ElementType> algebra, ElementType[] source_values)
        {
            this.filter_function = filter_function;
            this.source_values = source_values;
            this.algebra = algebra;
        }

        public void Filter(IList<MaxTreeNode<ElementType>> list_bottom_to_top)
        {
            list_bottom_to_top.Reverse();
            foreach (IMaxTreeNode<ElementType> current_node in list_bottom_to_top)
            {
                ElementType mean = this.algebra.AddIdentity;
                foreach (IMaxTreeNode<ElementType> child_node in current_node.GetNodeChildren())
                {
                    mean = this.algebra.Multiply(this.algebra.Add(mean, child_node.DisplayValue), algebra.ToDomain((float)child_node.CulmativeRealSize));
                }

                foreach (int element_index in current_node.GetElementIndexArrayNodeReal())
                {
                    mean = this.algebra.Add(mean, this.source_values[element_index]);
                }
                current_node.DisplayValue = this.algebra.Divide(mean, this.algebra.ToDomain((float)current_node.CulmativeRealSize));
            }

            list_bottom_to_top.Reverse();
            foreach (IMaxTreeNode<ElementType> current_node in list_bottom_to_top)
            {
                if (filter_function.Compute(current_node) && (current_node.Parent != null))
                {
                    current_node.DisplayValue = current_node.Parent.DisplayValue;
                }
                else
                {
                    current_node.DisplayValue = current_node.Value;
                }
            }
        }
    }
}
