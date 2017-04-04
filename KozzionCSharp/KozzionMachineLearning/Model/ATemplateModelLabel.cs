using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public abstract class ATemplateModelLabel<DomainType, LabelType> : ITemplateModelLabel<DomainType, LabelType>
    {
        public ReportResidual GenerateAndTestResidual(IDataSet<DomainType, LabelType> training_set, IDataSet<DomainType, LabelType> test_set)
        {
            throw new NotImplementedException();
        }

        public abstract IModelLabel<DomainType, LabelType> GenerateModel(IDataSet<DomainType, LabelType> training_set);

       
    }
}
