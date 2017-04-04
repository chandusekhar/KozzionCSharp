package com.kozzion.library.machinelearning;

import java.util.ArrayList;
import java.util.List;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.math.datastructure.kdtree.KDTreeDouble;
import com.kozzion.library.math.datastructure.kdtree.NearestNeighborsSearcherDouble;
import com.kozzion.library.math.datastructure.kdtree.NearestNeighborsSearcherDouble.Entry;
import com.kozzion.library.math.datastructure.matrix.DoubleVector;
import com.kozzion.library.math.function.domain.implementation.ValueInterval;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionDoubleNumber;
import com.kozzion.library.math.statistics.distribution.interfaces.IProbabilityDensityFunctionDouble;
import com.kozzion.library.math.utility.ParzenDistributionGenerator;
import com.kozzion.library.plotting.utility.PlottingTools;

public class BayesianParzenFilter 
{
    private KDTreeDouble<Double>                    d_kdtree;
    private int                               d_neighbor_count;
    private NearestNeighborsSearcherDouble<Double>  d_nearest_neighbors_searcher; 
    private ParzenDistributionGenerator		d_distribution_generator;
    private double                            d_parzen_sigma;
    
	public BayesianParzenFilter(List<Tuple2<DoubleVector, Double>> input_data, int neighbor_count, double parzen_sigma)
	{
	    if (neighbor_count < 1)
	        throw new IllegalArgumentException("neighbor_count must be larger than 0");
	    if (input_data.size() < neighbor_count)
	        throw new IllegalArgumentException("number of input_data elements must be at least the value of neighbor_count");
	        
	    d_kdtree = new KDTreeDouble<Double>(input_data.get(0).get_object1().get_dimension_count());
	    for (Tuple2<DoubleVector, Double> pair : input_data)
	        d_kdtree.put(pair.get_object1(), pair.get_object2());
	
	    d_neighbor_count = neighbor_count;
	    d_nearest_neighbors_searcher = new NearestNeighborsSearcherDouble<Double>();
	    d_distribution_generator = new ParzenDistributionGenerator(parzen_sigma);
	    d_parzen_sigma = parzen_sigma;
	}
	
	public void predict_plot(DoubleVector input, int sample_count)
	{
	    Entry<Double> [] return_data = d_nearest_neighbors_searcher.get(d_kdtree, input, d_neighbor_count);
	    List<Double> data_points = new ArrayList<Double>();
	    for (Entry<Double> entry : return_data)
	        data_points.add(entry.get_neighbor().getValue());
	    
	    IDistributionDoubleNumber<Double> distribution = d_distribution_generator.generate_distribution(data_points);
	    IProbabilityDensityFunctionDouble<Double> density_function = distribution.get_density_function();
	    
        ValueInterval<Double> domain = new ValueInterval<Double>(data_points).expand(d_parzen_sigma).multiply(4);
	    PlottingTools.plot(density_function, sample_count, domain);
	}
	


}
