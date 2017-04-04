using KozzionCore.tools;
using KozzionMachineLearning.Methods.multi_layer_perceptron;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearningTest.methods
{
    [TestClass]
    public class MultiLayerPerceptronTest
    {
        [TestMethod]
        public void LearnXORTest()
        {
            float [][] inputs = new float [4][];
            float[][] targets = new float [4][];
            inputs[0] = new float[] { 0, 0 };
            inputs[1] = new float[] { 0, 1 };
            inputs[2] = new float[] { 1, 0 };
            inputs[3] = new float[] { 1, 1 };
            targets[0] = new float[] { 0 };
            targets[1] = new float[] { 1 };
            targets[2] = new float[] { 1 };
            targets[3] = new float[] { 0 };
            MultiLayerPerceptron mlp = new MultiLayerPerceptron(new int []{2, 2, 1});
            mlp.set_learning_rate(0.2f);
            mlp.set_eligibility(0.1f);
            for (int i = 0; i < 10000; i++)
            {
                mlp.train_batch(inputs, targets); 
            }
            ToolsArray.print(mlp.estimate(inputs[0]));
            ToolsArray.print(mlp.estimate(inputs[1]));
            ToolsArray.print(mlp.estimate(inputs[2]));
            ToolsArray.print(mlp.estimate(inputs[3]));
        }
    }
}
