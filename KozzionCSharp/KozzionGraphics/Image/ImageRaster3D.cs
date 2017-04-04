using System;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function;

namespace KozzionGraphics.Image
{
    [Serializable()]
	public class ImageRaster3D<RangeType> : ImageRaster<IRaster3DInteger, RangeType>, IImageRaster3D<RangeType>
	{
        public ImageRaster3D()
            : this(new Raster3DInteger(1, 1, 1))
        {

        }

        public ImageRaster3D(IImageRaster<IRaster3DInteger, RangeType> other)
       : this(other.Raster, other.GetElementValues(true), false)
        {

        }

        public ImageRaster3D(IImageRaster<IRaster2DInteger, RangeType> other, bool copy_values)
            : this(other.Raster.Size0, other.Raster.Size1, 1, other.GetElementValues(copy_values), false)
        {

        }

        public ImageRaster3D(IImageRaster3D<RangeType> other, bool copy_values)
            : this(other.Raster, other.GetElementValues(copy_values), false)
        {

        }
	

        public ImageRaster3D(
            int size_x,
            int size_y,
            int size_z)
            : this(new Raster3DInteger(size_x, size_y, size_z))
        {
        }

        public ImageRaster3D(
			int size_x,
			int size_y,
            int size_z,
            RangeType[] image,
			bool copy_array):
			this(new Raster3DInteger(size_x, size_y, size_z), image, copy_array)
		{
		
		}

        public ImageRaster3D(IRaster3DInteger raster)
            : base(raster)
        {

        }   

        public ImageRaster3D(
            IRaster3DInteger raster,
			RangeType[] image,
			bool copy_array) 
            : base(raster, image, copy_array)
        {

        }

		public ImageRaster3D(
			  int size_x,
			  int size_y,
              int size_z,
              RangeType value) 
            : base(new Raster3DInteger(size_x, size_y, size_z), value)
		{		

		}



        public ImageRaster3D(IRaster3DInteger raster, RangeType value)
            : this(raster.Size0, raster.Size1, raster.Size2, value)
        {
        
        }



     

 

        public RangeType GetElementValue(int coordinate_x, int coordinate_y, int coordinate_z)
        {
            return GetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y, coordinate_z));
        }

        public void SetElementValue(int coordinate_x, int coordinate_y, int coordinate_z, RangeType value)
        {
            SetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y, coordinate_z), value);
        }


        public ImageRaster3D<TargetRangeType> ConvertTyped<TargetRangeType>(IFunction<RangeType, TargetRangeType> converter)
        {
            RangeType [] element_values = GetElementValues(false);
            TargetRangeType[] data = new TargetRangeType[element_values.Length];
            for (int index = 0; index < element_values.Length; index++)
			{
                data[index] = converter.Compute(element_values[index]);
			}
            return new ImageRaster3D<TargetRangeType>(Raster, data, false);
        }


        public ImageRaster3D<RangeType> GetSubImage(
            int x_offset, int x_size,
            int y_offset, int y_size,
            int z_offset, int z_size,
            bool copy_values)
        {
            //TODO check if it even fits
            if (copy_values)
            {
                IRaster3DInteger raster = new Raster3DInteger(x_size, y_size, z_size);
                ImageRaster3D<RangeType> sub_image = new ImageRaster3D<RangeType>(raster);
                for (int z_index = 0; z_index < z_size; z_index++)
                {
                    for (int y_index = 0; y_index < y_size; y_index++)
                    {
                        for (int x_index = 0; x_index < x_size; x_index++)
                        {
                            RangeType value = GetElementValue(x_index + x_offset, y_index + y_offset, z_index + z_offset);
                            sub_image.SetElementValue(x_index, y_index, z_index, value);
                        }
                    }
                }
                return sub_image;
            }
            else
            {
                //TODO this is buggy as fuck
                IRaster3DInteger raster = this.Raster.GetSubRaster(
                    x_offset, x_size,
                    y_offset, y_size,
                    z_offset, z_size);
                return new ImageRaster3D<RangeType>(raster, GetElementValues(false), false);
            }
        }

  

    }
}