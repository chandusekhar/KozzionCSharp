namespace KozzionGraphics.Image.Raster
{
    public interface IRaster<IntegerType>
    {
        IntegerType ElementCount { get; }

        int DimensionCount { get; }

        IntegerType[] SizeArray { get; }

        bool ContainsElement(IntegerType element_index);

        IntegerType GetElementIndex(IntegerType[] coordinates);

        IntegerType[] GetElementCoordinates(
            IntegerType element_index);

        void GetElementCoordinatesFill(
            IntegerType element_index,
            IntegerType[] coordinates);


        void GetNeigbourhoodElementIndexesFill(
            IntegerType[] coordinates,
            IntegerType[] neigbourhood_size,
            IntegerType[] neigbouthood_indexes);
    }
}
