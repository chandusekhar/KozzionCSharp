using KozzionCore.Tools;
using KozzionMathematics.Tools;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Statistics.Distribution;

namespace KozzionMathematics.Statistics.Test.OneSample
{
    public class TestShapiroWilk : ATestOneSample
    {

        public override string TestName { get { return "TestNormaletyShapiroWilk"; } }

        public override string TestStatisticName { get { return "TestNormaletyShapiroWilk???"; } }

        public override TestRequirement[] Requirements
        {
            get
            {
                return new TestRequirement[] { };
            }
        }

        public override TestAssertion[] Assumptions
        {
            get
            {
                return new TestAssertion[] { };

                //throw new NotImplementedException();
            }
        }

        public override TestAssertion NullHypothesis { get { return TestAssertion.SamplesDrawnFromNormalDistribution; } }



        public override double Test(IList<double> sample)
        {
            return TestStatic(sample);
        }

        public static double TestStatic(IList<double> sample)
        {
            // as in http://nl.mathworks.com/matlabcentral/fileexchange/13964-shapiro-wilk-and-shapiro-francia-normality-tests/content/swtest.m
            //% SWTEST Shapiro - Wilk parametric hypothesis test of composite normality.
            //%   [H, pValue, SWstatistic] = SWTEST(X, ALPHA) performs the
            //% Shapiro - Wilk test to determine if the null hypothesis of
            //% composite normality is a reasonable assumption regarding the
            //% population distribution of a random sample X. The desired significance
            //% level, ALPHA, is an optional scalar input(default = 0.05).
            //%
            //% The Shapiro - Wilk and Shapiro-Francia null hypothesis is: 
            //% "X is normal with unspecified mean and variance."
            //%
            //% This is an omnibus test, and is generally considered relatively
            //% powerful against a variety of alternatives.
            //% Shapiro - Wilk test is better than the Shapiro - Francia test for
            //% Platykurtic sample. Conversely, Shapiro - Francia test is better than the
            //% Shapiro - Wilk test for Leptokurtic samples.
            //%
            //% If the sample is Leptokurtic performs the Shapiro - Francia
            //% If the sample is Platykurtic performs the Shapiro - Wilk test.
            //%
            //%
            //% Inputs:
            //% X - a vector of deviates from an unknown distribution.The observation
            //% number must exceed 3 and less than 5000.
            //%

            //% Outputs:
            //% pValue - is the p - value, or the probability of observing the given
            //% result by chance given that the null hypothesis is true. Small values
            //% of pValue cast doubt on the validity of the null hypothesis.
            //%
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //% Copyright(c) 17 March 2009 by Ahmed Ben Sada %
            //% Department of Finance, IHEC Sousse - Tunisia %
            //% Email: ahmedbensaida@yahoo.com %
            //%                    $ Revision 3.0 $ Date: 18 Juin 2014 $               %
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            //%
            //% References:
            //%
            //% -Royston P. "Remark AS R94", Applied Statistics(1995), Vol. 44,
            //% No. 4, pp. 547 - 551.
            //% AS R94-- calculates Shapiro - Wilk normality test and P - value
            //%   for sample sizes 3 <= n <= 5000.Handles censored or uncensored data.
            //% Corrects AS 181, which was found to be inaccurate for n > 50.
            //% Subroutine can be found at: http://lib.stat.cmu.edu/apstat/R94
            //%
            //% -Royston P. "A pocket-calculator algorithm for the Shapiro-Francia test
            //%   for non - normality: An application to medicine", Statistics in Medecine
            //% (1993a), Vol. 12, pp. 181 - 184.
            //%
            //% -Royston P. "A Toolkit for Testing Non-Normality in Complete and
            //% Censored Samples", Journal of the Royal Statistical Society Series D
            //% (1993b), Vol. 42, No. 1, pp. 37 - 43.
            //%
            //% -Royston P. "Approximating the Shapiro-Wilk W-test for non-normality",
            //% Statistics and Computing (1992), Vol. 2, pp. 117 - 119.
            //%
            //% -Royston P. "An Extension of Shapiro and Wilk's W Test for Normality
            //% to Large Samples", Journal of the Royal Statistical Society Series C
            //% (1982a), Vol. 31, No. 2, pp. 115 - 124.
            //%




            if (sample.Count < 3)
            {
                throw new Exception("Sample vector  must have at least 3 valid observations.");
            }

            if (5000 < sample.Count)
            {
                throw new Exception("Shapiro-Wilk test might be inaccurate due to large sample size ( > 5000).");
            }

            // % First, calculate the a's for weights as a function of the m's
            // % See Royston(1992, p. 117) and Royston (1993b, p. 38) for details
            // % in the approximation.

            ToolsCollection.Sort(sample);
            //% Sort the vector X in ascending order.
            int n = sample.Count;

            double[] mtilde = new double[n];
            for (int index = 0; index < n; index++)
            {
                mtilde[index] =Normal.InvCDF(0.0, 1.0, ((index + 1) - (3.0 / 8.0)) / (n + (1.0 / 4.0)));
            }

            double mtilde_in_product = 0.0;
            for (int index = 0; index < n; index++)
            {
                mtilde_in_product += mtilde[index] * mtilde[index];
            }

            double[] weights = new double[n]; //% Preallocate the weights.
            for (int index = 0; index < n; index++)
            {
                //sould say  weights = 1 / sqrt(mtilde'*mtilde) * mtilde;
                weights[index] = 1.0 / Math.Sqrt(mtilde_in_product) * mtilde[index];
            }


            double sample_mean = ToolsMathStatistics.Mean(sample);
            double kurtosis = ToolsMathStatistics.KurtosisPlain(sample);
            if (kurtosis > 3)
            {
                //% The Shapiro - Francia test is better for leptokurtic samples.
        
                //% The Shapiro - Francia statistic W' is calculated to avoid excessive
                //% rounding errors for W' close to 1 (a potential problem in very
                //% large samples).
                double sf_nom = Inproduct(sample, weights);
                double sf_denom = 0.0;
                for (int index = 0; index < n; index++)
                {
                    sf_denom += (sample[index] - sample_mean) * (sample[index] - sample_mean);
                }
                double W = (sf_nom * sf_nom) / sf_denom;

                //% Royston(1993a, p. 183):
                double nu = Math.Log(n);
                double u1 = Math.Log(nu) - nu;
                double u2 = Math.Log(nu) + 2 / nu;
                double mu = -1.2725 + (1.0521 * u1);
                double sigma = 1.0308 - (0.26758 * u2);

                double newSFstatistic = Math.Log(1 - W);

                //% Compute the normalized Shapiro - Francia statistic and its p-value.
                double NormalSFstatistic = (newSFstatistic - mu) / sigma;

                //% Computes the p-value, Royston(1993a, p. 183).             
                double pValue = 1 - Normal.CDF(0, 1, NormalSFstatistic);
                return pValue;
            }
            else
            {
                //% The Shapiro - Wilk test is better for platykurtic samples.
                double u = 1 / Math.Sqrt(n);

                //% Royston(1992, p. 117) and Royston(1993b, p. 38):
                double[] PolyCoef_1 = new double [] { -2.706056, 4.434685, -2.071190, -0.147981, 0.221157, weights[n - 1]}; //TODO check was  weights[n]
                double[] PolyCoef_2 = new double [] { -3.582633, 5.682633, -1.752461, -0.293762, 0.042981, weights[n - 2]}; //TODO check was  weights[n - 1]

                //% Royston(1992, p. 118) and Royston (1993b, p. 40, Table 1)
                double[] PolyCoef_3 = new double [] { -0.0006714, 0.0250540, -0.39978, 0.54400 };
                double[] PolyCoef_4 = new double [] { -0.0020322, 0.0627670, -0.77857, 1.38220 };
                double[] PolyCoef_5 = new double [] { 0.00389150, -0.083751, -0.31082, -1.5861 };
                double[] PolyCoef_6 = new double [] { 0.00303020, -0.082676, -0.48030 };

                double[] PolyCoef_7 = new double[] { 0.459, -2.273 };

                weights[n -1] = Polyval(PolyCoef_1, u);
                weights[1] = -weights[n -1];

                int count = 0;
                double phi = 0.0;
                if (n > 5)
                {
                    weights[n - 2] = Polyval(PolyCoef_2, u);
                    weights[2] = -weights[n -2]; //TODO check n - 1
                    count = 3;
                    phi = (Inproduct(mtilde, mtilde) - 2 * Math.Pow(mtilde[n -1],2) - 2 * Math.Pow(mtilde[n-2],2)) / (1 - 2 * Math.Pow(weights[n -1],2) - 2 * Math.Pow(weights[n - 2], 2));
                }
                else
                {
                    count = 2;
                    phi = (Inproduct(mtilde, mtilde) - 2 * Math.Pow(mtilde[n -1],2)) / (1 - 2 * Math.Pow(weights[n -1],2));
                }

                //% Special attention when n = 3(this is a special case).
                if (n == 3)
                {
                    //% Royston(1992, p. 117)
                    weights[1] = 1 / Math.Sqrt(2);
                    weights[n - 1] = -weights[1];
                    phi = 1;
                }


                // % The vector 'WEIGHTS' obtained next corresponds to the same coefficients
                // % listed by Shapiro-Wilk in their original test for small samples.

                for (int index = count; index < n - count; index++)
                {
                    weights[index] = mtilde[index] / Math.Sqrt(phi);
                }


                //% The Shapiro - Wilk statistic W is calculated to avoid excessive rounding
                //% errors for W close to 1(a potential problem in very large samples).
                double[] residual = ToolsMathCollectionDouble.Subtract(sample, sample_mean);
                double W = Math.Pow(Inproduct(weights, sample), 2) / Inproduct(residual, residual);

                //%
                //% Calculate the normalized W and its significance level(exact for
                //% n = 3).Royston(1992, p. 118) and Royston (1993b, p. 40, Table 1).
                //%

                double newn = Math.Log(n);
                double mu = 0.0;
                double sigma = 0.0;
                double gam = 0.0;
                double newSWstatistic = 0.0;
                if (n > 11)
                {
                    mu = Polyval(PolyCoef_5, newn);
                    sigma = Math.Exp(Polyval(PolyCoef_6, newn));
                    newSWstatistic = Math.Log(1 - W);
                }
                else if ((n >= 4) && (n <= 11))
                {

                    mu = Polyval(PolyCoef_3, n);
                    sigma = Math.Exp(Polyval(PolyCoef_4, n));
                    gam = Polyval(PolyCoef_7, n);

                    newSWstatistic = -Math.Log(gam - Math.Log(1 - W));
                }
                else if (n == 3)
                {
                    mu = 0;
                    sigma = 1;
                    newSWstatistic = 0;
                }


                //% Compute the normalized Shapiro - Wilk statistic and its p-value.
                double NormalSWstatistic = (newSWstatistic - mu) / sigma;

                //% NormalSWstatistic is referred to the upper tail of N(0, 1),
                //% Royston(1992, p. 119).
                double pValue = 1 - Normal.CDF(mu, sigma, newSWstatistic);

                //% Special attention when n = 3(this is a special case).
                if (n == 3)
                {
                    pValue = 6 / Math.PI * (Math.Asin(Math.Sqrt(W)) - Math.Asin(Math.Sqrt(3.0 / 4.0)));
                    //% Royston(1982a, p. 121)
                }
                return pValue;
            }
        }

        private static double Inproduct(IList<double> operant_0, IList<double> operant_1)
        {
            double result = 0;
            for (int index = 0; index < operant_0.Count; index++)
            {
                result += operant_0[index] * operant_1[index];
            }
            return result;
        }

        // The polynomial p(x)= 3x^2 + 2x + 1 is evaluated at x = 5 with polyval([3,2,1], 5)
        private static double Polyval(double[] coefficeients, double domain_value)
        {
            double [] coefficeients_reverse = ToolsCollection.Copy(coefficeients);
            Array.Reverse(coefficeients_reverse);
            double result = 0;
            for (int index = 0; index < coefficeients.Length; index++)
            {
                result += Math.Pow(domain_value, index) * coefficeients[index];
            }
            return result;
        }


    }
}
