using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public interface IModelDiscreteIterative<DomainType, LabelType> :
        IModelDiscrete<DomainType, LabelType>,
        IModelLabelIterative<DomainType, LabelType>
    {
        IModelDiscreteIterative<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set); 

    }
}
