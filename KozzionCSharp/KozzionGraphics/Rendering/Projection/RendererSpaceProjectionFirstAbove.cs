using System.Drawing;
using KozzionCore.Tools;
using KozzionGraphics.Image;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;

namespace KozzionGraphics.Renderer.Projection
{
    public class RendererSpaceProjectionFirstAbove : ARendererImageSpace3DFloatProjection
    {
        private float min_value;
        public RendererSpaceProjectionFirstAbove(
            IFunction<float, Color> converter,
            int [] resolution,
            float[] render_origen, 
            float [] render_stride_x, 
            float [] render_stride_y, 
            float [] render_stride_z,
            float min_value)
            : base(converter, resolution, render_origen, render_stride_x, render_stride_y, render_stride_z)
        {
            this.min_value = min_value;
        }

        protected override float Projection(float[] origen, float[] stride_size, int stride_count, IImageSpace3D<float, float> source_image) 
        {
            float[] coordinates = ToolsCollection.Copy(origen);
            for (int index_z = 0; index_z < stride_count; index_z++)
            {
                float value = source_image.GetLocationValue(coordinates);
                if (this.comparer.Compare(value, min_value) == 1)
                {
                    return value;
                }
                ToolsMathCollection.AddRBA(algebra, coordinates, stride_size, coordinates);
            }
            return min_value;
        }
    }
}
