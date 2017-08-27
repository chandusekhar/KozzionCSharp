using System;
using KozzionCore.Tools;
using KozzionMathematics.Statistics.Distribution.implementation;
using MathNet.Numerics.Distributions;

namespace KozzionMathematics.Statistics.Distribution
{
    public class DistributionNormalUnivariateFloat64 : IDistribution<double, double>
    {
        public double Mean { get; private set; }
        public double StandardDeviation { get; private set; }

        public double[] Domain
        {
            get
            {
                throw new Exception("Distribution domain is real");
            }
        }

        public DistributionNormalUnivariateFloat64(double mean, double standard_deviation) 
        {
            Mean = (float)mean;
            StandardDeviation = (float)standard_deviation;
        }

   

        public IProbabilityDensityFunction<double, double> GetProbabilityDensityFunction()
        {
            return new ProbabilityDensityFunction<double, double>(this);
        }

        public ICulmativeDensityFunction<double, double> GetCulmativeDensityFunction()
        {
            return new CulmativeDensityFunction<double, double>(this);
        }

   

        public double[] RandomSample(int sample_size)
        {
            double[] sample = new double[sample_size];
            Normal.Samples(new Random(), sample, Mean, StandardDeviation);
            return sample;
        }

        public double ComputeProbabilityDensity(double value_domain)
        {
            return Normal.PDF(Mean, StandardDeviation, value_domain);
        }

        public double ComputeCulmativeDensity(double value_domain)
        {
            return Normal.CDF(Mean, StandardDeviation, value_domain);
        }

         public static double ComputeProbabilityDensity(double mean, double standard_deviation, double value_domain)
        {
            return Normal.PDF(mean, standard_deviation, value_domain);
        }

        public static double ComputeCulmativeDensity(double mean, double standard_deviation, double value_domain)
        {
            return Normal.CDF(mean, standard_deviation, value_domain);
        }

    }
}
