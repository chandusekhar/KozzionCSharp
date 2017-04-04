namespace KozzionGraphics.Image
{
    public interface IImageSpacel2D<ElementType>
	{
        ElementType GetValue(float coordinate_x, float coordinate_y);

	}
}