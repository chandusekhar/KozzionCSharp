using System;
using System.Numerics;
using KozzionMathematics.Tools;
using KozzionMathematics.Algebra;

namespace KozzionMathematics.FiniteField
{
	public class AlgebraFiniteFieldGenericPrime<IntegerType> : IAlgebraFieldFinite<IntegerType>
    {
        public IAlgebraInteger<IntegerType> Algebra { get; private set; }

        public IntegerType Prime { get; private set; }      

        public BigInteger ElementCount { get; private set; }

        public FiniteFieldElement<IntegerType> AddIdentity { get; private set; }

        public FiniteFieldElement<IntegerType> MultiplyIdentity { get; private set; }

        public AlgebraFiniteFieldGenericPrime(IAlgebraInteger<IntegerType> algebra,  IntegerType prime)
		{
            if (!ToolsMathBigIntegerPrime.IsPrime(algebra.ToBigInteger(prime)))
			{
				throw new Exception("Number " + prime.ToString() + " is not prime");
			}
            Prime = prime;
            Algebra = algebra;
            ElementCount = Algebra.ToBigInteger(prime);
            AddIdentity = new FiniteFieldElement<IntegerType>(this, Algebra.AddIdentity);
            MultiplyIdentity = new FiniteFieldElement<IntegerType>(this, Algebra.MultiplyIdentity);

        }


		public FiniteFieldElement<IntegerType> [] GetElements()
		{
			if (1000000 < ElementCount)
			{
				throw new Exception("Groups is to big, you do not want this");
			}

			FiniteFieldElement<IntegerType> [] elements = new FiniteFieldElement<IntegerType> [(int)ElementCount];
			for (int index = 0; index < elements.Length; index++)
			{
                elements[index] = new FiniteFieldElement<IntegerType>(this, Algebra.ToDomain(index));
			}
			return elements;
		}

        public bool Contains(FiniteFieldElement<IntegerType> input)
		{
            return input.Algebra == this;
		}

		public FiniteFieldElement<IntegerType> Add(
            FiniteFieldElement<IntegerType> element_0,
            FiniteFieldElement<IntegerType> element_1)
		{
            return new FiniteFieldElement<IntegerType>(this, Algebra.Modulo(Algebra.Add(element_0.Value, element_1.Value), Prime));
		}

        public FiniteFieldElement<IntegerType> Subtract(
            FiniteFieldElement<IntegerType> element_0,
            FiniteFieldElement<IntegerType> element_1)
		{
            return new FiniteFieldElement<IntegerType>(this, Algebra.Modulo(Algebra.Subtract(element_0.Value, element_1.Value), Prime));
        }

		public FiniteFieldElement<IntegerType> Multiply(
			FiniteFieldElement<IntegerType> element_0,
			FiniteFieldElement<IntegerType> element_1)
		{
            return new FiniteFieldElement<IntegerType>(this, Algebra.Modulo(Algebra.Multiply(element_0.Value, element_1.Value), Prime));
        }

		public FiniteFieldElement<IntegerType> Divide(
            FiniteFieldElement<IntegerType> element_0,
            FiniteFieldElement<IntegerType> element_1)
		{
            FiniteFieldElement<IntegerType> inverse = MutiplicativeInverse(element_1);
            return new FiniteFieldElement<IntegerType>(this, Algebra.Modulo(Algebra.Multiply(element_0.Value, inverse.Value), Prime));
        }

        private FiniteFieldElement<IntegerType> MutiplicativeInverse(FiniteFieldElement<IntegerType> element_0) 
        {
            Tuple<IntegerType, IntegerType, IntegerType> xyd = ToolsMath.ExtendedEuclideanAlgorithm(Algebra, element_0.Value, Prime);
            return new FiniteFieldElement<IntegerType>(this, Algebra.Modulo(xyd.Item1, Prime));
        }



        public FiniteFieldElement<IntegerType> ToDomain(BigInteger value)
        {
            FiniteFieldElement<IntegerType> element = new FiniteFieldElement<IntegerType>(this, Algebra.ToDomain(value));
            if (!Contains(element))
            {
                throw new Exception("Cannot convert to domain");
            }
            return element;
        }

        public FiniteFieldElement<IntegerType> ToDomain(int value)
        {
            FiniteFieldElement < IntegerType >  element = new FiniteFieldElement<IntegerType>(this, Algebra.ToDomain(value));
            if (!Contains(element))
            {
                throw new Exception("Cannot convert to domain");
            }
            return element;
        }


        public BigInteger ToBigInteger(FiniteFieldElement<IntegerType> value)
        {
            return Algebra.ToBigInteger(value.Value);
        }  

        public int ToInt32(FiniteFieldElement<IntegerType> value)
        {
            return Algebra.ToInt32(value.Value);
        }
    }
}