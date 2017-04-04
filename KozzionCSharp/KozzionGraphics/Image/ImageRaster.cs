using KozzionGraphics.Image.Raster;
using System;
using KozzionCore.Tools;
using KozzionMathematics.Function;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
namespace KozzionGraphics.Image
{

    [Serializable]
    public class ImageRaster<RasterType, RangeType>:
            IImageRaster<RasterType, RangeType>
        where RasterType : IRasterInteger
    {
        public string FunctionType { get { return "ImageRaster"; } }

        public RasterType Raster { get; private set; }
        private RangeType[] image;


        public RangeType this[int index]
        {
            get
            {
                if (this.Raster.ContainsElement(index))
                {
                    return image[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set
            {
                if (this.Raster.ContainsElement(index))
                {
                    image[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }


        public ImageRaster(
            RasterType raster)
        {
            this.Raster = raster;
            this.image = new RangeType[raster.ElementCount];
        }

        public ImageRaster(
            RasterType raster,
            RangeType value)
        {
            this.Raster = raster;
            this.image = new RangeType[raster.ElementCount];
            Parallel.For(0, raster.ElementCount, element_index =>
            {
                image[element_index] = value;
            });
        }

        protected ImageRaster(
            RasterType raster, 
            RangeType [] image,
            bool copy_array)
        {
            this.Raster = raster;
            if(copy_array)
            {
               this.image = ToolsCollection.Copy(image);
            }
            else
            {
                this.image = image;
            }
        }

        public ImageRaster(
            ImageRaster<RasterType, RangeType> other)
        {
            this.Raster = other.Raster;
            this.image = ToolsCollection.Copy(other.image);
        }

        public static ImageRaster<CommonRasterType, ToElementType> Convert<CommonRasterType, FromElementType, ToElementType>(
            IImageRaster<CommonRasterType, FromElementType> source,
            IFunction<FromElementType, ToElementType> converter)
            where CommonRasterType : IRasterInteger
        {
            CommonRasterType raster = source.Raster;
            int element_count = raster.ElementCount;
            ToElementType [] image = new ToElementType[element_count];
            Parallel.For(0, element_count, element_index =>
            {
                image[element_index] = converter.Compute(source.GetElementValue(element_index));
            });
            return new ImageRaster<CommonRasterType, ToElementType>(raster, image, false);
        }

        public int GetSize(
            int dimension_index)
        {
            return this.Raster.SizeArray[dimension_index];
        }

        public RangeType GetElementValue(int element_index)
        {
            return this.image[element_index];
        }

        public void SetElementValue(int element_index, RangeType value)
        {
            this.image[element_index] = value;
        }

        public RangeType Compute(int value_domain)
        {
            return GetElementValue(value_domain);
        }

        public int[] SizeArray
        {
            get
            {
                return this.Raster.SizeArray;
            }
        }

        public int DimensionCount
        {
            get
            {
                return this.Raster.DimensionCount;
            }
        }

        public int ElementCount
        {
            get
            {
                return this.Raster.ElementCount;
            }
        }


        public override bool Equals(
            object other)
        {
            if (other is ImageRaster<RasterType, RangeType>)
            {
                ImageRaster<RasterType, RangeType> other_typed = (ImageRaster<RasterType, RangeType>)other;
                if (!this.Raster.Equals(other_typed.Raster))
                {
                    return false;
                }
                for (int index = 0; index < this.image.Length; index++)
                {
                    if (!this.image[index].Equals(other_typed.image[index]))
                    {
                        return false;
                    }
                }

            }

            return true;
        }


        public override int GetHashCode()
        {
            int sum = 0;
            for (int index = 0; index < Raster.DimensionCount; index++)
            {
                sum += this.Raster.SizeArray[index];
            }
            return sum;
        }


        public RangeType GetElementValue(int[] coordinates)
        {
            return this.image[Raster.GetElementIndex(coordinates)];
        }

        public RangeType[] GetElementValues(IList<int> element_indexes)
        {
            RangeType[] element_values = new RangeType[element_indexes.Count];
            GetElementValuesRBA(element_indexes, element_values);
            return element_values;
        }

        public RangeType[] GetElementValues(bool copy_values)
        {
            if (copy_values)
            {
                return ToolsCollection.Copy(this.image);
            }
            else
            {
                return this.image;
            }
        }


        public void GetElementValuesRBA(IList<int> element_indexes, IList<RangeType> element_values)
        {
            if (element_indexes.Count != element_values.Count)
            {
                throw new Exception("input size mismatch");
            }
            for (int element_index_index = 0; element_index_index < element_indexes.Count; element_index_index++)
            {
                int element_index = element_indexes[element_index_index];
                if (!this.Raster.ContainsElement(element_index))
                {
                    throw new Exception("index out of bounds: " + element_index);
                }
                element_values[element_index_index] = this.image[element_index];
            }
        }

        public void SetElementValues(RangeType element_value)
        {
            Parallel.For(0, ElementCount, element_index =>
            {
                this.image[element_index] = element_value;
            });
        }

        public void SetElementValues(IList<int> element_indexes, IList<RangeType> element_values)
        {
            if (element_indexes.Count != element_values.Count)
            {
                throw new Exception("input size mismatch");
            }
            for (int element_index_index = 0; element_index_index < element_indexes.Count; element_index_index++)
            {
                int element_index = element_indexes[element_index_index];
                if (!this.Raster.ContainsElement(element_index))
                {
                    throw new Exception("index out of bounds: " + element_index);
                }
                this.image[element_index] = element_values[element_index];
            }
        }

        public void SetElementValues(IList<int> element_indexes, RangeType element_value)
        {
            for (int element_index_index = 0; element_index_index < element_indexes.Count; element_index_index++)
            {
                int element_index = element_indexes[element_index_index];
                if (!this.Raster.ContainsElement(element_index))
                {
                    throw new Exception("index out of bounds: " + element_index);
                }
                this.image[element_index] = element_value;
            }
        }

        public List<int> GetElementIndexesWithValue(RangeType value)
        { 
            List<int> element_indexes = new List<int>();
            for (int element_index = 0; element_index < this.image.Length; element_index++)
            {
                if (image[element_index].Equals(value))
                {
                    element_indexes.Add(element_index);
                }
            }
            return element_indexes;
        }

        public IImageRaster<RasterType, TargetRangeType> Convert<TargetRangeType>(IFunction<RangeType, TargetRangeType> converter)
        {
            RangeType[] source_values = GetElementValues(false);
            TargetRangeType[] target_values = new TargetRangeType[Raster.ElementCount];
            Parallel.For(0, this.Raster.ElementCount, index =>
            {
                target_values[index] = converter.Compute(source_values[index]);
            });
            return new ImageRaster<RasterType, TargetRangeType>(this.Raster, target_values, false);
        }

        public override string ToString()
        {
            return this.image.ToString();
        }
    }
}