using System;
using KozzionCore.Tools;
using KozzionMathematics.Statistics.Distribution.implementation;
using MathNet.Numerics.Distributions;

namespace KozzionMathematics.Statistics.Distribution
{
    public class DistributionNormalUnivariateFloat32 : IDistribution<float, float>
    {
        public float Mean { get; private set; }
        public float StandardDeviation { get; private set; }

        public float[] Domain
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DistributionNormalUnivariateFloat32(float mean, float standard_deviation) 
        {
            Mean = mean;
            StandardDeviation = standard_deviation;
        }

        public IProbabilityDensityFunction<float, float> GetProbabilityDensityFunction()
        {
            return new ProbabilityDensityFunction<float, float>(this);
        }

        public ICulmativeDensityFunction<float, float> GetCulmativeDensityFunction()
        {
            return new CulmativeDensityFunction<float, float>(this);
        }


        public float[] RandomSample(int sample_size)
        {
            double[] sample = new double[sample_size];
            Random random = new Random();
            Normal.Samples(random, sample, Mean, StandardDeviation);
            return ToolsCollection.ConvertToFloatArray(sample);
        }
        

        public float ComputeProbabilityDensity(float value_domain)
        {
            return (float)Normal.PDF(Mean, StandardDeviation, value_domain);
        }



        public float ComputeCulmativeDensity(float value_domain)
        {
            return (float)Normal.CDF(Mean, StandardDeviation, value_domain);
        }

  

        public static double ComputeProbabilityDensity(float mean, float standard_deviation, float value_domain)
        {
            return Normal.PDF(mean, standard_deviation, value_domain);
        }


        public static double ComputeProbabilityDensity(double mean, double standard_deviation, double value_domain)
        {
            return Normal.PDF(mean, standard_deviation, value_domain);
        }

        public float ComputeCulmativeDensity(float mean, float standard_deviation, float value_domain)
        {
            return (float)Normal.CDF(mean, standard_deviation, value_domain);
        }

        public double ComputeCulmativeDensity(double mean, double standard_deviation, double value_domain)
        {
            return Normal.CDF(mean, standard_deviation, value_domain);
        }
    }
}
