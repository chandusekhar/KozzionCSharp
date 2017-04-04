using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;
using System;
using System.Collections.Generic;
using KozzionMachineLearning.Reporting;
using System.Threading;
using KozzionMathematics.Tools;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.FeatureSelection
{
    public class EvaluatorReapetedRandomFold<DomainType, LabelType> : IFeatureSetEvaluatorDiscrete<DomainType,LabelType>
    {
 
        public int FoldCount {get; private set;}
        public double TrainingSetFraction {get; private set;}

        public EvaluatorReapetedRandomFold(int fold_count, double training_set_fraction)
        {
            this.FoldCount = fold_count;
            this.TrainingSetFraction = training_set_fraction;
        }

        public EvaluatorReapetedRandomFold(int fold_count)
       : this(fold_count, 0.5)
        {

        }

        public EvaluatorReapetedRandomFold()
            :this(1,  0.5)
        {
           
        }    

        public double Evaluate(ITemplateModelDiscrete<DomainType, LabelType> template, IDataSet<DomainType, LabelType> data_set, ISet<int> feature_set)
        {
            IDataSet<DomainType, LabelType> selected_data_set = data_set.SelectFeatures(new List<int>(feature_set));
            double[] scores = new double[this.FoldCount];
            Parallel.For(0, this.FoldCount, fold_index =>
            {
                Tuple<IDataSet<DomainType, LabelType>, IDataSet<DomainType, LabelType>> split = selected_data_set.Split(this.TrainingSetFraction);
                ReportDiscrete<DomainType, LabelType> report = template.GenerateAndTestDiscrete(split.Item1, split.Item2);
                scores[fold_index] = report.CorrectLabelRate;
            });
            return ToolsMathCollection.Sum(scores) / ((double)this.FoldCount);
        }     
    }
}