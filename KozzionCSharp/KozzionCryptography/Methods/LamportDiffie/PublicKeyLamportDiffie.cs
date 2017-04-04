
using KozzionCore.Tools;
using System.Numerics;

namespace KozzionCryptography.Methods.LamportDiffie
{
    public class PublicKeyLamportDiffie
    {
        private BigInteger[,] d_private_key;

        public PublicKeyLamportDiffie(System.Numerics.BigInteger[,] private_key)
        {
            d_private_key = private_key;
        }

        public BigInteger[,] KeyArray()
        {
            return ToolsArray.Copy(d_private_key);
        }
    }
}
