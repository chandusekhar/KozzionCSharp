using System.Collections.Generic;

namespace KozzionGraphics.ElementTree.MaxTree.Filter
{
    public interface IMaxTreeFilter<ElementType>
    {
        void Filter(IList<MaxTreeNode<ElementType>> list_bottom_to_top);
    }
}
