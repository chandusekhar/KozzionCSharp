using System;
using System.Collections.Generic;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.regression
{
    public interface IFunctionAproximator<DomainType, RangeType>
    {
        IFunction<DomainType, RangeType> Aproximate(IList<Tuple<DomainType, RangeType>> examples);

    }
}