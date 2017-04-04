using System;
using System.Collections.Generic;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.FeatureSelection
{
    public interface IFeatureSetEvaluatorDiscrete<DomainType,LabelType>
    {
        double Evaluate(ITemplateModelDiscrete<DomainType, LabelType> template, IDataSet<DomainType, LabelType> data_set, ISet<int> feature_set);
    }
}