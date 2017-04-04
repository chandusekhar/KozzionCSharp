using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation.Interpolation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Function.Implementation.Interpolation
{
    [TestClass]
    public class TestFunctionInterpolationLinear
    {
        [TestMethod]
        public void TestFunctionInterpolationLinearSimple()
        {
            double[] domain = new double []{ 1, 2, 4, 7};
            double[] range = new double[] { 1, 4, 4, 3 };
            IFunction<double, double> function = new FunctionInterpolationLinear(domain, range);
            Assert.AreEqual(1.0, function.Compute(0.0), 0.00001);
            Assert.AreEqual(1.0, function.Compute(0.5), 0.00001);
            Assert.AreEqual(1.0, function.Compute(1.0), 0.00001);
            Assert.AreEqual(1.3, function.Compute(1.1), 0.00001);
            Assert.AreEqual(2.5, function.Compute(1.5), 0.00001);
            Assert.AreEqual(4.0, function.Compute(2.5), 0.00001);
            Assert.AreEqual(3.0, function.Compute(7.5), 0.00001);
            Assert.AreEqual(3.0, function.Compute(9.5), 0.00001);
        }
        
    }
}
