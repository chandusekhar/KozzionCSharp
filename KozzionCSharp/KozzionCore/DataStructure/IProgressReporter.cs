namespace KozzionCore.DataStructure
{
    public interface IProgressReporter
    {
        float ReportFraction {get;}

        void Report(int done, int total);
    }
}
