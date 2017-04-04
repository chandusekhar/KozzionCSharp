using System.Numerics;

namespace KozzionCryptography.e_i_gamal
{
    public class EIGamalPrivateKey
    {
        public  BigInteger X {get; private set;}
        public BigInteger G { get; private set; }

        public EIGamalPrivateKey(BigInteger x, BigInteger g)
        {
            X = x;
            G = g;
        }
    }
}
