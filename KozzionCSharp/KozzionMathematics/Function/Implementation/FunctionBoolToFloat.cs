namespace KozzionMathematics.Function.Implementation
{
    public class FunctionBoolToFloat : IFunction<bool, float>
    {
        public string FunctionType { get { return "FunctionBoolToFloat"; } }
        private float false_value;
        private float true_value;

        public FunctionBoolToFloat(float false_value, float true_value)
        {
            this.false_value = false_value;
            this.true_value = true_value;
        }

        public float Compute(bool value_domain)
        {
            if (value_domain)
            {
                return this.true_value;
            }
            else
            {
                return this.false_value;
            }
        }
    }
}
