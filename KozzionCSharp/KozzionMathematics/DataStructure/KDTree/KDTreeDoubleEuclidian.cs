using KozzionMathematics.Algebra;
using KozzionMathematics.Function.Implementation.Distance;

namespace KozzionMathematics.Datastructure.k_d_tree
{
    public class KDTreeDoubleEuclidian<LabelType> : KDTree<double, double, LabelType>
    {
        /// <summary>
        /// Create a new KD-Tree given a number of dimensions and initial bucket capacity.
        /// </summary>
        /// <param name="dimension_count">The number of data sorting dimensions. i.e. 3 for a 3D point.</param>
        /// <param name="bucket_capacity">The default number of items that can be stored in each node.</param>
        public KDTreeDoubleEuclidian(int dimension_count, int bucket_capacity)
            : base(new AlgebraRealFloat64(), dimension_count, bucket_capacity)
        {
        }

        /// <summary>
        /// Create a new KD-Tree given a number of dimensions.
        /// </summary>
        /// <param name="dimension_count">The number of data sorting dimensions. i.e. 3 for a 3D point.</param>
        public KDTreeDoubleEuclidian(int dimension_count)
            : this(dimension_count, 24)
        {
        }

   

        /// <summary>
        /// Get the nearest neighbours to a point in the kd tree using a square euclidean distance function.
        /// </summary>
        /// <param name="tSearchPoint">The point of interest.</param>
        /// <param name="iMaxReturned">The maximum number of points which can be returned by the iterator.</param>
        /// <param name="fDistance">A threshold distance to apply.  Optional.  Negative values mean that it is not applied.</param>
        /// <returns>A new nearest neighbour iterator with the given parameters.</returns>
        public NearestNeighbourEnumerator<double, double, LabelType> NearestNeighbors(double[] tSearchPoint, int iMaxReturned)
        {
            return NearestNeighbors(tSearchPoint, new FunctionDistanceEuclidean(), 0.0, -1.0, false, iMaxReturned);
        }
    }



}
