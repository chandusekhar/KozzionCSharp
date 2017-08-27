using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Evaluation;
using KozzionMachineLearning.Model;
using KozzionMathematics.Statistics.Test.TwoSampleROC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Tools
{
    public class ToolsModelComparrison
    {
        public static double AUCP<DomainType, LabelType>  (
            IModelLikelihood<DomainType, LabelType> model_0,
            IDataSet<DomainType, LabelType> test_set)
        {
            return 0;
        }

        public static double ROCP<DomainType>(
            IModelLikelihood<DomainType, bool> model_0, 
            IModelLikelihood<DomainType, bool> model_1,
            IDataSet<DomainType, bool> test_set,
            bool label_value)  
        {
            if (!model_0.DataContext.Equals(model_0.DataContext))
            {
                throw new Exception("DataContext Mismatch");
            }
            TestROCHanleyMcNeil test = new TestROCHanleyMcNeil();

            return 0;
        }

        public static double ROCP<DomainType>(
            ReportROC report_0,
            ReportROC report_1)
        {
            TestROCHanleyMcNeil test = new TestROCHanleyMcNeil();
            return test.Test(report_0.Labels, report_0.Scores, report_1.Scores);
        }
    }
}
