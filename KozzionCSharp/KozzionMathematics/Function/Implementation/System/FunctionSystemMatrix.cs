using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Function.Implementation.System
{
    public class FunctionSystemMatrix : IFunction<double[], double[]>
    {
        public string FunctionType { get { return "FunctionSystemMatrix"; } }
        private double[,] system;

        public FunctionSystemMatrix(double[,] system)
        {
            this.system = system;
        }


        public double[] Compute(double[] domain_value_0)
        {
            double[] result = new double[system.GetLength(0)];
            for (int row_count = 0; row_count < system.GetLength(0); row_count++)
            {
                for (int column_count = 0; column_count < system.GetLength(1); column_count++)
                {
                    result[row_count] += domain_value_0[column_count] * system[row_count, column_count];
                }
            }
            return result;
        }
    }
}
