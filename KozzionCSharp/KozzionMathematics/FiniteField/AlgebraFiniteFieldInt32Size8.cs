using System;
using System.Numerics;
namespace KozzionMathematics.FiniteField
{
    public class AlgebraFiniteFieldInt32Size8 : IAlgebraFieldFinite<int>
    {
        private int size;

        public BigInteger ElementCount { get { return size; } }

        public FiniteFieldElement<int> AddIdentity { get { return new FiniteFieldElement<int>(this, 0); } }

        public FiniteFieldElement<int> MultiplyIdentity { get { return new FiniteFieldElement<int>(this, 1); } }

        public AlgebraFiniteFieldInt32Size8()
		{
            size = 8;
        }

		public FiniteFieldElement<int> [] GetElements()
		{
			if (1000000 < size)
			{
				throw new Exception("Groups is to big, you do not want this");
			}

			FiniteFieldElement<int> [] elements = new FiniteFieldElement<int> [(int)size];
			for (int index = 0; index < elements.Length; index++)
			{
                elements[index] = new FiniteFieldElement<int>(this, index);
			}
			return elements;
		}

        public bool Contains(FiniteFieldElement<int> input)
		{
            return input.Algebra == this;
		}


		public FiniteFieldElement<int> Add(
            FiniteFieldElement<int> element_0,
            FiniteFieldElement<int> element_1)
		{
            return new FiniteFieldElement<int>(this, (element_0.Value ^ element_1.Value));
		}

        public FiniteFieldElement<int> Subtract(
            FiniteFieldElement<int> element_0,
            FiniteFieldElement<int> element_1)
		{
            return new FiniteFieldElement<int>(this, (element_0.Value ^ element_1.Value));
		}

		public FiniteFieldElement<int> Multiply(
			FiniteFieldElement<int> element_0,
			FiniteFieldElement<int> element_1)
		{
            return new FiniteFieldElement<int>(this, gf_mult(element_0.Value, element_1.Value));
		}

		public FiniteFieldElement<int> Divide(
            FiniteFieldElement<int> element_0,
            FiniteFieldElement<int> element_1)
		{
            if (Math.Min(element_0.Value, element_1.Value) == 0)
            {
                return new FiniteFieldElement<int>(this, 0);
            }
            FiniteFieldElement<int> inverse = MutiplicativeInverse(element_1); //TODO really expensive for big fields
            return new FiniteFieldElement<int>(this, (gf_mult(element_0.Value, element_1.Value)));
		}

        private FiniteFieldElement<int> MutiplicativeInverse(FiniteFieldElement<int> element) 
        {
            FiniteFieldElement<int>[] elements = GetElements();
            FiniteFieldElement<int> indentity = MultiplyIdentity;
            for (int element_index = 0; element_index < elements.Length; element_index++)
            {
                if (element * elements[element_index] == indentity)
                {
                    return elements[element_index];
                }
            }
            throw new Exception("Mutiplicative Inverse should always exist also for " + element);
        }

        private int gf_mult(int v1, int v2)
        {
            int poly = 11; //(8 + 3)
            int prod = v1 * v2;

            int prod_significant_bit = prod.MostSignificantValueBitPosition();
            int poly_significant_bit = poly.MostSignificantValueBitPosition();
            while (prod_significant_bit >= poly_significant_bit)
            {    
                int increased_poly = poly << (prod_significant_bit - poly_significant_bit);
                prod = prod ^ increased_poly;
                prod_significant_bit = prod.MostSignificantValueBitPosition();
            }
            return prod;
        }

   

        public FiniteFieldElement<int> ToDomain(BigInteger value)
        {
            throw new NotImplementedException();
        }

        public BigInteger ToBigInteger(FiniteFieldElement<int> value)
        {
            throw new NotImplementedException();
        }

        public FiniteFieldElement<int> ToDomain(int value)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(FiniteFieldElement<int> value)
        {
            throw new NotImplementedException();
        }
    }
}