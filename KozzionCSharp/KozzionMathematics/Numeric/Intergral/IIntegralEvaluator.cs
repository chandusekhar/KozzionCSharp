namespace KozzionMathematics.Numeric.Intergral
{
    public interface IIntegralEvaluator<RealType>
    {
        RealType[] EvaluateSeries(RealType[] domain, RealType[] values);
        void EvaluateSeriesRBA(RealType[] domain, RealType[] values, RealType[] result);
        RealType EvaluateValue(RealType[] domain, RealType[] values, RealType domain_lower_bound, RealType domain_upper_bound);
    }
}