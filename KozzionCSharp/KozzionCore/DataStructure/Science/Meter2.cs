using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct Meter2
    {
        public double Value { get; private set; }

        public Meter2(double value)
            : this()
        {
            this.Value = value;
        }

        public static explicit operator Meter2(double value)
        {
            return new Meter2(value);
        }

    }
}
