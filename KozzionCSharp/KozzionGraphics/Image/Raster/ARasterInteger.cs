using KozzionCore.Tools;
using KozzionMathematics.Tools;
using System;
using KozzionMathematics.Algebra;

namespace KozzionGraphics.Image.Raster
{
    [Serializable()]
	public abstract class ARasterInteger : IRasterInteger
	{
        protected int[] size;

        public int[] SizeArray
        {
            get
            {
                return ToolsCollection.Copy(size);
            }
        }

        public int ElementCount { get; private set; }

        public int DimensionCount { get { return size.Length; } }

		protected ARasterInteger(
            int [] size)
		{
            this.size = size;
            ElementCount = ToolsMathCollectionInteger.product(this.size);   
		}

        public bool ContainsElement(int element_index)
        {
            if ((ElementCount <= element_index) || (element_index < 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ContainsCoordinates(int[] coordinates)
        {
            if (coordinates.Length != this.size.Length)
            {
                throw new ArgumentException("Lenghts do not agree");
            }
            for (int dimension_index = 0; dimension_index < this.size.Length; dimension_index++)
            {
                if ((coordinates[dimension_index] < 0) || (this.size[dimension_index] <= coordinates[dimension_index]))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object other)
		{
			if (other is IRasterInteger)
			{
				IRasterInteger other_typed = (IRasterInteger)other;
                return ToolsCollection.EqualsArray(size, other_typed.SizeArray);
			}
			return false;
		}

        public static IRasterInteger CreateDefaultRaster(int[] raster_dimensions)
        {
            switch (raster_dimensions.Length)
            {
                case 0:
                    throw new Exception("Cannot create 0-sized raster");
                case 1:
                    return new Raster1DInteger(raster_dimensions);
                case 2:
                    return new Raster2DInteger(raster_dimensions);
                case 3:
                    return new Raster3DInteger(raster_dimensions);
                case 4:
                    return new Raster4DInteger(raster_dimensions);
                default :
                    throw new Exception("Cannot create raster with more than 5 dimensions");
            }
        }

        public abstract int GetElementIndex(int[] coordinates);

        public int [] GetElementCoordinates(int element_index)
        {
            int [] coordinates = new int [size.Length];
            GetElementCoordinatesRBA(element_index, coordinates);
            return coordinates;
        }

        public abstract void GetElementCoordinatesRBA(int element_index, int[] coordinates);
        public abstract void GetNeigbourhoodElementIndexesRBA(int[] coordinates, int[] neigbourhood_size, int[] neigbourhood_indexes);


        public override int GetHashCode()
        {
            return ToolsMathCollection.Sum(new AlgebraIntegerInt32(), size);
        }
    
    }
}
