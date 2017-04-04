namespace KozzionGraphics.Image.Raster
{
	public interface IRasterInteger
	{    

		int ElementCount{ get; }

        int DimensionCount { get; }

        int[] SizeArray { get; }

        bool ContainsElement(int element_index);

        bool ContainsCoordinates(int [] coordinates);

        int GetElementIndex(int[] coordinates);

        int[] GetElementCoordinates(
            int element_index);

		void GetElementCoordinatesRBA(
			int element_index,
			int [] coordinates);


        void GetNeigbourhoodElementIndexesRBA(
            int[] coordinates, 
            int[] neigbourhood_size, 
            int[] neigbouthood_indexes);
	}
}