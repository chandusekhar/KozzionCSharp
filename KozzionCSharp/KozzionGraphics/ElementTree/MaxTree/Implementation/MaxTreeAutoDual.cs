using System;
using KozzionCore.Tools;
using KozzionGraphics.ElementTree.MaxTree.Filter;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionCore.DataStructure;

namespace KozzionGraphics.ElementTree.MaxTree.Implementation
{
    public class MaxTreeAutoDual<ElementType> : IMaxTree<ElementType>
        where ElementType : IComparable<ElementType>
    {
        private IAlgebraReal<ElementType> algebra;
        private IMaxTree<ElementType> max_tree;
        private IMaxTree<ElementType> min_tree;
        private ElementType[] element_values;

        public MaxTreeAutoDual(IAlgebraReal<ElementType> algebra, IMaxTreeBuilder<ElementType> builder, ITopologyElement topology, ElementType[] element_values, IProgressReporter reporter)
        {
            this.algebra = algebra;
            if (reporter == null)
            { 
                this.max_tree = builder.BuildMaxTree(element_values, new ComparerNatural<ElementType>(), topology, element_values.Length) ;
                this.min_tree = builder.BuildMaxTree(element_values, new ComparerNatural<ElementType>(true), topology, element_values.Length);
            }
            this.element_values = ToolsCollection.Copy(element_values);
        }

        public MaxTreeAutoDual(IAlgebraReal<ElementType> algebra, IMaxTreeBuilder<ElementType> builder, ITopologyElement topology, ElementType[] element_values)
        {
            this.algebra = algebra;
            this.max_tree = builder.BuildMaxTree(element_values, new ComparerNatural<ElementType>(), topology, element_values.Length);
            this.min_tree = builder.BuildMaxTree(element_values, new ComparerNatural<ElementType>(true), topology, element_values.Length);
            this.element_values = ToolsCollection.Copy(element_values);
        }

        public int FullElementCount
        {
            get { throw new NotImplementedException(); }
        }

        public int RealElementCount
        {
            get { throw new NotImplementedException(); }
        }

        public int[] GetFullElementsIndexesOfElementLevelAndAbove(int element_index)
        {
            throw new NotImplementedException();
        }

        public int[] GetFullElementsIndexesOfElementLevelAndAbove(int element_index, ElementType level)
        {
            throw new NotImplementedException();
        }

        public int[] GetRealElementsIndexesOfElementLevelAndAbove(int element_index, ElementType level)
        {
            throw new NotImplementedException();
        }

        public int[] GetRealElementsIndexesOfElementLevelAndAbove(int element_index)
        {
            throw new NotImplementedException();
        }

        public ElementType[] GetDisplayValues()
        {
            ElementType[] display_values = new ElementType[this.max_tree.RealElementCount];
            ElementType[] display_values_max = this.max_tree.GetDisplayValues();
            ElementType[] display_values_min = this.min_tree.GetDisplayValues();
            for (int index = 0; index < display_values.Length; index++)
            {
                display_values[index] = algebra.Subtract(algebra.Add(display_values_max[index], display_values_min[index]), element_values[index]);
            }
            return display_values;
        }

        public void FilterAbsorbing(IFunction<IMaxTreeNode<ElementType>, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void Filter(IMaxTreeFilter<ElementType> filter)
        {
            this.max_tree.Filter(filter);
            this.min_tree.Filter(filter);
        }


        public int NodeCount
        {
            get { throw new NotImplementedException(); }
        }

        public IMaxTreeNode<ElementType> BottomLevelNode
        {
            get { throw new NotImplementedException(); }
        }


        public IMaxTreeNode<ElementType> GetNode(int element_index)
        {
            throw new NotImplementedException();
        }
    }
}
