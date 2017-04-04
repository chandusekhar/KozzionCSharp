using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test.MultiSample
{
    public class TestIndependance : ATestMultiSample
    {

        // Larsen Marx 4th editon p632
        public override string TestName { get { return "TestIndependance"; } }

        public override string TestStatisticName { get { return "TestIndependance???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] {  };
            }
        }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] { TestAssertion.MeasurementsIndependant };
            }
        }

        public override TestAssertion NullHypothesis
        {
            get
            {
                return TestAssertion.SamplesIndependant;
            }
        }

        public override double Test(IList<IList<double>> samples)
        {
            return TestStatic(samples, new double[] { 0, 1, 2 });
        }

        public static double TestStatic(IList<IList<double>> samples, IList<double> limits)
        {
            // Create to blocks according to limits
            double[,] table = new double[samples.Count, limits.Count + 1];
            for (int index_0 = 0; index_0 < samples.Count; index_0++)
            {
                for (int index_1 = 0; index_1 < samples[index_0].Count; index_1++)
                {
                    double value = samples[index_0][index_1];
            
                  
                    //for any of the other bins
                    int limit_index = 0;
                    while ((limit_index < limits.Count) && (limits[limit_index] <= value))
                    {
                        limit_index++;
                    }
                    table[index_0, limit_index]++;                 
                }
            }

            // Compute test for independance
            double[] sums_0 = ToolsMathCollection.Sums0(table);
            double[] sums_1 = ToolsMathCollection.Sums1(table);
            double total = ToolsMathCollection.Sum(sums_0);

            double chi_square_statistic = 0;
            for (int index_0 = 0; index_0 < sums_0.Length; index_0++)
            {
                for (int index_1 = 0; index_1 < sums_1.Length; index_1++)
                {
                    double expectation = (sums_0[index_0] * sums_1[index_1]) / total;
                    chi_square_statistic += (ToolsMath.Sqr(table[index_0, index_1] - expectation) / expectation);
                }
            }
            double degrees_of_freedom = (sums_0.Length - 1) * (sums_1.Length - 1);
            return ChiSquared.CDF(degrees_of_freedom, chi_square_statistic);
        }   

	}
}
