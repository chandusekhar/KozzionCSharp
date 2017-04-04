package com.kozzion.library.machinelearning.transform;

import java.util.ArrayList;
import java.util.List;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.math.tools.MathToolsCollection;

public class PercentileTransformDouble
{
    int d_dimension_count;
    double [] d_percentile_means;
    double [] d_percentile_spreads;
    
    public PercentileTransformDouble(List<double[]> raw_data, double lower_percentile)
    {
        d_dimension_count = raw_data.get(0).length;
        d_percentile_means = new double [d_dimension_count] ;
        d_percentile_spreads = new double [d_dimension_count] ;
        List<List<Double>> data = new ArrayList<>();
        for (int dimension_index = 0; dimension_index < d_dimension_count; dimension_index++)
        {
            data.add(new ArrayList<Double>());          
        }
        
        for (double[] raw_data_row : raw_data)
        {
            for (int dimension_index = 0; dimension_index < d_dimension_count; dimension_index++)
            {
                data.get(dimension_index).add(raw_data_row[dimension_index]);          
            }
        }
        
        for (int dimension_index = 0; dimension_index < d_dimension_count; dimension_index++)
        {
            Tuple2<Double, Double> dual_percentile = MathToolsCollection.get_dual_percentile_double(data.get(dimension_index), ((float)lower_percentile));
            d_percentile_spreads[dimension_index] = (dual_percentile.get_object2() - dual_percentile.get_object1()) / 2;
            d_percentile_means[dimension_index] = dual_percentile.get_object1() + d_percentile_spreads[dimension_index];
        }
    }
    
    
    public void transform_no_copy(double [] data_array)
    {
        for (int dimension_index = 0; dimension_index < d_dimension_count; dimension_index++)
        {
            data_array[dimension_index] =  (data_array[dimension_index] - d_percentile_means[dimension_index])  /  d_percentile_spreads[dimension_index];   
        }
    }
    
    public void transform_no_copy(double [] src_array, double [] target_array)
    {
        for (int dimension_index = 0; dimension_index < d_dimension_count; dimension_index++)
        {
            target_array[dimension_index] =  (src_array[dimension_index] - d_percentile_means[dimension_index])  /  d_percentile_spreads[dimension_index];   
        }
    }


    public double[] transform_copy(double[] array)
    {
        double [] copy = new double[array.length]; 
        System.arraycopy(array, 0, copy, 0, array.length);
        transform_no_copy(copy);
        return copy;
    }
    

}
