using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCryptography.Methods.NewHope
{
    public class Fips202
    {
        /* Based on the public domain implementation in
 * crypto_hash/keccakc512/simple/ from http://bench.cr.yp.to/supercop.html
 * by Ronny Van Keer 
 * and the public domain "TweetFips202" implementation
 * from https://twitter.com/tweetfips202
 * by Gilles Van Assche, Daniel J. Bernstein, and Peter Schwabe */



        public static int NROUNDS = 24;
        public static uint SHAKE128_RATE = 168;
        public static uint SHA3_256_RATE = 136;

        public static ulong ROL(ulong a, int offset)
        {
            return ((a << offset) ^ (a >> (64 - offset)));
        }

        public static ulong load64(byte [] bytes)
        {
            //unsigned long long r = 0;
            //  ulong r = 0;
            //  for (int i = 0; i< 8; ++i) 
            //{
            //    r |= (long)x[i] << 8 * i;
            //  }
            //  return r;
            throw new NotImplementedException();
        }

        static void store64(byte [] bytes, ulong u)
        {
            //for (int i = 0; i < 8; ++i)
            //{
            //    x[i] = u;
            //    u >>= 8;
            //}
            throw new NotImplementedException();
        }

    public static ulong [] KeccakF_RoundConstants = new ulong[]
    {
        (ulong)0x0000000000000001UL,
        (ulong)0x0000000000008082UL,
        (ulong)0x800000000000808aUL,
        (ulong)0x8000000080008000UL,
        (ulong)0x000000000000808bUL,
        (ulong)0x0000000080000001UL,
        (ulong)0x8000000080008081UL,
        (ulong)0x8000000000008009UL,
        (ulong)0x000000000000008aUL,
        (ulong)0x0000000000000088UL,
        (ulong)0x0000000080008009UL,
        (ulong)0x000000008000000aUL,
        (ulong)0x000000008000808bUL,
        (ulong)0x800000000000008bUL,
        (ulong)0x8000000000008089UL,
        (ulong)0x8000000000008003UL,
        (ulong)0x8000000000008002UL,
        (ulong)0x8000000000000080UL,
        (ulong)0x000000000000800aUL,
        (ulong)0x800000008000000aUL,
        (ulong)0x8000000080008081UL,
        (ulong)0x8000000000008080UL,
        (ulong)0x0000000080000001UL,
        (ulong)0x8000000080008008UL
    };

public static void KeccakF1600_StatePermute(ulong [] state)
{
    int round;

    ulong Aba, Abe, Abi, Abo, Abu;
    ulong Aga, Age, Agi, Ago, Agu;
    ulong Aka, Ake, Aki, Ako, Aku;
    ulong Ama, Ame, Ami, Amo, Amu;
    ulong Asa, Ase, Asi, Aso, Asu;
    ulong BCa, BCe, BCi, BCo, BCu;
    ulong Da, De, Di, Do, Du;
    ulong Eba, Ebe, Ebi, Ebo, Ebu;
    ulong Ega, Ege, Egi, Ego, Egu;
    ulong Eka, Eke, Eki, Eko, Eku;
    ulong Ema, Eme, Emi, Emo, Emu;
    ulong Esa, Ese, Esi, Eso, Esu;

    //copyFromState(A, state)
    Aba = state[0];
    Abe = state[1];
    Abi = state[2];
    Abo = state[3];
    Abu = state[4];
    Aga = state[5];
    Age = state[6];
    Agi = state[7];
    Ago = state[8];
    Agu = state[9];
    Aka = state[10];
    Ake = state[11];
    Aki = state[12];
    Ako = state[13];
    Aku = state[14];
    Ama = state[15];
    Ame = state[16];
    Ami = state[17];
    Amo = state[18];
    Amu = state[19];
    Asa = state[20];
    Ase = state[21];
    Asi = state[22];
    Aso = state[23];
    Asu = state[24];

    for (round = 0; round < NROUNDS; round += 2)
    {
        //    prepareTheta
        BCa = Aba ^ Aga ^ Aka ^ Ama ^ Asa;
        BCe = Abe ^ Age ^ Ake ^ Ame ^ Ase;
        BCi = Abi ^ Agi ^ Aki ^ Ami ^ Asi;
        BCo = Abo ^ Ago ^ Ako ^ Amo ^ Aso;
        BCu = Abu ^ Agu ^ Aku ^ Amu ^ Asu;

        //thetaRhoPiChiIotaPrepareTheta(round  , A, E)
        Da = BCu ^ ROL(BCe, 1);
        De = BCa ^ ROL(BCi, 1);
        Di = BCe ^ ROL(BCo, 1);
        Do = BCi ^ ROL(BCu, 1);
        Du = BCo ^ ROL(BCa, 1);

        Aba ^= Da;
        BCa = Aba;
        Age ^= De;
        BCe = ROL(Age, 44);
        Aki ^= Di;
        BCi = ROL(Aki, 43);
        Amo ^= Do;
        BCo = ROL(Amo, 21);
        Asu ^= Du;
        BCu = ROL(Asu, 14);
        Eba = BCa ^ ((~BCe) & BCi);
        Eba ^= (ulong)KeccakF_RoundConstants[round];
        Ebe = BCe ^ ((~BCi) & BCo);
        Ebi = BCi ^ ((~BCo) & BCu);
        Ebo = BCo ^ ((~BCu) & BCa);
        Ebu = BCu ^ ((~BCa) & BCe);

        Abo ^= Do;
        BCa = ROL(Abo, 28);
        Agu ^= Du;
        BCe = ROL(Agu, 20);
        Aka ^= Da;
        BCi = ROL(Aka, 3);
        Ame ^= De;
        BCo = ROL(Ame, 45);
        Asi ^= Di;
        BCu = ROL(Asi, 61);
        Ega = BCa ^ ((~BCe) & BCi);
        Ege = BCe ^ ((~BCi) & BCo);
        Egi = BCi ^ ((~BCo) & BCu);
        Ego = BCo ^ ((~BCu) & BCa);
        Egu = BCu ^ ((~BCa) & BCe);

        Abe ^= De;
        BCa = ROL(Abe, 1);
        Agi ^= Di;
        BCe = ROL(Agi, 6);
        Ako ^= Do;
        BCi = ROL(Ako, 25);
        Amu ^= Du;
        BCo = ROL(Amu, 8);
        Asa ^= Da;
        BCu = ROL(Asa, 18);
        Eka = BCa ^ ((~BCe) & BCi);
        Eke = BCe ^ ((~BCi) & BCo);
        Eki = BCi ^ ((~BCo) & BCu);
        Eko = BCo ^ ((~BCu) & BCa);
        Eku = BCu ^ ((~BCa) & BCe);

        Abu ^= Du;
        BCa = ROL(Abu, 27);
        Aga ^= Da;
        BCe = ROL(Aga, 36);
        Ake ^= De;
        BCi = ROL(Ake, 10);
        Ami ^= Di;
        BCo = ROL(Ami, 15);
        Aso ^= Do;
        BCu = ROL(Aso, 56);
        Ema = BCa ^ ((~BCe) & BCi);
        Eme = BCe ^ ((~BCi) & BCo);
        Emi = BCi ^ ((~BCo) & BCu);
        Emo = BCo ^ ((~BCu) & BCa);
        Emu = BCu ^ ((~BCa) & BCe);

        Abi ^= Di;
        BCa = ROL(Abi, 62);
        Ago ^= Do;
        BCe = ROL(Ago, 55);
        Aku ^= Du;
        BCi = ROL(Aku, 39);
        Ama ^= Da;
        BCo = ROL(Ama, 41);
        Ase ^= De;
        BCu = ROL(Ase, 2);
        Esa = BCa ^ ((~BCe) & BCi);
        Ese = BCe ^ ((~BCi) & BCo);
        Esi = BCi ^ ((~BCo) & BCu);
        Eso = BCo ^ ((~BCu) & BCa);
        Esu = BCu ^ ((~BCa) & BCe);

        //    prepareTheta
        BCa = Eba ^ Ega ^ Eka ^ Ema ^ Esa;
        BCe = Ebe ^ Ege ^ Eke ^ Eme ^ Ese;
        BCi = Ebi ^ Egi ^ Eki ^ Emi ^ Esi;
        BCo = Ebo ^ Ego ^ Eko ^ Emo ^ Eso;
        BCu = Ebu ^ Egu ^ Eku ^ Emu ^ Esu;

        //thetaRhoPiChiIotaPrepareTheta(round+1, E, A)
        Da = BCu ^ ROL(BCe, 1);
        De = BCa ^ ROL(BCi, 1);
        Di = BCe ^ ROL(BCo, 1);
        Do = BCi ^ ROL(BCu, 1);
        Du = BCo ^ ROL(BCa, 1);

        Eba ^= Da;
        BCa = Eba;
        Ege ^= De;
        BCe = ROL(Ege, 44);
        Eki ^= Di;
        BCi = ROL(Eki, 43);
        Emo ^= Do;
        BCo = ROL(Emo, 21);
        Esu ^= Du;
        BCu = ROL(Esu, 14);
        Aba = BCa ^ ((~BCe) & BCi);
        Aba ^= (ulong)KeccakF_RoundConstants[round + 1];
        Abe = BCe ^ ((~BCi) & BCo);
        Abi = BCi ^ ((~BCo) & BCu);
        Abo = BCo ^ ((~BCu) & BCa);
        Abu = BCu ^ ((~BCa) & BCe);

        Ebo ^= Do;
        BCa = ROL(Ebo, 28);
        Egu ^= Du;
        BCe = ROL(Egu, 20);
        Eka ^= Da;
        BCi = ROL(Eka, 3);
        Eme ^= De;
        BCo = ROL(Eme, 45);
        Esi ^= Di;
        BCu = ROL(Esi, 61);
        Aga = BCa ^ ((~BCe) & BCi);
        Age = BCe ^ ((~BCi) & BCo);
        Agi = BCi ^ ((~BCo) & BCu);
        Ago = BCo ^ ((~BCu) & BCa);
        Agu = BCu ^ ((~BCa) & BCe);

        Ebe ^= De;
        BCa = ROL(Ebe, 1);
        Egi ^= Di;
        BCe = ROL(Egi, 6);
        Eko ^= Do;
        BCi = ROL(Eko, 25);
        Emu ^= Du;
        BCo = ROL(Emu, 8);
        Esa ^= Da;
        BCu = ROL(Esa, 18);
        Aka = BCa ^ ((~BCe) & BCi);
        Ake = BCe ^ ((~BCi) & BCo);
        Aki = BCi ^ ((~BCo) & BCu);
        Ako = BCo ^ ((~BCu) & BCa);
        Aku = BCu ^ ((~BCa) & BCe);

        Ebu ^= Du;
        BCa = ROL(Ebu, 27);
        Ega ^= Da;
        BCe = ROL(Ega, 36);
        Eke ^= De;
        BCi = ROL(Eke, 10);
        Emi ^= Di;
        BCo = ROL(Emi, 15);
        Eso ^= Do;
        BCu = ROL(Eso, 56);
        Ama = BCa ^ ((~BCe) & BCi);
        Ame = BCe ^ ((~BCi) & BCo);
        Ami = BCi ^ ((~BCo) & BCu);
        Amo = BCo ^ ((~BCu) & BCa);
        Amu = BCu ^ ((~BCa) & BCe);

        Ebi ^= Di;
        BCa = ROL(Ebi, 62);
        Ego ^= Do;
        BCe = ROL(Ego, 55);
        Eku ^= Du;
        BCi = ROL(Eku, 39);
        Ema ^= Da;
        BCo = ROL(Ema, 41);
        Ese ^= De;
        BCu = ROL(Ese, 2);
        Asa = BCa ^ ((~BCe) & BCi);
        Ase = BCe ^ ((~BCi) & BCo);
        Asi = BCi ^ ((~BCo) & BCu);
        Aso = BCo ^ ((~BCu) & BCa);
        Asu = BCu ^ ((~BCa) & BCe);
    }

    //copyToState(state, A)
    state[0] = Aba;
    state[1] = Abe;
    state[2] = Abi;
    state[3] = Abo;
    state[4] = Abu;
    state[5] = Aga;
    state[6] = Age;
    state[7] = Agi;
    state[8] = Ago;
    state[9] = Agu;
    state[10] = Aka;
    state[11] = Ake;
    state[12] = Aki;
    state[13] = Ako;
    state[14] = Aku;
    state[15] = Ama;
    state[16] = Ame;
    state[17] = Ami;
    state[18] = Amo;
    state[19] = Amu;
    state[20] = Asa;
    state[21] = Ase;
    state[22] = Asi;
    state[23] = Aso;
    state[24] = Asu;
}




        static void keccak_absorb(ulong[] s,
                                  uint r,
                                  byte [] m, long mlen,
                                  byte p)
        {
            byte [] t = new byte[200];

            for (int i = 0; i < 25; ++i)
                s[i] = 0;

            while (mlen >= r)
            {
                for (int i = 0; i < r / 8; ++i)
                {
                    s[i] ^= load64(m + 8 * i); // fuck pointer aritmetic
                }
                KeccakF1600_StatePermute(s);
                mlen -= r;
                m += r;
            }

            for (int i = 0; i < r; ++i)
            {
                t[i] = 0;
            }

            for (int i = 0; i < mlen; ++i)
            {
                t[i] = m[i];
            }

            t[i] = p;
            t[r - 1] |= 128;
            for (int i = 0; i < r / 8; ++i)
            {
                s[i] ^= load64(t + 8 * i);
            }
        }


        static void keccak_squeezeblocks(byte[] h, long nblocks,
                                         ulong [] s,
                                         uint r)
        {
    
            while (nblocks > 0)
            {
                KeccakF1600_StatePermute(s);
                for (int i = 0; i < (r >> 3); i++)
                {
                    store64(h + 8 * i, s[i]);
                }
                h += r;
                nblocks--;
            }
        }


        public static void shake128_absorb(ulong[] s, byte [] input, uint inputByteLen)
        {
            keccak_absorb(s, SHAKE128_RATE, input, inputByteLen, 0x1F);
        }


        public static void shake128_squeezeblocks(byte[] output, long nblocks, ulong [] s)
        {
            keccak_squeezeblocks(output, nblocks, s, SHAKE128_RATE);
        }


        void shake128(byte[] output, uint outputByteLen, byte[] input, uint inputByteLen)
        {
            ulong[] s = new ulong[25];
            if ((outputByteLen % SHAKE128_RATE) != 0)
            {
                throw new Exception("wrong bit lenght");
            }
            shake128_absorb(s, input, inputByteLen);
            shake128_squeezeblocks(output, outputByteLen / SHAKE128_RATE, s);
        }


        void sha3256(byte[] output, byte[] input, uint inputByteLen)
        {
            ulong []s = new ulong[25];
            byte[]  t = new byte[SHA3_256_RATE];
            keccak_absorb(s, SHA3_256_RATE, input, inputByteLen, 0x06);
            keccak_squeezeblocks(t, 1, s, SHA3_256_RATE);
            for (int i = 0; i < 32; i++)
            {
                output[i] = t[i];
            }
        }

    }
}
