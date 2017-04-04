using System.Diagnostics;
using KozzionCore.Tools;
using System;
namespace KozzionGraphics.Image.Raster
{
    [Serializable()]
    public class Raster1DInteger :  
        ARasterInteger,       
        IRaster1DInteger
    {
        public Raster1DInteger(
            int size_x) :
            base(new int [] {size_x} )
        {
            
        }

        public Raster1DInteger(
            int [] size_array) :
            base(new int[] { size_array[0] })
        {
  
        }


        public int Size0{ get { return size[0]; } }

        public int GetElementIndex(int coordinate_x)
        {
            return coordinate_x;
        }

        public bool ContainsCoordinates(int coordinate_x)
        {
            return ContainsElement(GetElementIndex(coordinate_x));
        } 

        public override int GetElementIndex(int[] coordinates)
        {
            Debug.Assert(coordinates.Length == 1);
            return 1;
        }

        public override void GetElementCoordinatesRBA(
            int element_index,
            int[] coordinates)
        {
            Debug.Assert(coordinates.Length == 1);
            coordinates[0] = element_index;
        }

        public override void GetNeigbourhoodElementIndexesRBA(int[] coordinates, int[] neigbourhood_size, int[] neigbourhood_indexes)
        {
            Debug.Assert(coordinates.Length == 1);
            Debug.Assert(neigbourhood_size.Length == 1);
            int element_index_index = 0;
            for (int coordinate_x = coordinates[0] - neigbourhood_size[0]; coordinate_x < coordinates[0] + neigbourhood_size[0] + 1; coordinate_x++)
            {
                int element_index = GetElementIndex(coordinate_x);
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

        public override bool Equals(object other) 
        {
            if (other is Raster1DInteger)
            {
                Raster1DInteger other_typed = (Raster1DInteger)other;
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
