using System;
using System.Collections.Generic;
namespace KozzionMachineLearning.regression
{
	public interface IFunctionAproximatorIncremental<DomainType, RangeType> : IFunctionAproximator<DomainType, RangeType>
	{
		IFunctionAproximateIncremental<DomainType, RangeType> AproximateIncremental(IList<Tuple<DomainType, RangeType>> examples);
    
	}
}