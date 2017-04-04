using System;

namespace KozzionGraphics.ElementTree.AlphaPartition
{
    public interface IAlphaPartitionTree<EdgeType>
            where EdgeType : IComparable<EdgeType>
    {
        int RealElementCount { get; }
        int FullElementCount { get; }

        int[] GetRealElementsIndexesWithMaxAlfa(
            int element_index,
            EdgeType max_alfa);


        int[] GetFullElementsIndexesWithMaxAlfa(
           int element_index,
           EdgeType max_alfa);


        int[] GetRealElementsIndexesWithMaxSize(
            int element_index,
            int max_size);


        int[] GetFullElementsIndexesWithMaxSize(
           int element_index,
           int max_size);


        int[] GetRealElementsIndexesWithMaxAlfaMaxSize(
            int element_index,
            int max_size,
            EdgeType max_alpha);

        int[] GetFullElementsIndexesWithMaxAlfaMaxSize(
            int element_index, 
            int max_size,
            EdgeType max_alpha);

    }
}
