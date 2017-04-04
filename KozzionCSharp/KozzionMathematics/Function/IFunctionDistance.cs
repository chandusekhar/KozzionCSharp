namespace KozzionMathematics.Function
{
    public interface IFunctionDistance<DomainType, RangeType> : IFunctionDissimilarity<DomainType, RangeType>
    {
        /// <summary>
        /// Find the shortest distance from a point to an axis aligned rectangle in n-dimensional space.
        /// </summary>
        /// <param name="point">The point of interest.</param>
        /// <param name="min">The minimum coordinate of the rectangle.</param>
        /// <param name="max">The maximum coorindate of the rectangle.</param>
        /// <returns>The shortest n-dimensional distance between the point and rectangle.</returns>
        RangeType ComputeToRectangle(DomainType valeu_0, DomainType upper, DomainType lower);
    }
}
