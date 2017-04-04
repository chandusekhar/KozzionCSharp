package com.kozzion.library.graphics.elementtree.feature;

import java.util.Arrays;

import com.kozzion.library.core.utility.CollectionTools;
import com.kozzion.library.graphics.elementtree.IElementNode;
import com.kozzion.library.graphics.image.raster.IIntegerRaster3D;

/**
 * 3
 * 
 * @author Joosterb
 */
public class FeatureGeneratorElementNode3DSlow
    implements
        IFeatureGeneratorFloat<IElementNode>
{
    private IIntegerRaster3D d_raster;
    private int              d_offset;
    // TODO move geometric moments to other generator and have generators play nice
    private int              d_index_s_x;
    private int              d_index_s_y;
    private int              d_index_s_z;

    private int              d_index_s_xx;
    private int              d_index_s_yy;
    private int              d_index_s_zz;

    private int              d_index_s_xy;
    private int              d_index_s_xz;
    private int              d_index_s_yz;

    private int              d_index_non_compactness;
    private int              d_index_elongation;
    private int              d_index_flatness;
    private int              d_index_sparceness;

    private float []         eigenvalues;
    private float []         covariance_matrix;
    private int []           element_coordinates;

    public FeatureGeneratorElementNode3DSlow(
        IIntegerRaster3D raster)
    {
        this(0, raster);
    }

    public FeatureGeneratorElementNode3DSlow(
        int offset,
        IIntegerRaster3D raster)
    {
        d_raster = raster;
        d_offset = offset;
        d_index_s_x = d_offset + 0;
        d_index_s_y = d_offset + 1;
        d_index_s_z = d_offset + 2;

        d_index_s_xx = d_offset + 3;
        d_index_s_yy = d_offset + 4;
        d_index_s_zz = d_offset + 5;

        d_index_s_xy = d_offset + 6;
        d_index_s_xz = d_offset + 7;
        d_index_s_yz = d_offset + 8;

        d_index_non_compactness = d_offset + 9;
        d_index_elongation = d_offset + 10;
        d_index_flatness = d_offset + 11;
        d_index_sparceness = d_offset + 12;

        eigenvalues = new float [3];
        covariance_matrix = new float [9];
        element_coordinates = new int [3];
    }

    @Override
    public int get_feature_count()
    {
        return 13;
    }

    /**
     * As in kawanuka page 79 & Wilkinson roerding
     */
    @Override
    public void generate_features(
        IElementNode node, float [] features)
    {

        // 1 get_size
        float size = node.get_culmative_real_size();
        node.set_feature_array(features);

        // 2 compute geometric moments
        int [] elements = node.get_culmative_real_element_array();
        for (int element_index : elements)
        {
            d_raster.get_element_coordinates_fill(element_index, element_coordinates);
            features[d_index_s_x] += element_coordinates[0];
            features[d_index_s_y] += element_coordinates[1];
            features[d_index_s_z] += element_coordinates[2];

            features[d_index_s_xx] += element_coordinates[0] * element_coordinates[0];
            features[d_index_s_yy] += element_coordinates[1] * element_coordinates[1];
            features[d_index_s_zz] += element_coordinates[2] * element_coordinates[2];

            features[d_index_s_xy] += element_coordinates[0] * element_coordinates[1];
            features[d_index_s_xz] += element_coordinates[0] * element_coordinates[2];
            features[d_index_s_yz] += element_coordinates[1] * element_coordinates[2];
        }
        
        // 2 compute covariance matrix (only the parts we will use note tha)
        float x_mean = features[d_index_s_x] / size;
        float y_mean = features[d_index_s_y] / size;
        float z_mean = features[d_index_s_z] / size;
        covariance_matrix[0] = 0;
        covariance_matrix[4] = 0;
        covariance_matrix[8] = 0;

        covariance_matrix[1] = 0;
        covariance_matrix[2] = 0;
        covariance_matrix[5] = 0;
                
        for (int element_index : elements)
        {
            d_raster.get_element_coordinates_fill(element_index, element_coordinates);
            covariance_matrix[0] += (element_coordinates[0] - x_mean) * (element_coordinates[0] - x_mean);
            covariance_matrix[4] += (element_coordinates[1] - y_mean) * (element_coordinates[1] - y_mean);
            covariance_matrix[8] += (element_coordinates[2] - z_mean) * (element_coordinates[2] - z_mean);

            covariance_matrix[1] += (element_coordinates[0] - x_mean) * (element_coordinates[1] - y_mean);
            covariance_matrix[2] += (element_coordinates[0] - x_mean) * (element_coordinates[2] - z_mean);
            covariance_matrix[5] += (element_coordinates[1] - y_mean) * (element_coordinates[2] - z_mean);
        }
        
        covariance_matrix[0] = (covariance_matrix[0]  + size/12.0f) / (float) Math.pow(size, 5.0 / 3.0);
        covariance_matrix[4] = (covariance_matrix[4]  + size/12.0f) / (float) Math.pow(size, 5.0 / 3.0);
        covariance_matrix[8] = (covariance_matrix[8]  + size/12.0f) / (float) Math.pow(size, 5.0 / 3.0);

        covariance_matrix[1] = covariance_matrix[1]  / (float) Math.pow(size, 5.0 / 3.0);
        covariance_matrix[2] = covariance_matrix[2]  / (float) Math.pow(size, 5.0 / 3.0);
        covariance_matrix[5] = covariance_matrix[5]  / (float) Math.pow(size, 5.0 / 3.0);

        CollectionTools.print(covariance_matrix);
        // 3 compute eigenvalues
        MathToolsFloatMatrix3.eigenvalues_symetric_fill(covariance_matrix, eigenvalues);
        
        
        // compute non-compactness
        features[d_index_non_compactness]  = eigenvalues[0] + eigenvalues[1] + eigenvalues[2];

        // 4 sort so that e_abs[0] < e_abs[1] < e_abs[2]
        eigenvalues[0] = Math.abs(eigenvalues[0]);
        eigenvalues[1] = Math.abs(eigenvalues[1]);
        eigenvalues[2] = Math.abs(eigenvalues[2]);
        Arrays.sort(eigenvalues);

        // 5 compute d values
        float d0 = (float) Math.sqrt((eigenvalues[0] * 20));
        float d1 = (float) Math.sqrt((eigenvalues[1] * 20));
        float d2 = (float) Math.sqrt((eigenvalues[2] * 20));

        // 6 Compute features:

        // compute elongation differ
        features[d_index_elongation] = eigenvalues[2] / eigenvalues[1];
        // compute flasness
        features[d_index_flatness] = eigenvalues[1] / eigenvalues[2];
        // compute sparceness
        features[d_index_sparceness] = (float) (d0 * d1 * d2);
    }

    public int getD_index_s_xx()
    {
        return d_index_s_xx;
    }

    public int get_index_s_yy()
    {
        return d_index_s_yy;
    }

    public int get_index_s_zz()
    {
        return d_index_s_zz;
    }

    public int get_index_s_xy()
    {
        return d_index_s_xy;
    }

    public int get_index_s_xz()
    {
        return d_index_s_xz;
    }

    public int get_index_s_yz()
    {
        return d_index_s_yz;
    }

    public int get_index_non_compactness()
    {
        return d_index_non_compactness;
    }

    public int get_index_elongation()
    {
        return d_index_elongation;
    }

    public int get_index_flatness()
    {
        return d_index_flatness;
    }

    public int get_index_sparceness()
    {
        return d_index_sparceness;
    }

    @Override
    public float [] compute(
        IElementNode input)
    {
        // TODO Auto-generated method stub
        return null;
    }

}
