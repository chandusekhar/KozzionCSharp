package com.kozzion.library.math.datastructure.kdtree;

import java.util.Iterator;
import java.util.Map;

/**
 * A RangeSearchTree is a spatial data structure that supports the retrieval of data associated with point keys as well
 * as the searching of data that occurs within a specified range of points.
 * <p>
 * Note: RangeSearchTree does not implement SortedMap for range searching because the SortedMap interface is not
 * well-suited to multidimensional range search trees.
 * </p>
 */
public interface IRangeSearchTreeFloat<LabellingType> extends Map<float [], LabellingType>
{
    /**
     * Returns an iterator for mappings that are contained in the rectangle defined by the given lower left-hand and
     * upper right-hand corners. The mappings returned include those occuring at points on the bounding rectangle, not
     * just those inside.
     * 
     * @param lower
     *            The lower left-hand corner of the bounding rectangle. A null value can be used to specify the region
     *            is unbounded in that direction.
     * @param upper
     *            The upper right-hand corner of the bounding rectangle. A null value can be used to specify the region
     *            is unbounded in that direction.
     * @return An iterator for mappings that are contained in the specified rectangle.
     */
    public Iterator<Map.Entry<float [], LabellingType>> iterator(float [] lower, float [] upper);

}
