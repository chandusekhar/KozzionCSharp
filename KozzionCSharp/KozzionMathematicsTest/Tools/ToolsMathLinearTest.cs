using KozzionMathematics.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kozzionmathematicstest.Tools
{
    [TestClass]
    public class ToolsMathLinearTest
    {

        //A =
        //0.6837    0.1175
        //0.1321    0.6407
        //0.7227    0.3288
        //0.1104    0.6538
        [TestMethod]
        public void TestCovarianceMatrix()
        {
            //Set up
            var array = new double[,] { 
                { 0.6837,    0.1175 },
                { 0.1321,    0.6407 },
                { 0.7227,    0.3288},
                { 0.1104,    0.6538}};
            Matrix<double> A = new DenseMatrix(4,2, array.ToArray1D01());

            //Act
            var result = ToolsMathLinear.CovarianceMatrix(A);            
 
            //Test
            Assert.AreEqual(result[0, 0], 0.1132, 0.0001);
            Assert.AreEqual(result[0, 1], -0.0809, 0.0001);
            Assert.AreEqual(result[1, 0], -0.0809, 0.0001);
            Assert.AreEqual(result[1, 1], 0.0674, 0.0001);
        }
    }
}
