

using System;
using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Test.TwoSample;
using KozzionMathematics.Tools;
using KozzionCore.Tools;
using KozzionMathematics.Statistics.Distribution;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;

namespace KozzionMathematics.Statistics.Test.OneSample
{
    public class TestGoodNessOfFitChiSquare<DomainType> : ITestGoodnessOfFitDiscrete<DomainType>
    {

        public string[] Assumptions
        {
            get
            {
                return new string[] { "Sample Independance"};
            }
        }

        public string Result
        {
            get
            {
                return "probability that sample is drawn from given distribution type";
            }
        }




        public double Test(IDistribution<DomainType, double> distribution, IList<DomainType> sample)
        {
            return TestStatic(distribution, sample);
        }

        public static double TestStatic(IDistribution<DomainType, double> distribution, IList<DomainType> sample)
        {
            DomainType[] domain = distribution.Domain;
            Dictionary<DomainType, int> domain_indexing = new Dictionary<DomainType, int>();
            double [] sample_densities = new double[domain.Length];
            for (int domain_index = 0; domain_index < sample.Count; domain_index++)
            {
                domain_indexing[domain[domain_index]] = domain_index;
            }

            for (int sample_index = 0; sample_index < sample.Count; sample_index++)
            {
                sample_densities[domain_indexing[sample[sample_index]]] += 1.0 / sample.Count;
            }

            double d1 = 0;
            for (int domain_index = 0; domain_index < sample.Count; domain_index++)
            {
                double sample_density = sample_densities[domain_index];
                double expectated_density = distribution.ComputeProbabilityDensity(domain[domain_index]);
                double error = (sample_density - expectated_density);
                d1 += ((error * error) / expectated_density);
            }
            
            int dof = domain.Length - 1; //TODO if fitted subtract distribution dof
            return 1 - ChiSquared.CDF(dof, d1);
        }

  
    }
}