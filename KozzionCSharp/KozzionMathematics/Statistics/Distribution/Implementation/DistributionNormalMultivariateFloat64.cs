using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Statistics.Distribution.implementation;

namespace KozzionMathematics.Statistics.Distribution.Implementation
{
    public class DistributionNormalMultivariateFloat64 :IDistribution<double [], double>
    {
        public MatrixNormal matrix_normal;

        public double[][] Domain
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DistributionNormalMultivariateFloat64(double[] means, double[,] covarriance)
        {
        }

 

        public double ComputeCulmativeDensity(double[] value_domain)
        {
            throw new NotImplementedException();
        }

        public double ComputeProbabilityDensity(double [] value_domain)
        {
            return this.matrix_normal.Density(new DenseMatrix(1, value_domain.Length, value_domain));         
        }

        public ICulmativeDensityFunction<double[], double> GetCulmativeDensityFunction()
        {
            throw new NotImplementedException();
        }

        public IProbabilityDensityFunction<double[], double> GetProbabilityDensityFunction()
        {
            throw new NotImplementedException();
        }

        public double[][] RandomSample(int sample_size)
        {
            throw new NotImplementedException();
        }
    }
}
