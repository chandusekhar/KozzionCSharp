using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Method.JointTable;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    [Serializable]
    public abstract class AModelDiscrete<DomainType, LabelType> :
        AModelLabel<DomainType, LabelType>,
        IModelDiscrete<DomainType, LabelType>
    {
        private Dictionary<LabelType, int> label_value_to_label_index;

        public AModelDiscrete(IDataContext data_context, string model_type)
            : base(data_context, model_type)
        {
    
        }

        public int GetLabelIndex(DomainType [] instance_features)
        {
            throw new NotImplementedException();
            //return DataContext.GetLabelDescriptor(0).ValueIndex(GetLabel(instance_features));
        }        
    }
}
