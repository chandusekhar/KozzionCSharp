using System.Collections.Generic;
using System.Drawing;
using KozzionCore.Tools;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.Image;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;

namespace KozzionGraphics.Renderer.Projection
{
    public class RendererImageSpace3DFloatZMIP
    {
        private IFunction<float, Color> converter;
        private IComparer<float> comparer;
        private IAlgebraReal<float> algebra;
        private int resolution_x;
        private int resolution_y;
        private int resolution_z;

        private float[] render_origen;
        private float[] render_stride_x;
        private float[] render_stride_y;
        private float[] render_stride_z;

        public RendererImageSpace3DFloatZMIP(IFunction<float, Color> converter,
            int resolution_x, int resolution_y, int resolution_z, float[] render_origen, float render_stride_x, float render_stride_y, float render_stride_z) 
        {
            this.converter = converter;
            this.comparer = new ComparerNatural<float>();
            this.algebra  = new AlgebraRealFloat32();

            this.resolution_x = resolution_x;
            this.resolution_y = resolution_y;
            this.resolution_z = resolution_z;

            this.render_origen = render_origen;

            this.render_stride_x = new float[3];
            this.render_stride_y = new float[3];
            this.render_stride_z = new float[3];
            this.render_stride_x[0] = render_stride_x;
            this.render_stride_y[1] = render_stride_y;
            this.render_stride_z[2] = render_stride_z;
        
        }

        public Bitmap Render(IImageSpace3D<float, float> source_image)
        {

            Bitmap destination_image = new Bitmap(resolution_x, resolution_y);
            float[] coordinates_y = ToolsCollection.Copy(render_origen);
            for (int index_y = 0; index_y < resolution_x; index_y++)
            {
                ToolsMathCollection.AddRBA<float>(algebra, coordinates_y, render_stride_y, coordinates_y);

                float[] coordinates_x = ToolsCollection.Copy(coordinates_y);
                for (int index_x = 0; index_x < resolution_y; index_x++)
                {
                    float[] coordinates_z = ToolsCollection.Copy(coordinates_x);
                    float max_value = source_image.GetLocationValue(coordinates_z);
                    ToolsMathCollection.AddRBA(algebra, coordinates_z, render_stride_z, coordinates_z);

                    for (int index_z = 1; index_z < resolution_z; index_z++)
                    {
                        float value = source_image.GetLocationValue(coordinates_z);
                        if (this.comparer.Compare(max_value, value) == -1)
                        {
                            max_value = value;
                        }
                        ToolsMathCollection.AddRBA(algebra, coordinates_z, render_stride_z, coordinates_z);
                    }
                    destination_image.SetPixel(index_x, index_y, this.converter.Compute(max_value));

                    ToolsMathCollection.AddRBA(algebra, coordinates_x, render_stride_x, coordinates_x);
                }
              
            }
            return destination_image;
        }
    }
}
