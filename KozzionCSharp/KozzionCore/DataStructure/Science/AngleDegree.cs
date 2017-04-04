using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct AngleDegree
    {
        public double Value { get; private set; }

        public AngleDegree(double value)
            : this()
        {
     
            if (value < -180)
            {
                this.Value = -(-(value - 180) % 360) + 180;
            }
            else
            {
                this.Value = ((value + 180) % 360) - 180;
            }
        }

        public static explicit operator AngleDegree(double operant_0)
        {
            return new AngleDegree(operant_0);
        }

        public static explicit operator double (AngleDegree operant_0)
        {
            return operant_0.Value;
        }

        public static implicit operator AngleDegree(AngleRadian operant_0)
        {
            return new AngleDegree(operant_0.Value * (180.0 / Math.PI));
        }

        public static AngleDegree operator +(AngleDegree operant_0, AngleDegree operant_1)
        {
            return new AngleDegree(operant_0.Value + operant_1.Value);
        }

        public static AngleDegree operator -(AngleDegree operant_0, AngleDegree operant_1)
        {
            return new AngleDegree(operant_0.Value + operant_1.Value);
        }

    }
}
