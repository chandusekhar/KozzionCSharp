using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.TensorFlow
{
    class ModelTensorFlow
    {

        public void Test()
        {
            using (var graph = new TFGraph())
            {
                graph.Import(File.ReadAllBytes("MySavedModel"));
                var session = new TFSession(graph);
                var runner = session.GetRunner();
                runner.AddInput(graph["input"][0], tensor);
                runner.Fetch(graph["output"][0]);

                var output = runner.Run();

                // Fetch the results from output:
                TFTensor result = output[0];
            }

            using (var session = new TFSession())
            {
                var graph = session.Graph;

                var a = graph.Const(2);
                var b = graph.Const(3);
                Console.WriteLine("a=2 b=3");

                // Add two constants
                var addingResults = session.GetRunner().Run(graph.Add(a, b));
                var addingResultValue = addingResults[0].GetValue();
                Console.WriteLine("a+b={0}", addingResultValue);

                // Multiply two constants
                var multiplyResults = session.GetRunner().Run(graph.Mul(a, b));
                var multiplyResultValue = multiplyResults.GetValue();
                Console.WriteLine("a*b={0}", multiplyResultValue);
            }
        }
    }
}
