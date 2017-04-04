using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public interface IModelLabelIterative<DomainType, LabelType> :
        IModelLabel<DomainType, LabelType>
    {
        IModelLabelIterative<DomainType, LabelType> GenerateModelLabel(IDataSet<DomainType, LabelType> training_set);

    }
}
