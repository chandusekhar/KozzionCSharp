using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Solver.LinearSolver
{
    public class SolverLinearResult
    {

        public List<Vector<double>> SolutionList { get; private set; }
        public bool IsHalted { get; private set; }

        public SolverLinearResult(List<Vector<double>> solution_list, bool is_halted)
        {
            SolutionList = solution_list;
            IsHalted = is_halted;
        }
    }
}
