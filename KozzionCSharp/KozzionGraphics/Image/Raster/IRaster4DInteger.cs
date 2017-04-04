namespace KozzionGraphics.Image.Raster
{
    public interface IRaster4DInteger : IRasterInteger
    {
        int Size0 { get; }

        int Size1 { get; }

        int Size2 { get; }

        int Size3 { get; }

        bool ContainsCoordinates(
            int coordinate_0,
            int coordinate_1,
            int coordinate_2,
            int coordinate_3);



        int GetElementIndex(
             int coordinate_0,
             int coordinate_1,
             int coordinate_2,
             int coordinate_3);
    }
}