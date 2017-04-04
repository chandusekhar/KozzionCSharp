using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;

namespace KozzionMathematics.Numeric.Solver.linear_solver
{
    public class SolverBiCGStab
    {

        public void solve()
        {
            Matrix<float> A = null;
            Vector<float> b = null;
            Vector<float> x = null;

            IIterationStopCriterion<float> count_stop_criterion = new IterationCountStopCriterion<float>(5000);
            IIterationStopCriterion<float> residual_stop_criterion = new ResidualStopCriterion<float>(1e-10f);
            Iterator<float> iterator = new Iterator<float>(count_stop_criterion, residual_stop_criterion);
            IPreconditioner<float> preconditioner = null;




            BiCgStab solver = new BiCgStab();
            solver.Solve(A, b, x,iterator, preconditioner);
        }


    }
}
