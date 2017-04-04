package com.kozzion.library.graphics.elementtree.feature;

import java.awt.Point;
import java.util.List;

import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.graphics.image.raster.IIntegerRaster3D;
import com.kozzion.library.math.tools.MathToolsInteger;

public class MomentGeneratorFloat3D
    implements
        IFeatureGeneratorFloat<List<Point>>
{
    int d_moment_order;

    public MomentGeneratorFloat3D(
        final int moment_order)
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
        float [] generate_features = null;
        ArrayTools.copy_fill(features, generate_features);        
    }

    public static double compute_central_moment_slow(
        int [] indexes,
        IIntegerRaster3D raster,
        int order_x,
        int order_y,
        int order_z)
    {
        double [][] coordinates = get_coordinates(indexes, raster);
        return compute_central_moment_slow(coordinates, order_x, order_y, order_z);
    }

    public static double compute_central_moment_fast(
        int [] indexes,
        IIntegerRaster3D raster,
        int order_x,
        int order_y,
        int order_z)
    {
        double [][] coordinates = get_coordinates(indexes, raster);
        return compute_central_moment_fast(coordinates, order_x, order_y, order_z);
    }

    private static double compute_central_moment_fast(
        double [][] coordinates,
        int order_x,
        int order_y,
        int order_z)
    {
        double [][][] geometric_moments = compute_geometric_moments(coordinates, MathToolsInteger.max(order_x, order_y, order_z));
        return compute_central_moments_fast(geometric_moments, order_x, order_y, order_z);
    }

    private static double compute_central_moments_fast(
        double [][][] geometric_moments,
        int order_x,
        int order_y,
        int order_z)
    {
        double cental_moment = 0.0f;

        final double average_x = geometric_moments[1][0][0] / geometric_moments[0][0][0];
        final double average_y = geometric_moments[0][1][0] / geometric_moments[0][0][0];
        final double average_z = geometric_moments[0][0][1] / geometric_moments[0][0][0];

        double m_factor = 1.0f;
        for (int m = 0; m <= order_z; m++)
        {
            double l_factor = 1.0f;
            for (int l = 0; l <= order_y; l++)
            {
                double k_factor = 1.0f;
                for (int k = 0; k <= order_x; k++)
                {
                    cental_moment += 
                        k_factor * 
                        l_factor * 
                        m_factor * 
                        Math.pow(-average_x, order_x - k) * 
                        Math.pow(-average_y, order_y - l) *
                        Math.pow(-average_z, order_z - m) * geometric_moments[k][l][m];

                    // computing pascals triangle term for x coordinates
                    k_factor *= (order_x - k) / (k + 1.0);
                }
                // computing pascals triangle term for y coordinates
                l_factor *= (order_y - l) / (l + 1.0);
            }
            // computing pascals triangle term for z coordinates
            m_factor *= (order_z - m) / (m + 1.0);
        }
        return cental_moment;
    }

    private static double [][][] compute_geometric_moments(
        double [][] coordinates,
        int order)
    {
        double [][][] geometric_moments = new double [order + 1] [order + 1] [order + 1];
        for (int order_x = 0; order_x <= order; order_x++)
        {
            for (int order_y = 0; order_y <= order; order_y++)
            {
                for (int order_z = 0; order_z <= order; order_z++)
                {
                    for (int index = 0; index < coordinates.length; index++)
                    {
                        geometric_moments[order_x][order_y][order_z] += Math.pow(coordinates[index][0], order_x)
                            * Math.pow(coordinates[index][1], order_y) * Math.pow(coordinates[index][2], order_z);
                    }
                }
            }
        }
        return geometric_moments;
    }

    private static double [][] get_coordinates(
        int [] element_indexes,
        IIntegerRaster3D raster)
    {
        int [] temp_coordinates = new int [3];
        double [][] coordinates = new double [element_indexes.length] [3];

        for (int index = 0; index < coordinates.length; index++)
        {
            raster.get_element_coordinates_fill(element_indexes[index], temp_coordinates);
            coordinates[index][0] = temp_coordinates[0];
            coordinates[index][1] = temp_coordinates[1];
            coordinates[index][2] = temp_coordinates[2];

        }
        return coordinates;
    }

    private static double compute_central_moment_slow(
        double [][] coordinates,
        double order_x,
        double order_y,
        double order_z)
    {
        double mean_x = 0;
        double mean_y = 0;
        double mean_z = 0;
        for (int index = 0; index < coordinates.length; index++)
        {
            mean_x += coordinates[index][0];
            mean_y += coordinates[index][1];
            mean_z += coordinates[index][2];
        }
        mean_x /= coordinates.length;
        mean_y /= coordinates.length;
        mean_z /= coordinates.length;
        double moment = 0;

        for (int index = 0; index < coordinates.length; index++)
        {
            moment += Math.pow(coordinates[index][0] - mean_x, order_x) * Math.pow(coordinates[index][1] - mean_y, order_y)
                * Math.pow(coordinates[index][2] - mean_z, order_z);
        }
        return moment;
    }

    @Override
    public float [] compute(
        List<Point> input)
    {
        // TODO Auto-generated method stub
        return null;
    }
}
