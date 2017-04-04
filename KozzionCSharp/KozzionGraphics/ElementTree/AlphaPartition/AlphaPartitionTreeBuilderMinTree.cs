using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Algebra;
using System;
using System.Diagnostics;
using KozzionMathematics.Function;
using KozzionCore.DataStructure;

namespace KozzionGraphics.ElementTree.AlphaPartition
{
    public class AlphaPartitionTreeBuilderMinTree<ElementValueType, EdgeValueType> : IAlphaPartitionTreeBuilder<ElementValueType, EdgeValueType>
	    where EdgeValueType : IComparable<EdgeValueType>
    {
        IMaxTreeBuilder<EdgeValueType> builder;
	    IAlgebraReal<EdgeValueType> algebra;

        public AlphaPartitionTreeBuilderMinTree(IAlgebraReal<EdgeValueType> algebra, IMaxTreeBuilder<EdgeValueType> builder)
        {
            this.builder = builder;
		    this.algebra = algebra;
        }


        public IAlphaPartitionTree<EdgeValueType> BuildAlphaPartitionTree(
            ITopologyElementEdge element_topology,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType[] element_values)

        {
            return BuildAlphaPartitionTree(element_topology, edge_function, element_values, null);
        }

        public IAlphaPartitionTree<EdgeValueType> BuildAlphaPartitionTree(            
            ITopologyElementEdge element_topology,
            IFunctionDissimilarity<ElementValueType, EdgeValueType> edge_function,
            ElementValueType[] element_values,
            IProgressReporter reporter)
        {
            Debug.Assert(element_topology.ElementCountReal == element_values.Length);

            Tuple<EdgeValueType[], EdgeValueType> element_and_edge_values = 
                element_topology.CreateAlphaPartitionTreeElementArray<ElementValueType, EdgeValueType>(
                    this.algebra,
                    edge_function,
                    element_values);
            EdgeValueType[] edge_values = element_and_edge_values.Item1;
            //Note max tree is really a min tree becuase of the edge value flip flip by the topology
            IMaxTree<EdgeValueType> min_tree = builder.BuildMaxTree(element_and_edge_values.Item1, new ComparerNatural<EdgeValueType>(), element_topology, element_values.Length, reporter);
            return new AlphaPartitionTreeMinTree<EdgeValueType>(algebra, min_tree, element_and_edge_values.Item2);
        }


        public IAlphaPartitionTree<EdgeValueType> BuildAlphaPartitionTree(ITopologyElementEdge element_topology, EdgeValueType[] element_and_egde_values, EdgeValueType max_edge_value, IProgressReporter reporter)
        {
            IMaxTree<EdgeValueType> min_tree = builder.BuildMaxTree(element_and_egde_values, new ComparerNatural<EdgeValueType>(), element_topology, element_topology.ElementCountReal, reporter);
            return new AlphaPartitionTreeMinTree<EdgeValueType>(algebra, min_tree, max_edge_value);
        }
    }
}