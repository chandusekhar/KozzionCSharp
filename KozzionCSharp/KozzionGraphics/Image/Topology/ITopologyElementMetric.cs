using KozzionGraphics.Image.Topology;

namespace KozzionGraphics.Image
{
    public interface ITopologyElementMetric : ITopologyElement
    {
        float[] NeigbourDistanceArray { get; }
    }
}
