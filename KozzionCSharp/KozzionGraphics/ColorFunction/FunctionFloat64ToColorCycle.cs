using KozzionMathematics.Function;
using System;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionFloat64ToColorCycle : IFunction<double, System.Drawing.Color>
    {
        public string FunctionType { get { return "FunctionFloat64ToColorCycle"; } }

        public FunctionFloat64ToColorCycle()
        {
 
        }

        public static Color ComputeStatic(double value)
        {
            double weight_r = GetSpike(value, 0.0 / 3.0);
            double weight_g = GetSpike(value, 1.0 / 3.0);
            double weight_b = GetSpike(value, 2.0 / 3.0);

            return Color.FromArgb(255, (int)(255 * weight_r), (int)(255 * weight_g), (int)(255 * weight_b));
        }

        public Color Compute(double value)
        {
            return ComputeStatic(value);
        }

        private static double GetSpike(double value, double offset)
        {
            double distance = Math.Min(Math.Abs(value - offset), Math.Abs(value - 1 - offset));
            return Math.Max (0, 1 - (distance * 3.0));
        }
    }
}