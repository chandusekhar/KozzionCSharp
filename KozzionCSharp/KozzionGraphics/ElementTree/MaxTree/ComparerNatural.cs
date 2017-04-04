using System;
using System.Collections.Generic;

namespace KozzionGraphics.ElementTree.MaxTree
{
    [Serializable]
    public class ComparerNatural<DomainType> : IComparer<DomainType>
        where DomainType :IComparable<DomainType>
    {
        private bool reverse;


        public ComparerNatural(bool reverse)
        {
            this.reverse = reverse;
        }


        public ComparerNatural()
            : this(false)
        {

        }


        public int Compare(DomainType value_0, DomainType value_1)
        {
            if (this.reverse)
            {
                return value_1.CompareTo(value_0);            
            }
            else
            {
                return value_0.CompareTo(value_1);    
            }
        }
    }
}
