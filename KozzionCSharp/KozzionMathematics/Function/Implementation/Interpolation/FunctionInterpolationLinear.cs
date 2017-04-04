using KozzionCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Function.Implementation.Interpolation
{
    public class FunctionInterpolationLinear : IFunction<double, double>
    {
        public string FunctionType { get { return "FunctionInterpolationLinear"; } }
        private double[] domain;
        private double[] values;

        public FunctionInterpolationLinear(double [] domain_sorted, double[] values_sorted)
        {
            if (domain_sorted.Length == 0)
            {
                throw new Exception("Array length cannot be 0");
            }

            if (domain_sorted.Length != values_sorted.Length)
            {
                throw new Exception("Array lengths do not match");
            }

            this.domain = ToolsCollection.Copy(domain_sorted); //Asume sorted
            this.values = ToolsCollection.Copy(values_sorted); //Asume sorted
        }

        public double Compute(double domain_value_0)
        {
            if (domain_value_0 <= domain[0])
            {
                return values[0];
            }

            if (this.domain[domain.Length - 1] <= domain_value_0)
            {
                return values[values.Length - 1];
            }

            int domain_index = 0;
            while (domain [domain_index] < domain_value_0)
            {
                domain_index++;
            }
            double distance = domain[domain_index] - domain[domain_index - 1];
            double lower_weight = (domain[domain_index] - domain_value_0) / distance;
            return (values[domain_index] * (1 - lower_weight)) + (values[domain_index -1] * (lower_weight));
        }
    }
}
