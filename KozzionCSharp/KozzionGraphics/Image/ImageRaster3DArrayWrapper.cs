using System;
using System.Collections.Generic;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function;

namespace KozzionGraphics.Image
{
    [Serializable()]
    public class ImageRaster3DArrayWrapper<RangeType> : IImageRaster3D<RangeType []>
    {
        public string FunctionType { get { return "ImageRaster3DArrayWrapper"; } }
        public IRaster3DInteger Raster { get; private set; }
        private IImageRaster4D<RangeType> inner;

        public ImageRaster3DArrayWrapper(IImageRaster4D<RangeType> inner)
        {
            this.Raster = new Raster3DInteger(inner.Raster.Size0, inner.Raster.Size1, inner.Raster.Size2);
            this.inner = inner;
        }


        public RangeType [] GetElementValue(int coordinate_x, int coordinate_y, int coordinate_z)
        {
            RangeType[] values = new RangeType[inner.Raster.Size3];
            for (int coordinate_t = 0; coordinate_t < inner.Raster.Size3; coordinate_t++)
            {
                values[coordinate_t] = inner.GetElementValue(coordinate_x, coordinate_y, coordinate_z, coordinate_t);
            }
            return values;
        }

        public RangeType[] GetElementValue(int index_element)
        {
            int[] coordinates = Raster.GetElementCoordinates(index_element);
            return GetElementValue(coordinates[0], coordinates[1], coordinates[2]);

        }

        public void SetElementValue(int coordinate_x, int coordinate_y, int coordinate_z, RangeType [] value)
        {
            SetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y, coordinate_z), value);
        }

        public void SetElementValue(int element_index, RangeType[] value)
        {
            throw new NotImplementedException();
        }


        public ImageRaster3D<TargetRangeType> ConvertTyped<TargetRangeType>(IFunction<RangeType[], TargetRangeType> converter)
        {
            RangeType[][] element_values = GetElementValues(false);
            TargetRangeType[] data = new TargetRangeType[element_values.Length];
            for (int index = 0; index < element_values.Length; index++)
            {
                data[index] = converter.Compute(element_values[index]);
            }
            return new ImageRaster3D<TargetRangeType>(Raster, data, false);
        }


        public ImageRaster3D<RangeType> GetSubImage(
            int offset_x, int size_x,
            int offset_y, int size_y,
            int offset_z, int size_z)
        {
            throw new NotImplementedException();
        }
    

        public RangeType[] GetElementValue(int[] coordinates)
        {
            throw new NotImplementedException();
        }

        public RangeType[][] GetElementValues(bool copy_values)
        {
            if (copy_values)
            {
                RangeType[][] data = new RangeType[this.Raster.ElementCount][];
                for (int coordinate_z = 0; coordinate_z < this.Raster.Size2; coordinate_z++)
                {
                    for (int coordinate_y = 0; coordinate_y < this.Raster.Size1; coordinate_y++)
                    {
                        for (int coordinate_x = 0; coordinate_x < this.Raster.Size0; coordinate_x++)
                        {
                            int element_index = Raster.GetElementIndex(coordinate_x, coordinate_y, coordinate_z);
                            data[element_index] = new RangeType[inner.Raster.Size3];
                            for (int coordinate_t = 0; coordinate_t < inner.Raster.Size3; coordinate_t++)
                            {
                                data[element_index][coordinate_t] = inner.GetElementValue(coordinate_x, coordinate_y, coordinate_z, coordinate_t);
                            }
                        }
                    }
                }
                return data;
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public void GetElementValuesRBA(IList<int> element_indexes, IList<RangeType[]> element_values)
        {
            throw new NotImplementedException();
        }

        public IImageRaster<IRaster3DInteger, TargetElementValueType> Convert<TargetElementValueType>(IFunction<RangeType[], TargetElementValueType> converter)
        {
            throw new NotImplementedException();
        }

        public RangeType[] Compute(int value_domain)
        {
            return GetElementValue(value_domain);
        }


        public List<int> GetElementIndexesWithValue(RangeType[] value)
        {
            throw new NotImplementedException();
        }
    }
}
