using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function.Implementation.Distance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.NearestNeighbor
{
    public class TemplateModelNearestNeighborNominal : ATemplateModelDiscrete<int, int>
    {
        public TemplateModelNearestNeighborNominal()
        {
        }

        public override IModelDiscrete<int, int> GenerateModelDiscrete(IDataSet<int, int> training_set)
        {
            return GenerateModelDiscreteIncremental(training_set);
        }

        public IModelDiscreteIterative<int, int> GenerateModelDiscreteIncremental(IDataSet<int, int> training_set)
        {
            //TODO check data context
            ModelNearestNeighborList<int, int, int> model = new ModelNearestNeighborList<int, int, int>(training_set.DataContext, new FunctionDistanceHamming());
            IList<Tuple<int[], int>> training_instances = new List<Tuple<int[], int>>();
            for (int instance_index = 0; instance_index < training_set.InstanceCount; instance_index++)
            {
                training_instances.Add(new Tuple<int[], int>(training_set.GetInstanceFeatureData(instance_index), training_set.GetInstanceLabelData(instance_index)[0]));
            }
            model.Add(training_instances);
            return model;
        }

     
    }
}
