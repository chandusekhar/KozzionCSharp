using System;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;

namespace KozzionMathematics.Datastructure.k_d_tree
{

    /// <summary>
    /// A KDTree class represents the root of a variable-dimension KD-Tree.
    /// </summary>
    /// <typeparam name="LabelType">The generic data type we want this tree to contain.</typeparam>
    /// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
    public class KDTree<DomainType, DistanceType, LabelType> : KDNode<DomainType, DistanceType, LabelType>
        where DomainType : IComparable<DomainType>
        where DistanceType : IComparable<DistanceType>
       
    {
        private IAlgebraReal<DomainType> algebra;

        public int DimensionCount { get; private set; }

        /// <summary>
        /// Create a new KD-Tree given a number of dimensions.
        /// </summary>
        /// <param name="dimension_count">The number of data sorting dimensions. i.e. 3 for a 3D point.</param>
        public KDTree(IAlgebraReal<DomainType> algebra, int dimension_count)
            : this(algebra, dimension_count, 24)
        {
        
        }

        /// <summary>
        /// Create a new KD-Tree given a number of dimensions and initial bucket capacity.
        /// </summary>
        /// <param name="dimension_count">The number of data sorting dimensions. i.e. 3 for a 3D point.</param>
        /// <param name="bucket_capacity">The default number of items that can be stored in each node.</param>
        public KDTree(IAlgebraReal<DomainType> algebra, int dimension_count, int bucket_capacity)
            : base(algebra, dimension_count, bucket_capacity)
        {
            this.algebra = algebra;
            DimensionCount = dimension_count;
        }

  

        /// <summary>
        /// Get the nearest neighbours to a point in the kd tree using a user defined distance function.
        /// </summary>
        /// <param name="search_point">The point of interest.</param>
        /// <param name="max_result_count">The maximum number of points which can be returned by the iterator.</param>
        /// <param name="kDistanceFunction">The distance function to use.</param>
        /// <param name="threshold">A threshold distance to apply.  Optional.  Negative values mean that it is not applied.</param>
        /// <returns>A new nearest neighbour iterator with the given parameters.</returns>
        public NearestNeighbourEnumerator<DomainType, DistanceType, LabelType> NearestNeighbors(DomainType[] search_point, IFunctionDistance<DomainType[], DistanceType> distance_function, DistanceType distance_zero, DistanceType threshold, bool use_threshold, int max_result_count)
        {
            return new NearestNeighbourEnumerator<DomainType, DistanceType, LabelType>(distance_zero, this, search_point, distance_function, max_result_count, threshold, true);
        }

        public KDTree<DomainType, DistanceType, LabelType> Copy()
        {
            throw new NotImplementedException();
        }
    }
}