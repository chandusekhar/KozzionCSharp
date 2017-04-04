using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using KozzionCryptography.e_i_gamal;
using KozzionCryptography.Methods.e_i_gamal;

namespace KozzionCryptography
{
    public class IEGamal
    {
        //http://en.wikipedia.org/wiki/ElGamal_encryption


        public Tuple<EIGamalPublicKey, EIGamalPrivateKey> GenerateKeyPair(CyclicGroup<BigInteger> group, RandomNumberGenerator random)
        {
            // Generate Group (public key)
            BigInteger g = 0; //generator
            BigInteger q = 0; //order   

            BigInteger x = random.RandomPositiveBigIntegerBelow(g);
            BigInteger h = g.Pow(x); //share secrey primer
            return new Tuple<EIGamalPublicKey, EIGamalPrivateKey>(new EIGamalPublicKey(g, q, h), new EIGamalPrivateKey(x, g));
        }

        public Tuple<EIGamalPublicKey, EIGamalPrivateKey> GenerateKeyPair(BigInteger order, BigInteger generator, RandomNumberGenerator random)
        {
            // Generate Group (public key)
            BigInteger g = 0; //generator
            BigInteger q = 0; //order   

            BigInteger x = random.RandomPositiveBigIntegerBelow(g);
            BigInteger h = g.Pow(x); //share secrey primer
            return new Tuple<EIGamalPublicKey, EIGamalPrivateKey>(new EIGamalPublicKey(g, q, h), new EIGamalPrivateKey(x, g));
        }

        public List<EIGamalMessage> Encrypt(byte [] message, EIGamalPublicKey public_key, RandomNumberGenerator random) 
        {     
            List<BigInteger> messages = null;
            //TODO convert byte [] to from group
            return Encrypt(messages, public_key, random);
        }


        public byte[] DeCrypt(List<EIGamalMessage> messages_ecrypted, EIGamalPrivateKey private_key)
        {
            List<BigInteger> messages_decrypted = Decrypt(messages_ecrypted, private_key);

            //TODO convert from group to byte []
            return null;
        }

        private List<EIGamalMessage> Encrypt(List<BigInteger> messages, EIGamalPublicKey public_key, RandomNumberGenerator random)
        {
            //Encryption
            List<EIGamalMessage> messages_encrypted = new List<EIGamalMessage>();
            foreach (BigInteger message in messages)
            {
                BigInteger y = random.RandomPositiveBigIntegerBelow(public_key.Q); // ephemeral key a unique one for every message
                BigInteger s = public_key.H.Pow(y); //Shared secret
                messages_encrypted.Add(new EIGamalMessage(public_key.G.Pow(y), message * s));
            }
            return messages_encrypted;
        }


        private List<BigInteger> Decrypt(List<EIGamalMessage> messages, EIGamalPrivateKey private_key)
        {
            //Encryption
            List<BigInteger> messages_plaintext = new List<BigInteger>();
            foreach (EIGamalMessage message in messages)
            {
                //Decryption
                BigInteger s2 = message.C1.Pow(private_key.X); //Shared secret
                messages_plaintext.Add(message.C2 * s2.ModInverse(private_key.G));
            }
            return messages_plaintext;
        }

 
    }
}
