using System.Collections.Generic;

namespace KozzionGraphics.ElementTree.MaxTree
{
    public interface IMaxTreeNode<ElementType>
    {
        ElementType DisplayValue { get; set; }

        ElementType Value { get; }

        ElementType PeakComponentValue { get; }

        int NodeIndex { get; }

        int NodeRealSize { get; }

        int NodeFullSize { get; }

        int CulmativeRealSize { get; }

        int CulmativeFullSize { get; }

        int[] GetElementIndexArrayNodeReal();

        int[] GetElementIndexArrayNodeFull();

        int[] GetElementIndexArrayCulmativeFull();

        int[] GetElementIndexArrayCulmativeReal();

        IList<IMaxTreeNode<ElementType>> GetNodeChildren();
        IList<IMaxTreeNode<ElementType>> GetCulmativeChildrenBottomToTop();

        IMaxTreeNode<ElementType> Parent { get; }

        IMaxTree<ElementType> MaxTree { get; }

        void ComputePrimitives(IMaxTree<ElementType> maxTree);

    }
}
