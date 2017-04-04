using System;
using System.Collections.Generic;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Rendering;
using KozzionMathematics.Tools;
using KozzionCore.Tools;

namespace KozzionGraphics.Renderer
{
    public class Renderer2DProjectionBool : IRenderer2D<Tuple<List<int>, IRaster3DInteger>, bool>
    {
 
        int bitmap_size_x;
        int bitmap_size_y;
        float[] projection_vector_x;
        float[] projection_vector_y;
        float[] projection_vector_z;
        float[] image_focus;
        public Renderer2DProjectionBool(int bitmap_size_x, int bitmap_size_y, float angle_axial_radians, float angle_slice_radians, float[] image_focus, float image_distance)
        {
            this.bitmap_size_x = bitmap_size_x;
            this.bitmap_size_y = bitmap_size_y;

            //isometric projection
            //http://www.wolframalpha.com/input/?i=%7B%7B1%2C+0+%2C0%7D%2C%7B0%2C+cos%28a%29%2C+sin%28a%29%7D%2C%7B0%2C+-sin%28a%29%2C+cos%28a%29%7D%7D.%7B%7Bcos%28b%29%2C+0%2C+-sin%28b%29%7D%2C%7B0%2C1%2C0%7D%2C%7Bsin%28b%29%2C+0+%2C+cos%28b%29%7D%7D
            float a = angle_slice_radians;
            float b = angle_axial_radians;
            
            this.projection_vector_x = ToolsCollection.ConvertToFloatArray(new double[] {  Math.Cos(b), Math.Sin(a) * Math.Sin(b),  Math.Cos(a) * Math.Sin(b) });
            this.projection_vector_y = ToolsCollection.ConvertToFloatArray(new double[] { 0,            Math.Cos(a),               -Math.Sin(a) });
            this.projection_vector_z = ToolsCollection.ConvertToFloatArray(new double[] { -Math.Sin(b), Math.Sin(a) * Math.Cos(b),  Math.Cos(a) * Math.Cos(b) });
            this.image_focus = image_focus;
        }

        public IImageRaster<IRaster2DInteger,bool> Render(Tuple<List<int>, IRaster3DInteger> render_source)
        {
            List<int> elements_true = render_source.Item1;
            IRaster3DInteger source_raster= render_source.Item2;
            
            float[,] coordinates_image = new float[elements_true.Count, 3];
            float[,] elements_projection = new float[elements_true.Count, 3];
            int [] coordinates = new int [3];

            for (int element_index_index = 0; element_index_index < elements_true.Count; element_index_index++)
            {
                source_raster.GetElementCoordinatesRBA(elements_true[element_index_index], coordinates);
                coordinates_image[element_index_index, 0] = coordinates[0];
                coordinates_image[element_index_index, 1] = coordinates[1];
                coordinates_image[element_index_index, 2] = coordinates[2];
            }

            for (int element_index = 0; element_index < elements_true.Count; element_index++)
            {

                elements_projection[element_index, 0] = ((coordinates_image[element_index, 0] - image_focus[0]) * projection_vector_x[0]) +
                                                        ((coordinates_image[element_index, 1] - image_focus[1]) * projection_vector_x[1]) +
                                                        ((coordinates_image[element_index, 2] - image_focus[2]) * projection_vector_x[2]);

                elements_projection[element_index, 1] = ((coordinates_image[element_index, 0] - image_focus[0]) * projection_vector_y[0]) +
                                                        ((coordinates_image[element_index, 1] - image_focus[1]) * projection_vector_y[1]) +
                                                        ((coordinates_image[element_index, 2] - image_focus[2]) * projection_vector_y[2]);

                elements_projection[element_index, 2] = ((coordinates_image[element_index, 0] - image_focus[0]) * projection_vector_z[0]) +
                                                        ((coordinates_image[element_index, 1] - image_focus[1]) * projection_vector_z[1]) +
                                                        ((coordinates_image[element_index, 2] - image_focus[2]) * projection_vector_z[2]);      
            }

            Raster2DInteger bitmap_raster = new Raster2DInteger(bitmap_size_x, bitmap_size_y);
            ImageRaster2D<bool> destination_image = new ImageRaster2D<bool>(bitmap_size_x, bitmap_size_y);
            for (int element_index = 0; element_index < elements_true.Count; element_index++)
            {
                int x_target = (int)elements_projection[element_index, 0] + (bitmap_raster.Size0 / 2);
                int y_target = (int)elements_projection[element_index, 1] + (bitmap_raster.Size1 / 2);

                if (bitmap_raster.ContainsCoordinates(x_target, y_target))               
                {
                    destination_image.SetElementValue(x_target, y_target, true);
                }
            }
            return destination_image;
        }
    }
}
