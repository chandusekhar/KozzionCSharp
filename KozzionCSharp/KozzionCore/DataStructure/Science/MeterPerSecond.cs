namespace KozzionCore.DataStructure.Science
{
    public struct MeterPerSecond
    {
        public double Value { get; private set; }

        public MeterPerSecond(double value)
            : this()
        {
            this.Value = value;
        }

        public static explicit operator MeterPerSecond(double value)
        {
            return new MeterPerSecond(value);
        }


        public override string ToString()
        {
            return (this.Value + "m/s");
        }
    }
}