using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Transform
{
    public interface ITransform
    {
        double[] TransformForward(double[] source);
        double[] TransformBackward(double[] source);
        IDataSet TransformForward(IDataSet source);
        IDataSet TransformBackward(IDataSet source);
    }
}