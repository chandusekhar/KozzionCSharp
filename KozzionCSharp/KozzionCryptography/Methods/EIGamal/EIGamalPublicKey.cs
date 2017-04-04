using System.Numerics;

namespace KozzionCryptography.e_i_gamal
{
    public class EIGamalPublicKey
    {
        public BigInteger H { get; private set; }
        public BigInteger Q { get; private set; }
        public BigInteger G { get; private set; }

        public EIGamalPublicKey(BigInteger g, BigInteger q, BigInteger h)
        {
            G = g;
            Q = q;
            H = h;
        }
    }
}
