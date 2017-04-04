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
    public class TemplateModelNearestNeighborIntervalMean : ATemplateModelLabel<double, double>
    {
        public TemplateModelNearestNeighborIntervalMean()
        {
        }

        public override IModelLabel<double, double> GenerateModel(IDataSet<double, double> training_set)
        {
            return GenerateModelLabelIterative(training_set);
        }

        public IModelLabelIterative<double, double> GenerateModelLabelIterative(IDataSet<double, double> training_set)
        {

            //ModelNearestNeighborKDTreeDefault model = new ModelNearestNeighborKDTreeDefault(training_set.DataContext);
            ModelNearestNeighborKDTree<double, double, double> model = new ModelNearestNeighborKDTree<double, double, double>("ModelKNNIneterval", training_set.DataContext, new AlgebraRealFloat64(), new FunctionDistanceEuclidean());
            IList <Tuple<double[], double>> training_instances = new List<Tuple<double[], double>>();
            for (int instance_index = 0; instance_index < training_set.InstanceCount; instance_index++)
            {
                training_instances.Add(new Tuple<double[], double>(training_set.GetInstanceFeatureData(instance_index), training_set.GetInstanceLabelData(instance_index)[0]));
            }
            model.Add(training_instances);
            return model;
        }

     
    }
}
