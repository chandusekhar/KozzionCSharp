using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCryptography.Methods.NewHope
{
    public class Poly
    {
        static const byte[] nonce = null;
        public ushort[]  coeffs;

        public Poly()
        {
            coeffs = new ushort[NewHope.PARAM_N];
        }

        public static ushort barrett_reduce(ushort a)
        {
            uint u;

            u = ((uint)a * 5) >> 16;
            u *= NewHope.PARAM_Q;
            a -= u;
            return a;
        }

        public static void poly_frombytes(Poly r, byte[] a)
        {
          int i;
          for(i=0;i< NewHope.PARAM_N /4;i++)
          {
            r.coeffs[4 * i + 0] =                                 a[7 * i + 0]        | (((ushort)a[7 * i + 1] & 0x3f) << 8);
            r.coeffs[4 * i + 1] = (a[7 * i + 1] >> 6) | (((ushort)a[7 * i + 2]) << 2) | (((ushort)a[7 * i + 3] & 0x0f) << 10);
            r.coeffs[4 * i + 2] = (a[7 * i + 3] >> 4) | (((ushort)a[7 * i + 4]) << 4) | (((ushort)a[7 * i + 5] & 0x03) << 12);
            r.coeffs[4 * i + 3] = (a[7 * i + 5] >> 2) | (((ushort)a[7 * i + 6]) << 6); 
          }
        }

        public static void poly_tobytes(byte [] r, Poly p)
        {
            int i;
            ushort t0, t1, t2, t3, m;
            ushort c;
            for (i = 0; i < NewHope.PARAM_N / 4; i++)
            {
                t0 = barrett_reduce(p.coeffs[4 * i + 0]); //Make sure that coefficients have only 14 bits
                t1 = barrett_reduce(p.coeffs[4 * i + 1]);
                t2 = barrett_reduce(p.coeffs[4 * i + 2]);
                t3 = barrett_reduce(p.coeffs[4 * i + 3]);

                m = t0 - NewHope.PARAM_Q;
                c = m;
                c >>= 15;
                t0 = m ^ ((t0 ^ m) & c); // <Make sure that coefficients are in [0,q]

                m = t1 - NewHope.PARAM_Q;
                c = m;
                c >>= 15;
                t1 = m ^ ((t1 ^ m) & c); // <Make sure that coefficients are in [0,q]

                m = t2 - NewHope.PARAM_Q;
                c = m;
                c >>= 15;
                t2 = m ^ ((t2 ^ m) & c); // <Make sure that coefficients are in [0,q]

                m = t3 - NewHope.PARAM_Q;
                c = m;
                c >>= 15;
                t3 = m ^ ((t3 ^ m) & c); // <Make sure that coefficients are in [0,q]

                r[7 * i + 0] = t0 & 0xff;
                r[7 * i + 1] = (t0 >> (ushort)8) | (t1 << (ushort)6);
                r[7 * i + 2] = (t1 >> 2);
                r[7 * i + 3] = (t1 >> 10) | (t2 << 4);
                r[7 * i + 4] = (t2 >> 4);
                r[7 * i + 5] = (t2 >> 12) | (t3 << 2);
                r[7 * i + 6] = (t3 >> 6);
            }
        }



        public static void poly_uniform(Poly a, byte [] seed)
        {
            uint pos = 0, ctr = 0;
            ushort val;
            ulong []state = new ulong[25];
            int nblocks = 16;
            byte [] buf = new byte[Fips202.SHAKE128_RATE * nblocks];

            Fips202.shake128_absorb(state, seed, NewHope.NEWHOPE_SEEDBYTES);

            Fips202.shake128_squeezeblocks( buf, nblocks, state);

            while (ctr < NewHope.PARAM_N)
            {
                val = (buf[pos] | ((ushort)buf[pos + 1] << 8)) & 0x3fff; // Specialized for q = 12889
                if (val < NewHope.PARAM_Q)
                    a.coeffs[ctr++] = val;
                pos += 2;
                if (pos > Fips202.SHAKE128_RATE * nblocks - 2)
                {
                    nblocks = 1;
                    Fips202.shake128_squeezeblocks( buf, nblocks, state);
                    pos = 0;
                }
            }
        }


        extern static void cbd(Poly r, byte [] b)
        {
            throw new NotImplementedException();
        }

        public static void poly_getnoise(Poly r, byte [] seed, byte nonce)
        {
            if (NewHope.PARAM_K != 16)
            {
                throw new Exception("poly_getnoise in poly.c only supports k=16");
            }

            byte [] buf = new byte[4 * NewHope.PARAM_N];
            byte[] n = new byte[NewHope.CRYPTO_STREAM_NONCEBYTES];

            for (int i = 1; i < NewHope.CRYPTO_STREAM_NONCEBYTES; i++)
            {
                n[i] = 0;
            }
            n[0] = nonce;

            crypto_stream(buf, 4 * NewHope.PARAM_N, n, seed);
            cbd(r, buf);
        }

        public static void poly_pointwise(Poly r, Poly a, Poly b)
        {
            for (int i = 0; i < NewHope.PARAM_N; i++)
            {
                r.coeffs[i] = a.coeffs[i] * b.coeffs[i] % NewHope.PARAM_Q; /* XXX: Get rid of the % here! */
            }
        }

        public static void poly_add(Poly r, Poly a, Poly b)
        {
            for (int i = 0; i < NewHope.PARAM_N; i++)
            {
                r.coeffs[i] = a.coeffs[i] + b.coeffs[i] % NewHope.PARAM_Q; /* XXX: Get rid of the % here! */
            }
        }

        public static void poly_ntt(Poly r)
        {
            double [] temp = new double[NewHope.PARAM_N];

            pwmul_double(r.coeffs, psis_bitrev);
            ntt_double(r.coeffs, omegas_double, temp);
        }

        public static void poly_invntt(Poly r)
        {
            double []temp = new double[NewHope.PARAM_N];

            bitrev_vector(r.coeffs);
            ntt_double(r.coeffs, omegas_inv_double, temp);
            pwmul_double(r.coeffs, psis_inv);
        }
    }
}
