namespace KozzionGraphics.Image.Raster
{
    public interface IRaster2DInteger : IRasterInteger
    {
        int Size0 { get; }

        int Size1 { get; }

        bool ContainsCoordinates(
            int coordinate_0,
            int coordinate_1);

        int GetElementIndex(
            int coordinate_0,
            int coordinate_1);



    }
}