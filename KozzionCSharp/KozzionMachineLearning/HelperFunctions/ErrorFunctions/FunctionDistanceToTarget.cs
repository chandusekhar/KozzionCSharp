using KozzionMathematics.Function;

namespace KozzionMachineLearning.HelperFunctions.ErrorFunctions
{
    public class FunctionDistanceToTarget<DomaineType, DistanceType> : IFunction<DomaineType, DistanceType>
    {
        public string FunctionType { get { return "FunctionDistanceToTarget"; } }
        IFunctionDistance<DomaineType, DistanceType> d_distance_function;
        DomaineType d_target;

        public FunctionDistanceToTarget(IFunctionDistance<DomaineType, DistanceType> distance_function, DomaineType target) 
        {
            d_distance_function = distance_function;
            d_target = target;
        }

        public DistanceType Compute(DomaineType value_domain)
        {
            return d_distance_function.Compute(value_domain, d_target);
        }
    }
}
