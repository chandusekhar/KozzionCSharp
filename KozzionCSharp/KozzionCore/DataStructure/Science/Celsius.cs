using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct Celsius
    {
        public double Value { get; private set; }

        public Celsius(double value)
            : this()
        {
            Value = value;
        }

        public static explicit operator Celsius(double value)
        {
            return new Celsius(value);
        }

        public static implicit operator Celsius(Fahrenheit other)
        {
            return new Celsius((5.0f / 9.0f) * (other.Value - 32));
        }
    }
}
