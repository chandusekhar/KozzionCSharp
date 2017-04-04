using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure
{

    public abstract class IdString
    {
        private string id;

        public IdString(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }
        public override bool Equals(object other)
        {
            if (EqualsType(other))
            {
                return ((IdString)other).id.Equals(id);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public abstract bool EqualsType(object other);
    }
}
