using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface ITemplateCentroidCalculator<DomainType, CentroidType>
    {
        IFunction<IList<DomainType[]>, CentroidType> Generate(IDataContext data_context);
    }
}
