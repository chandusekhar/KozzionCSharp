using System;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.HelperFunctions.TweenerFunctions
{
    public class FunctionLinearDecay :IFunction<int, float>
    {
        public string FunctionType { get { return "FunctionLinearDecay"; } }
        float intial_value;
        float decay_factor;

        public FunctionLinearDecay()
        {

        }

        public FunctionLinearDecay(float intial_value, float decay_time)
        {
            this.intial_value = intial_value;
            decay_factor = 1 / decay_time;
        }

        public float Compute(int value_domain)
        {
            return Math.Min(Math.Max(intial_value - decay_factor * value_domain, 0), intial_value);
        }
    }
}
