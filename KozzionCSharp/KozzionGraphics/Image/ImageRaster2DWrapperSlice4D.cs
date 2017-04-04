using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KozzionMathematics.Function;
using System.Threading.Tasks;

namespace KozzionGraphics.Image
{
    [Serializable()]
    public class ImageRaster2DWrapperSlice4D<RangeType> : IImageRaster2D<RangeType>
    {
        public string FunctionType { get { return "ImageRaster2DWrapperSlice4D"; } }
        public IRaster2DInteger Raster { get; private set; }

        private IImageRaster<IRaster4DInteger, RangeType> wrapped_image;
        private int index_2;
        private int index_3;

        public ImageRaster2DWrapperSlice4D(IImageRaster<IRaster4DInteger, RangeType> image, int index_2, int index_3) 
        {
            if (image.Raster.Size2 <= index_2)
            {
                throw new Exception("index_2 with value " + index_2 + " out of bounds size_2 = " + image.Raster.Size2);
            }

            if (image.Raster.Size3 <= index_3)
            {
                throw new Exception("index_3 with value " + index_3 + " out of bounds size_3 = " + image.Raster.Size3);
            }

            Raster = new Raster2DInteger(image.Raster.Size0,image.Raster.Size1);
            this.wrapped_image = image;
            this.index_2 = index_2;
            this.index_3 = index_3;
        }

        public RangeType GetElementValue(int element_index)
        {
            int[] coordinates_x_y = Raster.GetElementCoordinates(element_index);
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates_x_y[0], coordinates_x_y[1], index_2, index_3);
            return wrapped_image.GetElementValue(element_index_inner);
        }

        public RangeType GetElementValue(int[] coordinates)
        {
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates[0], coordinates[1], index_2, index_3);
            return wrapped_image.GetElementValue(element_index_inner);
        }

        public void SetElementValue(int element_index, RangeType value)
        {
            int[] coordinates_x_y = Raster.GetElementCoordinates(element_index);
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates_x_y[0], coordinates_x_y[1], index_2, index_3);
            wrapped_image.SetElementValue(element_index_inner, value);
        }

        public RangeType GetElementValue(int coordinate_x, int coordinate_y)
        {
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinate_x, coordinate_y, index_2, index_3);
            return wrapped_image.GetElementValue(element_index_inner);
        }

        public void SetElementValue(int coordinate_x, int coordinate_y, RangeType value)
        {
            int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinate_x, coordinate_y, index_2, index_3);
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
                if (Raster.ContainsElement(element_index))
                {
                    int[] coordinates_x_y = Raster.GetElementCoordinates(element_index);
                    int element_index_inner = wrapped_image.Raster.GetElementIndex(coordinates_x_y[0], coordinates_x_y[1], index_2, index_3);
                    element_values[element_index_index] = wrapped_image.GetElementValue(element_index_inner);
                }
                else
                {      
                    throw new Exception("index out of bounds: " + element_index);      
                }
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
