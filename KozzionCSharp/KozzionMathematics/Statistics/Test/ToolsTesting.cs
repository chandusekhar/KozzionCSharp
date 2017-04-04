using KozzionMathematics.Statistics.Test.OneSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Statistics.Test
{
    public class ToolsTesting
    {
        public static IList<TestRequirement> CheckRequirements(double[] sample_0, double[] sample_1)
        {
            IList < TestRequirement > requirements = new List<TestRequirement>();
            if (sample_0.Length == sample_1.Length)
            {
                requirements.Add(TestRequirement.SamplesAreOfEqualSize);
            }    
            return requirements;
        }


        public static string TestDifferent(double[] sample_0, double[] sample_1)
        {
            //Test if the two samples come from the same distribution

            // Check samples independant

            /// if indepentdant check normal
            TestNormalety(sample_0);
            TestNormalety(sample_1);
            /// if dependant
            //// error
            return "error";
        }


        public static double TestNormalety(double [] sample_0)
        {
            //Kolmogorov–Smirnov statistic
            return TestKolmogorovSmirnov.TestStatic(sample_0);
        }
    }
}
