using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Solver.LinearSolver
{
    public class SolverLinearGMRESMathNet
    {

        public SolverLinearGMRESMathNet()
        {

        }

        public SolverLinearResult Solve(Matrix<double> A, Vector<double> b)
        {
            return Solve(A, b, b);
        }

        public SolverLinearResult Solve(Matrix<double> A, Vector<double> b, Vector<double> initial)
        {
            return Solve(A, b, b, A.RowCount);
        }

        public SolverLinearResult Solve(Matrix<double> A, Vector<double> b, Vector<double> x, int max_iter)
        {

            List<Vector<double>> solution_list = new List<Vector<double>>();
            solution_list.Add(x);

            Vector<double> r = b - (A * x);
            double rho0 = r.L2Norm();
            if (rho0 == 0)
            {
                return new SolverLinearResult(solution_list, false);
            }

            List<int> REPEATED = new List<int>();
  
            List<double> errors = new List<double>();
            errors.Add(rho0);
            List<double> norms = new List<double>();
            norms.Add(x.L2Norm());


            // scale tol for relative residual reduction

            Matrix<double> H = null;//new DenseMatrix(1, 0);
            Matrix<double> V = new DenseMatrix(r.Count,1, (r / rho0).ToArray());
            Vector<double> gamma = new DenseVector(1);
            gamma[0] =  1;

            double nu = 1;
            for (int iteration = 0; iteration < max_iter; iteration++)
            {

                Tuple<Matrix<double>, Matrix<double>> tuple = ArnoldiStep(A, V, H, REPEATED, -1);
                V = tuple.Item1;
                H = tuple.Item2;

                // compute the nul vector of H'
                double gk = ((gamma * H.SubMatrix(0, iteration + 1, iteration,1).Column(0)) / H[iteration + 1, iteration]);
                gamma = AppendElement(gamma, -gk);

                // Compute the residual norm
                nu = nu + (gk * gk);
                errors.Add(rho0 / Math.Sqrt(nu));

                // TODO compute explicit solution and residual every step (no esential if we have an error only halt)
                Vector<double> e = new DenseVector(iteration + 2); 
                e[0] = 1;
                Vector<double> y = H.Solve(e);
                x = V.SubMatrix(0, V.RowCount, 0, iteration + 1) * (y * rho0);
                solution_list.Add(x);
                norms.Add(x.L2Norm());
            }
            return new SolverLinearResult(solution_list, false);
            // compute the approximate solution
            //solution  = solutions(chosen_solution,:);
        }

        private Vector<double> AppendElement(Vector<double> vector, double element)
        {
            Vector<double> result = new DenseVector(vector.Count + 1);
            result.SetSubVector(0, vector.Count, vector);
            result[vector.Count] = element;
            return result;
        }

        public Tuple<Matrix<double>, Matrix<double>> ArnoldiStep(
            Matrix<double> A,
            Matrix<double> V,
            Matrix<double> H,
            List<int> REPEATED,
            double kappa = 0.2)
        {
            int k = V.ColumnCount - 1;
            Vector<double> v = A * V.Column(k);
            Tuple<Vector<double>, Vector<double>> tuple = Orthogonalise(V, v, REPEATED, kappa);
            v = tuple.Item1;
            Vector<double> h = tuple.Item2;
            V = V.InsertColumn(V.ColumnCount, v);

            if (H == null)
            {
                Matrix<double> H_new = new DenseMatrix(2, 1);
                H_new.SetColumn(H_new.ColumnCount - 1, h);
                return new Tuple<Matrix<double>, Matrix<double>>(V, H_new);
            }
            else
            {
                Matrix<double> H_new = Matrix<double>.Build.Dense(H.RowCount + 1, H.ColumnCount + 1);
                H_new.SetSubMatrix(0, 0, H);
                H_new.SetColumn(H_new.ColumnCount - 1, h);
                return new Tuple<Matrix<double>, Matrix<double>>(V, H_new);
            }

        }



        private Tuple<Vector<double>, Vector<double>> Orthogonalise(Matrix<double> V, Vector<double> w, List<int> REPEATED, double kappa = 0)
        {
            //  w=[V, v]* h; with v such that v'*V=0;
            //
            //  kappa=0: classical Gram-Schmidt
            //  kappa>0: repeated Gram-Schmidt with DGKS stopping criterion
            //           repeat if tan(angle)<kappa
            //  kappa<0: modified Gram-Schmidt
            //

            // global REPEATED
            int n = V.RowCount;
            int k = V.ColumnCount;
            Vector<double> v = null;
            Vector<double> h = null;
            if (k >= V.RowCount)
            {
                kappa = 0.0;
            }

            if (k == 0)
            {
                h = new DenseVector(new double[] { w.L2Norm() });
                //v = this.simple_solver.Solve(w, h); //lookup in original code
                REPEATED = new List<int>();
                return new Tuple<Vector<double>, Vector<double>>(v, h);
            }

            double zero = k * k * n * w.L2Norm() * double.Epsilon; // numerical zero
            double rho = 0;
            double mu = 0;
            v = w;

            if (kappa < 0)//modified Gram-Schmidt
            {
                h = new DenseVector(k);

                for (int j = 0; j < k; j++)
                {
                    double value = (V.Column(j) * v);
                    h[j] = value;
                    v = v - V.Column(j) * value;
                }
                rho = v.L2Norm();
            }
            else // repeated Gram-Schmidt
            {
                h = V * v;
                v = v - V * h;
                rho = v.L2Norm();
                mu = h.L2Norm();
                int t = 0;
                // Daniel Gragg Kaufman Stewart update criterion
                // if kappa==0, classical Gram-Schmidt
                while (rho < kappa * mu & rho > zero)
                {
                    Vector<double> g = V.Transpose() * v;
                    v = v - (V * g);
                    rho = v.L2Norm();
                    mu = g.L2Norm();
                    h = h + g;
                    t = t + 1;
                }
                REPEATED[V.ColumnCount] = t;
            }

            if (rho < 2 * zero)
            {
                // if full dimension then no expansion
                if (k >= n)
                {
                    v = new DenseVector(n);
                    return new Tuple<Vector<double>, Vector<double>>(v, h);
                }
                // if W in span(V) expand with random vector
                Tuple<Vector<double>, Vector<double>> tuple = Orthogonalise(V, Matrix<double>.Build.Random(1, n).Row(0), REPEATED, kappa);
                v = tuple.Item1;
                h = AppendElement(h, 0);
            }
            else
            {
                h = AppendElement(h, rho);
                v = v / rho;
            }
            return new Tuple<Vector<double>, Vector<double>>(v, h);
        }
    }
}
