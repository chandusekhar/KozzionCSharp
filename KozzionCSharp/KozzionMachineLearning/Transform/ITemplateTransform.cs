
using KozzionMachineLearning.DataSet;
using System.Collections.Generic;

namespace KozzionMachineLearning.Transform
{
    public interface ITemplateTransform
    {
        ITransform Generate(IDataSet dataset);
    }
}