using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KozzionCore.Tools;
using KozzionMathematics.Algebra;

namespace KozzionMathematics.FiniteField
{
    [TestClass]
    public class FiniteFieldToolsTest
    {
        [TestMethod]
        public void ATestAlgebraFiniteFieldGenericPrime2()
        {
            CheckField(new AlgebraFiniteFieldGenericPrime<int>(new AlgebraSymbolInt32(), 2));
        }
        [TestMethod]
        public void ATestAlgebraFiniteFieldGenericPrime3()
        {
            CheckField(new AlgebraFiniteFieldGenericPrime<int>(new AlgebraSymbolInt32(), 3));
        }
        [TestMethod]
        public void ATestAlgebraFiniteFieldGenericPrime5()
        {
            CheckField(new AlgebraFiniteFieldGenericPrime<int>(new AlgebraSymbolInt32(), 5));
        }

        [TestMethod]
        public void ATestAlgebraFiniteFieldGenericPrime23()
        {
            CheckField(new AlgebraFiniteFieldGenericPrime<int>(new AlgebraSymbolInt32(), 23));
        }

        [TestMethod]
        public void ATestAlgebraFiniteFieldGenericPrime997()
        {
            CheckField(new AlgebraFiniteFieldGenericPrime<int>(new AlgebraSymbolInt32(), 157));
        }

         
 

        [TestMethod]
        public void AlgebraFiniteFieldInt32Size8()
        {
            CheckField(new AlgebraFiniteFieldInt32Size8());
        }

        [TestMethod]
        public void TestFieldInteger256()
        {
            CheckField(new AlgebraFiniteFieldUInt32Size256());
        }

        [TestMethod]
        public void TestBigInteger2()
        {
            CheckField(new AlgebraFiniteFieldBigInteger (2, 1));
        }

        [TestMethod]
        public void TestBigInteger3()
        {
            CheckField(new AlgebraFiniteFieldBigInteger (3, 1));
        }

        [TestMethod]
        public void TestBigInteger4()
        {
            CheckField(new AlgebraFiniteFieldBigInteger (2, 2));
        }

        [TestMethod]
        public void TestBigInteger8()
        {
            CheckField(new AlgebraFiniteFieldBigInteger (2, 3));
        }

        [TestMethod]
        public void TestFieldBigInteger256()
        {
            CheckField(new AlgebraFiniteFieldBigInteger (2, 8));
        }


        [TestMethod]
        public void TestByte2()
        {
             CheckField(new AlgebraFiniteFieldByte(2, 1));
        }

        [TestMethod]
        public void TestByte3()
        {
            CheckField(new AlgebraFiniteFieldByte(3, 1));
        }

        [TestMethod]
        public void TestByte4()
        {
            CheckField(new AlgebraFiniteFieldByte(2, 2)); 
        }

        [TestMethod]
        public void TestByte8()
        {
            CheckField(new AlgebraFiniteFieldByte(2, 3));
        }

        [TestMethod]
        public void TestByte256()
        {
            CheckField(new AlgebraFiniteFieldByte(2, 8));
        }

        public static void CheckField<ElementType>(
            IAlgebraFieldFinite<ElementType> field)
        {
            PrintField(field);
            CheckFieldIdentities(field);
            CheckFieldInverses(field);
            CheckFieldDistributivity(field);
        }


        public static void PrintField<ElementType>(
           IAlgebraFieldFinite<ElementType> field)
        {
            Console.WriteLine("add table");
            Console.WriteLine(ToolsCollection.ToString(ToolsMathFiniteField.GetAdditionTable(field)));
            Console.WriteLine("subtract table");
            Console.WriteLine(ToolsCollection.ToString(ToolsMathFiniteField.GetSubtractionTable(field)));
            Console.WriteLine("multiply table");
            Console.WriteLine(ToolsCollection.ToString(ToolsMathFiniteField.GetMultiplicationTable(field)));
            Console.WriteLine("divide table");
            Console.WriteLine(ToolsCollection.ToString(ToolsMathFiniteField.GetDivisionTable(field)));
        }

        public static void CheckFieldIdentities<ElementType>(
           IAlgebraFieldFinite<ElementType> field)
        {
            FiniteFieldElement<ElementType>[] elements = field.GetElements();
            FiniteFieldElement<ElementType> add_identity = field.AddIdentity;
            FiniteFieldElement<ElementType> multiply_identity = field.MultiplyIdentity;
            Assert.AreNotEqual(add_identity, multiply_identity, "add_identiy should be different from multiply_identity");
            // check identity elements
            for (int index_0 = 0; index_0 < elements.Length; index_0++)
            {
                Assert.AreEqual(elements[index_0], elements[index_0] + add_identity, elements[index_0] + " + " + add_identity + " should yield " + elements[index_0] + " but yielded " + (elements[index_0] + add_identity));
                Assert.AreEqual(elements[index_0], add_identity + elements[index_0], add_identity + " + " + elements[index_0] + " should yield " + elements[index_0] + " but yielded " + (add_identity + elements[index_0]));

                Assert.AreEqual(elements[index_0], elements[index_0] * multiply_identity, elements[index_0] + " * " + multiply_identity + " should yield " + elements[index_0] + " but yielded " + (elements[index_0] * multiply_identity));
                Assert.AreEqual(elements[index_0], multiply_identity * elements[index_0], multiply_identity + " * " + elements[index_0] + " should yield " + elements[index_0] + " but yielded " + (multiply_identity * elements[index_0]));
           }
        }

        public static void CheckFieldDistributivity<ElementType>(
            IAlgebraFieldFinite<ElementType> field)
        {
            FiniteFieldElement<ElementType>[] elements = field.GetElements();
            // check distributivity  a X (b + c) = (a X b) + (a X c).
            for (int index_0 = 0; index_0 < elements.Length; index_0++)
            {
                for (int index_1 = 0; index_1 < elements.Length; index_1++)
                {
                    for (int index_2 = 0; index_2 < elements.Length; index_2++)
                    {
                        FiniteFieldElement<ElementType> result_0 = (elements[index_0] + elements[index_1]) * elements[index_2];
                        FiniteFieldElement<ElementType> result_1 = (elements[index_0] * elements[index_2]) + (elements[index_1] * elements[index_2]);
                        Assert.AreEqual(result_0, result_1);
                    }
                }
            }
        }
        public static void CheckFieldInverses<ElementType>(
            IAlgebraFieldFinite<ElementType> field)
        {

            FiniteFieldElement<ElementType>[] elements = field.GetElements();

            // check + inverse
            for (int index_0 = 1; index_0 < elements.Length; index_0++)
            {
                for (int index_1 = 1; index_1 < elements.Length; index_1++)
                {
                    Assert.AreEqual(elements[index_0], (elements[index_0] + elements[index_1]) - elements[index_1], elements[index_0] + " +- " + elements[index_1] + " should yield " + elements[index_0] + " but yielded " + ((elements[index_0] + elements[index_1]) - elements[index_1]));
                    Assert.AreEqual(elements[index_0], (elements[index_0] - elements[index_1]) + elements[index_1], elements[index_0] + " -+ " + elements[index_1] + " should yield " + elements[index_0] + " but yielded " + ((elements[index_0] - elements[index_1]) + elements[index_1]));
                }
            }

            // check * inverse
            for (int index_0 = 2; index_0 < elements.Length; index_0++)
            {
                for (int index_1 = 2; index_1 < elements.Length; index_1++)
                {
                    Assert.AreEqual( elements[index_0], (elements[index_0] * elements[index_1]) / elements[index_1]);
                    Assert.AreEqual( elements[index_0], (elements[index_0] / elements[index_1]) * elements[index_1]);
                }
            }


        }
    }
}