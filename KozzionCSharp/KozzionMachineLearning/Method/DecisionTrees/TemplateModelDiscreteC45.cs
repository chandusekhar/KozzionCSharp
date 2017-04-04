using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.DecisionTrees
{
    public class TemplateModelDiscreteC45 : ATemplateModelDiscrete<double, int>
    {
        public override IModelDiscrete<double, int> GenerateModelDiscrete(IDataSet<double, int> training_set)
        {        
            double[][] instance_features_array = training_set.FeatureData;
            int[] outputs = ToolsCollection.ConvertToArray2D(training_set.LabelData).Select1DIndex1(0);

            // Specify the input variables
            List<DecisionVariable> variables = new List<DecisionVariable>();
            foreach (VariableDescriptor feature_descriptor in training_set.DataContext.FeatureDescriptors)
            {
                variables.Add(new DecisionVariable(feature_descriptor.Name, DecisionVariableKind.Continuous));
            }      

            // Create the discrete Decision tree
            DecisionTree tree = new DecisionTree(variables, training_set.DataContext.GetLabelDescriptor(0).ValueCount);

            // Create the C4.5 learning algorithm
            C45Learning c45 = new C45Learning(tree); //TODO are there others?

            // Learn the decision tree using C4.5
            double error = c45.Run(instance_features_array, outputs);

            return new ModelDiscreteC45<int>(training_set.DataContext, tree);
        }
    }
}
