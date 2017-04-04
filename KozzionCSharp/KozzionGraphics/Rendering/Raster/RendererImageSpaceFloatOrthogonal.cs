using KozzionGraphics.Image;
using KozzionCore.Tools;

namespace KozzionGraphics.Renderer
{
    public class RendererImageSpaceFloatOrthogonal
    {
        private int resolution_x;
        private int resolution_y;
        private int dimension_count;
        
        private float[] render_origen;
        private float[] render_stride_x;
        private float[] render_stride_y;

        RendererImageSpaceFloatOrthogonal(int resolution_x, int resolution_y, float[] render_origen, int dimension_x, int dimension_y) 
        {
            this.resolution_x = resolution_x;
            this.resolution_y = resolution_y;
            this.render_origen = render_origen;
            this.dimension_count = render_origen.Length;
            this.render_stride_x = new float[this.dimension_count];
            this.render_stride_x[dimension_x] = 1;
            this.render_stride_y = new float[this.dimension_count];
            this.render_stride_y[dimension_y] = 1;
        }


        public IImageRaster2D<float> Render(IImageSpace<float, float> source_image) 
        {
            IImageRaster2D<float> destination_image = new ImageRaster2D<float>();
            float[] coordinates = new float [render_origen.Length];
            for (int index_y = 0; index_y < this.resolution_y; index_y++)
            {
                for (int index_x = 0; index_x < this.resolution_x; index_x++)
                {
                    ToolsCollection.CopyRBA(this.render_origen, coordinates);
                    for (int index_dimension = 0; index_dimension < this.dimension_count; index_dimension++)
			        {
                        coordinates[index_dimension] += (render_stride_x[index_dimension] * index_x) + (render_stride_y[index_dimension] * index_y);
			        }
                    destination_image.SetElementValue(index_x, index_y, source_image.GetLocationValue(coordinates));
                }
            }
            return destination_image;
        }
    }
}
