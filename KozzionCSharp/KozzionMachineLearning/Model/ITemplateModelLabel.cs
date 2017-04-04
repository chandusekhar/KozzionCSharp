using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{

    //Lowest form of model organisation : it just gives a label, this is what neural networks do. We can only compare it to the dataset label and compute residuals
    public interface ITemplateModelLabel<DomainType, LabelType>
    {
        IModelLabel<DomainType, LabelType> GenerateModel(IDataSet<DomainType, LabelType> training_set); //TODO We could take out the model type and swap it for its domain and label type

        ReportResidual GenerateAndTestResidual(IDataSet<DomainType, LabelType> training_set, IDataSet<DomainType, LabelType> test_set);
    }
}
