using System;
using System.Threading.Tasks;
using KozzionGraphics.Image.Topology;

namespace KozzionGraphics.supershape
{
    public class ShapeTree
    {
        public Tuple<int[,], float[,]> build_super_shape(float[] image, int neigbour_count, ITopologyElement topology)
        {


            int[,] neigbour_elements = new int[image.Length, neigbour_count];
            float[,] neigbour_distances = new float[image.Length, neigbour_count];
            Parallel.For(0, image.Length, index_element =>
            {
                //int index_current = 0;
                //int index_fringe_end = 0;
                for (int i = 0; i < neigbour_count; i++)
                {
                    
                }
            });


            return new Tuple<int[,], float[,]>(neigbour_elements, neigbour_distances);
        }
    }
}
