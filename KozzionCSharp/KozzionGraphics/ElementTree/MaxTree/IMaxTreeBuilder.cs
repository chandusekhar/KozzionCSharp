using System.Collections.Generic;
using KozzionGraphics.Image.Topology;
using KozzionCore.DataStructure;

namespace KozzionGraphics.ElementTree.MaxTree
{
    public interface IMaxTreeBuilder<ElementType>
    {

        //IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<ElementType> element_value_comparator, ITopologyElement topology);

        //IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<ElementType> element_value_comparator, ITopologyElement topology, IProgressReporter reporter);

        IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<ElementType> element_value_comparator, ITopologyElement topology, int real_element_count);

        IMaxTree<ElementType> BuildMaxTree(ElementType[] element_array, IComparer<ElementType> element_value_comparator, ITopologyElement topology, int real_element_count, IProgressReporter reporter);


    
    }


}
