package com.kozzion.library.machinelearning.clustering.kmeans;

import com.kozzion.library.math.function.measures.implementation.DistanceManhattanInteger;

public class KMeansManhattan
    extends
        KMeans<int [], Integer>
{

    public KMeansManhattan(
        int cluster_count)
    {
        super(new DistanceManhattanInteger(), new CentroidCalculatorMeanIntegerArray(), cluster_count);
    }

}
