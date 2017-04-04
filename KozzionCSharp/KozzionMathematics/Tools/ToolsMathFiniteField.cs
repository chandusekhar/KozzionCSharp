using System;
namespace KozzionMathematics.FiniteField
{
	public class ToolsMathFiniteField
	{
		public static  string [,] GetAdditionTable<ElementType> (
			IAlgebraFieldFinite<ElementType> field)
		{
			FiniteFieldElement<ElementType> [] elements = field.GetElements();
            string[,] table = new string[elements.Length, elements.Length];

			for (int index_0 = 0; index_0 < elements.Length; index_0++)
			{
				for (int index_1 = 0; index_1 < elements.Length; index_1++)
				{
					table[index_0, index_1] = field.Add(elements[index_0], elements[index_1]).ToString();

				}
			}
			return table;
		}

		public static  string [,] GetSubtractionTable<ElementType> (
            IAlgebraFieldFinite<ElementType> field)
		{
			FiniteFieldElement<ElementType> [] elements = field.GetElements();
            string[,] table = new string[elements.Length, elements.Length];
			for (int index_0 = 0; index_0 < elements.Length; index_0++)
			{
				for (int index_1 = 0; index_1 < elements.Length; index_1++)
				{
					table[index_0, index_1] = field.Subtract(elements[index_0], elements[index_1]).ToString();
				}
			}
			return table;
		}

		public static  string [,] GetMultiplicationTable<ElementType>(
            IAlgebraFieldFinite<ElementType> field)
		{
			FiniteFieldElement<ElementType> [] elements = field.GetElements();
            string[,] table = new string[elements.Length, elements.Length];
			for (int index_0 = 0; index_0 < elements.Length; index_0++)
			{
				for (int index_1 = 0; index_1 < elements.Length; index_1++)
				{
					table[index_0, index_1] = field.Multiply(elements[index_0], elements[index_1]).ToString();

				}
			}
			return table;
		}

		public static string [,] GetDivisionTable<ElementType>(
            IAlgebraFieldFinite<ElementType> field)
		{
			FiniteFieldElement<ElementType> [] elements = field.GetElements();
            string[,] table = new string[elements.Length, elements.Length];
			for (int index_0 = 0; index_0 < elements.Length; index_0++)
			{
				for (int index_1 = 0; index_1 < elements.Length; index_1++)
				{
					table[index_0, index_1] = field.Divide(elements[index_0], elements[index_1]).ToString();

				}
			}
			return table;
		}



	}
}