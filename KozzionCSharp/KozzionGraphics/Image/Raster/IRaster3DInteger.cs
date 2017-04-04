namespace KozzionGraphics.Image.Raster
{
    public interface IRaster3DInteger : IRasterInteger
    {
        int Size0 { get; }

        int Size1 { get; }

        int Size2 { get; }

        bool ContainsCoordinates(
            int coordinate_0, 
            int coordinate_1, 
            int coordinate_2);

        int GetElementIndex(
            int coordinate_0,
            int coordinate_1,
            int coordinate_2);

        IRaster3DInteger GetSubRaster(
            int offset_0,
            int size_0,
            int offset_1,
            int size_1,
            int offset_2,
            int size_2);
    }
}
