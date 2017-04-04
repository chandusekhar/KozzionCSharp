
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation.Distance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.LVQ
{
    public class TemplateModelLVQ : ATemplateModelDiscrete<double, int>
    {
        private RandomNumberGenerator random;
        private IFunctionDistance<double[], double> distance_function;
        private int prototype_count;
        private double LearningRate;
        private int epoch_count;


        public TemplateModelLVQ(IFunctionDistance<double[], double> distance_function, int prototype_count, double learning_rate, int epoch_count, RandomNumberGenerator random)
        {
            this.random = random;
            this.distance_function = distance_function;
            this.prototype_count = prototype_count;
            this.LearningRate = learning_rate;
            this.epoch_count = epoch_count;
        }


        public TemplateModelLVQ(IFunctionDistance<double[], double> distance_function, int prototype_count, double learning_rate, int epoch_count)
            : this(distance_function, prototype_count, learning_rate, epoch_count, new RNGCryptoServiceProvider())
        {

        }

        public void Train(IList<double[]> prototype_features, IList<int> prototype_labels, IList<double[]> instance_features, IList<int> instance_labels)
        {    
            for (int iteration = 0; iteration < this.epoch_count; iteration++)
            {
                int[] random_indexes = random.RandomPermutation(instance_features.Count);
                for (int random_index = 0; random_index < instance_features.Count; random_index++)
                {
                    int instance_index = random_indexes[random_index];

                    int best_prototype_index = 0;
                    double best_prototype_distance = this.distance_function.Compute(instance_features[instance_index], prototype_features[0]);
                    for (int prototype_index = 1; prototype_index < prototype_features.Count; prototype_index++)
                    {
                        //Find closest prototype
                        double new_distance = this.distance_function.Compute(instance_features[instance_index], prototype_features[prototype_index]);
                        if (new_distance < best_prototype_distance)
                        {
                            best_prototype_index = prototype_index;
                            best_prototype_distance = new_distance;
                        }
                    }

                    if (prototype_labels[best_prototype_index] == instance_labels[instance_index])
                    {
                        //attract
                        for (int feature_index = 0; feature_index < prototype_features[best_prototype_index].Length; feature_index++)
                        {
                            prototype_features[best_prototype_index][feature_index] += (instance_features[instance_index][feature_index] - prototype_features[best_prototype_index][feature_index]) * LearningRate;
                        }
                    }
                    else
                    {
                        //repulse
                        for (int prototype_index = 0; prototype_index < prototype_features[best_prototype_index].Length; prototype_index++)
                        {
                            prototype_features[best_prototype_index][prototype_index] -= (instance_features[instance_index][prototype_index] - prototype_features[best_prototype_index][prototype_index]) * LearningRate;

                        }
                    }
                }
            }
        }

        public override IModelDiscrete<double, int> GenerateModelDiscrete(IDataSet<double, int> training_set)
        {
      
            IList<double[]> instance_features = training_set.FeatureData; 
            IList<int> instance_labels = training_set.GetLabelDataColumn(0);


            IList<double[]> prototype_features = new List<double[]>();
            IList< int > prototype_labels = new List<int>();
            int[] random_indexes = random.RandomPermutation(instance_features.Count);

            for (int prototype_index = 0; prototype_index < this.prototype_count; prototype_index++)
            {
                int instance_index = random_indexes[prototype_index];
                prototype_features.Add(instance_features[instance_index]);
                prototype_labels.Add(instance_labels[instance_index]);
            }

            Train(prototype_features, prototype_labels, training_set.FeatureData, training_set.GetLabelDataColumn(0));
            return new ModelLVQDefault(training_set.DataContext, prototype_features, prototype_labels, this.distance_function);
        }

     
    }
}
