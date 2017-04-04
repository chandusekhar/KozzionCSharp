using KozzionMathematics.Function;
using System;

namespace KozzionMachineLearning.Method.expectation_maximalization
{
    public interface IExpectationFunctionGenerator<ObeservationType, LabelType>
    {
        IFunction<Tuple<ObeservationType[], LabelType[], int>, LabelType> generate(ObeservationType[] observations, LabelType[] state);
    }
}
