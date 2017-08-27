using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public interface ITemplateModelLikelihood<DomainType, LabelType>:
        ITemplateModelDiscrete<DomainType, LabelType>
    {
        IModelLikelihood<DomainType, LabelType> GenerateModelLikelihood(IDataSet<DomainType, LabelType> training_set); //TODO We could take out the model type and swap it for its domain and label type

        ReportLikelihood<DomainType, LabelType> GenerateAndTestLikelihood(IDataSet<DomainType, LabelType> training_set, IDataSet<DomainType, LabelType> test_set);
    }
}
