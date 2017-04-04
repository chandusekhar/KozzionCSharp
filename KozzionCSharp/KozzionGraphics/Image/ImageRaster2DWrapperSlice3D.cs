using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KozzionMathematics.Function;
using System.Threading.Tasks;

namespace KozzionGraphics.Image
{
    [Serializable()]
    public class ImageRaster2DWrapperSlice3D<RangeType> : IImageRaster2D<RangeType>
    {
        public string FunctionType { get { return "ImageRaster2DWrapperSlice3D"; } }
        public IRaster2DInteger Raster { get; private set; }

        private IImageRaster<IRaster3DInteger, RangeType> wrapped_image;
        private int index_2;

        public ImageRaster2DWrapperSlice3D(IImageRaster<IRaster3DInteger,RangeType> image, int index_2) 
        {
            Debug.Assert(index_2 < image.Raster.Size2);
            Raster = new Raster2DInteger(image.Raster.Size0,image.Raster.Size1);
            this.wrapped_image = image;
            this.index_2 = index_2;
        }

        public RangeType GetElementValue(int element_index)
        {
            int[] coordinates_x_y = Raster.GetElementCoordinates(element_index);
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates_x_y[0], coordinates_x_y[1], index_2);
            return wrapped_image.GetElementValue(element_index_inner);
        }

        public RangeType GetElementValue(int[] coordinates)
        {
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates[0], coordinates[1], index_2);
            return wrapped_image.GetElementValue(element_index_inner);
        }

        public void SetElementValue(int element_index, RangeType value)
        {
            int[] coordinates_x_y = Raster.GetElementCoordinates(element_index);
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates_x_y[0], coordinates_x_y[1], index_2);
            wrapped_image.SetElementValue(element_index_inner, value);
        }

        public RangeType GetElementValue(int coordinate_x, int coordinate_y)
        {
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinate_x, coordinate_y, index_2);
            return wrapped_image.GetElementValue(element_index_inner);
        }

        public void SetElementValue(int coordinate_x, int coordinate_y, RangeType value)
        {
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinate_x, coordinate_y, index_2);
            wrapped_image.SetElementValue(element_index_inner, value);
        }

        public RangeType Compute(int element_index)
        {
            return GetElementValue(element_index);
        }

        public void GetElementValuesRBA(IList<int> element_indexes, IList<RangeType> element_values)
        {
            Debug.Assert(element_indexes.Count == element_values.Count);
            for (int element_index_index = 0; element_index_index < element_indexes.Count; element_index_index++)
            {
                int element_index = element_indexes[element_index_index];


                if (!this.Raster.ContainsElement(element_index))
                {
                    throw new Exception("index out of bounds: " + element_index);
                }



                int[] coordinates_x_y = Raster.GetElementCoordinates(element_index);
                int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates_x_y[0], coordinates_x_y[1], index_2);
                element_values[element_index_index] = wrapped_image.GetElementValue(element_index_inner);

            }
        }


        public RangeType[] GetElementValues(bool copy_values)
        {
            RangeType[] values = new RangeType[Raster.ElementCount];
            for (int element_index = 0; element_index < Raster.ElementCount; element_index++)
            {
                values[element_index] = GetElementValue(element_index);  
            }
            return values;
        }


        public IImageRaster<IRaster2DInteger, TargetRangeType> Convert<TargetRangeType>(IFunction<RangeType, TargetRangeType> converter)
        {
            RangeType[] source_values = GetElementValues(false);
            TargetRangeType [] target_values = new TargetRangeType[Raster.ElementCount];
            Parallel.For(0, Raster.ElementCount, index =>
            {
                target_values[index] = converter.Compute(source_values[index]);
            });
            return new ImageRaster2D<TargetRangeType>(this.Raster, target_values, false);
        }


        public List<int> GetElementIndexesWithValue(RangeType value)
        {
            throw new NotImplementedException();
        }
    }
}
