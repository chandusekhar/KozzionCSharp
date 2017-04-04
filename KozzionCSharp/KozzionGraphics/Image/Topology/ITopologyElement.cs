namespace KozzionGraphics.Image.Topology
{
    public interface ITopologyElement
    {

        int MaximumConnectivity {get; }

        int ElementCount {get; }

        ITopologyElementEdge GetTopologyElementEdge();

        void ElementNeighboursRBA(int element_index, int[] element_neigbour_array);
    }
}
