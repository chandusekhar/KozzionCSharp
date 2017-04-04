using System.Drawing;
using KozzionCore.Tools;
using KozzionGraphics.Image;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;

namespace KozzionGraphics.Renderer.Projection
{
    public class RendererSpaceProjectionMIP : ARendererImageSpace3DFloatProjection
    {

        public RendererSpaceProjectionMIP(
            IFunction<float, Color> converter,
            int [] resolution,
            float[] render_origen, 
            float [] render_stride_x, 
            float [] render_stride_y, 
            float [] render_stride_z)
            : base(converter, resolution, render_origen, render_stride_x, render_stride_y, render_stride_z)
        {
            
        }

        protected override float Projection(float[] origen, float[] stride_size, int stride_count, IImageSpace3D<float, float> source_image) 
        {
            float[] coordinates = ToolsCollection.Copy(origen);
            float max_value = source_image.GetLocationValue(coordinates);
            ToolsMathCollection.AddRBA(algebra, coordinates, stride_size, coordinates);

            for (int index_z = 1; index_z < stride_count; index_z++)
            {
                float value = source_image.GetLocationValue(coordinates);
                if (this.comparer.Compare(max_value, value) == -1)
                {
                    max_value = value;
                }
                ToolsMathCollection.AddRBA(algebra, coordinates, stride_size, coordinates);
            }
            return max_value;
        }
    }
}
