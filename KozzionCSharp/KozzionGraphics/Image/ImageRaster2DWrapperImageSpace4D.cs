using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KozzionMathematics.Function;
using System.Threading.Tasks;
using KozzionCore.Tools;

namespace KozzionGraphics.Image
{
    [Serializable()]
    public class ImageRaster2DWrapperImageSpace4D<RangeType> : IImageRaster2D<RangeType>
    {
        public string FunctionType { get { return "ImageRaster2DWrapperSlice4D"; } }
        public IRaster2DInteger Raster { get; private set; }

        private IImageSpace<float, RangeType> wrapped_image;
        private float[] offset;
        private float[] spaceing_0;
        private float[] spaceing_1;

        public ImageRaster2DWrapperImageSpace4D(IImageSpace<float, RangeType> image, float[] offset, float[] spaceing_0, float[] spaceing_1)
        {
            if (wrapped_image.DimensionCount != 4)
            {
                throw new Exception();
            }

            this.wrapped_image = image;
            this.offset = ToolsCollection.Copy(offset);
            this.spaceing_0 = ToolsCollection.Copy(spaceing_0);
            this.spaceing_1 = ToolsCollection.Copy(spaceing_1);
        }

        public RangeType GetElementValue(int element_index)
        {
            return GetElementValue(Raster.GetElementCoordinates(element_index));
        }

        public RangeType GetElementValue(int[] coordinates)
        {
            return GetElementValue(coordinates[0], coordinates[1]);
        }

        public RangeType GetElementValue(int coordinate_x, int coordinate_y)
        {
            throw new NotImplementedException();
        }

        public void SetElementValue(int element_index, RangeType value)
        {
            throw new NotImplementedException();
        }

        public void SetElementValue(int coordinate_x, int coordinate_y, RangeType value)
        {
            throw new NotImplementedException();
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
                    element_values[element_index_index] = GetElementValue(Raster.GetElementCoordinates(element_index));
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
