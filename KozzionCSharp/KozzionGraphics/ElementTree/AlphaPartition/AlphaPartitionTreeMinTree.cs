using System;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionMathematics.Algebra;

namespace KozzionGraphics.ElementTree.AlphaPartition
{
    [Serializable]
    public class AlphaPartitionTreeMinTree<EdgeValueType> : IAlphaPartitionTree<EdgeValueType>
        where EdgeValueType : IComparable<EdgeValueType>
    {
        private IAlgebraReal<EdgeValueType> algebra;
        private IMaxTree<EdgeValueType> min_tree;
        private EdgeValueType max_edge_value;

        public int RealElementCount
        {
            get { return min_tree.RealElementCount; }
        }

        public int FullElementCount
        {
            get { return min_tree.FullElementCount; }
        }
        public AlphaPartitionTreeMinTree(IAlgebraReal<EdgeValueType> algebra, IMaxTree<EdgeValueType> min_tree, EdgeValueType max_edge_value)
        {
            this.algebra = algebra;
            this.min_tree = min_tree;
            this.max_edge_value = max_edge_value;
        }

        public int[] GetRealElementsIndexesWithMaxAlfa(
            int element_index,
            EdgeValueType max_alfa)
        {
            return this.min_tree.GetRealElementsIndexesOfElementLevelAndAbove(element_index, this.algebra.Subtract(this.max_edge_value, max_alfa));
        }

        public int[] GetFullElementsIndexesWithMaxAlfa(
           int element_index,
           EdgeValueType max_alfa)
        {
            return this.min_tree.GetFullElementsIndexesOfElementLevelAndAbove(element_index, this.algebra.Subtract(this.max_edge_value, max_alfa));
        }


        public int[] GetRealElementsIndexesWithMaxSize(int element_index, int max_size)
        {
            throw new NotImplementedException();
        }

        public int[] GetFullElementsIndexesWithMaxSize(int element_index, int max_size)
        {
            throw new NotImplementedException();
        }



        public int[] GetRealElementsIndexesWithMaxAlfaMaxSize(int element_index, int max_real_size, EdgeValueType max_alpha)
        {
            IMaxTreeNode<EdgeValueType> node = min_tree.GetNode(element_index);
            EdgeValueType min_alpha = this.algebra.Subtract(this.max_edge_value, max_alpha);
            while ((node.Parent != null) && (this.algebra.Compare(min_alpha, node.Parent.Value) == -1) && (max_real_size > node.Parent.CulmativeRealSize))
            {
                node = node.Parent;
            }
            return node.GetElementIndexArrayCulmativeReal();
        }

        public int[] GetFullElementsIndexesWithMaxAlfaMaxSize(int element_index, int max_full_size, EdgeValueType max_alpha)
        {
            IMaxTreeNode<EdgeValueType> node = min_tree.GetNode(element_index);
            EdgeValueType min_alpha = this.algebra.Subtract(this.max_edge_value, max_alpha);
            while ((node.Parent != null) && (this.algebra.Compare(min_alpha, node.Parent.Value) == -1) && (max_full_size > node.Parent.CulmativeFullSize))
            {
                node = node.Parent;
            }
            return node.GetElementIndexArrayCulmativeFull();
        }
    }
}
