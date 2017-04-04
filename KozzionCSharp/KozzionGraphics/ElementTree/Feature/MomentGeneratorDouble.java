package com.kozzion.library.graphics.elementtree.feature;

import java.awt.Point;
import java.util.List;
import java.util.Set;
import java.util.Vector;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.graphics.elementtree.maxtree.MaxTreeNode2DInteger;
import com.kozzion.library.math.datastructure.matrix.DoubleVector;

public class MomentGeneratorDouble
{

    public static double [] fast_binary_scale_invariant_centroid_moments(final List<Point> points, final int moment_order)
    {
        final double [] geometric_moments = slow_binary_geometric_moments(moment_order, points);
        return fast_binary_scale_invariant_centroid_moments(geometric_moments, moment_order);
    }

    public static double [] fast_binary_scale_invariant_centroid_moments(final double [] geometric_moments, final int moment_order)
    {
        final double [] binary_scale_invariant_centroid_moments = new double [MomentTools.compute_momemt_count(moment_order)];
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

    public static double fast_binary_scale_invariant_centroid_moment(final double [] geometric_moments, final int moment_order,
        final int x_power, final int y_power)
    {

        final double centroid_moment = fast_binary_centroid_moment(geometric_moments, moment_order, x_power, y_power);
        final double zeroth_moment = fast_binary_centroid_moment(geometric_moments, moment_order, 0, 0);
        final double correction_power = ((x_power + y_power) / 2.0) + 1;
        return centroid_moment / Math.pow(zeroth_moment, correction_power);
    }

    public static double [] fast_binary_scale_invariant_centroid_moments(final MaxTreeNode2DInteger max_tree_node, final int moment_order)
    {
        final double [] geometric_moments = max_tree_node.get_geometric_moments(moment_order);
        return fast_binary_scale_invariant_centroid_moments(geometric_moments, moment_order);
    }

    public static double [] fast_binary_centroid_moments(final int moment_order, final List<Point> points)
    {
        final double [] geometric_moments = slow_binary_geometric_moments(moment_order, points);

        return fast_binary_centroid_moments(geometric_moments, moment_order);
    }

    public static double [] fast_binary_centroid_moments(final double [] geometric_moments, final int moment_order)
    {
        final double [] binary_centroid_moments = new double [MomentTools.compute_momemt_count(moment_order)];
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

    public static double fast_binary_centroid_moment(final double [] geometric_moments, final int moment_order, final int x_power,
        final int y_power)
    {
        double cental_moment = 0.0;
        double k_factor = 1.0;
        double l_factor = 1.0;

        if (moment_order == 0)
        {
            throw new IllegalArgumentException("Fast centroid_moment not defined for order 0, try slow version");
        }

        final double average_x = get_moment(geometric_moments, moment_order, 1, 0) / get_moment(geometric_moments, moment_order, 0, 0);
        final double average_y = get_moment(geometric_moments, moment_order, 0, 1) / get_moment(geometric_moments, moment_order, 0, 0);

        for (int l = 0; l <= y_power; l++)
        {
            k_factor = 1.0;
            for (int k = 0; k <= x_power; k++)
            {
                cental_moment += k_factor * l_factor * Math.pow(-average_x, x_power - k) * Math.pow(-average_y, y_power - l)
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

    public static double [] slow_binary_geometric_moments(final int moment_order, final List<Point> points)
    {
        final double [] geometric_moments = new double [MomentTools.compute_momemt_count(moment_order)];
        for (int power_y = 0; power_y <= moment_order; power_y++)
        {
            for (int power_x = 0; (power_x + power_y) <= moment_order; power_x++)
            {
                geometric_moments[MomentTools.compute_momemt_index(moment_order, power_x, power_y)] = MomentGeneratorDouble
                    .binary_geometric_moment(points, power_x, power_y);
            }
        }
        return geometric_moments;
    }

    public static double [] fast_binary_geometric_moments(final Set<MaxTreeNode2DInteger> nodes, final int moment_order)
    {
        final double [] geometric_moments = new double [MomentTools.compute_momemt_count(moment_order)];
        for (final MaxTreeNode2DInteger max_tree_node : nodes)
        {
            final double [] node_moments = max_tree_node.get_geometric_moments(moment_order);
            for (int index = 0; index < node_moments.length; index++)
            {
                geometric_moments[index] += node_moments[index];
            }
        }
        return geometric_moments;
    }

    public static double binary_geometric_moment(final List<Point> points, final int power_x, final int power_y)
    {
        double moment = 0;
        for (final Point point : points)
        {
            moment += Math.pow(point.x, power_x) * Math.pow(point.y, power_y);
        }
        return moment;
    }

    public static double fast_binary_geometric_moment(final int [] elements, final int image_width, final int power_x, final int power_y)
    {
        double moment = 0;
        for (final int element : elements)
        {
            final int element_x = element % image_width;
            final int element_y = element / image_width;
            moment += Math.pow(element_x, power_x) * Math.pow(element_y, power_y);
        }
        return moment;
    }

    public static List<Tuple2<DoubleVector, Double>> convert_to_coordinates(final List<Tuple2<Integer, Integer>> elements, final int image_width)
    {
        final List<Tuple2<DoubleVector, Double>> coordinates = new Vector<Tuple2<DoubleVector, Double>>();
        for (final Tuple2<Integer, Integer> element : elements)
        {
            final double element_x = (element.get_object1() % image_width);
            final double element_y = (element.get_object1() / image_width);
            coordinates.add(new Tuple2<DoubleVector, Double>(new DoubleVector(element_x, element_y), (double) element.get_object2()));
        }
        return coordinates;
    }

    public static double get_moment(final double [] moments, final int moment_order, final int power_x, final int power_y)
    {
        if (moment_order < (power_x + power_y))
        {
            throw new IllegalArgumentException("Moment powers are to big for order");
        }
        return moments[MomentTools.compute_momemt_index(moment_order, power_x, power_y)];
    }

}
