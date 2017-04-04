using System;
using MathNet.Numerics.Distributions;
using KozzionMathematics.Statistics.Distribution.implementation;

namespace KozzionMathematics.Statistics.Distribution
{
    public class DistributionPossion : IDistribution<int, float>
    {
        private float d_lambda;

        public int[] Domain
        {
            get
            {
                throw new Exception("Distribution domain is inifite");
            }
        }

        public DistributionPossion(
            float lambda)
        {
            d_lambda = lambda;
        }

        public IProbabilityDensityFunction<int, float> GetProbabilityDensityFunction()
        {
            return new ProbabilityDensityFunction<int, float>(this);
        }

        public ICulmativeDensityFunction<int, float> GetCulmativeDensityFunction()
        {
            return new CulmativeDensityFunction<int, float>(this);
        }

        public int[] RandomSample(int sample_size)
        {
			int [] sample = new int [sample_size];
            Random random = new Random();
            Poisson.Samples(random, sample, d_lambda);
            return sample;
        }


        public float ComputeProbabilityDensity(int value_domain)
        {
            return (float)Poisson.PMF(d_lambda, value_domain);
        }

        public float ComputeCulmativeDensity(int value_domain)
        {
            return (float)Poisson.CDF(d_lambda, value_domain);
        }

     
    }
}
