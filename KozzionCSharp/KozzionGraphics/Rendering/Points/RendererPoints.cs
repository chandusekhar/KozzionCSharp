using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionGraphics.ColorFunction;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionCore.DataStructure.Science;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Rendering.Raster
{
    public class RendererPoints<MatrixType> : IRendererBitmapFast<Tuple<double[][], Color[]>>
    {
        private int bitmap_size_x;
        private int bitmap_size_y;
        private double scale; //TODO scale  and offset should be in matrix (quaternions needed)
        private AMatrix<MatrixType> projection_matrix;
        private AMatrix<MatrixType> offset_matrix;


        public RendererPoints(IAlgebraLinear<MatrixType> algebra, int bitmap_size_x, int bitmap_size_y, AngleRadian pitch, AngleRadian yaw, double[] offset, double scale)
        {

            this.bitmap_size_x = bitmap_size_x;
            this.bitmap_size_y = bitmap_size_y;
            this.scale = scale;
            this.offset_matrix = algebra.Create(offset);
            //float [] vector_view = new float[3]; 
            // unit vector in the direction we view, 0,0 is from the top the regular way,  should produce [0 0 0]
            //(0,0 should procux x = [1 0 0], y = [0 1 0]) 

            //isometric projection
            //http://www.wolframalpha.com/input/?i=%7B%7B1%2C+0+%2C0%7D%2C%7B0%2C+cos%28a%29%2C+sin%28a%29%7D%2C%7B0%2C+-sin%28a%29%2C+cos%28a%29%7D%7D.%7B%7Bcos%28b%29%2C+0%2C+-sin%28b%29%7D%2C%7B0%2C1%2C0%7D%2C%7Bsin%28b%29%2C+0+%2C+cos%28b%29%7D%7D
            double a = (double)pitch;
            double b = (double)yaw;
            double[,]  projection_array = new double[,] {
                { Math.Cos(b), Math.Sin(a) * Math.Sin(b), Math.Cos(a) * Math.Sin(b) },
                { 0, Math.Cos(a), -Math.Sin(a) },
            { -Math.Sin(b), Math.Sin(a) * Math.Cos(b), Math.Cos(a) * Math.Cos(b) } };    

            projection_matrix = algebra.Create(projection_array);
        }

        public RendererPoints(IAlgebraLinear<MatrixType> algebra, int bitmap_size_x, int bitmap_size_y, AngleRadian pitch,  AngleRadian yaw)
            : this(algebra, bitmap_size_x, bitmap_size_y, yaw, pitch, new double []{0,0,0}, 1)
        {


        }

        public RendererPoints(IAlgebraLinear<MatrixType> algebra, int bitmap_size_x, int bitmap_size_y, AngleRadian pitch, AngleRadian yaw, double scale)
           : this(algebra, bitmap_size_x, bitmap_size_y, yaw, pitch, new double[] { 0, 0, 0 }, scale)
        {


        }

        public BitmapFast Render(Tuple<double[][], Color[]> render_image)
        {
            double[][] points = render_image.Item1;
            Color[] colors = render_image.Item2;
            double [][] projected_points = new double [points.Length][];
            int[] coordinates = new int[3];

            for (int point_index = 0; point_index < points.Length; point_index++)
            {
                projected_points[point_index] = (projection_matrix * (points[point_index] - offset_matrix)).ToArray1DFloat64();
            }



            Raster2DInteger bitmap_raster = new Raster2DInteger(bitmap_size_x, bitmap_size_y);
            BitmapFast destination_image = new BitmapFast(this.bitmap_size_x, this.bitmap_size_y);
            destination_image.Lock();
            for (int index_y = 0; index_y < bitmap_raster.Size1; index_y++)
            {
                for (int index_x = 0; index_x < bitmap_raster.Size0; index_x++)
                {
                    destination_image.SetPixel(index_x, index_y, Color.Black);
                }
            }
   

            for (int element_index = 0; element_index < points.Length; element_index++)
            {
                int x_target = (int)(projected_points[element_index][0] * this.scale) + (bitmap_raster.Size0 / 2);
                int y_target = (int)(projected_points[element_index][1] * this.scale) + (bitmap_raster.Size1 / 2);

                if (bitmap_raster.ContainsCoordinates(x_target, y_target))
                {
                    destination_image.SetPixel(x_target, y_target, colors[element_index]);
                }
            }

            destination_image.Unlock();
            return destination_image;
        }
    }
}
