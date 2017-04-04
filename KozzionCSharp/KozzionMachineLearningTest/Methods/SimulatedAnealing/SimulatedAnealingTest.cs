using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.HelperFunctions.ErrorFunctions;
using KozzionMachineLearning.HelperFunctions.Mutators;
using KozzionMachineLearning.HelperFunctions.TweenerFunctions;

using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation.Distance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KozzionCore.Tools;
using KozzionMachineLearning.Method.SimulatedAnealing;

namespace KozzionMachineLearningTest.methods.simulated_anealing
{
    [TestClass]
    public class SimulatedAnealingTest
    {
        [TestMethod]
        public void TestSimulatedAnealing()
        {
            int [] initial = new int[] { 0, 0, 0, 0, 0, 0, 0};
            int [] target = new int [] { 1, 2, 3, 4, 3, 5, 4};
            IMutatorPoint<int> mutator = new MutatorPointOneInt();
            IFunctionDistance<int[], float> distance = new FunctionDistanceEuclidean();
            IFunction<int[], float> evaluation_function = new FunctionDistanceToTarget<int[], float>(distance, target);
            int iteration_count = 1000;
            IFunction<int, float> accept_chance = new FunctionLinearDecay(1, 500);

            SimulatedAnealing<int, float> simulate_anealing = new SimulatedAnealing<int, float>(
                mutator, accept_chance, iteration_count);

            int[] result = simulate_anealing.Minimize(initial, evaluation_function);
            ToolsCollection.print(result);
        }
    }
}
