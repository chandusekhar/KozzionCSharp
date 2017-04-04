using System.Collections.Generic;

namespace KozzionMathematics.Algebra
{
    public interface IAlgebraReal<DomainType> : IComparer<DomainType>
    {
        DomainType PositiveInfinity { get; }

        DomainType NegativeInfinity { get; }

        DomainType MinValue { get; }

        DomainType MaxValue { get; }

        DomainType AddIdentity { get; }

        DomainType MultiplyIdentity { get; }

        DomainType NaN { get; }

        DomainType Add(DomainType value_0, DomainType value_1);

        DomainType Subtract(DomainType value_0, DomainType value_1);

        DomainType Multiply(DomainType value_0, DomainType value_1);

        DomainType Divide(DomainType value_0, DomainType value_1);

        DomainType Modulo(DomainType value_0, DomainType value_1);

        DomainType Max(DomainType value_0, DomainType value_1);

        DomainType Min(DomainType value_0, DomainType value_1);

        DomainType Mean(DomainType value_0, DomainType value_1);

        int CompareTo(DomainType value_0, DomainType value_1);

        bool IsNaN(DomainType value_0);

        DomainType Pow(DomainType base_value, DomainType power);

        DomainType Abs(DomainType realType);

        DomainType Sqrt(DomainType value_0);

        DomainType Sqr(DomainType value_0);

        DomainType LogE(DomainType value_0);

        DomainType Log10(DomainType value_0);

        int FloorInt(DomainType value_0);

        DomainType ToDomain(int value);

        DomainType ToDomain(float value);

        DomainType ToDomain(double value);
    
    }
}
