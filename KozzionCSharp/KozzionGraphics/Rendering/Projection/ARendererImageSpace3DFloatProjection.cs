using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.Image;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;

namespace KozzionGraphics.Renderer.Projection
{
    public abstract class ARendererImageSpace3DFloatProjection
    {
        protected IFunction<float, Color> converter;
        protected IComparer<float> comparer;
        protected IAlgebraReal<float> algebra;
        private int []  resolution;
        private float[] coordinates_origen;
        private float[] render_stride_x;
        private float[] render_stride_y;
        private float[] render_stride_z;

        public ARendererImageSpace3DFloatProjection(
            IFunction<float, Color> converter,
            int[] resolution, 
            float[] render_origen, 
            float [] render_stride_x, 
            float [] render_stride_y, 
            float [] render_stride_z) 
        {
            this.converter = converter;
            this.comparer = new ComparerNatural<float>();
            this.algebra  = new AlgebraRealFloat32();

            this.resolution = resolution;
            this.coordinates_origen = render_origen;

            this.render_stride_x = render_stride_x;
            this.render_stride_y = render_stride_y;
            this.render_stride_z = render_stride_z;        
        }

        public Bitmap Render(IImageSpace3D<float, float> source_image)
        {
            float [,] image= new float [this.resolution[0], this.resolution[1]];
            
            Parallel.For(0, this.resolution[1], index_y =>
            {
                float[] coordinates_y = ToolsMathCollection.AddMultiple<float>(algebra, coordinates_origen, render_stride_y, index_y);
                for (int index_x = 0; index_x < this.resolution[0]; index_x++)
                {
                    float[] coordinates_x = ToolsMathCollection.AddMultiple<float>(algebra, coordinates_y, render_stride_x, index_x);
                    image[index_x, index_y] = Projection(coordinates_x, this.render_stride_z, this.resolution[2], source_image);
                }
            });

             
            //for (int index_y = 0; index_y < this.resolution[1]; index_y++)
            //{
            //    float[] coordinates_y = ToolsMathArray.AddMultiple<float>(algebra, coordinates_origen, render_stride_y, index_y);
            //    for (int index_x = 0; index_x < this.resolution[0]; index_x++)
            //    {
            //        float[] coordinates_x = ToolsMathArray.AddMultiple<float>(algebra, coordinates_y, render_stride_x, index_x);

            //        float value = Projection(coordinates_x, this.render_stride_z, this.resolution[2], source_image);
            //        destination_image.SetPixel(index_x, index_y, this.converter.Compute(value));
            //    }

            //}
            return ConvertToBitmap(image);
        }

        public Bitmap ConvertToBitmap(float [,] image)
        {
            Bitmap destination_image = new Bitmap(this.resolution[0], this.resolution[1]);
            for (int index_y = 0; index_y < this.resolution[1]; index_y++)
            {    
                for (int index_x = 0; index_x < this.resolution[0]; index_x++)
                {
                    destination_image.SetPixel(index_x, index_y, this.converter.Compute(image[index_x, index_y]));
                }
            }
            return destination_image;
        }

        protected abstract float Projection(float[] origen, float[] stride_size, int stride_count, IImageSpace3D<float, float> source_image);
    }
}
