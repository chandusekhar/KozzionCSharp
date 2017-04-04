using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public class Second
    {
        public double Value { get; private set; }

        public Second(double value)
        {
            this.Value = value;
        }


        public static implicit operator Second(double operant_0)
        {
            return new Second(operant_0);
        }

        public static explicit operator double (Second operant_0)
        {
            return operant_0.Value;
        }
  

        public static Second operator +(Second operant_0, Second operant_1)
        {
            return new Second(operant_0.Value + operant_1.Value);
        }

        public static Second operator -(Second operant_0, Second operant_1)
        {
            return new Second(operant_0.Value + operant_1.Value);
        }

        public static MeterPerSecond operator /(Meter operant_0, Second operant_1)
        {
            return new MeterPerSecond(operant_0.Value / operant_1.Value);
        }

        public static Second operator *(Second operant_0, double operant_1)
        {
            return new Second(operant_0.Value * operant_1);
        }

        public static Second operator /(Second operant_0, double operant_1)
        {
            return new Second(operant_0.Value / operant_1);
        }
        public override string ToString()
        {
            return (this.Value + "m");
        }
    }
}
