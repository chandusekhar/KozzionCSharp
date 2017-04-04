
using KozzionCore.Tools;
using System.Numerics;

namespace KozzionCryptography.Methods.LamportDiffie
{
    public class PrivateKeyLamportDiffie
    {
        private BigInteger[,] d_public_key;

        public PrivateKeyLamportDiffie(BigInteger[,] public_key)
        {
            d_public_key = public_key;
        }

        public BigInteger[,] KeyArray()
        {
            return ToolsArray.Copy(d_public_key);
        }
    }
}
