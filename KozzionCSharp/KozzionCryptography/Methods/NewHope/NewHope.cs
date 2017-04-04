using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCryptography.Methods.NewHope
{
    public class NewHope
    {
        bool STATISTICAL_TEST = true;

        public static int PARAM_N = 1024;

        public static int PARAM_K = 16; /* used in sampler */
        public static ushort PARAM_Q = 12289;

        public static int POLY_BYTES = 1792;
        public static uint NEWHOPE_SEEDBYTES = 32;
        public static int NEWHOPE_RECBYTES = 256;

        public static int NEWHOPE_SENDABYTES = (POLY_BYTES + NEWHOPE_SEEDBYTES);
        public static int NEWHOPE_SENDBBYTES = (POLY_BYTES + NEWHOPE_RECBYTES);

        static void encode_a(byte[] r, Poly pk, byte[] seed)
        {
            Poly.poly_tobytes(r, pk);
            for (int i = 0; i < NEWHOPE_SEEDBYTES; i++)
            {
                r[POLY_BYTES + i] = seed[i];
            }
        }

        static void decode_a(Poly pk, byte[] seed, byte[] r)
        {

            Poly.poly_frombytes(pk, r);
            for (int i = 0; i < NEWHOPE_SEEDBYTES; i++)
            {
                seed[i] = r[POLY_BYTES + i];
            }
        }

        static void encode_b(byte[] r, Poly b, Poly c)
        {
            Poly.poly_tobytes(r, b);
            for (int i = 0; i < PARAM_N / 4; i++)
            {
                r[POLY_BYTES + i] = c.coeffs[4 * i] | (c.coeffs[4 * i + 1] << 2) | (c.coeffs[4 * i + 2] << 4) | (c.coeffs[4 * i + 3] << 6);
            }
        }

        static void decode_b(Poly b, Poly c, byte[] r)
        {
            Poly.poly_frombytes(b, r);
            for (int i = 0; i < PARAM_N / 4; i++)
            {
                c.coeffs[4 * i + 0] = r[POLY_BYTES + i] & 0x03;
                c.coeffs[4 * i + 1] = (r[POLY_BYTES + i] >> 2) & 0x03;
                c.coeffs[4 * i + 2] = (r[POLY_BYTES + i] >> 4) & 0x03;
                c.coeffs[4 * i + 3] = (r[POLY_BYTES + i] >> 6);
            }
        }

        static void gen_a(Poly a, byte[] seed)
        {
            Poly.poly_uniform(a, seed);
        }


        // API FUNCTIONS 

        void newhope_keygen(byte[] send, Poly sk)
        {
            Poly a, e, r, pk;
            unsigned char seed[NEWHOPE_SEEDBYTES];
            unsigned char noiseseed[32];

            randombytes(seed, NEWHOPE_SEEDBYTES);
            randombytes(noiseseed, 32);

            gen_a(a, seed);

            Poly.poly_getnoise(sk, noiseseed, 0);
            Poly.poly_ntt(sk);

            Poly.poly_getnoise(e, noiseseed, 1);
            Poly.poly_ntt(&e);

            Poly.poly_pointwise(r, sk, &a);
            Poly.poly_add(pk, e, r);

            encode_a(send, pk, seed);
        }


        void newhope_sharedb(byte[] sharedkey, byte[] send, byte[] received)
        {
            Poly sp, ep, v, a, pka, c, epp, bp;
            byte[] seed[NEWHOPE_SEEDBYTES];
            byte[] noiseseed[32];

            randombytes(noiseseed, 32);

            decode_a(&pka, seed, received);
            gen_a(&a, seed);

            poly_getnoise(&sp, noiseseed, 0);
            poly_ntt(&sp);
            poly_getnoise(&ep, noiseseed, 1);
            poly_ntt(&ep);

            poly_pointwise(&bp, &a, &sp);
            poly_add(&bp, &bp, &ep);

            poly_pointwise(&v, &pka, &sp);
            poly_invntt(&v);

            poly_getnoise(&epp, noiseseed, 2);
            poly_add(&v, &v, &epp);

            helprec(&c, &v, noiseseed, 3);

            encode_b(send, &bp, &c);

            rec(sharedkey, &v, &c);

            if (STATISTICAL_TEST)
            {
                sha3256(sharedkey, sharedkey, 32);
            }
        }


        void newhope_shareda(byte[] sharedkey, Poly sk, byte[] received)
        {
            Poly v, bp, c;

            decode_b(&bp, &c, received);

            poly_pointwise(&v, sk, &bp);
            poly_invntt(&v);

            rec(sharedkey, &v, &c);

            if (STATISTICAL_TEST)
            {
                sha3256(sharedkey, sharedkey, 32);
            }
        }
    }
}
