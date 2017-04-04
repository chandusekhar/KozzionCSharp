using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.Reporting;
using KozzionMachineLearning.FeatureSelection;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Model
{
    public class TemplateModelFeatureSelecting<DomainType, LabelType> : 
        ATemplateModelDiscrete<DomainType, LabelType>
    {
        private IFeatureSetSelectorDiscrete<DomainType, LabelType> selector;
        private ITemplateModelDiscrete<DomainType, LabelType> template;


        public TemplateModelFeatureSelecting(
            IFeatureSetSelectorDiscrete<DomainType, LabelType> selector,
            ITemplateModelDiscrete<DomainType, LabelType> template)
        {
            this.selector = selector;
            this.template = template;
        }

        public override IModelDiscrete<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set)
        {
            // do feature selection here
            Tuple <IModelDiscrete<DomainType, LabelType>, IList <int>> selected_features = selector.SelectFeatureSet(this.template, training_set);
            return new ModelDiscreteSelecting<DomainType, LabelType>(training_set.DataContext, selected_features.Item1, selected_features.Item2);
        }
    }
}
