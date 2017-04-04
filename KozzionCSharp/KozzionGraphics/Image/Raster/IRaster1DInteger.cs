namespace KozzionGraphics.Image.Raster
{
	public interface IRaster1DInteger : IRasterInteger
	{

        int Size0 { get; }

        bool ContainsCoordinates(
			int coordinate_0);

		int GetElementIndex(
			 int coordinate_0);

	}
}