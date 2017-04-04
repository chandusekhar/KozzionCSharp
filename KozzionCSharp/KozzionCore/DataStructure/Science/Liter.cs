using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct Liter
    {
        public double Value { get; private set; }

        public Liter(double value)
            : this()
        {
            this.Value = value;
        }

        public static explicit operator Liter(double value)
        {
            return new Liter(value);
        }

    }
}
