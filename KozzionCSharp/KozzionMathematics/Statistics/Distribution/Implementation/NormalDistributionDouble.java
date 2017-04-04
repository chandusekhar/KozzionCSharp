package com.kozzion.library.math.statistics.distribution.implementation;

import org.apache.commons.math3.util.FastMath;

import com.kozzion.library.math.statistics.distribution.interfaces.ICumulativeDistributionFunctionDouble;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionDoubleNumber;
import com.kozzion.library.math.statistics.distribution.interfaces.IProbabilityDensityFunctionDouble;

public class NormalDistributionDouble
    implements
        IDistributionDoubleNumber<Double>
{
    private final double                                             d_mean;
    private final double                                             d_standard_deviation;

    private final IProbabilityDensityFunctionDouble<Double>          d_density_function;
    private final ICumulativeDistributionFunctionDouble<Double>      d_culmative_function;

    private org.apache.commons.math3.distribution.NormalDistribution d_distribution;

    public NormalDistributionDouble(
        final double mean,
        final double standard_deviation)
    {
        d_mean = mean;
        d_standard_deviation = standard_deviation;
        d_density_function = new ProbabilityDensityFunctionDouble<Double>(this);
        d_culmative_function = new CumulativeDistributionFunctionDouble<Double>(this);
        d_distribution = new org.apache.commons.math3.distribution.NormalDistribution(d_mean, d_standard_deviation);
    }

    @Override
    public IProbabilityDensityFunctionDouble<Double> get_density_function()
    {
        return d_density_function;
    }

    @Override
    public ICumulativeDistributionFunctionDouble<Double> get_culmative_function()
    {
        return d_culmative_function;
    }

    @Override
    public Double get_probability_density(
        final Double input)
    {
        return d_distribution.density(input);
    }

    @Override
    public double get_cumulative_probability(
        final Double input)
    {
        return d_distribution.cumulativeProbability(input);
    }

    @Override
    public double get_mean()
    {
        return d_mean;
    }

    @Override
    public double get_variance()
    {
        return FastMath.pow(d_standard_deviation, 2);
    }

    @Override
    public String toString()
    {
        return "Normal Distribution\tMean:" + d_mean + "\tDeviation:" + d_standard_deviation;
    }

    public static double compute_upper_p_value(
        double z_value)
    {
        org.apache.commons.math3.distribution.NormalDistribution distribution = new org.apache.commons.math3.distribution.NormalDistribution(
            0, 1);
        return (1.0 - distribution.cumulativeProbability(z_value));
    }

    public static double compute_lower_p_value(
        double z_value)
    {
        org.apache.commons.math3.distribution.NormalDistribution distribution = new org.apache.commons.math3.distribution.NormalDistribution(
            0, 1);
        return distribution.cumulativeProbability(z_value);
    }

}
