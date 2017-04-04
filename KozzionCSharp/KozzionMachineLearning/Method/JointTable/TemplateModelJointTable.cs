using System.Collections.Generic;

using KozzionMachineLearning.Reporting;
using KozzionCore.Collections;
using System;
using KozzionCore.DataStructure.Collections;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Method.JointTable
{
    public class TemplateModelJointTable : ATemplateModelLikelihood<int, int>
    {
        public TemplateModelJointTable() 
        {
        }

        public override IModelLikelihood<int, int, double> GenerateModelLikelihood (IDataSet<int, int> training_set)
        {
            //TODO this asumes our label is finite and what not, maybe a bit silly
            double[] priors = new double[training_set.DataContext.GetLabelDescriptor(0).ValueCount];
            int[] instance_labels = training_set.GetLabelDataColumn(0);

            DictionaryCount<int> count_map = new DictionaryCount<int>(instance_labels);
            foreach (int key in count_map.Keys)
            { 
                priors[key] = ((double)count_map.Get(key) / ((double)count_map.TotalCount));
            }


            IDictionary<int[], int[]> occurences = new DictionaryArrayKey<int, int[]>();
            for (int instance_index = 0; instance_index < training_set.InstanceCount; instance_index++)
            {
                int [] instance_features = training_set.GetInstanceFeatureData(instance_index);
                if (!occurences.ContainsKey(instance_features))
                {
                    occurences[instance_features] = new int[priors.Length + 1]; //index 0 is for total
                }
                occurences[instance_features][0]++;
                occurences[instance_features][instance_labels[instance_index] + 1]++;
            }
            return new ModelLikelihoodNominalJointTable<int>(training_set.DataContext, priors, occurences);
        }

  

    
    }
}
