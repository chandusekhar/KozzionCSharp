using System.Numerics;

namespace KozzionCryptography.e_i_gamal
{
    public class EIGamalMessage
    {
        public BigInteger C1 { get; private set; }
        public BigInteger C2 { get; private set; }

        public EIGamalMessage(BigInteger c1, BigInteger c2) 
        {
            C1 = c1;
            C2 = c2;
        }
    }
}
