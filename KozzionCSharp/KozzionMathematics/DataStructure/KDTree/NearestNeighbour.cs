using System;
using System.Collections;
using System.Collections.Generic;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;

namespace KozzionMathematics.Datastructure.k_d_tree
{
    /// <summary>
    /// A NearestNeighbour iterator for the KD-tree which intelligently iterates and captures relevant data in the search space.
    /// </summary>
    /// <typeparam name="LabelType">The type of data the iterator should handle.</typeparam>
    public class NearestNeighbourEnumerator<DomainType, DistanceType, LabelType> : IEnumerator<Tuple<DomainType[], DistanceType, LabelType>>
        where DomainType : IComparable<DomainType>
        where DistanceType : IComparable<DistanceType>
    {
        /// <summary>The point from which are searching in n-dimensional space.</summary>
        private DomainType[] search_point;
        /// <summary>A distance function which is used to compare nodes and value positions.</summary>
        private IFunctionDistance<DomainType [], DistanceType> distance_function;
        /// <summary>The tree nodes which have yet to be evaluated.</summary>
        private MinHeap<KDNode<DomainType, DistanceType, LabelType>, DistanceType> pPending;
        /// <summary>The values which have been evaluated and selected.</summary>
        private IntervalHeap<LabelType, DistanceType> pEvaluated;
        /// <summary>The root of the kd tree to begin searching from.</summary>
        private KDNode<DomainType, DistanceType, LabelType> pRoot = null;

        /// <summary>The max number of points we can return through this iterator.</summary>
        private int iMaxPointsReturned = 0;
        /// <summary>The number of points we can still test before conclusion.</summary>
        private int iPointsRemaining;
        /// <summary>Threshold to apply to tree iteration.  Negative numbers mean no threshold applied.</summary>
        private DistanceType fThreshold;
        private bool d_use_threshold;

        /// <summary>Current value distance.</summary>
        private DistanceType _CurrentDistance;
        private bool d_current_distance_set;
        /// <summary>Current value reference.</summary>
        private LabelType _Current = default(LabelType);


       // private IAlgebraReal<DomainType> algebra_domain;
        private DistanceType distance_zero;

        /// <summary>
        /// Construct a new nearest neighbour iterator.
        /// </summary>
        /// <param name="pRoot">The root of the tree to begin searching from.</param>
        /// <param name="tSearchPoint">The point in n-dimensional space to search.</param>
        /// <param name="distance_function">The distance function used to evaluate the points.</param>
        /// <param name="max_result_count">The max number of points which can be returned by this iterator.  Capped to max in tree.</param>
        /// <param name="threshold">Threshold to apply to the search space.  Negative numbers indicate that no threshold is applied.</param>
        public NearestNeighbourEnumerator(DistanceType distance_zero, KDNode<DomainType, DistanceType, LabelType> pRoot, DomainType[] tSearchPoint, IFunctionDistance<DomainType[], DistanceType> distance_function, int max_result_count, DistanceType threshold, bool use_threshold)
        {     
            // Check the dimensionality of the search point.
            if (tSearchPoint.Length != pRoot.d_dimension_count)
            {
                throw new Exception("Dimensionality of search point and kd-tree are not the same.");
            }

            //this.algebra_domain = algebra;

            // Store the search point.
            this.search_point = new DomainType[tSearchPoint.Length];
            Array.Copy(tSearchPoint, this.search_point, tSearchPoint.Length);

            // Store the point count, distance function and tree root.
            this.iPointsRemaining = Math.Min(max_result_count, pRoot.Size);
            this.fThreshold = threshold;
            this.d_use_threshold = use_threshold;

            this.distance_function = distance_function;
            this.pRoot = pRoot;
            this.iMaxPointsReturned = max_result_count;

            this._CurrentDistance = distance_zero;
            this.d_current_distance_set = false;
            this.distance_zero = distance_zero;
            // Create an interval heap for the points we check.
            this.pEvaluated = new IntervalHeap<LabelType, DistanceType>();

            // Create a min heap for the things we need to check.
            this.pPending = new MinHeap<KDNode<DomainType, DistanceType, LabelType>, DistanceType>();
            this.pPending.Insert(distance_zero, pRoot);
        }

        /// <summary>
        /// Check for the next iterator item.
        /// </summary>
        /// <returns>True if we have one, false if not.</returns>
        public bool MoveNext()
        {
            // Bail if we are finished.
            if (iPointsRemaining == 0)
            {
                _Current = default(LabelType);
                return false;
            }

            // While we still have paths to evaluate.
            while (pPending.Size > 0 && (pEvaluated.Size == 0 || (pPending.MinKey.CompareTo(pEvaluated.MinKey) == -1)))
            {
                // If there are pending paths possibly closer than the nearest evaluated point, check it out
                KDNode<DomainType, DistanceType, LabelType> pCursor = pPending.Min;
                pPending.RemoveMin();

                // Descend the tree, recording paths not taken
                while (!pCursor.IsLeaf)
                {
                    KDNode<DomainType, DistanceType, LabelType> pNotTaken;

                    // If the seach point is larger, select the right path.
                    if (search_point[pCursor.d_split_dimension].CompareTo(pCursor.d_split_value) == 1)
                    {
                        pNotTaken = pCursor.pLeft;
                        pCursor = pCursor.pRight;
                    }
                    else
                    {
                        pNotTaken = pCursor.pRight;
                        pCursor = pCursor.pLeft;
                    }

                    // Calculate the shortest distance between the search point and the min and max bounds of the kd-node.
                    DistanceType fDistance = distance_function.ComputeToRectangle(search_point, pNotTaken.tMinBound, pNotTaken.tMaxBound);

                    // If it is greater than or equal to the threshold, skip.
                    if (d_use_threshold && (fDistance.CompareTo(fThreshold) != -1))
                    {
                        //pPending.Insert(fDistance, pNotTaken);
                        continue;
                    }

                    // Only add the path we need more points or the node is closer than furthest point on list so far.
                    // if (pEvaluated.Size < iPointsRemaining || fDistance <= pEvaluated.MaxKey)
                    if (pEvaluated.Size < iPointsRemaining || fDistance.CompareTo(pEvaluated.MaxKey) != 1)
                    {
                        pPending.Insert(fDistance, pNotTaken);
                    }
                }

                // If all the points in this KD node are in one place.
                if (pCursor.bSinglePoint)
                {
                    // Work out the distance between this point and the search point.
                    DistanceType fDistance = distance_function.Compute(pCursor.d_points[0], search_point);

                    // If it is greater than or equal to the threshold, skip.
                    if (d_use_threshold && (fDistance.CompareTo(fThreshold) != -1))
                    {
                        continue;
                    }

                    // Add the point if either need more points or it's closer than furthest on list so far.
                    if (pEvaluated.Size < iPointsRemaining || (fDistance.CompareTo(pEvaluated.MaxKey) != 1))
                    {
                        for (int i = 0; i < pCursor.Size; ++i)
                        {
                            // If we don't need any more, replace max
                            if (pEvaluated.Size == iPointsRemaining)
                            {
                                pEvaluated.ReplaceMax(fDistance, pCursor.d_values[i]);
                            }

                            // Otherwise insert.
                            else
                            {
                                pEvaluated.Insert(fDistance, pCursor.d_values[i]);
                            }
                        }
                    }
                }

                // If the points in the KD node are spread out.
                else
                {
                    // Treat the distance of each point seperately.
                    for (int i = 0; i < pCursor.Size; ++i)
                    {
                        // Compute the distance between the points.
                        DistanceType fDistance = distance_function.Compute(pCursor.d_points[i], search_point);

                        // If it is greater than or equal to the threshold, skip.
                        if (d_use_threshold && (fDistance.CompareTo(fThreshold) != -1))
                        {
                            continue;
                        }
                        // Insert the point if we have more to take.
                        if (pEvaluated.Size < iPointsRemaining)
                        {
                            pEvaluated.Insert(fDistance, pCursor.d_values[i]);
                        }
                        // Otherwise replace the max.
                        else if (fDistance.CompareTo(pEvaluated.MaxKey) == -1)
                        {
                            pEvaluated.ReplaceMax(fDistance, pCursor.d_values[i]);
                        }
                    }
                }
            }

            // Select the point with the smallest distance.
            if (pEvaluated.Size == 0)
                return false;

            iPointsRemaining--;
            _CurrentDistance = pEvaluated.MinKey;
            _Current = pEvaluated.Min;
            pEvaluated.RemoveMin();
            return true;
        }

        /// <summary>
        /// Reset the iterator.
        /// </summary>
        public void Reset()
        {
            // Store the point count and the distance function.
            this.iPointsRemaining = Math.Min(iMaxPointsReturned, pRoot.Size);
            _CurrentDistance = distance_zero;
            d_current_distance_set = false;

            // Create an interval heap for the points we check.
            this.pEvaluated = new IntervalHeap<LabelType, DistanceType>();

            // Create a min heap for the things we need to check.
            this.pPending = new MinHeap<KDNode<DomainType, DistanceType, LabelType>, DistanceType>();
            this.pPending.Insert(distance_zero, pRoot);
        }

        /// <summary>
        /// Return the current value referenced by the iterator as an object.
        /// </summary>
        object IEnumerator.Current
        {
            get { return _Current; }
        }

        /// <summary>
        /// Return the distance of the current value to the search point.
        /// </summary>
        public DistanceType CurrentDistance
        {
            get 
            {
                if (d_current_distance_set)
                {
                    return _CurrentDistance;
                }
                else
                {
                    throw new Exception("CurrentDistance not set!");
                }
            }
        }

        /// <summary>
        /// Return the current value referenced by the iterator.
        /// </summary>
        public Tuple<DomainType[], DistanceType, LabelType> Current
        {
            get { return new Tuple<DomainType[], DistanceType, LabelType>(null, _CurrentDistance, _Current); }
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}