using System.Diagnostics;
using KozzionCore.Tools;
using System;
namespace KozzionGraphics.Image.Raster
{
    [Serializable()]
    public class Raster2DInteger: 
        ARasterInteger,
         IRaster2DInteger
    {
        public int Size0 { get { return size[0]; } }

        public int Size1 { get { return size[1]; } }

        public Raster2DInteger(
            int size_0,
            int size_1) : 
            base (new int [] {size_0, size_1})
        {

        }

        public Raster2DInteger(
            int [] size_array) :
            base(new int [] {size_array[0], size_array[1]})
        {
            
        } 




        public int GetElementIndex(
            int coordinate_0,
            int coordinate_1)
        {
            return coordinate_0 + (coordinate_1 * size[0]);
        }

     

        public bool ContainsCoordinates(int coordinate_0, int coordinate_1)
        {
            if (coordinate_0 < 0 || size[0] <= coordinate_0)
            { 
                return false;
            }
            if (coordinate_1 < 0 || size[1] <= coordinate_1)
            {
                return false;
            }
            return true;
        }

        public override int GetElementIndex(
            int [] coordinates)
        {
            Debug.Assert(coordinates.Length == 2);
            return coordinates[0] + (coordinates[1] * size[0]);
        }

        public override void GetElementCoordinatesRBA(
            int element_index,
            int [] coordinates)
        {
            Debug.Assert(coordinates.Length == 2);
            coordinates[0] = element_index % size[0];
            coordinates[1] = element_index / size[0];
        
        }

        public override void GetNeigbourhoodElementIndexesRBA(int[] coordinates, int[] neigbourhood_size, int[] neigbourhood_indexes)
        {
            Debug.Assert(coordinates.Length == 2);
            Debug.Assert(neigbourhood_size.Length == 2);
            int element_index_index = 0;

            for (int coordinate_y = coordinates[1] - neigbourhood_size[1]; coordinate_y < coordinates[1] + neigbourhood_size[1] + 1; coordinate_y++)
            {
                for (int coordinate_x = coordinates[0] - neigbourhood_size[0]; coordinate_x < coordinates[0] + neigbourhood_size[0] + 1; coordinate_x++)
                {
                    int element_index = GetElementIndex(coordinate_x, coordinate_y);
                    if (ContainsElement(element_index))
                    {
                        neigbourhood_indexes[element_index_index] = element_index;
                    }
                    else
                    {
                        neigbourhood_indexes[element_index_index] = -1;
                    }

                    element_index_index++;
                }
            }
        }

        public override bool Equals(object other)
        {
            if (other is Raster2DInteger)
            {
                Raster2DInteger other_typed = (Raster2DInteger)other;
                return ToolsCollection.EqualsArray(SizeArray, other_typed.SizeArray);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
