using KozzionCore.DataStructure;

namespace KozzionGraphics.ElementTree.MaxTree.Implementation
{
    public class ReporterPart : IProgressReporter
    {
        private IProgressReporter inner_reporter;
        private float fraction;

        public ReporterPart(KozzionCore.DataStructure.IProgressReporter inner_reporter, float fraction)
        {
            this.inner_reporter = inner_reporter;
            this.fraction = fraction;
        }

        public float ReportFraction
        {
            get { return inner_reporter.ReportFraction; }
        }

        public void Report(int done, int total)
        {
            inner_reporter.Report((int)(done * fraction), total);
        }
    }
}
