using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Method.LVQ
{
    public class ModelLVQDefault : AModelDiscrete<double, int>
    {
        public int FeatureCount  {get; private set;}

        IFunctionDistance<double[], double> distance_function;
        IList<double[]> prototype_features;
        IList<int> prototype_labels;

        public ModelLVQDefault(IDataContext data_context, IList<double[]> prototype_features, IList<int> prototype_labels, IFunctionDistance<double[], double> distance_function)
            : base(data_context, "ModelLVQDefault")
        {
            this.prototype_features = prototype_features;
            this.prototype_labels = prototype_labels;
            this.distance_function = distance_function;
            this.FeatureCount = prototype_features[0].Length;
        }


       


        public override int GetLabel(double[] instance_features)
        {
            int best_prototype_index = 0;
            double bestPrototypeDistance = this.distance_function.Compute(instance_features, prototype_features[0]);
            for (int prototype_index = 1; prototype_index < prototype_features.Count; prototype_index++)
            {
                //Find closest proto
                double new_distance = this.distance_function.Compute(instance_features, prototype_features[prototype_index]);
                if (new_distance < bestPrototypeDistance)
                {
                    best_prototype_index = prototype_index;
                    bestPrototypeDistance = new_distance;
                }
            }
            return prototype_labels[best_prototype_index];
        }
    }
}