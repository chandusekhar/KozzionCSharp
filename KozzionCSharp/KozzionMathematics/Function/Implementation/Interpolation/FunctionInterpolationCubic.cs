using KozzionCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Function.Implementation.Interpolation
{
    //TODO check with Math.NET
    //TODO check with matlab
    public class FunctionInterpolationCubic : IFunction<double, double>
    {
        public string FunctionType { get { return "FunctionInterpolationCubic"; } }

        private double[] a;
        private double[] b;

        // Save the original x and y for Eval
        private double[] domain;
        private double[] range;

        public FunctionInterpolationCubic(double[] domain, double[] range)
        {
            if (domain.Length == 0)
            {
                throw new Exception("Array length cannot be 0");
            }

            if (domain.Length != range.Length)
            {
                throw new Exception("Array lengths do not match");
            }
            this.domain = ToolsCollection.Copy(domain);
            this.range = ToolsCollection.Copy(range);

            Fit();
        }

        public void Fit()
        {
            double startSlope = double.NaN;
            double endSlope = double.NaN;

            // Save x and y for eval

            int n = domain.Length;
            double[] r = new double[n]; // the right hand side numbers: wikipedia page overloads b

            TriDiagonalMatrixF m = new TriDiagonalMatrixF(n);
            double dx1, dx2, dy1, dy2;

            // First row
            if (double.IsNaN(startSlope))
            {
                dx1 = domain[1] - domain[0];
                m.C[0] = 1.0 / dx1;
                m.B[0] = 2.0 * m.C[0];
                r[0] = 3 * (range[1] - range[0]) / (dx1 * dx1);
            }
            else
            {
                m.B[0] = 1;
                r[0] = startSlope;
            }

            // Centre rows
            for (int i = 1; i < n - 1; i++)
            {
                dx1 = domain[i] - domain[i - 1];
                dx2 = domain[i + 1] - domain[i];

                m.A[i] = 1.0 / dx1;   
                m.B[i] = 2.0 * (m.A[i] + m.C[i]);
                m.C[i] = 1.0 / dx2;

                dy1 = range[i] - range[i - 1];
                dy2 = range[i + 1] - range[i];
                r[i] = 3 * (dy1 / (dx1 * dx1) + dy2 / (dx2 * dx2));
            }

            // Last row
            if (double.IsNaN(endSlope))
            {
                dx1 = domain[n - 1] - domain[n - 2];
                dy1 = range[n - 1] - range[n - 2];
                m.A[n - 1] = 1.0 / dx1;
                m.B[n - 1] = 2.0 * m.A[n - 1];
                r[n - 1] = 3 * (dy1 / (dx1 * dx1));
            }
            else
            {
                m.B[n - 1] = 1;
                r[n - 1] = endSlope;
            }






            // k is the solution to the matrix
            double[] k = m.Solve(r);

            // a and b are each spline's coefficients
            this.a = new double[n - 1];
            this.b = new double[n - 1];

            for (int i = 1; i < n; i++)
            {
                dx1 = domain[i] - domain[i - 1];
                dy1 = range[i] - range[i - 1];
                a[i - 1] = k[i - 1] * dx1 - dy1; // equation 10 from the article
                b[i - 1] = -k[i] * dx1 + dy1; // equation 11 from the article
            }
        }

        private int GetSplineIndex(double querry, int index_start)
        {
            while ((index_start < domain.Length - 2) && (querry > domain[index_start + 1]))
            {
                index_start++;
            }
            return index_start;
        }

        public double Compute(double domain)
        {
            return Compute(domain, GetSplineIndex(domain, 0));
        }

        public double[] Compute(double[] x)
        {

            double[] y = new double[x.Length];
            int spline_index = 0; // Reset simultaneous traversal in case there are multiple calls

            for (int i = 0; i < x.Length; i++)
            {
                // Find which spline can be used to compute this x (by simultaneous traverse)
                spline_index = GetSplineIndex(x[i], spline_index);

                // Evaluate spline
                y[i] = Compute(x[i], spline_index);
            }

            return y;
        }

        private double Compute(double x, int index_spline)
        {
            double dx = domain[index_spline + 1] - domain[index_spline];
            double t = (x - domain[index_spline]) / dx;
            double y = (1 - t) * range[index_spline] + t * range[index_spline + 1] + t * (1 - t) * (a[index_spline] * (1 - t) + b[index_spline] * t); // equation 9
            return y;
        }


        public double[] ComputeSlope(double[] x)
        {
            double[] slope = new double[x.Length];
            int index_spline = 0; // Reset simultaneous traversal in case there are multiple calls

            for (int index = 0; index < x.Length; index++)
            {
                // Find which spline can be used to compute this x (by simultaneous traverse)
                index_spline = GetSplineIndex(x[index], index_spline);

                // Evaluate using j'th spline
                double dx = domain[index_spline + 1] - domain[index_spline];
                double dy = range[index_spline + 1] - range[index_spline];
                double t = (x[index] - domain[index_spline]) / dx;

                // From equation 5 we could also compute q' (qp) which is the slope at this x
                slope[index] = dy / dx
                    + (1 - 2 * t) * (a[index_spline] * (1 - t) + b[index_spline] * t) / dx
                    + t * (1 - t) * (b[index_spline] - a[index_spline]) / dx;
            }

            return slope;
        }

        public static double[] Compute(double[] x, double[] y, double[] xs)
        {
            FunctionInterpolationCubic spline = new FunctionInterpolationCubic(x, y);
            return spline.ComputeSlope(xs);
        }     


        //TODO ref is two-ways, out is out-only
        private static void NormalizeVector(ref double dx, ref double dy)
        {
            if (!double.IsNaN(dx) && !double.IsNaN(dy))
            {
                double d = (double)Math.Sqrt(dx * dx + dy * dy);

                if (d > double.Epsilon) // probably not conservative enough, but catches the (0,0) case at least
                {
                    dx = dx / d;
                    dy = dy / d;
                }
                else
                {
                    throw new ArgumentException("The input vector is too small to be normalized.");
                }
            }
            else
            {
                // In case one is NaN and not the other
                dx = dy = double.NaN;
            }
        }
    }
}
