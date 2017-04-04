using System;
using System.Numerics;
using KozzionMathematics.Tools;


namespace KozzionMathematics.FiniteField
{
    public class FiniteFieldElement<DomainType>
    {
        public IAlgebraFieldFinite<DomainType> Algebra { get; private set; } 
        public DomainType Value { get; private set; }

        public FiniteFieldElement(
            IAlgebraFieldFinite<DomainType> field,
            DomainType value)
        {
            Algebra = field;
            Value = value;
        }

        public FiniteFieldElement<DomainType> Add(
            FiniteFieldElement<DomainType> other)
        {
            return Algebra.Add(this, other);
        }

        public FiniteFieldElement<DomainType> Subtract(
            FiniteFieldElement<DomainType> other)
        {
            return  Algebra.Subtract(this, other);
        }

        public FiniteFieldElement<DomainType> Multiply(
            FiniteFieldElement<DomainType> other)
        {
            return Algebra.Multiply(this, other);
        }

        public FiniteFieldElement<DomainType> Divide(
            FiniteFieldElement<DomainType> other)
        {
            return Algebra.Divide(this, other);
        }

        public static FiniteFieldElement<DomainType> operator +(FiniteFieldElement<DomainType> element_0, FiniteFieldElement<DomainType> element_1)
        {
            return element_0.Add(element_1);
        }

        public static FiniteFieldElement<DomainType> operator -(FiniteFieldElement<DomainType> element_0, FiniteFieldElement<DomainType> element_1)
        {
            return element_0.Subtract(element_1);
        }

        public static FiniteFieldElement<DomainType> operator *(FiniteFieldElement<DomainType> element_0, FiniteFieldElement<DomainType> element_1)
        {
            return element_0.Multiply(element_1);
        }

        public static FiniteFieldElement<DomainType> operator /(FiniteFieldElement<DomainType> element_0, FiniteFieldElement<DomainType> element_1)
        {
            return element_0.Divide(element_1);
        }

        public static bool operator ==(FiniteFieldElement<DomainType> element_0, FiniteFieldElement<DomainType> element_1)
        {
            return ((element_0.Algebra == element_1.Algebra) && (element_0.Value.Equals(element_1.Value)));
        }

        public static bool operator !=(FiniteFieldElement<DomainType> element_0, FiniteFieldElement<DomainType> element_1)
        {
            return ((element_0.Algebra != element_1.Algebra) || (!element_0.Value.Equals(element_1.Value)));
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is FiniteFieldElement<DomainType>)
            {
                FiniteFieldElement<DomainType> other_typed = (FiniteFieldElement<DomainType>)other;
                return this == other_typed;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}