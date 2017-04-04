using KozzionCore.Tools;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;

namespace KozzionMathematics.Numeric.system
{
    public class RungeKutta4<MatrixType> : ATimeInvariantIntegrator<MatrixType>
    {

        public override AMatrix<MatrixType> Integrate(IFunction<AMatrix<MatrixType>, AMatrix<MatrixType>> differntial, AMatrix<MatrixType> inital_value, double step_size)
        {
            AMatrix<MatrixType> k1 = differntial.Compute(inital_value) * (step_size / 6.0);
            AMatrix<MatrixType> k2 = differntial.Compute(inital_value + (k1 * 0.5)) * (step_size / 3.0);
            AMatrix<MatrixType> k3 = differntial.Compute(inital_value + (k2 * 0.5)) * (step_size / 3.0);
            AMatrix<MatrixType> k4 = differntial.Compute(inital_value + k3) * (step_size / 6.0);
            AMatrix<MatrixType> increment = k1 + k2 + k3 + k4;
            return inital_value + increment;
        }
    }

    public class RungeKutta4
    {
        public static double[] IntegrateStatic(IFunction<double[], double[]> differntial, double[] inital_value, double step_size)
        {
            double[] k1   = ToolsMathCollectionDouble.Multply(differntial.Compute(inital_value), step_size / 6.0);
            double[] temp = ToolsMathCollectionDouble.Add(inital_value, ToolsMathCollectionDouble.Multply(k1, 0.5));
            double[] k2   = ToolsMathCollectionDouble.Multply(differntial.Compute(temp), step_size / 3.0);
                    temp = ToolsMathCollectionDouble.Add(inital_value, ToolsMathCollectionDouble.Multply(k2, 0.5));
            double[] k3   = ToolsMathCollectionDouble.Multply(differntial.Compute(temp), step_size/ 3.0);
                    temp = ToolsMathCollectionDouble.Add(inital_value, k2);
            double[] k4   = ToolsMathCollectionDouble.Multply(differntial.Compute(temp), step_size / 6.0);
            ToolsCollection.CopyRBA(inital_value, temp);
            for (int index = 0; index < inital_value.Length; index++)
            {
                temp[index] += k1[index] + k2[index] +  k3[index] + k4[index];
            }
            return temp;
        }
    }

    public class RungeKutta4Time
    {
        public static double[] IntegrateStatic(IFunction<double[], double[], double[]> differntial, double[] history, double[] inital_value, double step_size)
        {
            double[] k1 = ToolsMathCollectionDouble.Multply(differntial.Compute(inital_value, history), step_size / 6.0);
            double[] temp = ToolsMathCollectionDouble.Add(inital_value, ToolsMathCollectionDouble.Multply(k1, 0.5));
            double[] k2 = ToolsMathCollectionDouble.Multply(differntial.Compute(temp, history), step_size / 3.0f);
            temp = ToolsMathCollectionDouble.Add(inital_value, ToolsMathCollectionDouble.Multply(k2, 0.5));
            double[] k3 = ToolsMathCollectionDouble.Multply(differntial.Compute(temp, history), step_size / 3.0f);
            temp = ToolsMathCollectionDouble.Add(inital_value, k2);
            double[] k4 = ToolsMathCollectionDouble.Multply(differntial.Compute(temp, history), step_size / 6.0f);
            ToolsCollection.CopyRBA(inital_value, temp);
            for (int index = 0; index < inital_value.Length; index++)
            {
                temp[index] += k1[index] + k2[index] + k3[index] + k4[index];
            }
            return temp;
        }
    }
}