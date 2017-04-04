using KozzionGraphics.Image;

namespace KozzionGraphics.Filter
{
    public class FilterAutoDualSize
    {
        public int minimal_size;

        public FilterAutoDualSize(int minimal_size) 
        {
            this.minimal_size = minimal_size;
        }

        public IImageRaster3D<float> Filter(IImageRaster3D<float> source_normal)
        {

            IImageRaster3D<float> source_inverted = new ImageRaster3D<float>(source_normal);



            return null;
        }

    }
}
