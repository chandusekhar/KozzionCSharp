package com.kozzion.library.graphics.texturegeneration;

import com.kozzion.library.graphics.image.CyclicFloatRgbRasterImage2D;

public class TextureGeneratorWeiLevoy implements ITextureGenerator
{
    int d_scale;
    int d_iterations;

    TextureGeneratorWeiLevoy(final int scale, final int iterations)
    {
        d_scale = scale;
        d_iterations = iterations;
    }

    @Override
    public CyclicFloatRgbRasterImage2D generate_texture(final CyclicFloatRgbRasterImage2D input_image, final int output_width, final int output_height)
    {
        final CyclicFloatRgbRasterImage2D output_image = intialize_output_image(input_image, output_width, output_height);

        final float [][][] input_neighborhoods = build_input_neighborhoods(input_image);

        for (int iteration_index = 0; iteration_index < d_iterations; iteration_index++)
        {
            for (int index_y = 0; index_y < output_height; index_y++)
            {
                System.out.println("line " + index_y + " of " + output_height);
                for (int index_x = 0; index_x < output_width; index_x++)
                {
                    set_best_match_pixel(input_image, input_neighborhoods, output_image, index_x, index_y);
                }

            }
        }

        return output_image;
    }

    private float [][][] build_input_neighborhoods(final CyclicFloatRgbRasterImage2D input_image)
    {
        final int neigbourhood_size = ((((d_scale * 2) + 1) * ((d_scale * 2) + 1)) - 1) / 2;
        final float [][][] input_neighborhoods = new float [input_image.get_width()] [input_image.get_height()] [neigbourhood_size];
        for (int index_y = 0; index_y < input_image.get_height(); index_y++)
        {
            for (int index_x = 0; index_x < input_image.get_width(); index_x++)
            {
                input_neighborhoods[index_x][index_y] = build_neighborhood(input_image, index_x, index_y);
            }
        }
        return input_neighborhoods;
    }

    /*
     * Build L shape Neighborhood
     */
    private float [] build_neighborhood(final CyclicFloatRgbRasterImage2D image, final int index_x, final int index_y)
    {
        final int neigbourhood_size = ((((d_scale * 2) + 1) * ((d_scale * 2) + 1)) - 1) / 2;
        final float [] neigbourhood = new float [neigbourhood_size * 3];
        int pixel_number = 0;
        for (int trans_y = -d_scale; trans_y < 1; trans_y++)
        {
            for (int trans_x = -d_scale; trans_x < (d_scale + 1); trans_x++)
            {
                // System.out.println(trans_x + " " + trans_y);

                if (pixel_number == neigbourhood_size)
                {
                    return neigbourhood;
                }
                neigbourhood[pixel_number] = image.get_r(index_x + trans_x, index_y + trans_y);
                neigbourhood[pixel_number + 1] = image.get_g(index_x + trans_x, index_y + trans_y);
                neigbourhood[pixel_number + 2] = image.get_b(index_x + trans_x, index_y + trans_y);
                pixel_number++;

            }
        }
        return null;
    }

    private CyclicFloatRgbRasterImage2D intialize_output_image(final CyclicFloatRgbRasterImage2D input_image, final int output_width,
        final int output_height)
    {
        final CyclicFloatRgbRasterImage2D output_image = new CyclicFloatRgbRasterImage2D(output_width, output_height);
        /* randomize the last few columns from the input image */
        for (int index_x = 0; index_x < d_scale; index_x++)
        {
            for (int index_y = 0; index_y < output_height; index_y++)
            {
                // int random_x = (int) (Math.random() * (double) input_image.get_width());
                // int random_y = (int) (Math.random() * (double) input_image.get_height());
                // output_image.set_rgb(output_width - (index_x + 1), index_y, input_image.get_r(random_x, random_y),
                // input_image.get_g(random_x, random_y), input_image.get_b(random_x, random_y));
                output_image.set_rgb(index_x, output_width - (index_y + 1), (float) Math.random(), (float) Math.random(),
                    (float) Math.random());
            }
        }

        // randomize the last few row
        for (int index_x = 0; index_x < output_width; index_x++)
        {
            for (int index_y = 0; index_y < d_scale; index_y++)
            {
                // int random_x = (int) (Math.random() * (double) input_image.get_width());
                // int random_y = (int) (Math.random() * (double) input_image.get_height());
                // output_image.set_rgb(index_x, output_width - (index_y + 1), input_image.get_r(random_x, random_y),
                // input_image.get_g(random_x, random_y), input_image.get_b(random_x, random_y));
                output_image.set_rgb(index_x, output_width - (index_y + 1), (float) Math.random(), (float) Math.random(),
                    (float) Math.random());
            }
        }
        return output_image;

    }

    private void set_best_match_pixel(final CyclicFloatRgbRasterImage2D input_image, final float [][][] input_neighborhoods,
        final CyclicFloatRgbRasterImage2D output_image, final int output_index_x, final int output_index_y)
    {

        final float [] output_N = build_neighborhood(output_image, output_index_x, output_index_y);
        float [] Nbest = null;
        float best_distance = 0;
        int best_index = 0;
        for (int input_index_y = 0; input_index_y < input_image.get_height(); input_index_y++)
        {
            for (int input_index_x = 0; input_index_x < input_image.get_width(); input_index_x++)
            {
                final float [] input_N = input_neighborhoods[input_index_x][input_index_y];
                if (Nbest == null)
                {
                    best_distance = distance(input_N, output_N);
                    Nbest = input_N;
                    best_index = 0;
                }
                else
                {
                    final float new_distance = distance(input_N, output_N);
                    if (new_distance < best_distance)
                    {
                        best_distance = new_distance;
                        Nbest = input_N;
                        best_index = input_index_x + (input_index_y * input_image.get_width());
                    }
                }
            }
        }
        final int best_index_x = best_index % input_image.get_width();
        final int best_index_y = best_index / input_image.get_width();
        output_image.set_rgb(output_index_x, output_index_y, input_image.get_r(best_index_x, best_index_y),
            input_image.get_g(best_index_x, best_index_y), input_image.get_b(best_index_x, best_index_y));

    }

    // TODO optimize add current best as a comparison. return -1 if distance is bigger?
    private float distance(final float [] array_0, final float [] array_1)
    {
        float distance = 0;
        for (int index = 0; index < array_0.length; index++)
        {
            distance += Math.abs(array_0[index] - array_1[index]);
        }
        return distance;
    }

}
