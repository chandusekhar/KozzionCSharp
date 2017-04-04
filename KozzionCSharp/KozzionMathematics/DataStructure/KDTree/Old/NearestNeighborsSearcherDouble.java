package com.kozzion.library.math.datastructure.kdtree;

import java.io.Serializable;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Map;
import java.util.PriorityQueue;

import com.kozzion.library.math.datastructure.matrix.DoubleVector;
import com.kozzion.library.math.function.measures.IDistanceMeasureDoubleVectorToDouble;
import com.kozzion.library.math.function.measures.implementation.DistanceDoubleEuclidean;

/**
 * NearestNeighbors implements an algorithm for finding the k-nearest neighbors to a query point within the set of
 * points contained by a {@link KDTreeDouble} instance. The algorithm can be specialized with a custom distance-finding
 * function by passing a {@link IDistanceMeasureDoubleVectorToDouble} instance to its constructor.
 */
public class NearestNeighborsSearcherDouble<LabellingType> implements Serializable
{
    /**
     * 
     */
    private static final long                   serialVersionUID = 1L;

    private boolean                             d_omit_query_point;
    private int                                 d_neighbor_count;
    private double                              d_minimal_distance;
    private IDistanceMeasureDoubleVectorToDouble   d_distance_measure;
    private PriorityQueue<Entry<LabellingType>> d_priority_queue;
    private DoubleVector                        d_query;

    /**
     * Constructs a new NearestNeighbors instance, using the specified distance-finding functor to calculate distances
     * during searches.
     * 
     * @param distance
     *            A distance-finding functor implementing the {@link IDistanceMeasureDoubleVectorToDouble} interface.
     */
    public NearestNeighborsSearcherDouble(final IDistanceMeasureDoubleVectorToDouble distance_measure)
    {
        d_distance_measure = distance_measure;
    }

    /**
     * Constructs a NearestNeighbors instance using a {@link DistanceDoubleEuclidean} instance to calculate distances between
     * points.
     */
    public NearestNeighborsSearcherDouble()
    {
        this(new DistanceDoubleEuclidean());
    }

    /**
     * Sets the distance-finding functor used to calculate distances during searches.
     * 
     * @param distance
     *            The distance-finding functor to use for distance calculations.
     */
    public void setDistance(final IDistanceMeasureDoubleVectorToDouble distance_measure)
    {
        d_distance_measure = distance_measure;
    }

    /**
     * Finds the k-nearest neighbors to a query point withina KDTree instance. The neighbors are returned as an array of
     * {@link Entry} instances, sorted from nearest to farthest.
     * 
     * @param tree
     *            The KDTree to search.
     * @param queryPoint
     *            The query point.
     * @param neighbor_count
     *            The number of nearest neighbors to find. This should be a positive value. Non-positive values result
     *            in no neighbors being found.
     * @param omit_query_point
     *            If true, point-value mappings at a distance of zero are omitted from the result. If false, mappings at
     *            a distance of zero are included.
     * @return An array containing the nearest neighbors and their distances sorted by least distance to greatest
     *         distance. If no neighbors are found, the array will have a length of zero.
     */
    public Entry<LabellingType> [] get(final KDTreeDouble<LabellingType> tree, final DoubleVector queryPoint, final int neighbor_count,
        final boolean omit_query_point)
    {
        d_omit_query_point = omit_query_point;
        d_neighbor_count = neighbor_count;
        d_query = queryPoint;
        d_minimal_distance = Double.POSITIVE_INFINITY;

        d_priority_queue = new PriorityQueue<Entry<LabellingType>>(neighbor_count, new EntryComparator());

        if (d_neighbor_count > 0)
        {
            find(tree.d_root_node);
        }

        final Entry<LabellingType> [] neighbors = new Entry [d_priority_queue.size()];

        if (d_priority_queue.size() < neighbor_count)
        {
            throw new IllegalArgumentException("Not enough entries in kd-tree to find " + d_neighbor_count + " neighbors.");
        }

        d_priority_queue.toArray(neighbors);
        Arrays.sort(neighbors);

        d_priority_queue = null;
        d_query = null;

        return neighbors;
    }

    /**
     * Same as {@link #get get(tree, queryPoint, numNeighbors, true)}.
     */
    public Entry<LabellingType> [] get(final KDTreeDouble<LabellingType> tree, final DoubleVector queryPoint, final int numNeighbors)
    {
        return get(tree, queryPoint, numNeighbors, true);
    }

    private void find(final KDTreeDouble<LabellingType>.KdNode node)
    {
        if (node == null)
        {
            return;
        }

        final int discriminator = node.d_discriminator;
        final DoubleVector point = node.getKey();
        double distance_squared = d_distance_measure.distance_squared(d_query, point);

        if ((distance_squared < d_minimal_distance) && ((distance_squared != 0.0) || !d_omit_query_point))
        {
            if (d_priority_queue.size() == d_neighbor_count)
            {
                d_priority_queue.poll();
                d_priority_queue.add(new NearestNeighborEntry(distance_squared, node));
                d_minimal_distance = d_priority_queue.peek().get_squared_distance();
            }
            else
            {
                d_priority_queue.add(new NearestNeighborEntry(distance_squared, node));
                if (d_priority_queue.size() == d_neighbor_count)
                {
                    d_minimal_distance = d_priority_queue.peek().get_squared_distance();
                }
            }
        }

        final double dp = d_query.get_coordinate(discriminator) - point.get_coordinate(discriminator);

        distance_squared = dp * dp;

        if (dp < 0)
        {
            find(node.d_low);
            if (distance_squared < d_minimal_distance)
            {
                find(node.d_high);
            }
        }
        else
        {
            find(node.d_high);
            if (distance_squared < d_minimal_distance)
            {
                find(node.d_low);
            }
        }
    }

    /**
     * The Entry interface makes accessible the results of a {@link NearestNeighborsSearcherDouble} search. An Entry exposes
     * both the point-value mapping and its distance from the query point.
     */
    public interface Entry<LabellingType>
    {
        /**
         * Returns the distance from result to the query point. This will usually be implemented by dynamically taking
         * the square root of {@link #get_squared_distance}. Therefore, repeated calls may be expensive.
         * 
         * @return The distance from result to the query point.
         */
        public double get_distance();

        /**
         * Returns the square of the distance from result to the query point. This will usually be implemented as
         * returning a cached value used during the nearest neighbors search.
         * 
         * @return The square of the distance from result to the query point.
         */
        public double get_squared_distance();

        /**
         * Returns the point-value mapping stored in this query result.
         * 
         * @return The point-value mapping stored in this query result.
         */
        public Map.Entry<DoubleVector, LabellingType> get_neighbor();
    }

    private final class NearestNeighborEntry implements Entry<LabellingType>, Comparable<Entry<LabellingType>>
    {
        double                                 squared_distance;
        Map.Entry<DoubleVector, LabellingType> _neighbor;

        NearestNeighborEntry(final double distance2, final Map.Entry<DoubleVector, LabellingType> neighbor)
        {
            squared_distance = distance2;
            _neighbor = neighbor;
        }

        @Override
        public double get_distance()
        {
            return StrictMath.sqrt(squared_distance);
        }

        @Override
        public double get_squared_distance()
        {
            return squared_distance;
        }

        @Override
        public Map.Entry<DoubleVector, LabellingType> get_neighbor()
        {
            return _neighbor;
        }

        @Override
        public int compareTo(final Entry<LabellingType> obj)
        {
            final double d = obj.get_squared_distance();

            if (squared_distance < d)
            {
                return -1;
            }
            else
                if (squared_distance > d)
                {
                    return 1;
                }

            return 0;
        }
    }

    private final class EntryComparator implements Comparator<Entry<LabellingType>>
    {
        // Invert relationship so priority queue keeps highest on top.
        @Override
        public int compare(final Entry<LabellingType> n1, final Entry<LabellingType> n2)
        {
            final double d1 = n1.get_squared_distance();
            final double d2 = n2.get_squared_distance();

            if (d1 < d2)
            {
                return 1;
            }
            else
                if (d1 > d2)
                {
                    return -1;
                }

            return 0;
        }

        @Override
        public boolean equals(final Object obj)
        {
            return ((obj != null) && (obj == this));
        }
    }
}
