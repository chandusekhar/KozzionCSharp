using System;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Algebra;

namespace KozzionGraphics.Image
{
    public class ImageSpace3DFloatLinear : IImageSpace3D<float, float>
    {
        private IImageRaster<IRaster3DInteger, float> image;
        private IAlgebraReal<float> algebra;
        private float space_size_0;
        private float space_size_1;
        private float space_size_2;
        private float background_value;

        public int DimensionCount
        {
            get
            {
                return 3;
            }
        }

        public ImageSpace3DFloatLinear(IImageRaster<IRaster3DInteger, float> image) 
        {
            this.image = image;
            this.algebra = new AlgebraRealFloat32();
            this.space_size_0 = 1;
            this.space_size_1 = 1;
            this.space_size_2 = 1;
            this.background_value = 0;
        }

        public float GetLocationValue(float coordinate_0_space, float coordinate_1_space, float coordinate_2_space)
        {
            float coordinate_0_image = coordinate_0_space / this.space_size_0;
            float coordinate_1_image = coordinate_1_space / this.space_size_1;
            float coordinate_2_image = coordinate_2_space / this.space_size_2;

            float sum_weights = 0;
            float sum_values  = 0;
            int offset_0 = (int)Math.Floor(coordinate_0_image);
            int offset_1 = (int)Math.Floor(coordinate_1_image);
            int offset_2 = (int)Math.Floor(coordinate_2_image);

            for (int index_0 = offset_0; index_0 < offset_0 + 2; index_0++)
			{
			   for (int index_1 = offset_1; index_1 < offset_1 + 2; index_1++)
			   {   
                    for (int index_2 = offset_2; index_2 < offset_2 + 2; index_2++)
			        {
                        float weight = 1 - (Math.Abs(index_0 - coordinate_0_image) * Math.Abs(index_1 - coordinate_2_image) * Math.Abs(index_2 - coordinate_2_image));
                        sum_weights += weight;
                        if(this.image.Raster.ContainsCoordinates(index_0,index_1, index_2))
                        {
                            sum_values += weight  * this.image.GetElementValue(this.image.Raster.GetElementIndex(index_0, index_1, index_2));
                        }
                        else
                        {
                            sum_values += weight * background_value;
                        }
                    }
               }
			}
            return sum_values / sum_weights;
        }

        public float GetLocationValue(float[] coordinates_space)
        {
            return GetLocationValue(coordinates_space[0], coordinates_space[1], coordinates_space[2]);
        }
    }
}
