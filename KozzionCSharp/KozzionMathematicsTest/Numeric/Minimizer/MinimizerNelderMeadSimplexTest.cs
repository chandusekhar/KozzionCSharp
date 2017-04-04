using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;
using KozzionMathematics.Numeric.Minimizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Numeric.Minimizer
{

    [TestClass]
    public class MinimizerNelderMeadSimplexTest
    {
        [TestMethod]
        public void MinimizeFunctionBeale0()
        {
            MinimizerNelderMead minimizer = new MinimizerNelderMead(true);
            IFunction<double[], double> minimization_function = new FunctionBeale();
            IMinimizerHaltingCriterion<Simplex> halting_criterion = new HaltingCriterionIterationsSimplex(100);
            double[] parameters_initial_final = new double[] { 4, 4 };
            double[] initial_vextex_size = new double[] { 1, 1 };

            MinimizerResult result = minimizer.Minimize(
                    minimization_function,
                    halting_criterion,
                    parameters_initial_final,
                    initial_vextex_size);
   
            // 3.0, 0.5 == 0
            Assert.AreEqual(3.0, result.Simplex.SmallestVertex[0], 0.001);
            Assert.AreEqual(0.5, result.Simplex.SmallestVertex[1], 0.001);            
        }


        [TestMethod]
        public void FunctionRosenbrock0()
        {
            MinimizerNelderMead minimizer = new MinimizerNelderMead();
            IFunction<double[], double> minimization_function = new FunctionRosenbrock();
            IMinimizerHaltingCriterion<Simplex> halting_criterion = new HaltingCriterionIterationsSimplex(1000);
            double[] parameters_initial = new double[] { -2, -2 };
            double[] initial_vextex_size = new double[] { 1, 1 };

            MinimizerResult result = minimizer.Minimize(
                    minimization_function,
                    halting_criterion,
                    parameters_initial,
                    initial_vextex_size);

            // 1.0, 1.0 == 0
            Assert.AreEqual(1.0, result.Simplex.SmallestVertex[0], 0.001);
            Assert.AreEqual(1.0, result.Simplex.SmallestVertex[1], 0.001);
        }

        [TestMethod]
        public void FunctionFunctionHimmelblau0()
        {
            MinimizerNelderMead minimizer = new MinimizerNelderMead(true);
            IFunction<double[], double> minimization_function = new FunctionHimmelblau();
            IMinimizerHaltingCriterion<Simplex> halting_criterion = new HaltingCriterionIterationsSimplex(1000);
            double[] parameters_initial = new double[] { -2, -2 };
            double[] initial_vextex_size = new double[] { 1, 1 };

            MinimizerResult result = minimizer.Minimize(
                    minimization_function,
                     new FunctionContstant<double[], bool>(true),
                    halting_criterion,
                    parameters_initial,
                    initial_vextex_size);

            //Any of these are zeros:
            //  3.0000,  2.0000 == 0
            // -2.8051,  3.1313 == 0
            // -3.7793, -3.2832 == 0
            //  3.5844, -1.8481 == 0

            Assert.AreEqual(-3.7793, result.Simplex.SmallestVertex[0], 0.001);
            Assert.AreEqual(-3.2832, result.Simplex.SmallestVertex[1], 0.001);
        }

        
    }
}
