using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Numeric.Minimizer
{
    public class MinimizerResult
    {
        public List<double[]> EvaluationList { get; private set; }
        public List<double> ValueList { get; private set; }
        public List<int> IterationList { get; private set; }
        public Simplex Simplex { get; private set; }

        public bool IsSuccesFull { get; set; }
        public bool IsHalted { get; set; }

        public MinimizerResult(Simplex simplex)
        {
            EvaluationList = new List<double[]>();
            ValueList = new List<double>();
            IterationList = new List<int>();
            Simplex = simplex;
            IsSuccesFull = false;
            IsHalted = false;
        }

        public MinimizerResult(
            List<double[]> evaluation_list , 
            List<double> value_list,
            List<int> iteration_list,
            Simplex final_simplex,
            bool is_succes_full,
            bool is_halted)
        {
            this.EvaluationList = evaluation_list;
            this.ValueList = value_list;
            this.IterationList = iteration_list;
            this.Simplex = final_simplex;
            this.IsSuccesFull = is_succes_full;
            this.IsHalted = is_halted;
        }
    }
}
