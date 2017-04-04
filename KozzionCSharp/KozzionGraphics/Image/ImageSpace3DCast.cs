using System;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image
{
    public class ImageSpace3DCast<RangeType> : IImageSpace3D<float, RangeType>
    {
        private IImageRaster<IRaster3DInteger, RangeType> image;
        private float [] space_size;
        private RangeType background_value;

        public int DimensionCount
        {
            get
            {
                return 3;
            }
        }

        public ImageSpace3DCast(IImageRaster<IRaster3DInteger, RangeType> image, float[] space_size, RangeType background_value) 
        {
            this.image = image;
            this.space_size = space_size;
            this.background_value = background_value;
        }

        public RangeType GetLocationValue(float coordinate_0_space, float coordinate_1_space, float coordinate_2_space)
        {
            int coordinate_0_image = (int)Math.Round(coordinate_0_space / this.space_size[0]);
            int coordinate_1_image = (int)Math.Round(coordinate_1_space / this.space_size[1]);
            int coordinate_2_image = (int)Math.Round(coordinate_2_space / this.space_size[2]);

            if (this.image.Raster.ContainsCoordinates(coordinate_0_image, coordinate_1_image, coordinate_2_image))
            {
                return this.image.GetElementValue(this.image.Raster.GetElementIndex(coordinate_0_image, coordinate_1_image, coordinate_2_image));
            }
            else
            {
                return this.background_value;
            }
        }

        public RangeType GetLocationValue(float[] coordinates_space)
        {
            return GetLocationValue(coordinates_space[0], coordinates_space[1], coordinates_space[2]);
        }
    }
}
