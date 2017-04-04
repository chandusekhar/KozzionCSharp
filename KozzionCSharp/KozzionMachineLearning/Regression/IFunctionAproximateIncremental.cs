using System;
using System.Collections.Generic;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.regression
{
    public interface IFunctionAproximateIncremental<DomainType, RangeType> : IFunction<DomainType, RangeType> 
    {
        void Improve(IList<Tuple<DomainType, RangeType>> examples);
    }
}
