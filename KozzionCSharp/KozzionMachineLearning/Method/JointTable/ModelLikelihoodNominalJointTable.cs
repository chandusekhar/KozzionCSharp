using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMathematics.Tools;
using System.Collections.Generic;
using System;

namespace KozzionMachineLearning.Method.JointTable
{
    public class ModelLikelihoodNominalJointTable<LabelType> : 
        AModelLikelihood<int, LabelType>
    {
        private double[] priors;
        private IDictionary<int[], int[]> occurences;

        public ModelLikelihoodNominalJointTable(IDataContext data_context, double[] priors, IDictionary<int[], int[]> occurences)
            :base(data_context, "ModelLikelihoodNominalJointTable")
        {      
            this.priors = ToolsCollection.Copy(priors);
            this.occurences = occurences;
  
        }

        public override double[] GetLikelihoods(int [] instance_features) 
        {
            //Use join occurrence table of B to A to compute P(A|B) if occurance of B = 0 then revert to priors of A
            if (!this.occurences.ContainsKey(instance_features))
            {
                return ToolsCollection.Copy(priors);
            }
            else
            {
                int[] occurence = this.occurences[instance_features];
                double[] probabilities = new double[this.DataContext.LabelDescriptors[0].ValueCount];
                for (int label_index = 0; label_index <  this.DataContext.LabelDescriptors[0].ValueCount; label_index++)
                {
                    probabilities[label_index] = ((double)occurence[label_index + 1]) / ((double)occurence[0]);
                }
                return probabilities;
            }      
        }  
    }
}
