using System;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image
{
    [Serializable()]
	public class ImageRaster4D<ElementType> : ImageRaster<IRaster4DInteger, ElementType>, IImageRaster4D<ElementType>
	{
        public ImageRaster4D() :
			base(new Raster4DInteger(1, 1, 1, 1))
		{
		
		}

        public ImageRaster4D(IRaster4DInteger raster)
            : base(raster)
        {

        }

        public ImageRaster4D(IRaster4DInteger raster, ElementType value)
            : base(raster, value)
        {

        }

        public ImageRaster4D(IImageRaster4D<ElementType> other)
            : this(other.Raster, other.GetElementValues(true), false)
        {

        }

        public ImageRaster4D(
            int size_x,
            int size_y,
            int size_z,
            int size_t) :
            base(new Raster4DInteger(size_x, size_y, size_z, size_t))
        {

        }

		public ImageRaster4D(
			int size_x,
			int size_y,
            int size_z,
            int size_t,
            ElementType[] image,
			bool copy_array):
            base(new Raster4DInteger(size_x, size_y, size_z, size_t), image, copy_array)
		{
		
		}

        public ImageRaster4D(
            IRaster4DInteger raster,
			ElementType[] image,
			bool copy_array) :
            base(raster, image, copy_array)
        {

        }

		public ImageRaster4D(
			  int size_x,
			  int size_y,
              int size_z,
              int size_t,
              ElementType value) :
            base(new Raster4DInteger(size_x, size_y, size_z, size_t), value)
		{		

		}

        public ImageRaster4D(IImageRaster<IRaster4DInteger, ElementType> other, bool copy_values)
            : this(other.Raster, other.GetElementValues(false), copy_values)
        {

        }

 

        public ElementType GetElementValue(int coordinate_x, int coordinate_y, int coordinate_z, int coordinate_t)
        {
            return GetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y, coordinate_z, coordinate_t));
        }

        public void SetElementValue(int coordinate_x, int coordinate_y, int coordinate_z, int coordinate_t, ElementType value)
        {
            SetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y, coordinate_z, coordinate_t), value);
        }



        public ImageRaster4D<ElementType> GetSubImage(
            int x_offset,
            int x_size,
            int y_offset,
            int y_size,
            int z_offset,
            int z_size,
            int t_offset,
            int t_size,            
            bool copy_values)
        {
            if (copy_values)
            {
                ImageRaster4D<ElementType> sub_image = new ImageRaster4D<ElementType>(x_size, y_size, z_size, t_size);
                for (int t_index = 0; t_index < t_size; t_index++)
                {
                    for (int z_index = 0; z_index < z_size; z_index++)
                    {
                        for (int y_index = 0; y_index < y_size; y_index++)
                        {
                            for (int x_index = 0; x_index < x_size; x_index++)
                            {
                                ElementType value = GetElementValue(x_index + x_offset, y_index + y_offset, z_index + z_offset, t_index + t_offset);
                                sub_image.SetElementValue(x_index, y_index, z_index, t_index, value);
                            }
                        }
                    }
                }
                return sub_image;
            }
            else {
                throw new NotImplementedException();
            }
        }
    }
}