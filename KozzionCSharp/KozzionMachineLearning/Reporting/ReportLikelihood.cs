using KozzionCore.Tools;
using KozzionMachineLearning.Model;
using KozzionMathematics.Algebra;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.Reporting
{
    public class ReportLikelihood<DomainType, LabelType, LikelihoodType>
        where LikelihoodType : IComparable<LikelihoodType>
    {
        public IAlgebraReal<LikelihoodType> Algebra {get;}
        public IModelLikelihood<DomainType, LabelType, LikelihoodType> Model { get; private set; }

        private LikelihoodType[][] likelihoods;
        public LikelihoodType[][] Likelihoods { get { return ToolsCollection.Copy(likelihoods); } }

        private LabelType[] label_values;
        public LabelType[] LabelValues { get { return ToolsCollection.Copy(this.label_values); } }

        public ReportLikelihood(IAlgebraReal<LikelihoodType> algebra, IModelLikelihood<DomainType, LabelType, LikelihoodType> model, LikelihoodType[][] likelihoods, LabelType [] label_values)
        {
            this.Algebra = algebra;
            this.Model = model;
            this.likelihoods = likelihoods;
            this.label_values = null;
        }

        public double GetAUC(LabelType label_value)
        {
            int label_index = this.Model.DataContext.GetLabelDescriptor(0).GetValueIndex(label_value);
            LikelihoodType[] label_scores = new LikelihoodType[label_values.Length];
            LikelihoodType[] second_scores = new LikelihoodType[label_values.Length];
            bool[] labels = new bool[label_values.Length];
            for (int index = 0; index < label_values.Length; index++)
            {
                label_scores[index] = likelihoods[index][label_index];
                labels[index] = (this.Model.DataContext.GetLabelDescriptor(0).GetValueIndex(label_values[index]) == label_index);
                List<int> ordering_indexes = ToolsMathCollection.Ordering(likelihoods[index]);
                if (ordering_indexes[0] == label_index)
                {
                    second_scores[index] = likelihoods[index][label_index];
                }
                else
                {
                    second_scores[index] = likelihoods[index][ordering_indexes[0]];
                }                
            }
     
            LikelihoodType[] scores = ToolsMathCollection.DivideElements(this.Algebra, label_scores, second_scores);      
            return ToolsMathStatistics.ComputeROCAUCTrapeziod(labels, scores);
        }
    }
}