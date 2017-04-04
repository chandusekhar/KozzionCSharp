namespace KozzionMathematics.Numeric.Signal.Windowing
{
    public class RectangularWindowFloat : AWindowFloat
    {
        public RectangularWindowFloat(float filter_width)
            		: base(filter_width)
        {
          
        }

        protected override float ComputeInside(float input)
        {
            return 1;
        }
    }
}