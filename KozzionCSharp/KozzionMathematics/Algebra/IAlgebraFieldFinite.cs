using KozzionMathematics.Algebra;
using System.Numerics;

namespace KozzionMathematics.FiniteField
{
    public interface IAlgebraFieldFinite<DomainType> : IAlgebraField<FiniteFieldElement<DomainType>>
    {
        BigInteger ElementCount { get; }

        FiniteFieldElement<DomainType>[] GetElements();

        bool Contains(FiniteFieldElement<DomainType> input);
    }
}