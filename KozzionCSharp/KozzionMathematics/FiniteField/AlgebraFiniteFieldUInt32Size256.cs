using System;
using System.Numerics;
namespace KozzionMathematics.FiniteField
{
    public class AlgebraFiniteFieldUInt32Size256 : IAlgebraFieldFinite<uint>
    {
        public BigInteger ElementCount { get { return 256; } }

        public FiniteFieldElement<uint> AddIdentity { get { throw new NotImplementedException(); } }

        public FiniteFieldElement<uint> MultiplyIdentity { get { throw new NotImplementedException(); } }

        public FiniteFieldElement<uint>[] GetElements()
        {

            FiniteFieldElement < uint>[] elements = new FiniteFieldElement<uint>[256];
            for (int index = 0; index < 256; index++)
            {
                elements[index] = new FiniteFieldElement<uint>(this,(uint)index);
            }
            return elements;
        }

        public bool Contains(FiniteFieldElement<uint> input)
        {
            return input.Algebra == this;
        }       

        public FiniteFieldElement<uint> Add(FiniteFieldElement<uint> value_0, FiniteFieldElement<uint> value_1)
        {
			return new FiniteFieldElement<uint>(this, value_0.Value ^ value_1.Value);
		}

        public FiniteFieldElement<uint> Subtract(FiniteFieldElement<uint> value_0, FiniteFieldElement<uint> value_1)
        {
			return new FiniteFieldElement<uint>(this, value_0.Value ^ value_1.Value);
		}

        /* Multiply two numbers in the GF(2^8) finite field defined 
			* by the polynomial x^8 + x^4 + x^3 + x + 1 = 0 */
        public FiniteFieldElement<uint> Multiply(FiniteFieldElement<uint> value_0, FiniteFieldElement<uint> value_1)
        {
            uint a = value_0.Value;
            uint b = value_1.Value;
            uint p = 0;
            uint counter;
            uint carry;
            for (counter = 0; counter < 8; counter++)
            {
                if ((b & 1) == 0)
                {
                    p ^= a;
                }
                carry = (a & 0x80);  /* detect if x^8 term is about to be generated */
                a <<= 1;
                if (carry == 0)
                {
                    a ^= 0x1B; /* replace x^8 with x^4 + x^3 + x + 1 */
                }
                b >>= 1;
            }
            return new FiniteFieldElement<uint>(this, p);
        }

        public FiniteFieldElement<uint> Divide(FiniteFieldElement<uint> value_0, FiniteFieldElement<uint> value_1)
        {
            throw new NotImplementedException();
        }

        public FiniteFieldElement<uint> ToDomain(BigInteger value)
        {
            throw new NotImplementedException();
        }

        public BigInteger ToBigInteger(FiniteFieldElement<uint> value)
        {
            throw new NotImplementedException();
        }

        public FiniteFieldElement<uint> ToDomain(int value)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(FiniteFieldElement<uint> value)
        {
            throw new NotImplementedException();
        }
    }
}
