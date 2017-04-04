using System;
using System.Collections.Generic;
using KozzionMachineLearning.Method.JointTable;
using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Model
{
    internal class ModelDiscreteSelecting<DomainType, LabelType> :
        AModelDiscrete<DomainType, LabelType>
    {
        private IModelDiscrete<DomainType, LabelType> model;
        private IList<int> selected_feature_indexes;

        public ModelDiscreteSelecting(IDataContext data_context, IModelDiscrete<DomainType, LabelType> model, IList<int> selected_feature_indexes)
            : base(data_context, "ModelDiscreteSelecting")
        {
            this.model = model;
            this.selected_feature_indexes = selected_feature_indexes;
        }

        public override LabelType GetLabel(DomainType[] instance_features)
        {
            return model.GetLabel(ToolsCollection.Select(instance_features, selected_feature_indexes));
        } 
    }
}