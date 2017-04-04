namespace KozzionMachineLearning.Evaluation
{
    public class ReportROC
    {

        public double[] Scores { get; internal set; }
        public bool[] Labels { get; internal set; }

        public ReportROC(double [] scores, bool[] labels)
        {
        }


    }
}
