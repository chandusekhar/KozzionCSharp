package com.kozzion.library.graphics.image;

import java.awt.Color;
import java.awt.Point;
import java.awt.image.BufferedImage;
import java.util.List;
import java.util.Vector;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.graphics.color.ColorTools;
import com.kozzion.library.graphics.tools.BufferedImageTools;

public class MaskedIntegerImage2D
{
    List<Tuple2<Point, Integer>> d_unmasked_pixels;

    public MaskedIntegerImage2D(final BufferedImage mask_image, final Color mask_color)
    {
        final int mask_color_integer = mask_color.getRGB();
        d_unmasked_pixels = new Vector<Tuple2<Point, Integer>>();

        for (int index_y = 0; index_y < mask_image.getHeight(); index_y++)
        {
            for (int index_x = 0; index_x < mask_image.getWidth(); index_x++)
            {
                if (mask_image.getRGB(index_x, index_y) != mask_color_integer)
                {
                    d_unmasked_pixels.add(new Tuple2<Point, Integer>(new Point(index_x, index_y), mask_image.getRGB(index_x, index_y)));
                }
            }
        }
    }

    public List<Point> find_match_locations(final BufferedImage screen_capture)
    {
        final List<Point> matches = new Vector<Point>();
        for (int index_y = 0; index_y < screen_capture.getHeight(); index_y++)
        {
            for (int index_x = 0; index_x < screen_capture.getWidth(); index_x++)
            {
                if (is_match_location(screen_capture, index_x, index_y))
                {
                    matches.add(new Point(index_x, index_y));
                }
            }
        }
        return matches;
    }

    private boolean is_match_location(final BufferedImage screen_capture, final int starting_x, final int starting_y)
    {
        for (final Tuple2<Point, Integer> unmasked_pixel : d_unmasked_pixels)
        {
            final Point unmasked_point = unmasked_pixel.get_object1();
            final int unmasked_value = unmasked_pixel.get_object2();
            if (BufferedImageTools.contains(screen_capture, starting_x + unmasked_point.x, starting_y + unmasked_point.y))
            {
                if (screen_capture.getRGB(starting_x + unmasked_point.x, starting_y + unmasked_point.y) != unmasked_value)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public double find_best_partial_match(final BufferedImage screen_capture)
    {
        double match_score = 0;
        for (int index_y = 0; index_y < screen_capture.getHeight(); index_y++)
        {
            for (int index_x = 0; index_x < screen_capture.getWidth(); index_x++)
            {
                match_score = Math.max(match_score, get_partial_match(screen_capture, index_x, index_y));
            }
        }
        System.out.println(match_score);
        return match_score;
    }

    private double get_partial_match(final BufferedImage screen_capture, final int starting_x, final int starting_y)
    {
        double error = 0;
        for (final Tuple2<Point, Integer> unmasked_pixel : d_unmasked_pixels)
        {
            final Point unmasked_point = unmasked_pixel.get_object1();
            final int unmasked_value = unmasked_pixel.get_object2();
            if (BufferedImageTools.contains(screen_capture, starting_x + unmasked_point.x, starting_y + unmasked_point.y))
            {
                error += ColorTools.get_absolute_color_error(unmasked_value,
                    screen_capture.getRGB(starting_x + unmasked_point.x, starting_y + unmasked_point.y));
            }
            else
            {
                error += 255;
            }
        }
        return 1 / error;
    }
}
