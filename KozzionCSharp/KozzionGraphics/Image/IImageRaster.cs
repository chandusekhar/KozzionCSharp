using System.Collections.Generic;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function;

namespace KozzionGraphics.Image
{
    public interface IImageRaster<RasterType, RangeType> : IImage<int, RangeType>
        where RasterType : IRasterInteger
    {
        RasterType Raster { get; }

        RangeType GetElementValue(int index_element);

        RangeType GetElementValue(int[] coordinates);

        RangeType[] GetElementValues(bool copy_values);

        void SetElementValue(int index_element, RangeType value);

        void GetElementValuesRBA(IList<int> element_indexes, IList<RangeType> element_values);

        List<int> GetElementIndexesWithValue(RangeType value);

        IImageRaster<RasterType, TargetElementValueType> Convert<TargetElementValueType>(IFunction<RangeType, TargetElementValueType> converter);


    }
}
