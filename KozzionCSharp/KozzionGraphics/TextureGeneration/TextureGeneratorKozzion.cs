

using System;
using System.Drawing;
using KozzionGraphics.Image;

public class TextureGeneratorKozzion //: ITextureGenerator
{
    private int scale;
    private int iterations;
    private int neigbourhood_size;

    public TextureGeneratorKozzion(int scale, int iterations)
    {
        this.scale = scale;
        this.iterations = iterations;
        this.neigbourhood_size = (((this.scale * 2) + 1) * ((this.scale * 2) + 1)) - 1;
    }

    public ImageRaster2D<Color> GenerateTexture(IImageRaster2D<Color> input_image, int output_width, int output_height)
    {
        ImageRaster2D<Color> output_image = IntializeOutputImage(input_image, output_width, output_height);
        Random random = new Random();
        Tuple<Color,Color[]>[] input_neighborhoods = BuildInputNeighborhoods(input_image);

        for (int iteration_index = 0; iteration_index < this.iterations; iteration_index++)
        {
            //get random index
            int random_x = random.Next((output_width  - 1) - (scale * 2)) + scale;
            int random_y = random.Next((output_height - 1) - (scale * 2)) + scale;
            //finde
            SetBestMatchPixel(input_neighborhoods, output_image, random_x, random_y);
   
        }

        return output_image;
    }

    private ImageRaster2D<Color> IntializeOutputImage(IImageRaster2D<Color> input_image, int output_width,
       int output_height)
    {
        ImageRaster2D<Color> output_image = new ImageRaster2D<Color>(output_width, output_height);
        Random random = new Random();
        /* randomize the last few columns from the input image */
        for (int element_index = 0; element_index < output_image.Raster.ElementCount; element_index++)
        {
            int random_x = random.Next((input_image.Raster.Size0 - 1) - (scale * 2)) + scale;
            int random_y = random.Next((input_image.Raster.Size1 - 1) - (scale * 2)) + scale;
            output_image.SetElementValue(element_index, input_image.GetElementValue(random_x, random_y));
        }
        return output_image;

    }

    private Tuple<Color,Color[]>[]  BuildInputNeighborhoods(IImageRaster2D<Color> input_image)
    {
        int neighborhood_count = (input_image.Raster.Size0 - (this.scale * 2)) * (input_image.Raster.Size1 - (this.scale * 2));
        Tuple<Color,Color[]>[] input_neighborhoods = new Tuple<Color,Color[]>[neighborhood_count];
        int index = 0 ;
        for (int index_y = scale; index_y < input_image.Raster.Size0 - scale; index_y++)
        {
            for (int index_x = scale; index_x < input_image.Raster.Size1 - scale; index_x++)
            {
                input_neighborhoods[index] = BuildNeighborhood(input_image, index_x, index_y);
                index++;
            }
        }
        return input_neighborhoods;
    }

    private Tuple<Color,Color[]>  BuildNeighborhood(IImageRaster2D<Color> image, int index_x, int index_y)
    {
        Color[] input_neighborhood = new Color[neigbourhood_size];
        int neighborhood_index = 0;
        for (int offset_y = -scale; offset_y <= scale; offset_y++)
        {
            for (int offset_x = -scale; offset_x <= scale; offset_x++)
            {
                if ((offset_x != 0) == (offset_y != 0))
                {
                    input_neighborhood[neighborhood_index] = image.GetElementValue(index_x + offset_x, index_y + offset_y);
                    neighborhood_index++;
                }
            }
        }
        return new Tuple<Color,Color[]>(image.GetElementValue(index_x, index_y), input_neighborhood);
    }

    private void SetBestMatchPixel(Tuple<Color, Color[]>[] input_neighborhoods,  IImageRaster2D<Color> output_image, int output_index_x, int output_index_y)
    {
        Color[] output_neighborhood = BuildNeighborhood(output_image, output_index_x, output_index_y).Item2;
        Color best_color = input_neighborhoods[0].Item1;
        float best_distance = Distance(output_neighborhood, input_neighborhoods[0].Item2);
        for (int index = 1; index < input_neighborhoods.Length; index++)
		{
            float distance = Distance(output_neighborhood, input_neighborhoods[index].Item2);
            if (distance < best_distance)
            {
                best_distance = distance;
                best_color = input_neighborhoods[index].Item1;
            }
		}
    }

    private float Distance(Color [] array_0, Color [] array_1)
    {
        float distance = 0;
        for (int index = 0; index < array_0.Length; index++)
        {
            distance += Math.Abs(array_0[index].R - array_1[index].R);
            distance += Math.Abs(array_0[index].G - array_1[index].G);
            distance += Math.Abs(array_0[index].B - array_1[index].B);
        }
        return distance;
    }

}
