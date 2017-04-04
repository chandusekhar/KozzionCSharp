namespace KozzionGraphics.ElementTree
{
    public interface IElementTreeNode<ElementValueType>
    {
        int get_culmative_full_size();

        int get_culmative_real_size();

        IElementTreeNode<ElementValueType>[] get_local_children();

        int[] get_culmative_real_element_array();

        int[] get_local_real_elements();

  

        int get_node_index();


        int NodeIndex { get; set; }
    }
}