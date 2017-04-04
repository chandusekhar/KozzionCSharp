using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public interface IModelLabel<DomainType, LabelType> : 
        IFunction<DomainType [], LabelType>
    {
        IDataContext DataContext { get;}

        LabelType GetLabel(DomainType [] instance_features);

        string ModelType { get; }

        //void Write(BinaryWriter write);
    }
}
