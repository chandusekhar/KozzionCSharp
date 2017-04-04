using System.Diagnostics;
using KozzionCore.Tools;
using System;

namespace KozzionGraphics.Image.Raster
{
    [Serializable()]
    public class Raster4DInteger :
            ARasterInteger,
            IRaster4DInteger
    {
        public Raster4DInteger(
            int size_x,
            int size_y,
            int size_z,
            int size_t) :
            base(new int[] { size_x, size_y, size_z, size_t })
        {

        }

        public Raster4DInteger(
            int[] size_array) :
            base(new int[] { size_array[0], size_array[1], size_array[2], size_array[3] })
        {

        }

 



        public int Size0 { get { return size[0]; } }

        public int Size1 { get { return size[1]; } }

        public int Size2 { get { return size[2]; } }

        public int Size3 { get { return size[3]; } }

        public int GetElementIndex(
            int coordinate_x,
            int coordinate_y,
            int coordinate_z,
            int coordinate_t)
        {

            return coordinate_x + (coordinate_y * size[0]) + (coordinate_z * size[0] * size[1])
                + (coordinate_t * size[0] * size[1] * size[2]);
        }

        public bool ContainsCoordinates(int coordinate_x, int coordinate_y, int coordinate_z, int coordinate_t)
        {
            if (coordinate_x < 0 || size[0] <= coordinate_x)
            {
                return false;
            }
            if (coordinate_y < 0 || size[1] <= coordinate_y)
            {
                return false;
            }

            if (coordinate_z < 0 || size[2] <= coordinate_z)
            {
                return false;
            }

            if (coordinate_t < 0 || size[3] <= coordinate_t)
            {
                return false;
            }
            return true;
        }

        public override int GetElementIndex(
            int[] coordinates)
        {
            Debug.Assert(coordinates.Length == 4);

            return coordinates[0] + (coordinates[1] * size[0]) + (coordinates[2] * size[0] * size[1])
       + (coordinates[3] * size[0] * size[1] * size[2]);
        }

        public override void GetElementCoordinatesRBA(
            int element_index,
            int[] coordinates)
        {
            Debug.Assert(coordinates.Length == 4);
            coordinates[0] = element_index % size[0];
            coordinates[1] = (element_index % (size[0] * size[1])) / size[0];
            coordinates[2] = (element_index % (size[0] * size[1] * size[2])) / (size[0] * size[1]);
            coordinates[3] = element_index / (size[0] * size[1] * size[2]);

        }

        public override void GetNeigbourhoodElementIndexesRBA(int[] coordinates, int[] neigbourhood_size, int[] neigbourhood_indexes)
        {
            Debug.Assert(coordinates.Length == 4);
            Debug.Assert(neigbourhood_size.Length == 4);
            int element_index_index = 0;
            for (int coordinate_t = coordinates[3] - neigbourhood_size[3]; coordinate_t < coordinates[3] + neigbourhood_size[3] + 1; coordinate_t++)
            {
                for (int coordinate_z = coordinates[2] - neigbourhood_size[2]; coordinate_z < coordinates[2] + neigbourhood_size[2] + 1; coordinate_z++)
                {
                    for (int coordinate_y = coordinates[1] - neigbourhood_size[1]; coordinate_y < coordinates[1] + neigbourhood_size[1] + 1; coordinate_y++)
                    {
                        for (int coordinate_x = coordinates[0] - neigbourhood_size[0]; coordinate_x < coordinates[0] + neigbourhood_size[0] + 1; coordinate_x++)
                        {
                            int element_index = GetElementIndex(coordinate_x, coordinate_y, coordinate_z, coordinate_t);
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
            }
        }

        public override bool Equals(object other)
        {
            if (other is Raster4DInteger)
            {
                Raster4DInteger other_typed = (Raster4DInteger)other;
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
