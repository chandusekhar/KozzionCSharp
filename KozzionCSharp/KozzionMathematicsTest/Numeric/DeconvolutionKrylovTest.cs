using KozzionMathematics.Algebra;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation.Interpolation;
using KozzionMathematics.Numeric;
using KozzionMathematics.Numeric.Deconvolution;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematicsTest.Numeric
{
    [TestClass]
    public class DeconvolutionKrylovTest
    {
        [TestMethod]
        public void TestDeconvolutionKrylovPerf()
        {
            double[] aif = new double[] { 0, 4.4000, 18.3000, 50.6000, 105.8000, 172.8000, 226.0000, 240.1000, 208.8000, 150.3000, 94.5000, 60.9000, 49.3000, 49.8000, 53.2000, 54.3000, 52.9000, 50.0000, 47.3000, 45.6000, 47.8000, 47.0000, 45.6000, 43.3000, 41.2000, 39.9000, 39.6000, 39.4000, 38.6000, 37.5000, 35.6000, 34.3000, 32.9000, 35.7000 };
            double[] tac = new double[] { 0, 0.3000, 1.30000, 4.2001, 8.6001, 14.4000, 21.6001, 29.8000, 36.9000, 40.9000, 41.5000, 40.0000, 38.9000, 38.6001, 38.2001, 37.3000, 35.9000, 34.0000, 32.5000, 31.8000, 32.3000, 31.3000, 31.4000, 30.0000, 28.7001, 29.8000, 30.7001, 30.6001, 30.9000, 31.2001, 24.9000, 21.3000, 32.2001, 28.8000 };
            double[] tim = new double[] { 0, 3.0800, 6.1640, 9.2440, 12.3240, 15.4040, 18.4850, 21.5660, 24.6460, 27.7270, 30.8100, 33.8900, 36.9710, 40.0530, 43.1350, 46.2170, 49.2970, 52.3770, 55.4570, 58.5380, 70.0000, 76.0840, 82.1670, 88.2500, 94.3340, 100.4180, 106.5010, 112.5850, 118.6700, 124.7530, 137.0000, 157.1010, 177.1980, 197.2960 };

            ITemplateBasisFunction template = new TemplateLancsos(0.1, 5);
            IFunction<double, double> aif_function = new FunctionInterpolationLinear(tim, aif);

            DeconvolutionKrylov<Matrix<double>> decon = new DeconvolutionKrylov<Matrix<double>>(new AlgebraLinearReal64MathNet(), template, aif_function, tim);


            IFunction<double, double> irf = decon.GetIRF(tac);

        }
    }
}
