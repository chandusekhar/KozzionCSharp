using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct Kelvin
    {
        public double Value { get; private set; }

        public Kelvin(double value)
            : this()
        {
            Value = value;
        }

        public static explicit operator Kelvin(double value)
        {
            return new Kelvin(value);
        }

        public static explicit operator Kelvin(Celsius other)
        {
            return new Kelvin(other.Value + 273.15);
        }
    }
}
