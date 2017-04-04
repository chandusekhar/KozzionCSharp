using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.Algebra;

using KozzionMathematics.Datastructure.Matrix;

namespace KozzionMathematics.Numeric.linear_solver
{
    public class SolverLinearGMRES<MatrixDataType> : ISolverLinear<MatrixDataType>
    {

        private IAlgebraLinear<MatrixDataType> algebra;
        private ISolverLinear<MatrixDataType> simple_solver;

        public SolverLinearGMRES(IAlgebraLinear<MatrixDataType> algebra)
        {
            this.algebra = algebra;
            this.simple_solver = algebra.GetSimpleSolver();
        }

        public AMatrix<MatrixDataType> Solve(AMatrix<MatrixDataType> A, AMatrix<MatrixDataType> b)
        {
            return Solve(A, b, b);
        }

        public AMatrix<MatrixDataType> Solve(AMatrix<MatrixDataType> A, AMatrix<MatrixDataType> b, AMatrix<MatrixDataType> initial)
        {
            return Solve(A, b, b, A.RowCount);
        }

        public AMatrix<MatrixDataType> Solve(AMatrix<MatrixDataType> A, AMatrix<MatrixDataType> b, AMatrix<MatrixDataType> x0, int max_iter)
        {
            // [solution, solutions, errors, norms] = gmres_simple(A, b, x0, kmax, chosen_solution)
            AMatrix<MatrixDataType> x = x0;
            AMatrix<MatrixDataType> r = b - (A * x);
            double rho0 = r.L2Norm();
            if (rho0 == 0)
            {
                return x0;
            }

            List<int> REPEATED = new List<int>();
            List<AMatrix<MatrixDataType>> solutions = new List<AMatrix<MatrixDataType>>();
            solutions.Add(x.Transpose());
            List <double> errors = new List<double>();
            errors.Add(rho0);         
            List<double> norms = new List<double>();
            norms.Add(x.L2Norm());

          
            // scale tol for relative residual reduction

            AMatrix<MatrixDataType> H = algebra.CreateZeros(1, 0);
            AMatrix<MatrixDataType> V = r / rho0;
            AMatrix<MatrixDataType> gamma = this.algebra.CreateZeros(1,1);
            gamma.SetElement(0,0,1);

            double nu = 1;
            for (int iteration = 0; iteration < max_iter; iteration++)
            {

                Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>> tuple = ArnoldiStep(A, V, H, REPEATED, - 1);
                V = tuple.Item1;
                H = tuple.Item2;
                // compute the nul vector of H'

                AMatrix<MatrixDataType> gk = ((gamma * H.GetColumnSection(0, iteration + 1, iteration)) / H.GetElement(iteration + 1, iteration));
                gamma = gamma.AppendColumns(gk * -1.0);

                // Compute the residual norm
                nu     = nu + (gk.Transpose() * gk).GetElement(); 
                errors.Add(rho0 / Math.Sqrt(nu));

                // compute explicit residual every step
                int k1 = H.ColumnCount;
                AMatrix<MatrixDataType> e  = this.algebra.CreateZeros(iteration + 2, 1); 
                e.SetElement(0,0,1);
                AMatrix<MatrixDataType> y  = simple_solver.Solve(H,e);
                AMatrix<MatrixDataType> x1 = V.GetColumns(0, iteration + 1) * (y * rho0);
                solutions.Add(x1);
                norms.Add(x1.L2Norm());
            }
            return solutions[solutions.Count - 1];
            // compute the approximate solution
            //solution  = solutions(chosen_solution,:);
        }

        public Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>> ArnoldiStep(
            AMatrix<MatrixDataType> A,
            AMatrix<MatrixDataType> V,
            AMatrix<MatrixDataType> H,
            List<int> REPEATED,
            double kappa = 0.2)
        {
            int k = V.ColumnCount - 1;
            AMatrix<MatrixDataType> v = A * V.GetColumn(k); 
            Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>> tuple = Orthogonalise(V, v, REPEATED, kappa);
            v                         = tuple.Item1;
            AMatrix<MatrixDataType> h = tuple.Item2;
            V                         = V.AppendColumns(v);
            H                         = (H.AppendRows(this.algebra.CreateZeros(1, k)).AppendColumns(h)); // TODO this is wrong
            return new Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>>(V, H);
        }



        private Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>> Orthogonalise(AMatrix<MatrixDataType> V, AMatrix<MatrixDataType> w, List<int> REPEATED, double kappa = 0)
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
            AMatrix<MatrixDataType> v = null;
            AMatrix<MatrixDataType> h = null;
            if (k >= V.RowCount)
            {
                kappa = 0.0;
            }

            if (k == 0)
            {
                h = algebra.Create(w.L2Norm());
                v = this.simple_solver.Solve(w, h);
                REPEATED = new List<int>();
                return new Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>>(v, h);
            }

            double zero = k * k * n * w.L2Norm() * double.Epsilon; // numerical zero
            double rho = 0;
            double mu = 0;
            v = w;

            if (kappa < 0)//modified Gram-Schmidt
            {
                h = algebra.CreateZeros(k, 1);

                for (int j = 0; j < k; j++)
                {
                    double value = (V.GetColumn(j).Transpose() * v).GetElement();
                    h.SetElement(j, 0, value);
                    v = v - V.GetColumn(j) * value;
                }
                rho = v.L2Norm();
            }
            else // repeated Gram-Schmidt
            {
                h = V.Transpose() * v;
                v = v - V * h;
                rho = v.L2Norm();
                mu = h.L2Norm();
                int t = 0;
                // Daniel Gragg Kaufman Stewart update criterion
                // if kappa==0, classical Gram-Schmidt
                while (rho < kappa * mu & rho > zero)
                {
                    AMatrix<MatrixDataType> g = V.Transpose() * v;
                    v = v - V * g;
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
                    v = this.algebra.CreateZeros(n, 0);
                    return new Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>>(v, h);
                }
                // if W in span(V) expand with random vector
                Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>> tuple = Orthogonalise(V, this.algebra.CreateRandomUnit(n, 1), REPEATED, kappa);
                v = tuple.Item1;
                h = h.AppendRow(0);
            }
            else
            {
                h = h.AppendRow(rho);
                v = v / rho;
            }
            return new Tuple<AMatrix<MatrixDataType>, AMatrix<MatrixDataType>>(v, h);
        }

     
    }
}
