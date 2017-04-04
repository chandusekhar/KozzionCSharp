using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Science
{
    public struct AngleRadian
    {
        public double Value { get; private set; }

        public AngleRadian(double value)
            : this()
        {
            if (value < -Math.PI)
            {
                this.Value = -(-(value - Math.PI) % (Math.PI * 2)) + Math.PI;
            }
            else
            {
                this.Value = ((value + Math.PI) % (Math.PI * 2)) - Math.PI;
            }
        }

        public static explicit operator AngleRadian(double operant_0)
        {
            return new AngleRadian(operant_0);
        }

        public static explicit operator double (AngleRadian operant_0)
        {
            return operant_0.Value;
        }

        public static implicit operator AngleRadian(AngleDegree operant_0)
        {
            return new AngleRadian(operant_0.Value * (Math.PI / 180));
        }

        public static AngleRadian operator +(AngleRadian operant_0, AngleRadian operant_1)
        {
            return new AngleRadian(operant_0.Value + operant_1.Value);
        }

        public static AngleRadian operator -(AngleRadian operant_0, AngleRadian operant_1)
        {
            return new AngleRadian(operant_0.Value + operant_1.Value);
        }
    }
}
