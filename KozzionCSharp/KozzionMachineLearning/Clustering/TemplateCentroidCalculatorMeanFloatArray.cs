using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation.Distance;

namespace KozzionMachineLearning.Clustering
{
    public class TemplateCentroidCalculatorMeanFloatArray : ITemplateCentroidCalculator<float, ICentroidDistance<float>>
    {
        private IFunctionDistance<float[], float> distance_function;

        public TemplateCentroidCalculatorMeanFloatArray(IFunctionDistance<float[], float> distance_function)
        {
            this.distance_function = distance_function;
        }

        public IFunction<IList<float[]>, ICentroidDistance<float>> Generate(IDataContext data_context)
        {
            return new CentroidCalculatorMeanFloatArray(distance_function);
        }
    }
}
