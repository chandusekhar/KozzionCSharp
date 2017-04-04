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
    public class TemplateModelNearestNeighborKDTreeDefault : ATemplateModelDiscrete<double, int>
    {
        public TemplateModelNearestNeighborKDTreeDefault()
        {
        }

        public override IModelDiscrete<double, int> GenerateModelDiscrete(IDataSet<double, int> training_set)
        {
            return GenerateModelDiscreteIncremental(training_set);
        }

        public IModelDiscreteIterative<double, int> GenerateModelDiscreteIncremental(IDataSet<double, int> training_set)
        {
            ModelNearestNeighborKDTreeDefault model = new ModelNearestNeighborKDTreeDefault(training_set.DataContext);
            IList<Tuple<double[], int>> training_instances = new List<Tuple<double[], int>>();
            for (int instance_index = 0; instance_index < training_set.InstanceCount; instance_index++)
            {
                training_instances.Add(new Tuple<double[], int>(training_set.GetInstanceFeatureData(instance_index), training_set.GetInstanceLabelData(instance_index)[0]));
            }
            model.Add(training_instances);
            return model;
        }

     
    }
}
