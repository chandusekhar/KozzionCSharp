using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using KozzionMathematics.Function;
using KozzionMathematics.Statistics.Distribution.implementation;

namespace KozzionMathematics.Statistics.Distribution
{
    public abstract class ADistributionMultyVariate : IDistribution<float [], float>
    {
        public int DimensionCount { get; private set; }

        public float[][] Domain
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ADistributionMultyVariate() 
        {

        }

        public float[][] RandomSample(int sample_size)
        {
            throw new NotImplementedException();
        }


        public float[][] RandomSample(int sample_size, RandomNumberGenerator random)
        {

            float [][] sample = new float[sample_size][ ];
            // float [] 
            List<float> values = new List<float>();
            IFunctionBijective<float, float> first_marginal = GetMarginalCulmativeDensityFunction(0);
            for (int index_sample = 0; index_sample < sample_size; index_sample++)
			{
                values.Clear();
			    for (int index_dimension = 1; index_dimension < DimensionCount; index_dimension++)
			    {
                   
			        IFunctionBijective<float, float> marginal = GetMarginalCulmativeDensityFunction(values);
                    float result = marginal.ComputeInverse(random.RandomFloat32Unit());
                    values.Add(result);
			    }  
                sample[index_sample] = values.ToArray();
			}
            return sample;
        }

        private IFunctionBijective<float, float> GetMarginalCulmativeDensityFunction(int dimension)
        {
            throw new NotImplementedException();
        }

        private IFunctionBijective<float, float> GetMarginalCulmativeDensityFunction(List<float> values)
        {
            throw new NotImplementedException();
        }


        public IProbabilityDensityFunction<float[], float> GetProbabilityDensityFunction()
        {
            throw new NotImplementedException();
        }


    
        public float ComputeProbabilityDensity(float[] value_domain)
        {
            throw new NotImplementedException();
        }

        public float ComputeCulmativeDensity(float[] value_domain)
        {
            throw new NotImplementedException();
        }

        public ICulmativeDensityFunction<float[], float> GetCulmativeDensityFunction()
        {
            throw new NotImplementedException();
        }
    }
}
