using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct Meter
    {
        public double Value { get; private set; }

        public Meter(double value)
            : this()
        {
            this.Value = value;
        }

        public static implicit operator Meter(double operant_0)
        {
            return new Meter(operant_0);
        }

        public static explicit operator double(Meter operant_0)
        {
            return operant_0.Value;
        }

        public static Meter operator +(Meter operant_0, Meter operant_1)
        {
            return new Meter(operant_0.Value + operant_1.Value);
        }

        public static Meter operator -(Meter operant_0, Meter operant_1)
        {
            return new Meter(operant_0.Value + operant_1.Value);
        }

        public static Meter2 operator *(Meter operant_0, Meter operant_1)
        {
            return new Meter2(operant_0.Value * operant_1.Value);
        }

        public static double operator /(Meter operant_0, Meter operant_1)
        {
            return operant_0.Value / operant_1.Value;
        }

        public static Meter operator *(Meter operant_0, double operant_1)
        {
            return new Meter(operant_0.Value * operant_1);
        }

        public static Meter operator *(double operant_0, Meter operant_1)
        {
            return new Meter(operant_0 * operant_1.Value);
        }

        public static Meter operator /(Meter operant_0, double operant_1)
        {
            return new Meter(operant_0.Value / operant_1);
        }

        public override string ToString()
        {
            return (this.Value + "m");
        }

        public static bool operator <(Meter operant_0, Meter operant_1)
        {
            return operant_0.Value < operant_1.Value;
        }

        public static bool operator >(Meter operant_0, Meter operant_1)
        {
            return operant_0.Value > operant_1.Value;
        }
    }
}
