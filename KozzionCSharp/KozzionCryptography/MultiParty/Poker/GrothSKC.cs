using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using KozzionMathematics.Tools;

namespace KozzionCryptography.multiparty
{

    public class GrothSKC
    {
        private const long        TMCG_GROTH_L_E = 0;
        private const long        TMCG_DDH_SIZE  = 0;
        private const long        TMCG_DLSE_SIZE = 0;
        private long                     l_e;
        private BigInteger               exp2l_e;
        private PedersenCommitmentScheme com;

        public GrothSKC(
            int n)
		    : this(n, TMCG_GROTH_L_E, TMCG_DDH_SIZE, TMCG_DLSE_SIZE)
        {
        
        }

        public GrothSKC(
            int n,
            IInputChannel input_channel)
		    :  this(n, input_channel, TMCG_GROTH_L_E, TMCG_DDH_SIZE, TMCG_DLSE_SIZE)
        {
       
        }

        public GrothSKC(
            int n,
            IInputChannel input_channel,
            long ell_e)
		    :  this(n, input_channel, ell_e, TMCG_DDH_SIZE, TMCG_DLSE_SIZE)
        {
      
        }

        public GrothSKC(
            int n,
            long ell_e,
            long fieldsize,
            long subgroupsize)
        {
            l_e = ell_e;
            com = new PedersenCommitmentScheme(n, fieldsize, subgroupsize);

            // Compute $2^{\ell_e}$ for the input reduction.
            exp2l_e = new BigInteger(2).Pow((int) ell_e);
        }

        public GrothSKC(
            int n,
            IInputChannel input_channel,
            long ell_e,
            long fieldsize,
            long subgroupsize)
        {
            l_e = ell_e;
            com = new PedersenCommitmentScheme(n, input_channel, fieldsize, subgroupsize);

            // Compute $2^{\ell_e}$ for the input reduction.
            exp2l_e = new BigInteger(2).Pow((int) ell_e);
        }

        public bool CheckGroup()
        {
            return com.CheckGroup();
        }

        public void PublishGroup(
            IOutputChannel output_channel)
        {
            com.PublishGroup(output_channel);
        }

        public void Prove_interactive(
            List<int> pi,
            BigInteger r,
            List<BigInteger> m,
            IInputChannel input_channel,
            IOutputChannel output_channel)
        {
            Debug.Assert (com.g.Count >= pi.Count);
            Debug.Assert (pi.Count == m.Count);
            Debug.Assert (m.Count >= 2);

            BigInteger x, r_d, r_Delta, r_a, c_d, c_Delta, c_a, e, z, z_Delta, foo, bar;
            List<BigInteger> d = new List<BigInteger>();
            List<BigInteger> Delta = new List<BigInteger>();
            List<BigInteger> a = new List<BigInteger>();
            List<BigInteger> f = new List<BigInteger>();
            List<BigInteger> f_Delta = new List<BigInteger>();
            List<BigInteger> lej = new List<BigInteger>();
            // initialize
            for (int i = 0; i < m.Count; i++)
            {
                d.Add(0);
                Delta.Add(0);
                a.Add(0);
                f.Add(0);
                f_Delta.Add(0);
                lej.Add(0);
            }

            // prover: first move
            x = input_channel.Recieve(); // get $x$ from the verifier

            // check whether $x$ is from $\{0, 1\}^{\ell_e}$, otherwise reduce
            if (x.BitLength() > l_e)
                x = x % exp2l_e;

            // prover: second move
            r_d = mpz_srandom.mpz_srandomm(com.q); // $r_d \gets \mathbb{Z}_q$
            r_Delta = mpz_srandom.mpz_srandomm(com.q); // $r_{\Delta} \gets \mathbb{Z}_q$
            for (int index = 0; index < d.Count; index++)
            {
                d[index] = mpz_srandom.mpz_srandomm(com.q); // $d_1,\ldots,d_n \gets \mathbb{Z}_q$
            }

            Delta[0] = d[0]; // $\Delta_1 := d_1$
            for (int index = 1; index < (Delta.Count - 1); index++)
            {
                Delta[index] = mpz_srandom.mpz_srandomm(com.q); // $\Delta_2,\ldots,\Delta_{n-1}
            } // \gets \mathbb{Z}_q$

            Delta[Delta.Count - 1] =  0;// $\Delta_n := 0$

            for (int i = 0; i < a.Count; i++)
            {
                a[i] = 1;
                // compute a_i = \prod_{j=1}^i (m_{\pi(j)} - x)
                for (int j = 0; j <= i; j++)
                {
                    foo = m[pi[j]] - x;
                    foo = foo % com.q;
                    a[i] = a[i] * foo;
                    a[i] = a[i] * com.q; //TODO CHECK!!!
                }
            }
            r_a = mpz_srandom.mpz_srandomm(com.q); // $r_a \gets \mathbb{Z}_q$
            // $c_d = \mathrm{com}_{ck}(d_1,\ldots,d_n;r_d)$
            c_d = com.CommitBy(r_d, d);
            for (int i = 0; i < lej.Count; i++)
            {
                if (i < (lej.Count - 1))
                {
                    foo = Delta[i];
                    foo = foo.FlipSign();
                    lej[i] =  foo * d[i + 1];
                    lej[i] = lej[i] % com.q;
                }
                else
                {
                    lej[i] = 0;
                }
            }
            // $c_{\Delta} = \mathrm{com}_{ck}(-\Delta_1 d_2,\ldots,
            // -\Delta_{n-1} d_n;r_{\Delta})$
            c_Delta = com.CommitBy(r_Delta, lej);
            for (int i = 0; i < lej.Count; i++)
            {
                if (i < (lej.Count - 1))
                {
                    foo = Delta[i + 1];
                    bar = m[pi[i + 1]] - x;
                    bar = bar % com.q;
                    bar = bar * Delta[i];
                    bar = bar % com.q;
                    foo = foo - bar;
                    foo = foo % com.q;
                    bar = a[i] * d[i + 1];
                    bar = bar % com.q;
                    foo = foo - bar;
                    foo = foo % com.q;
                    lej[i] = foo;
                }
                else
                {
                    lej[i] = 0;
                }
            }
            // $c_a = \mathrm{com}_{ck}(\Delta_2 - (m_{\pi(2)} - x)\Delta_1 - a_1 d_2,
            // \ldots,\Delta_n - (m_{\pi(n)} - x)\Delta_{n-1}
            // - a_{n-1} d_n;r_a)$
            c_a = com.CommitBy(r_a, lej);
            // send $c_d$, $c_\Delta$, and $c_a$ to the verifier
            output_channel.Send(c_d);
            output_channel.Send(c_Delta);
            output_channel.Send(c_a);

            // prover: third move
            e = input_channel.Recieve(); // get $e$ from the verifier

            // check whether $x$ is from $\{0, 1\}^{\ell_e}$, otherwise reduce
            if (e.BitLength() > l_e)
            {
                e = e.ModInverse(exp2l_e);
            }

            // prover: fourth move
            // compute $f_i = e m_{\pi(i)} + d_i$
            for (int i = 0; i < f.Count; i++)
            {
                f[i] =  e * m[pi[i]];
                f[i] =  f[i] % com.q;
                f[i] =  f[i] + d[i];
                f[i] =  f[i] % com.q;
            }
            // compute $z = e r + r_d$
            z = e * r;
            z = z % com.q;
            z = z + r_d;
            z = z % com.q;

            // compute $f_{\Delta_i} = e (\Delta_{i+1} - (m_{\pi(i+1)} - x)\Delta_i
            // - a_i d_{i+1}) - \Delta_i d_{i+1}$
            for (int i = 0; i < (f_Delta.Count - 1); i++)
            {
                foo = Delta[i + 1];
                bar = m[pi[i + 1]] - x;
                bar = bar % com.q;
                bar = bar * Delta[i];
                bar = bar % com.q;
                foo = foo - bar;
                foo = foo % com.q;
                bar = a[i] * d[i + 1];
                bar = bar % com.q;
                foo = foo - bar;
                foo = foo % com.q;
                foo = foo * e;
                foo = foo % com.q;
                bar = Delta[i] * d[i + 1];
                bar = bar % com.q;
                foo = foo - bar;
                foo = foo % com.q;
                f_Delta[i] =  foo;
            }
            // compute $z_{\Delta} = e r_a + r_{\Delta}$
            z_Delta = e * r_a;
            z_Delta = z_Delta % com.q;
            z_Delta = z_Delta + r_Delta;
            z_Delta = z_Delta % com.q;

            for (int i = 0; i < f.Count; i++)
            {
                output_channel.Send(f[i]); // send $f_1,\ldots,f_n$ to the verifier
            }
            output_channel.Send(z); // send $z$ to the verifier

            for (int i = 0; i < (f_Delta.Count - 1); i++)
            {
                output_channel.Send(f_Delta[i]); // send $f_{\Delta_1},\ldots,
            }
            output_channel.Send(z_Delta); // f_{\Delta_{n-1}}$ to verifier
        }

       public bool Verify_interactive(
            BigInteger c,
            List<BigInteger> m,
            IInputChannel input_channel,
            IOutputChannel output_channel)
        {
            return Verify_interactive(c, m, input_channel, output_channel, true);
        }

       public bool Verify_interactive(
            BigInteger c,
            List<BigInteger> m,
            IInputChannel input_channel,
            IOutputChannel output_channel,
            bool optimizations)
        {
            Debug.Assert (com.g.Count >= m.Count);
            Debug.Assert (m.Count >= 2);

            // initialize
            BigInteger x, c_d, c_Delta, c_a, e, z, z_Delta, foo, bar, foo2, bar2;
            BigInteger [] f = new BigInteger [m.Count];
            BigInteger [] f_Delta = new BigInteger [m.Count];
            BigInteger [] lej = new BigInteger [m.Count];
            for (int i = 0; i < m.Count; i++)
            {

                f[i] = 0;
                f_Delta[i] = 0;
                lej[i] = 0;
            }

            // verifier: first move
            x = mpz_srandom.mpz_srandomb(l_e);
            output_channel.Send(x);// send $x\in\{0,1\}^{\ell_e}$ to the prover

            // verifier: second move
            c_d = input_channel.Recieve();
            c_Delta = input_channel.Recieve();
            c_a = input_channel.Recieve(); // get $c_d$, $c_{\Delta}$, and $c_a$
            // from the prover

            // verifier: third move
            e = mpz_srandom.mpz_srandomb(l_e);
            output_channel.Send(e); // send $e\in\{0,1\}^{\ell_e}$ to prover

            // verifier: fourth move
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = input_channel.Recieve(); // get $f_1,\ldots,f_n$ from the prover
            }
            z = input_channel.Recieve(); // get $z$ from the prover
            for (int i = 0; i < (f_Delta.Length - 1); i++)
            {
                f_Delta[i] = input_channel.Recieve();
                // get $f_{\Delta_1},\ldots,f_{\Delta_{n-1}}$ from the prover
            }
        
            // get $z_{\Delta}$ from the prover
            z_Delta = input_channel.Recieve();
        
            // check whether $c_d, c_a, c_{\Delta} \in\mathcal{C}_{ck}$
            if (!(com.TestMembership(c_d) && com.TestMembership(c_a) && com.TestMembership(c_Delta)))
            {
                return false;
            }
            // check whether $f_1, \ldots, f_n, z \in\mathbb{Z}_q$
            if (!(z.CompareTo(com.q) < 0))
            {
                return false;
            }
            for (int i = 0; i < f.Length; i++)
            {
                if (!(f[i].CompareTo(com.q) < 0))
                {
                    return false;
                }
            }

            // check whether $f_{\Delta_1}, \ldots, f_{\Delta_{n-1}}$
            // and $z_{\Delta}$ are from $\mathbb{Z}_q$
            if (!(z_Delta.CompareTo(com.q) < 0))
            {
                return false;
            }

            for (int i = 0; i < (f_Delta.Length - 1); i++)
            {
                if (!(f_Delta[i].CompareTo(com.q) < 0))
                {
                    return false;
                }
            }

            if (optimizations)
            {
                // randomization technique from section 6,
                // paragraph 'Batch verification' [Gr05]
                BigInteger alpha = 0;
                // pick $\alpha\in_R\{0, 1\}^{\ell_e}$ at random
                alpha = mpz_srandom.mpz_srandomb(l_e);
                // compute $(c^e c_d)^{\alpha}$
                foo = c.ModPow(e, com.p);
                foo = foo * c_d;
                foo = foo % com.p;
                foo = foo.ModPow(alpha, com.p);

                // compute $c_a^e c_{\Delta}$
                bar = c_a.ModPow(e, com.p);
                bar = bar * c_Delta;
                bar = bar % com.p;

                // compute the product
                foo = foo * bar;
                foo = foo % com.p;
                // compute the messages for the commitment
                for (int i = 0; i < f.Length; i++)
                {
                    lej[i] = alpha * f[i];
                    lej[i] =  lej[i] % com.q;
                    lej[i] = lej[i] + f_Delta[i];
                    lej[i] =  lej[i] % com.q;
                }
                bar = alpha * z;
                bar = bar % com.q;
                bar = bar + z_Delta;
                bar = bar % com.q;
                // check the randomized commitments
                if (!com.Verify(foo, bar, lej))
                {
                    return false;
                }
            }
            else
            {
                // check whether $c^e c_d = \mathrm{com}_{ck}(f_1,\ldots,f_n; z)$

                foo = c.ModPow(e, com.p);
                foo = foo * c_d;
                foo = foo % com.p;

                if (!com.Verify(foo, z, f))
                    return false;
                // check whether $c_a^e c_{\Delta} = \mathrm{com}_{ck}
                // (f_{\Delta_1},\ldots,f_{\Delta_{n-1}}; z_{\Delta})$

                foo = c_a.ModPow(e, com.p);
                foo = foo * c_Delta;
                foo = foo % com.p;

                if (!com.Verify(foo, z_Delta, f_Delta))
                    return false;
            }
            // check $F_n = e \prod_{i=1}^n (m_i - x)$
            foo = e * x;
            foo = foo % com.q;
            Debug.Assert (ToolsMathBigInteger.AreCoprime(e, com.q));
            bar = e.ModInverse(com.q);
            foo2 = 1;
            for (int i = 0; i < f.Length; i++)
            {
                bar2 = f[i]- foo;
                bar2 = bar2 % com.q;
                bar2 = bar2 * foo2;
                bar2 = bar2 % com.q;
                if (i > 0)
                {
                    bar2 = bar2 + f_Delta[i - 1];
                    bar2 = bar2 % com.q;

                    bar2 = bar2 * bar;
                    bar2 = bar2 % com.q;
                }
                foo2 = bar;
            }

            foo2 = 1;
            for (int i = 0; i < m.Count; i++)
            {
                foo = m[i] - x;
                foo = foo % com.q;
                foo2 = foo2 * foo;
                foo2 = foo2 % com.q;
            }
            foo2 = foo2 * e;
            foo2 = foo2 % com.q;
  
            if (foo2 != bar)
            {
                return false;
            }
            return true;
        }

       public bool Verify_interactive(
            BigInteger c,
            List<BigInteger> f_prime,
            List<BigInteger> m,
            IInputChannel input_channel,
            IOutputChannel output_channel)
        {
            return Verify_interactive(c, f_prime, m, input_channel, output_channel, true);
        }

       public bool Verify_interactive(
            BigInteger c,
            List<BigInteger> f_prime,
            List<BigInteger> m,
            IInputChannel input_channel,
            IOutputChannel output_channel,
            bool optimizations)
        {
            Debug.Assert (com.g.Count >= m.Count);
            Debug.Assert (m.Count == f_prime.Count);
            Debug.Assert (m.Count >= 2);

            // initialize
            BigInteger x, c_d, c_Delta, c_a, e, z, z_Delta, foo, bar, foo2;
            BigInteger [] f = new BigInteger [m.Count];
            BigInteger [] f_Delta = new BigInteger [m.Count];
            BigInteger [] lej = new BigInteger [m.Count];
            for (int i = 0; i < m.Count; i++)
            {

                f[i] = 0;
                f_Delta[i] = 0;
                lej[i] = 0;
            }

            // verifier: first move
            x = mpz_srandom.mpz_srandomb(l_e);
            output_channel.Send(x);

            // verifier: second move
            c_d = input_channel.Recieve();
            c_Delta = input_channel.Recieve();
            c_a = input_channel.Recieve();

            // verifier: third move
            e = mpz_srandom.mpz_srandomb(l_e);
            output_channel.Send(e);

            // verifier: fourth move
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = input_channel.Recieve();
            }

            z = input_channel.Recieve();
            for (int i = 0; i < (f_Delta.Length - 1); i++)
            {
                f_Delta[i] = input_channel.Recieve();
            }
            z_Delta = input_channel.Recieve();

            // check whether $c_d, c_a, c_{\Delta} \in\mathcal{C}_{ck}$
            if (!(com.TestMembership(c_d) && com.TestMembership(c_a) && com.TestMembership(c_Delta)))
            {
                return false;
            }

            // check whether $f_1, \ldots, f_n, z \in\mathbb{Z}_q$
            if (!(z.CompareTo(com.q) < 0))
            {
                return false;
            }
            for (int i = 0; i < f.Length; i++)
            {
                if (!(f[i].CompareTo(com.q) < 0))
                {
                    return false;
                }
            }

            // check whether $f_{\Delta_1}, \ldots, f_{\Delta_{n-1}}$
            // and $z_{\Delta}$ are from $\mathbb{Z}_q$
            if (!(z_Delta.CompareTo(com.q) < 0))
            {
                return false;
            }
            for (int i = 0; i < (f_Delta.Length - 1); i++)
            {
                if (!(f_Delta[i].CompareTo(com.q) < 0))
                {
                    return false;
                }
            }

            if (optimizations)
            {
                // randomization technique from section 6,
                // paragraph 'Batch verification'
                // pick $\alpha\in_R\{0, 1\}^{\ell_e}$ at random
                BigInteger alpha = mpz_srandom.mpz_srandomb(l_e);
                // compute $(c^e c_d)^{\alpha}$
                foo = c.ModPow(e, com.p);
                foo = foo * c_d;
                foo = foo % com.p;
                foo = foo.ModPow(alpha, com.p);
                // compute $c_a^e c_{\Delta}$
                bar = c_a.ModPow(e, com.p);
                bar = bar * c_Delta;
                bar = bar % com.p;
                // compute the product
                foo = foo * bar;
                foo = foo % com.p;
                // compute the messages for the commitment
                for (int i = 0; i < f.Length; i++)
                {
                    BigInteger tmp2 = alpha * f[i];
                    tmp2 = tmp2 % com.q;
                    tmp2 = tmp2 + f_Delta[i];
                    tmp2 = tmp2 % com.q;

                    // compute $f'_i e \alpha$ (optimized commitment)
                    bar = alpha * f_prime[i];
                    bar = bar % com.q;
                    bar = bar * e;
                    bar = bar % com.q;
                    bar = bar.FlipSign();

                    tmp2 = tmp2 + bar;
                    tmp2 = tmp2 % com.q;
                    lej[i] = tmp2;
                }
                bar = alpha * z;
                bar = bar % com.q;
                bar = bar + z_Delta;
                bar = bar % com.q;
                // check the randomized commitments
                if (!com.Verify(foo, bar, lej))
                {
                    return false;
                }
            }
            else
            {
                // check whether $c^e c_d = \mathrm{com}(f''_1, \ldots, f''_n; z)$
                foo = c.ModPow(e, com.p);
                foo = foo * c_d;
                foo = foo % com.q;
                // compute $f''_i = f_i - f'_i e$
                for (int i = 0; i < f.Length; i++)
                {
                    BigInteger tmp2 = f_prime[i] * e;
                    tmp2 = tmp2 % com.q;
                    tmp2 = tmp2.FlipSign();
                    tmp2 = tmp2 + f[i];
                    tmp2 = tmp2 % com.q;
                    lej[i] = tmp2;
                }

                if (!com.Verify(foo, z, lej))
                {
                    return false;
                }

                // check whether $c_a^e c_{\Delta} = \mathrm{com}(f_{\Delta_1},
                // \ldots, f_{\Delta_{n-1}}; z_{\Delta})$
                foo = c_a.ModPow(e, com.p);
                foo = foo * c_Delta;
                foo = foo % com.p;
                if (!com.Verify(foo, z_Delta, f_Delta))
                {
                    return false;
                }
            }

            // check $F_n = e \prod_{i=1}^n (m_i - x)$
            foo = e * x;
            foo = foo % com.q;
            Debug.Assert (ToolsMathBigInteger.AreCoprime(e, com.q));
            bar = e.ModInverse(com.q);
            foo2 = 1;
            BigInteger bar2 = 0;
            for (int i = 0; i < f.Length; i++)
            {
                bar2 = f[i] - foo;
                bar2 = bar2 % com.q;
                bar2 = bar2 * foo2;
                bar2 = bar2 % com.q;
                if (i > 0)
                {
                    bar2 = bar2 + f_Delta[i - 1];
                    bar2 = bar2 % com.q;
                    bar2 = bar2 * bar;
                    bar2 = bar2 % com.q;
                }
                foo2 = bar2;
            }
            foo2 = 1;
            for (int i = 0; i < m.Count; i++)
            {
                foo = m[i] - x;
                foo = foo % com.q;
                foo2 = foo2 * foo;
                foo2 = foo2 % com.q;
            }
            foo2 = foo2 * e;
            foo2 = foo2 % com.q;           
            return foo2 == bar2;
        }
    }
}