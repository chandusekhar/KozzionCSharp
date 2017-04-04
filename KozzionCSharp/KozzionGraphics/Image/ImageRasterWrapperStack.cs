using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KozzionMathematics.Function;

namespace KozzionGraphics.Image
{
    public class ImageRasterWrapperStack<RasterType, RangeType> : IImageRaster<RasterType, RangeType []>
        where RasterType : IRasterInteger
    {
        public string FunctionType { get { return "ImageRasterWrapperStack"; } }
        private IList<IImageRaster<RasterType, RangeType>> d_images;
        public RasterType Raster { get; private set; }
         
        public ImageRasterWrapperStack(IList<IImageRaster<RasterType, RangeType>> images) 
        {
            Debug.Assert(0 < images.Count);
            Raster = images[0].Raster;
            for (int image_index = 1; image_index < images.Count; image_index++)
            {
                  Debug.Assert(0 < images.Count);
            }
            d_images = new List<IImageRaster<RasterType, RangeType>>(images);
        }

        public RangeType[] GetElementValue(int element_index)
        {
            RangeType[] array = new RangeType[d_images.Count];
            for (int image_index = 0; image_index < d_images.Count; image_index++)
            {
                array[image_index] = d_images[image_index].GetElementValue(element_index);
            }
            return array;
        }

        public RangeType[] GetElementValue(int[] coordinates)
        {
            int element_index = Raster.GetElementIndex(coordinates);
            RangeType[] array = new RangeType[d_images.Count];
            for (int image_index = 0; image_index < d_images.Count; image_index++)
            {
                array[image_index] = d_images[image_index].GetElementValue(element_index);
            }
            return array;
        }

        public void SetElementValue(int element_index, RangeType[] value)
        {
            for (int image_index = 0; image_index < d_images.Count; image_index++)
            {
                d_images[image_index].SetElementValue(element_index, value[image_index]);
            }
        }

        public RangeType[] Compute(int element_index)
        {
            return GetElementValue(element_index);
        }


        public void GetElementValuesRBA(IList<int> element_indexes, IList<RangeType[]> element_values)
        {
            Debug.Assert(element_indexes.Count == element_values.Count);
            for (int element_index_index = 0; element_index_index < element_indexes.Count; element_index_index++)
            {
                int element_index = element_indexes[element_index_index];
                if (Raster.ContainsElement(element_index))
                {
                    for (int image_index = 0; image_index < d_images.Count; image_index++)
                    {
                        element_values[element_index_index][image_index] = d_images[image_index].GetElementValue(element_index);
                    }
                }
                else
                {
                    throw new Exception("Index out of bounds");
                }
            }
        }


        public RangeType[][] GetElementValues(bool copy_values)
        {
            throw new NotImplementedException();
        }


        public IImageRaster<RasterType, TargetElementValueType> Convert<TargetElementValueType>(IFunction<RangeType[], TargetElementValueType> converter)
        {
            throw new NotImplementedException();
        }


        public List<int> GetElementIndexesWithValue(RangeType[] value)
        {
            throw new NotImplementedException();
        }
    }
}
