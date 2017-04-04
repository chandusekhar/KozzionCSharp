using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Method.JointTable;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KozzionMachineLearning.Model
{
    public abstract class AModelLabel<DomainType, LabelType> :  IModelLabel<DomainType, LabelType>
    {
        public string FunctionType { get { return ModelType; } }

        public IDataContext DataContext { get; private set; }

        public string ModelType  { get; }
        
        protected AModelLabel(IDataContext data_context, string model_type)
        {
            DataContext = data_context;
            ModelType = model_type;
        }

        public LabelType Compute(DomainType [] instance_features)
        {
            return GetLabel(instance_features);
        }

        public abstract LabelType GetLabel(DomainType [] instance_features);


    }
}
