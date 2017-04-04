using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;

namespace KozzionMachineLearning.FeatureSelection
{
    public interface IFeatureSetSelectorDiscrete<DomainType, LabelType>
    {
        Tuple<IModelDiscrete<DomainType, LabelType>, IList<int>> SelectFeatureSet(ITemplateModelDiscrete<DomainType, LabelType> template, IDataSet<DomainType, LabelType> data_set);
    }
}