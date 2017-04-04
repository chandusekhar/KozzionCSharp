using System.Collections.Generic;

namespace KozzionGraphics.ElementTree.MaxTree
{

    public class ComparerArrayIndex<ElementType>  :IComparer<int>
    {
        private IComparer<ElementType> element_value_comparer;
        private ElementType[] element_values;

        public ComparerArrayIndex(IComparer<ElementType> element_value_comparer, ElementType [] element_values)
        {
            this.element_value_comparer = element_value_comparer;
            this.element_values = element_values;
        }

        public int Compare(int index_0, int index_1)
        {
            return element_value_comparer.Compare(element_values[index_0], element_values[index_1]);
        }
    }
}
