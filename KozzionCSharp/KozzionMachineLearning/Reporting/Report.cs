using System;

namespace KozzionMachineLearning.Reporting
{
    public abstract class AReport
    {
        public DateTime TrainingBegin { get; private set; }
        public DateTime TrainingEnd { get; private set; }
        public DateTime TestingBegin { get; private set; }
        public DateTime TestingEnd { get; private set; }

        public int TrainingSetSize { get; private set; }
        public float AproximateTrainingCostInFlops { get; private set; }
        public int TestingSetSize { get; private set; }
        public float AproximateTestingCostInFlops { get; private set; }

        public AReport()
        { 
        }



    }
}
