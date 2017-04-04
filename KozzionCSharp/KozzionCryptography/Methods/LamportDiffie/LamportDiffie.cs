using KozzionMathematics.Function;
using System;
using System.Numerics;
using System.Security.Cryptography;

namespace KozzionCryptography.Methods.LamportDiffie
{
    public class LamportDiffie
    {
        RandomNumberGenerator d_random;
        IFunction<BigInteger, BigInteger> d_hash_key;
        IFunction<BigInteger, BigInteger> d_hash_message;

        public LamportDiffie(RandomNumberGenerator random, 
            IFunction<BigInteger, BigInteger> hash_key,
            IFunction<BigInteger, BigInteger> hash_message) 
        {
            d_random = random;
            d_hash_key = hash_key;
            d_hash_message = hash_message;
        }

        public Tuple<PublicKeyLamportDiffie, PrivateKeyLamportDiffie> GenerateKeys()
        {
            BigInteger[,] private_key_array = new BigInteger[256, 2];
            BigInteger[,] public_key_array = new BigInteger[256, 2];
            for (int index_key = 0; index_key < private_key_array.GetLength(0); index_key++)
			{
                private_key_array[index_key, 0] = d_random.RandomPositiveBigIntegerOfSizeInBits(256);
                private_key_array[index_key, 1] = d_random.RandomPositiveBigIntegerOfSizeInBits(256);
                public_key_array[index_key, 0] = d_hash_key.Compute(private_key_array[index_key, 0]);
                public_key_array[index_key, 1] = d_hash_key.Compute(private_key_array[index_key, 1]);
			}
            PublicKeyLamportDiffie public_key = new PublicKeyLamportDiffie(private_key_array);
            PrivateKeyLamportDiffie private_key = new PrivateKeyLamportDiffie(public_key_array);
            return new Tuple<PublicKeyLamportDiffie, PrivateKeyLamportDiffie>(public_key, private_key);
        }

        public BigInteger [] Sign(BigInteger message, PrivateKeyLamportDiffie private_key)
        {
            BigInteger message_hash = d_hash_message.Compute(message);
            BigInteger [] signature = new BigInteger[256];
            BigInteger[,] key_array = private_key.KeyArray();
            for (int index_key = 0; index_key < signature.Length; index_key++)
			{
                if (message_hash.IsBitSet(index_key))// check bit is set)
                {
                    signature[index_key] = key_array[index_key, 0];
                }
                else
                {
                    signature[index_key] = key_array[index_key, 1];
                }
            }
            return signature;
        }

        public bool Verify(BigInteger message, BigInteger[] signature, PublicKeyLamportDiffie public_key)
        {
            BigInteger message_hash = d_hash_message.Compute(message);
            BigInteger[,] key_array = public_key.KeyArray();
            for (int index_key = 0; index_key < signature.Length; index_key++)
            {
                if (message_hash.IsBitSet(index_key))// check bit is set)
                {
                    if(d_hash_key.Compute(signature[index_key]) != key_array[index_key, 0])
                    {
                        return false;
                    }
                }
                else
                {
                    if (d_hash_key.Compute(signature[index_key]) != key_array[index_key, 1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
