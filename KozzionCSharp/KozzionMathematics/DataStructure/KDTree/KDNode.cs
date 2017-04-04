using System;
using KozzionMathematics.Algebra;

namespace KozzionMathematics.Datastructure.k_d_tree
{
    /// <summary>
    /// A KD-Tree node which supports a generic number of dimensions.  All data items
    /// need the same number of dimensions.
    /// This node splits based on the largest range of any dimension.
    /// </summary>
    /// <typeparam name="DomainType">The generic data type this structure uses for coordinates.</typeparam>
    /// <typeparam name="LabelType">The generic data type this structure contains.</typeparam>
    /// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
    public class KDNode<DomainType, DistanceType, LabelType>
        where DomainType : IComparable<DomainType>
    {
        #region Internal properties and constructor
        // All types
        /// <summary>
        /// The number of dimensions for this node.
        /// </summary>
        protected internal int d_dimension_count;

        /// <summary>
        /// The maximum capacity of this node.
        /// </summary>
        protected internal int d_bucket_capacity;

        // Leaf only
        /// <summary>
        /// The array of locations.  [index][dimension]
        /// </summary>
        protected internal DomainType[][] d_points;

        /// <summary>
        /// The array of data values. [index]
        /// </summary>
        protected internal LabelType[] d_values;

        // Stem only
        /// <summary>
        /// The left and right children.
        /// </summary>
        protected internal KDNode<DomainType, DistanceType, LabelType> pLeft, pRight;
        /// <summary>
        /// The split dimension.
        /// </summary>
        protected internal int d_split_dimension;
        /// <summary>
        /// The split value (larger go into the right, smaller go into left)
        /// </summary>
        protected internal DomainType d_split_value;

        // Bounds
        /// <summary>
        /// The min and max bound for this node.  All dimensions.
        /// </summary>
        protected internal DomainType[] tMinBound, tMaxBound;

        /// <summary>
        /// Does this node represent only one point.
        /// </summary>
        protected internal bool bSinglePoint;

        /// <summary>
        /// The Algbra used on <typeparam name="DomainType">
        /// </summary>
        private IAlgebraReal<DomainType> d_algebra;

        /// <summary>
        /// Protected method which constructs a new KDNode.
        /// </summary>
        /// <param name="dimension_count">The number of dimensions for this node (all the same in the tree).</param>
        /// <param name="d_bucket_capacity">The initial capacity of the bucket.</param>
        protected KDNode(IAlgebraReal<DomainType> algebra, int dimension_count, int d_bucket_capacity)
        {
            // Algebra
            this.d_algebra = algebra;

            // Variables.
            this.d_dimension_count = dimension_count;
            this.d_bucket_capacity = d_bucket_capacity;
            this.Size = 0;
            this.bSinglePoint = true;

            // Setup leaf elements.
            this.d_points = new DomainType[d_bucket_capacity + 1][];
            this.d_values = new LabelType[d_bucket_capacity + 1];
        }
        #endregion

        #region External Operations
        /// <summary>
        /// The number of items in this leaf node and all children.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Is this KDNode a leaf or not?
        /// </summary>
        public bool IsLeaf { get { return d_points != null; } }

        /// <summary>
        /// Insert a new point into this leaf node.
        /// </summary>
        /// <param name="tPoint">The position which represents the data.</param>
        /// <param name="kValue">The value of the data.</param>
        public void AddPoint(DomainType[] tPoint, LabelType kValue)
        {
            // Find the correct leaf node.
            KDNode<DomainType, DistanceType, LabelType> pCursor = this;
            while (!pCursor.IsLeaf)
            {
                // Extend the size of the leaf.
                pCursor.ExtendBounds(tPoint);
                pCursor.Size++;

                // If it is larger select the right, or lower,  select the left.
                if (tPoint[pCursor.d_split_dimension].CompareTo(pCursor.d_split_value) == 1)
                {
                    pCursor = pCursor.pRight;
                }
                else
                {
                    pCursor = pCursor.pLeft;
                }
            }

            // Insert it into the leaf.
            pCursor.AddLeafPoint(tPoint, kValue);
        }
        #endregion

        #region Internal Operations
        /// <summary>
        /// Insert the point into the leaf.
        /// </summary>
        /// <param name="tPoint">The point to insert the data at.</param>
        /// <param name="kValue">The value at the point.</param>
        private void AddLeafPoint(DomainType[] tPoint, LabelType kValue)
        {
            // Add the data point to this node.
            d_points[Size] = tPoint;
            d_values[Size] = kValue;
            ExtendBounds(tPoint);
            Size++;

            // Split if the node is getting too large in terms of data.
            if (Size == d_points.Length - 1)
            {
                // If the node is getting too physically large.
                if (CalculateSplit())
                {
                    // If the node successfully had it's split value calculated, split node.
                    SplitLeafNode();
                }
                else
                {
                    // If the node could not be split, enlarge node data capacity.
                    IncreaseLeafCapacity();
                }
            }
        }

        /// <summary>
        /// If the point lies outside the boundaries, return false else true.
        /// </summary>
        /// <param name="tPoint">The point.</param>
        /// <returns>True if the point is inside the boundaries, false outside.</returns>
        private bool CheckBounds(double[] tPoint)
        {
            for (int i = 0; i < d_dimension_count; ++i)
            {
                if (tPoint[i].CompareTo(tMaxBound[i]) == 1)
                {
                    return false;
                }
                if (tPoint[i].CompareTo(tMinBound[i]) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Extend this node to contain a new point.
        /// </summary>
        /// <param name="tPoint">The point to contain.</param>
        private void ExtendBounds(DomainType[] tPoint)
        {
            // If we don't have bounds, create them using the new point then bail.
            if (tMinBound == null)
            {
                tMinBound = new DomainType[d_dimension_count];
                tMaxBound = new DomainType[d_dimension_count];
                Array.Copy(tPoint, tMinBound, d_dimension_count);
                Array.Copy(tPoint, tMaxBound, d_dimension_count);
                return;
            }

            // For each dimension.
            for (int i = 0; i < d_dimension_count; ++i)
            {
                if (d_algebra.IsNaN(tPoint[i]))
                {
                    if (!d_algebra.IsNaN(tMinBound[i]) || !d_algebra.IsNaN(tMaxBound[i]))
                    {
                        bSinglePoint = false;
                    }
                    tMinBound[i] = d_algebra.NaN;
                    tMaxBound[i] = d_algebra.NaN;
                }
                else if (tMinBound[i].CompareTo(tPoint[i]) == 1)
                {
                    tMinBound[i] = tPoint[i];
                    bSinglePoint = false;
                }
                else if (tMaxBound[i].CompareTo(tPoint[i]) == -1)
                {
                    tMaxBound[i] = tPoint[i];
                    bSinglePoint = false;
                }
            }
        }

        /// <summary>
        /// Double the capacity of this leaf.
        /// </summary>
        private void IncreaseLeafCapacity()
        {
            Array.Resize<DomainType[]>(ref d_points, d_points.Length * 2);
            Array.Resize<LabelType>(ref d_values, d_values.Length * 2);
        }

        /// <summary>
        /// Work out if this leaf node should split.  If it should, a new split value and dimension is calculated
        /// based on the dimension with the largest range.
        /// </summary>
        /// <returns>True if the node split, false if not.</returns>
        private bool CalculateSplit()
        {
            // Don't split if we are just one point.
            if (bSinglePoint)
            {
                return false;
            }

            // Find the dimension with the largest range.  This will be our split dimension.
            DomainType fWidth = d_algebra.AddIdentity;
            for (int i = 0; i < d_dimension_count; i++)
            {
                DomainType fDelta = d_algebra.Subtract(tMaxBound[i], tMinBound[i]);
                if (d_algebra.IsNaN(fDelta))
                {
                    fDelta = d_algebra.AddIdentity;
                }

                if (fDelta.CompareTo(fWidth) == 1)
                {
                    d_split_dimension = i;
                    fWidth = fDelta;
                }
            }

            // If we are not wide (i.e. all the points are in one place), don't split.
            if (fWidth.Equals(d_algebra.AddIdentity))
            {
                return false;
            }

            // Split in the middle of the node along the widest dimension.
            d_split_value = d_algebra.Mean(tMinBound[d_split_dimension], tMaxBound[d_split_dimension]);

            // Never split on infinity or NaN.
            if (d_split_value.Equals(d_algebra.PositiveInfinity))
            {
                d_split_value = d_algebra.MaxValue;
            }
            else if (d_split_value.Equals(d_algebra.NegativeInfinity))
            {
                d_split_value = d_algebra.MinValue;
            }

            // Don't let the split value be the same as the upper value as
            // can happen due to rounding errors!
            if (d_split_value.Equals(tMaxBound[d_split_dimension]))
            {
                d_split_value = tMinBound[d_split_dimension];
            }

            // Success
            return true;
        }

        /// <summary>
        /// Split this leaf node by creating left and right children, then moving all the children of
        /// this node into the respective buckets.
        /// </summary>
        private void SplitLeafNode()
        {
            // Create the new children.
            pRight = new KDNode<DomainType, DistanceType, LabelType>(d_algebra, d_dimension_count, d_bucket_capacity);
            pLeft = new KDNode<DomainType, DistanceType, LabelType>(d_algebra, d_dimension_count, d_bucket_capacity);

            // Move each item in this leaf into the children.
            for (int i = 0; i < Size; ++i)
            {
                // Store.
                DomainType[] tOldPoint = d_points[i];
                LabelType kOldData = d_values[i];

                // If larger, put it in the right.
                if (tOldPoint[d_split_dimension].CompareTo(d_split_value) == 1)
                    pRight.AddLeafPoint(tOldPoint, kOldData);

                // If smaller, put it in the left.
                else
                    pLeft.AddLeafPoint(tOldPoint, kOldData);
            }

            // Wipe the data from this KDNode.
            d_points = null;
            d_values = null;
        }
        #endregion
    }
}