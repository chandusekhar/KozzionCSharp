using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Vector
{
    public struct Vector3Int32
    {
        public int Element0 { get; private set; }
        public int Element1 { get; private set; }
        public int Element2 { get; private set; }

        public Vector3Int32(int element_0, int element_1, int element_2)
        {
            Element0 = element_0;
            Element1 = element_1;
            Element2 = element_2;
        }
    }
}
