using KozzionGraphics.ElementTree.MaxTree.Filter;

namespace KozzionGraphics.ElementTree.MaxTree
{
    public interface IMaxTree<ElementType>
    {
        int FullElementCount { get; } // includes virtual elements
        int RealElementCount { get; }
        int NodeCount { get;}
        IMaxTreeNode<ElementType> BottomLevelNode { get; }

        IMaxTreeNode<ElementType> GetNode(int element_index);
      
        int [] GetFullElementsIndexesOfElementLevelAndAbove(
            int element_index);

        int[] GetFullElementsIndexesOfElementLevelAndAbove(
            int element_index,
            ElementType level);

        int[] GetRealElementsIndexesOfElementLevelAndAbove(
            int element_index,
            ElementType level);

        int[] GetRealElementsIndexesOfElementLevelAndAbove(
            int element_index);



        ElementType[] GetDisplayValues();

        //void FilterAbsorbing(IFunction<IMaxTreeNode<ElementType>, bool> filter);

        void Filter(IMaxTreeFilter<ElementType> filter);

      
    }
}
