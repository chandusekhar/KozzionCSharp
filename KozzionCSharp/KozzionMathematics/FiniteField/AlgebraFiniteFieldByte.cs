using System;
using System.Numerics;
using KozzionMathematics.Tools;
namespace KozzionMathematics.FiniteField
{
	public class AlgebraFiniteFieldByte : IAlgebraFieldFinite<byte>
    {

        private BigInteger size;

        public BigInteger ElementCount { get { return size; } }

        public FiniteFieldElement<byte> AddIdentity { get { return new FiniteFieldElement<byte>(this, 0); } }

        public FiniteFieldElement<byte> MultiplyIdentity { get { return new FiniteFieldElement<byte>(this, 1); } }

        public AlgebraFiniteFieldByte(
            byte prime,
            int power)
        {
            if (!ToolsMathInteger.IsPrime(prime))
            {
                throw new Exception("Number " + prime + " is not prime");
            }

            size = ToolsMathInteger.Pow(prime, power);
        }

        public bool Contains(FiniteFieldElement<byte> value)
        {
            return value.Algebra == this;
        }

        public FiniteFieldElement<byte>[] GetElements()
        {
            FiniteFieldElement<byte>[] elements = new FiniteFieldElement<byte>[(int)size];

            for (int index = 0; index < elements.Length; index++)
            {
                elements[index] = new FiniteFieldElement<byte>(this, (byte)index);
            }
            return elements;
        }

        public FiniteFieldElement<byte> Add(FiniteFieldElement<byte> value_0, FiniteFieldElement<byte> value_1)
        {
            return new FiniteFieldElement<byte>(this, (byte)(value_0.Value ^ value_1.Value));
        }

        public FiniteFieldElement<byte> Subtract(
			   FiniteFieldElement<byte> value_0,
			   FiniteFieldElement<byte> value_1)
        {
            return new FiniteFieldElement<byte>(this, (byte)(value_0.Value ^ value_1.Value));
        }
		
        public FiniteFieldElement<byte> Multiply(
			FiniteFieldElement<byte> value_0,
			FiniteFieldElement<byte> value_1)
        {
            if ((value_0.Value == 0) || (value_1.Value == 0))
            {
                return new FiniteFieldElement<byte>(this, 0);
            }
            byte result= (byte)(((value_0.Value + value_1.Value - 2) % (size - 1)) + 1);
            return new FiniteFieldElement<byte>(this, result);
        }

        public FiniteFieldElement<byte> Divide(FiniteFieldElement<byte> value_0, FiniteFieldElement<byte> value_1)
        {
            if ((value_0.Value == 0) || (value_1.Value == 0))
            {
                return new FiniteFieldElement<byte>(this, 0);
            }
            byte result= (byte)((((value_0.Value - value_1.Value) + size) - 1) % (size - 1) + 1);

            return new FiniteFieldElement<byte>(this, result);
        }

        public FiniteFieldElement<byte> ToDomain(BigInteger value)
        {
            throw new NotImplementedException();
        }

        public BigInteger ToBigInteger(FiniteFieldElement<byte> value)
        {
            throw new NotImplementedException();
        }

        public FiniteFieldElement<byte> ToDomain(int value)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(FiniteFieldElement<byte> value)
        {
            throw new NotImplementedException();
        }
    }
}