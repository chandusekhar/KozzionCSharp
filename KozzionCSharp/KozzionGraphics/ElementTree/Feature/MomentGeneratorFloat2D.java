package com.kozzion.library.graphics.elementtree.feature;

import java.awt.Point;
import java.util.List;

import com.kozzion.library.core.utility.ArrayTools;

public class MomentGeneratorFloat2D implements IFeatureGeneratorFloat<List<Point>>
{
    int d_moment_order;

    public MomentGeneratorFloat2D(final int moment_order)
    {
        d_moment_order = moment_order;
    }

    @Override
    public int get_feature_count()
    {
        return MomentTools.compute_momemt_count(d_moment_order);
    }
    
    @Override
    public void generate_features(
        List<Point> input,
        float [] features)
    {
        float [] generate_features = generate_features(input);
        ArrayTools.copy_fill(features, generate_features);
    }

    public float [] generate_features(final List<Point> points)
    {
        return fast_binary_scale_invariant_centroid_moments(points, d_moment_order);
    }

    public static float [] fast_binary_scale_invariant_centroid_moments(final List<Point> points, final int moment_order)
    {
        final float [] geometric_moments = slow_binary_geometric_moments(moment_order, points);
        return fast_binary_scale_invariant_centroid_moments(geometric_moments, moment_order);
    }

    public static float [] fast_binary_scale_invariant_centroid_moments(final float [] geometric_moments, final int moment_order)
    {
        final float [] binary_scale_invariant_centroid_moments = new float [MomentTools.compute_momemt_count(moment_order)];
        for (int y_power = 0; y_power <= moment_order; y_power++)
        {
            for (int x_power = 0; (x_power + y_power) <= moment_order; x_power++)
            {
                binary_scale_invariant_centroid_moments[MomentTools.compute_momemt_index(moment_order, x_power, y_power)] = fast_binary_scale_invariant_centroid_moment(
                    geometric_moments, moment_order, x_power, y_power);
            }
        }
        return binary_scale_invariant_centroid_moments;
    }

    public static float fast_binary_scale_invariant_centroid_moment(final float [] geometric_moments, final int moment_order,
        final int x_power, final int y_power)
    {

        final float centroid_moment = fast_binary_centroid_moment(geometric_moments, moment_order, x_power, y_power);
        final float zeroth_moment = fast_binary_centroid_moment(geometric_moments, moment_order, 0, 0);
        final float correction_power = ((x_power + y_power) / 2.0f) + 10.0f;
        return (centroid_moment / (float) Math.pow(zeroth_moment, correction_power));
    }

    public static float [] fast_binary_centroid_moments(final int moment_order, final List<Point> points)
    {
        final float [] geometric_moments = slow_binary_geometric_moments(moment_order, points);
        return fast_binary_centroid_moments(geometric_moments, moment_order);
    }

    public static float [] fast_binary_centroid_moments(final float [] geometric_moments, final int moment_order)
    {
        final float [] binary_centroid_moments = new float [MomentTools.compute_momemt_count(moment_order)];
        for (int y_power = 0; y_power <= moment_order; y_power++)
        {
            for (int x_power = 0; (x_power + y_power) <= moment_order; x_power++)
            {
                binary_centroid_moments[MomentTools.compute_momemt_index(moment_order, x_power, y_power)] = fast_binary_centroid_moment(
                    geometric_moments, moment_order, x_power, y_power);
            }
        }
        return binary_centroid_moments;
    }

    public static float fast_binary_centroid_moment(final float [] geometric_moments, final int moment_order, final int x_power,
        final int y_power)
    {
        float cental_moment = 0.0f;
        float k_factor = 1.0f;
        float l_factor = 1.0f;

        if (moment_order == 0)
        {
            throw new IllegalArgumentException("Fast centroid_moment not defined for order 0, try slow version");
        }

        final float average_x = get_moment(geometric_moments, moment_order, 1, 0) / get_moment(geometric_moments, moment_order, 0, 0);
        final float average_y = get_moment(geometric_moments, moment_order, 0, 1) / get_moment(geometric_moments, moment_order, 0, 0);

        for (int l = 0; l <= y_power; l++)
        {
            k_factor = 1.0f;
            for (int k = 0; k <= x_power; k++)
            {
                cental_moment += k_factor * l_factor * 
                    Math.pow(-average_x, x_power - k) * 
                    Math.pow(-average_y, y_power - l)
                    * get_moment(geometric_moments, moment_order, k, l);
                k_factor *= (x_power - k) / (k + 1.0); // computing pascals
                                                       // triangle term for x
                                                       // coordinates
            }
            l_factor *= (y_power - l) / (l + 1.0); // computing pascals triangle
                                                   // term for y coordinates
        }
        return (cental_moment);

    }

    public static float [] slow_binary_geometric_moments(final int moment_order, final List<Point> points)
    {
        final float [] geometric_moments = new float [MomentTools.compute_momemt_count(moment_order)];
        for (int power_y = 0; power_y <= moment_order; power_y++)
        {
            for (int power_x = 0; (power_x + power_y) <= moment_order; power_x++)
            {
                geometric_moments[MomentTools.compute_momemt_index(moment_order, power_x, power_y)] = MomentGeneratorFloat2D
                    .binary_geometric_moment(points, power_x, power_y);
            }
        }
        return geometric_moments;
    }

    public static float binary_geometric_moment(final List<Point> points, final int power_x, final int power_y)
    {
        float moment = 0;
        for (final Point point : points)
        {
            moment += Math.pow(point.x, power_x) * Math.pow(point.y, power_y);
        }
        return moment;
    }

    public static float fast_binary_geometric_moment(final int [] elements, final int image_width, final int power_x, final int power_y)
    {
        float moment = 0;
        for (final int element : elements)
        {
            final int element_x = element % image_width;
            final int element_y = element / image_width;
            moment += Math.pow(element_x, power_x) * Math.pow(element_y, power_y);
        }
        return moment;
    }

    public static float get_moment(final float [] moments, final int moment_order, final int power_x, final int power_y)
    {
        if (moment_order < (power_x + power_y))
        {
            throw new IllegalArgumentException("Moment powers are to big for order");
        }
        return moments[MomentTools.compute_momemt_index(moment_order, power_x, power_y)];
    }

    @Override
    public float [] compute(
        List<Point> input)
    {
        // TODO Auto-generated method stub
        return null;
    }

}
