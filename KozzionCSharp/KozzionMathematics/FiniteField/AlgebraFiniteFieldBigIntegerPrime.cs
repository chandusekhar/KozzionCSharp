using System;
using System.Numerics;
using KozzionMathematics.Tools;

namespace KozzionMathematics.FiniteField
{
	public class AlgebraFiniteFieldBigInteger : IAlgebraFieldFinite<BigInteger>
	{
		private BigInteger size;

        public BigInteger ElementCount { get { return size; } }
        	
        public FiniteFieldElement<BigInteger> AddIdentity { get { return new FiniteFieldElement<BigInteger>(this, 0); } }

        public FiniteFieldElement<BigInteger> MultiplyIdentity { get { return new FiniteFieldElement<BigInteger>(this, 1); } }

        public AlgebraFiniteFieldBigInteger (
			BigInteger prime,
			int power)
		{
			if (!ToolsMathBigIntegerPrime.IsPrime(prime))
			{
				throw new Exception("Number " + prime.ToString() + " is not prime");
			}
			size = prime.Pow(power);
		}



		public FiniteFieldElement<BigInteger> [] GetElements()
		{
			if (1000000 < size)
			{
				throw new Exception("Groups is to big, you do not want this");
			}

			FiniteFieldElement<BigInteger> [] elements = new FiniteFieldElement<BigInteger> [(int)size];
			for (int index = 0; index < elements.Length; index++)
			{
                elements[index] = new FiniteFieldElement<BigInteger>(this, index);
			}
			return elements;
		}

		public bool Contains( FiniteFieldElement<BigInteger> input)
		{
            return input.Algebra == this;
		}



		public FiniteFieldElement<BigInteger> Add(
            FiniteFieldElement<BigInteger> element_0,
            FiniteFieldElement<BigInteger> element_1)
		{
            return new FiniteFieldElement<BigInteger>(this, element_0.Value ^ element_1.Value);
		}

        public FiniteFieldElement<BigInteger> Subtract(
            FiniteFieldElement<BigInteger> element_0,
            FiniteFieldElement<BigInteger> element_1)
		{
            return new FiniteFieldElement<BigInteger>(this, element_0.Value ^ element_1.Value);
		}

		public FiniteFieldElement<BigInteger> Multiply(
			FiniteFieldElement<BigInteger> element_0,
			FiniteFieldElement<BigInteger> element_1)
		{
            return new FiniteFieldElement<BigInteger>(this, ((element_0.Value + element_1.Value - 2) % (size - 1)) + 1);
		}

		public FiniteFieldElement<BigInteger> Divide(
			FiniteFieldElement<BigInteger> value_0,
			FiniteFieldElement<BigInteger> value_1)
		{
            return new FiniteFieldElement<BigInteger>(this, (((value_0.Value - value_1.Value) + size) - 1) % (size - 1) + 1);
		}


        public FiniteFieldElement<BigInteger> ToDomain(BigInteger value)
        {
            throw new NotImplementedException();
        }

        public BigInteger ToBigInteger(FiniteFieldElement<BigInteger> value)
        {
            throw new NotImplementedException();
        }

        public FiniteFieldElement<BigInteger> ToDomain(int value)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(FiniteFieldElement<BigInteger> value)
        {
            throw new NotImplementedException();
        }
    }
}
