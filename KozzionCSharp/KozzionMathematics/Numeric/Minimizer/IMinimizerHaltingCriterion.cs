namespace KozzionMathematics.Numeric.Minimizer
{
    public interface IMinimizerHaltingCriterion<HaltingInfoType>
    {
        bool CheckHalt(HaltingInfoType halting_info);

        int MaximumIterationCount { get; }
    }
}
