using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public interface ITemplateModelDiscrete<DomainType, LabelType>:
        ITemplateModelLabel<DomainType, LabelType>
    {
        IModelDiscrete<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set); //TODO We could take out the model type and swap it for its domain and label type

        ReportDiscrete<DomainType, LabelType> GenerateAndTestDiscrete(IDataSet<DomainType, LabelType> training_set, IDataSet<DomainType, LabelType> test_set);
    }
}
