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
    public class FunctionInterpolationCubicTest
    {
        [TestMethod]
        public void Interpolat_0()
        {
            double[] domain = new double []{ 0, 1, 2, 3};
            double[] range = new double[] { 0, 1, 1, 0 };
            FunctionInterpolationCubic function = new FunctionInterpolationCubic(domain, range);
            double[] domain_querry = new double[] { 0, 0.5, 1, 1.5, 2, 2.5, 3 };
            double[] target_matlab = new double[] { 0, 0.6250, 1.0000, 1.1250, 1.0000, 0.6250, 0 };

            double[] target = function.Compute(domain_querry);
            Assert.AreEqual(target_matlab[0], target[0], 0.00001);
            Assert.AreEqual(target_matlab[1], target[1], 0.00001);
            Assert.AreEqual(target_matlab[2], target[2], 0.00001);
            Assert.AreEqual(target_matlab[3], target[3], 0.00001);
            Assert.AreEqual(target_matlab[4], target[4], 0.00001);
            Assert.AreEqual(target_matlab[5], target[5], 0.00001);
            Assert.AreEqual(target_matlab[6], target[6], 0.00001);
        }
        
    }
}
