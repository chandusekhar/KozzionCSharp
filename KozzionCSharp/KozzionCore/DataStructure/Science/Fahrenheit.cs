using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public class Fahrenheit
    {
        public double Value { get; private set; }

        public Fahrenheit(double value)
        {
            Value = value;
        }

        public static explicit operator Fahrenheit(double value)
        {
            return new Fahrenheit(value);
        }

        public static explicit operator Fahrenheit(Celsius other)
        {
            return new Fahrenheit(((9.0f / 5.0f) * other.Value) + 32);
        }
    }
}
