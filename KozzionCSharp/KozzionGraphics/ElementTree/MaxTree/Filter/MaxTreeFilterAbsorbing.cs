using System.Collections.Generic;
using KozzionMathematics.Function;

namespace KozzionGraphics.ElementTree.MaxTree.Filter
{
    public class MaxTreeFilterAbsorbing<ElementType> : IMaxTreeFilter<ElementType>
    {
        IFunction<IMaxTreeNode<ElementType>, bool> filter_function;

        public MaxTreeFilterAbsorbing(IFunction<IMaxTreeNode<ElementType>, bool> filter_function)
        {
            this.filter_function = filter_function;
        }

        public void Filter(IList<MaxTreeNode<ElementType>> list_bottom_to_top)
        {
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
