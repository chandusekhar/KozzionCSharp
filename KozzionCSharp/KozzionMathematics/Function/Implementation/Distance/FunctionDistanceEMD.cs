
using KozzionMathematics.Numeric.Intergral;
using KozzionMathematics.Tools;
using System;

namespace KozzionMathematics.Function.Implementation.Distance
{
    public class FunctionDistanceEMD :IFunctionDistance<double[], double>
    {
        public string FunctionType { get { return "FunctionDistanceEMD"; } }
        public double Compute(double[] values_0, double[] values_1)
        {
            double[] temp_0 = new double[values_0.Length];
            double[] temp_1 = new double[values_0.Length];
            double[] domain = new double[values_0.Length];		
            for (int index = 0; index < temp_0.Length; index++)
            {
                temp_0[index] = values_0[index] - values_1[index];
                domain[index] = index;
            }
            IntegralEvaluatorTrapezoid.EvaluateStaticSeriesRBA(domain, temp_0, temp_1);
            ToolsMathCollectionDouble.AbsRBA(temp_1);
            return IntegralEvaluatorTrapezoid.EvaluateStaticValue(domain, temp_1) / (domain[domain.Length - 1] - domain[0]);
        }

   

        public static double compute(
            double[] domain,
            double[] values_0,
            double[] values_1)
        {
            double[] temp_0 = new double[domain.Length];
            double[] temp_1 = new double[domain.Length];
            return compute(domain, values_0, values_1, temp_0, temp_1);
        }

        public static double compute(
            double[] domain,
            double[] values_0,
            double[] values_1,
            double[] temp_0,
            double[] temp_1)
        {
            double auc_0 = IntegralEvaluatorTrapezoid.EvaluateStaticValue(domain, values_0);
            double auc_1 = IntegralEvaluatorTrapezoid.EvaluateStaticValue(domain, values_1);
            for (int index = 0; index < temp_0.Length; index++)
            {
                temp_0[index] = (values_0[index] / auc_0) - (values_1[index] / auc_1);
            }
            IntegralEvaluatorTrapezoid.EvaluateStaticSeriesRBA(domain, temp_0, temp_1);
            ToolsMathCollectionDouble.AbsRBA(temp_1);
            return IntegralEvaluatorTrapezoid.EvaluateStaticValue(domain, temp_1) / (domain[domain.Length - 1] - domain[0]);

        }



        public double ComputeToRectangle(double[] valeu_0, double[] upper, double[] lower)
        {
            throw new NotImplementedException();
        }
    }
}