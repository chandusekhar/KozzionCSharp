using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace KozzionCryptography.multiparty
{
    public class BarnettSmartVTMF_dlog
    {

        private BigInteger              x_i;
        private BigInteger              h_i;
        private BigInteger              d;
        private BigInteger              h_i_fp;
        private Dictionary<String, BigInteger> h_j;

        long                            F_size;
        long                            G_size;

        public BigInteger               p;
        public BigInteger               q;
        public BigInteger               g;
        public BigInteger               k;
        public BigInteger               h;

        public BarnettSmartVTMF_dlog()
		    : this(Constants.TMCG_DDH_SIZE, Constants.TMCG_DLSE_SIZE)
        {
        
        }

        public BarnettSmartVTMF_dlog(
            IInputChannel input_channel)
		    : this(input_channel, Constants.TMCG_DDH_SIZE, Constants.TMCG_DLSE_SIZE)
        {
        
        }

        BarnettSmartVTMF_dlog(
            long fieldsize,
            long subgroupsize)
        {
            F_size = fieldsize;
            G_size = subgroupsize;
            BigInteger foo;

            // Create a finite abelian group $G$ where the DDH problem is hard:
            // We use the unique subgroup of prime order $q$ where $p = kq + 1$.
            g = 0;

            if (subgroupsize != 0)
            {
                Tuple<BigInteger, BigInteger, BigInteger> result = MPPrime.MPZLPrime(fieldsize, subgroupsize, Constants.TMCG_MR_ITERATIONS);
                p = result.Item1;
                q = result.Item2;
                k = result.Item3;
            }
            System.Diagnostics.Debug.WriteLine("got primes");
            // Initialize all members of the key.
            x_i = 0;
            h_i = 0;
            h = 1;
            d = 0;
            h_i_fp = 0;
            h_j = new Dictionary<String, BigInteger>();

            // Choose randomly a generator $g$ of the unique subgroup of order $q$.
            if (subgroupsize != 0)
            {
                foo = p - 1;
                do
                {
                    d = mpz_srandom.mpz_wrandomm(p);
                    g = d.ModPow(k, p); // compute $g := d^k \bmod p$
                }
                while ((g == 0 || g == 1) || g == foo); // check, whether $1 < g < p-1$
            }
        }

        BarnettSmartVTMF_dlog(
            IInputChannel input_channel,
            long fieldsize,
            long subgroupsize)
        {

            F_size = fieldsize;
            G_size = subgroupsize;
            // Initialize the members for the finite abelian group $G$.
            p = input_channel.Recieve();
            q = input_channel.Recieve();
            g = input_channel.Recieve();
            k = input_channel.Recieve();

            // Initialize all members for the key.
            x_i = 0;
            h_i = 0;
            h = 0;
            d = 0;
            h_i_fp = 0;

        }

        public BarnettSmartVTMF_dlog(
            BarnettSmartVTMF_dlog other)
        {
            x_i = other.x_i;
            h_i = other.h_i;
            d = other.d;
            h_i_fp = other.h_i_fp;
            h_j = new Dictionary<String, BigInteger>(other.h_j);

            F_size = other.F_size;
            G_size = other.G_size;

            p = other.p;
            q = other.q;
            g = other.g;
            k = other.k;
            h = other.h;
        }

        public bool CheckGroup()
        {
            // Check whether $p$ and $q$ have appropriate sizes.
            if ((p.BitLength() < F_size) || (q.BitLength() < G_size))
            {
                //return false;
                throw new Exception("Bitlenght mismatch");
            }
            // Check whether $p$ has the correct form, i.e. $p = qk + 1$.
            if ((q * k) + 1 != p)
            {
                throw new Exception("p of incorrect form: (q * k) + 1 != p");
            }
            // Check whether $p$ and $q$ are both (probable) prime with
            // a soundness error probability ${} \le 4^{-TMCG_MR_ITERATIONS}$.
            if (!p.IsProbablePrime(Constants.TMCG_MR_ITERATIONS) || !q.IsProbablePrime(Constants.TMCG_MR_ITERATIONS))
            {
                //return false;
                throw new Exception("p or q not prime");
            }

            // Check whether $k$ is not divisible by $q$, i.e. $q$ and $k$ are
            // coprime.
            if (!ToolsMathBigInteger.AreCoprime(q, k))
            {
                //return false;
                throw new Exception("p or q not coprime"); //TODO this seems redundant given the former test
            }
            // Check whether $g$ is a generator for the subgroup $G$ of prime
            // order $q$. We have to assert that $g^q \equiv 1 \pmod{p}$,
            // which means that the order of $g$ is $q$. Of course, we must
            // ensure that $g$ is not trivial, i.e., $1 < g < p-1$.
            if (g <= 1 || p - 1 <= g || g.ModPow(q, p) != 1)
            {
                //return false;
                throw new Exception("g not a subgroup generator of G:  $1 < g < p-1 && g.ModPow(q, p) == 1 fails"); 
            }
            // finish
            return true;

        }

        void PublishGroup(
            IOutputChannel output_channel)
        {
            output_channel.Send(p);
            output_channel.Send(q);
            output_channel.Send(g);
            output_channel.Send(k);
        }

        bool CheckElement(
            BigInteger a)
        {

            // Check whether $0 < a < p$.
            if ((a.CompareTo(0) <= 0) || (a.CompareTo(p) >= 0))
            {
                 System.Diagnostics.Debug.WriteLine("$0 < a < p$ does not hold for a size: " + a.BitLength() + " and p size: " + p.BitLength());
                return false;
            }

            // Check whether $a^q \equiv 1 \pmod{p}$.
            if (a.ModPow(q, p) != 1)
            {
                 System.Diagnostics.Debug.WriteLine("a^q \\equiv 1 \\pmod{p} does not hold");
                return false;
            }
            return true;

        }

        BigInteger RandomElement()
        {

            // Choose randomly and uniformly an element $b$ from
            // $\mathbb{Z}_q \setminus \{ 0 \}$.
            BigInteger b = mpz_srandom.mpz_srandomm(q);
            while (b == 0)
            {
                b = mpz_srandom.mpz_srandomm(q);
            }

            // Compute $a := g^b \bmod p$.
            return g.ModPow(b, p);
        }

        void IndexElement(
            BigInteger a,
            int index)
        {
            // Simply compute $a := g^i \bmod p$.
            a = g.ModPow(new BigInteger(index), p);
        }

        public void KeyGenerationProtocol_GenerateKey()
        {
            // generate the private key $x_i \in \mathbb{Z}_q$ randomly
            x_i = mpz_srandom.mpz_srandomm(q);

            // compute $h_i = g^{x_i} \bmod p$ (with timing attack protection)
            h_i = g.ModPow(x_i, p);
            System.Diagnostics.Debug.WriteLine("KeyGenerationProtocol_GenerateKey::h_i: " + h_i.BitLength());

            // compute the fingerprint of the public key
            h_i_fp = mpz_shash_tools.mpz_shash(h_i);

            // set the initial value of the global key $h$
            h = h_i;
        }

        public void KeyGenerationProtocol_PublishKey(
            IOutputChannel output_channel)
        {
            // proof of knowledge [CaS97] for the public key

            // commitment
            BigInteger v = mpz_srandom.mpz_srandomm(q);
            BigInteger t = g.ModPow(v, p);
            // challenge
            // Here we use the well-known "Fiat-Shamir heuristic" to make
            // the PK non-interactive, i.e. we turn it into a statistically
            // zero-knowledge (Schnorr signature scheme style) proof of
            // knowledge (SPK) in the random oracle model.
            BigInteger c = mpz_shash_tools.mpz_shash(new BigInteger [] {g, h_i, t});

            // response
            BigInteger r = c * x_i;
            r = r.FlipSign();
            r += v;
            r = r % q;

            output_channel.Send(h_i);
            output_channel.Send(c);
            output_channel.Send(r);
            System.Diagnostics.Debug.WriteLine("g " + g.BitLength() + " h_i " + h_i.BitLength() + " t " + t.BitLength());

        }

        public bool KeyGenerationProtocol_UpdateKey(
            IInputChannel input_channel)
        {
            BigInteger foo = input_channel.Recieve();
            BigInteger c = input_channel.Recieve();
            BigInteger r = input_channel.Recieve();

            // verify the in-group property
            if (!CheckElement(foo))
            {

                 System.Diagnostics.Debug.WriteLine("CheckElement incorrect");
                return false;
            }

            // check the size of $r$
            if (ToolsMathBigInteger.CompareAbs(r, q) >= 0)
            {
                 System.Diagnostics.Debug.WriteLine("compare_absolutes incorrect");
                return false;
            }
            // verify the proof of knowledge [CaS97]
            BigInteger t = g.ModPow(r, p);
            r = foo.ModPow(c, p);
            t = t * r;
            t = t % p;
            r = mpz_shash_tools.mpz_shash(new BigInteger [] {g, foo, t});
            // c = mpz_shash_tools.mpz_shash(new BigInteger [] {g, h_i, t});

            if (c != r)
            {
                System.Diagnostics.Debug.WriteLine("c != r " + c.BitLength() + " " + r.BitLength());
                System.Diagnostics.Debug.WriteLine("g " + g.BitLength() + " foo " + foo.BitLength() + " t " + t.BitLength());
                return false;
            }

            // update the global key h
            h *= foo;
            h = h % p;

            // store the public key
            BigInteger tmp = foo;
            t = mpz_shash_tools.mpz_shash(foo);
            h_j.Add(t.ToString(), tmp);

            // finish
            return true;
        }

        bool KeyGenerationProtocol_RemoveKey(
            IInputChannel input_channel)
        {
            //  = input_channel.read_big_integer();foo >> bar >> bar;
            BigInteger foo = input_channel.Recieve();
            input_channel.Recieve(); // ???
            BigInteger bar = input_channel.Recieve();

            // compute the fingerprint
            bar = mpz_shash_tools.mpz_shash(foo);

            // public key with this fingerprint stored?
            if (h_j.ContainsKey(bar.ToString()))
            {
                // update the global key
                BigInteger temp = h_j[bar.ToString()];
                if (!ToolsMathBigInteger.AreCoprime(temp, p))
                {
                    return false;
                }
                else
                {
                    foo = temp.ModInverse(p);
                }
                foo = temp.ModInverse(p);
                h = h * foo;
                h = h % p;

                // release the public key
                h_j.Remove(bar.ToString());

                // finish
                return true;
            }
            else
            {
                return false;
            }
        }

        void CP_Prove(
            BigInteger x,
            BigInteger y,
            BigInteger gg,
            BigInteger hh,
            BigInteger alpha,
            IOutputChannel output_channel,
            bool fpowm_usage)
        {
      
            BigInteger b;
            BigInteger c;
            BigInteger r;
            BigInteger omega = mpz_srandom.mpz_srandomm(q);
            // proof of knowledge (equality of discrete logarithms) [CaS97]

            BigInteger a = g.ModPow(omega, p);
            b = g.ModPow(omega, p);

            // challenge
            // Here we use the well-known "Fiat-Shamir heuristic" to make
            // the PK non-interactive, i.e. we turn it into a statistically
            // zero-knowledge (Schnorr signature scheme style) proof of
            // knowledge (SPK) in the random oracle model.
            c = mpz_shash_tools.mpz_shash(new BigInteger [] {a, b, x, y, gg, hh});

            // response
            r = c * a;
            r = r.FlipSign();
            r = r + omega;
            r = r % q;
            output_channel.Send(c);
            output_channel.Send(r);
        }

        bool CP_Verify(
            BigInteger x,
            BigInteger y,
            BigInteger gg,
            BigInteger hh,
            IInputChannel input_channel,
            bool fpowm_usage)
        {
            BigInteger a;
            BigInteger b;
            BigInteger c = input_channel.Recieve();
            BigInteger r = input_channel.Recieve();

            // check the size of $r$
            if (ToolsMathBigInteger.CompareAbs(r, q) >= 0)
            {
                return false;
            }

            // verify proof of knowledge (equality of discrete logarithms) [CaS97]
            a = gg.ModPow(r, p);
            b = x.ModPow(c, p);
            a = a * b;
            a = a % p;

            b = hh.ModPow(r, p);
            r = y.ModPow(c, p);
            b = b * r;
            b = b % p;
            r = mpz_shash_tools.mpz_shash(new BigInteger [] {a, b, x, y, gg, hh});
            if (r != c)
            {
                return false;
            }
            // finish
            return true;

        }

        void OR_ProveFirst(
            BigInteger y_1,
            BigInteger y_2,
            BigInteger g_1,
            BigInteger g_2,
            BigInteger alpha,
            IOutputChannel output_channel)
        {
            // proof of knowledge ($y_1 = g_1^\alpha \vee y_2 = g_2^\beta$) [CaS97]

            // 1. choose $v_1, v_2$ and $w\in_R\mathbb{Z}_q$ and compute
            // $t_2 = y_2^w g_2^{v_2}$ and $t_1 = g_1^{v_1}$
            BigInteger v_1 = mpz_srandom.mpz_srandomm(q);
            BigInteger v_2 = mpz_srandom.mpz_srandomm(q);
            BigInteger w = mpz_srandom.mpz_srandomm(q);
            BigInteger t_2 = y_2.ModPow(w, p);
            BigInteger t_1 = g_1.ModPow(v_1, p);
            BigInteger tmp = g_2.ModPow(v_2, p);
            t_2 = t_2 * tmp;
            t_2 = t_2 % p;

            // 2. compute $c = \mathcal{H}(g_1, y_1, g_2, y_2, t_1, t_2)$
            BigInteger c = mpz_shash_tools.mpz_shash(new BigInteger [] {g_1, y_1, g_2, y_2, t_1, t_2});
            c = c % q;

            // 3. split the challenge: $c_2 = w$ and $c_1 = c - c_2$
            BigInteger c_2 = w;
            BigInteger c_1 = c - c_2;
            c_1 = c_1 % q;

            // 4. forge $r_2 = v_2$ and set $r_1 = v_1 - c_1\alpha$
            BigInteger r_2 = v_2 % q;
            ;
            tmp = c_1 * alpha;
            tmp = tmp % q;
            BigInteger r_1 = v_1 - tmp;
            r_1 = r_1 % q;
            output_channel.Send(c_1);
            output_channel.Send(c_2);
            output_channel.Send(r_1);
            output_channel.Send(r_2);

        }

        void OR_ProveSecond(
            BigInteger y_1,
            BigInteger y_2,
            BigInteger g_1,
            BigInteger g_2,
            BigInteger alpha,
            IOutputChannel output_channel)
        {
            // proof of knowledge ($y_1 = g_1^\beta \vee y_2 = g_2^\alpha$) [CaS97]

            // 1. choose $v_1, v_2$ and $w\in_R\mathbb{Z}_q$ and compute
            // $t_1 = y_1^w g_1^{v_1}$ and $t_2 = g_2^{v_2}$
            BigInteger v_1 = mpz_srandom.mpz_srandomm(q);
            BigInteger v_2 = mpz_srandom.mpz_srandomm(q);
            BigInteger w = mpz_srandom.mpz_srandomm(q);
            BigInteger t_1 =y_1.ModPow( w, p);
            BigInteger t_2 = g_2.ModPow(v_2, p);
            BigInteger tmp = g_1.ModPow(v_1, p);
            t_1 = t_1 * tmp;
            t_1 = t_1 % p;

            // 2. compute $c = \mathcal{H}(g_1, y_1, g_2, y_2, t_1, t_2)$
            BigInteger c = mpz_shash_tools.mpz_shash(new BigInteger [] {g_1, y_1, g_2, y_2, t_1, t_2});
            c = c % q;

            // 3. split the challenge: $c_1 = w$ and $c_2 = c - c_1$
            BigInteger c_1 = w;
            BigInteger c_2 = c - c_1;
            c_2 = c_2 % q;

            // 4. forge $r_1 = v_1$ and set $r_2 = v_2 - c_2\alpha$
            BigInteger r_1 = v_1 % q;
            tmp = c_2 * alpha;
            tmp = tmp % q;

            BigInteger r_2 = v_2 - tmp;
            r_2 = r_2 % q;

            output_channel.Send(c_1);
            output_channel.Send(c_2);
            output_channel.Send(r_1);
            output_channel.Send(r_2);
        }

        bool OR_Verify(
            BigInteger y_1,
            BigInteger y_2,
            BigInteger g_1,
            BigInteger g_2,
            IInputChannel input_channel)
        {
            BigInteger c_1 = input_channel.Recieve();
            BigInteger c_2 = input_channel.Recieve();
            BigInteger r_1 = input_channel.Recieve();
            BigInteger r_2 = input_channel.Recieve();
            BigInteger c;

            // check the size of $r_1$ and $r_2$
            if ((ToolsMathBigInteger.CompareAbs(r_1, q) >= 0L) || (ToolsMathBigInteger.CompareAbs(r_2, q) >= 0L))
            {
                return false;
            }

            // verify (S)PK ($y_1 = g_1^\alpha \vee y_2 = g_2^\beta$) [CaS97]
            BigInteger t_1 = y_1.ModPow(c_1, p);
            BigInteger tmp = g_1.ModPow(r_1, p);
            t_1 = t_1 * tmp;
            t_1 = t_1 % p;

            BigInteger t_2 = y_2.ModPow(c_2, p);
            tmp = g_2.ModPow(r_2, p);
            t_2 = t_2 * tmp;
            t_2 = t_2 % p;

            // check the equation
            // $c_1 + c_2 \stackrel{?}{=} \mathcal{H}(g_1, y_1, g_2, y_2, t_1, t_2)$
            tmp = c_1 + c_2;
            // c = c % q; ERROR this does nothing
            c = mpz_shash_tools.mpz_shash(new BigInteger [] {g_1, y_1, g_2, y_2, t_1, t_2});
            c = c % q;

            if (tmp != c)
            {
                return false;
            }
            // finish
            return true;

        }

        BigInteger MaskingValue()
        {
            // Choose randomly and uniformly an element from
            // $\mathbb{Z}_q \setminus \{0, 1\}$.
            BigInteger r = 0;
            do
            {
                r = mpz_srandom.mpz_srandomm(q);
            } while (r == 0 || r == 1);
            return r;
        }

        Tuple<BigInteger, BigInteger, BigInteger> VerifiableMaskingProtocol_Mask(
            BigInteger m)
        {
            BigInteger r = MaskingValue();

            // compute $c_1 = g^r \bmod p$
            BigInteger c_1 = g.ModPow(r, p);

            // compute $c_2 = m \cdot h^r \bmod p$
            BigInteger c_2 = h.ModPow(r, p);
            c_2 = c_2 * m;
            c_2 = c_2 % p;
            return new Tuple<BigInteger, BigInteger, BigInteger>(c_1, c_2, r);
        }

        void VerifiableMaskingProtocol_Prove(
            BigInteger m,
            BigInteger c_1,
            BigInteger c_2,
            BigInteger r,
            IOutputChannel output_channel)
        {
            BigInteger foo;

            // invoke CP(c_1, c_2/m, g, h; r) as prover
            Debug.Assert(ToolsMathBigInteger.AreCoprime(m, p));
            foo = m.ModInverse(p);
            foo = m.ModInverse(p);
            foo = foo * c_2;
            foo = foo % p;
            CP_Prove(c_1, foo, g, h, r, output_channel, true);
        }

        public bool VerifiableMaskingProtocol_Verify(
            BigInteger m,
            BigInteger c_1,
            BigInteger c_2,
            IInputChannel input_channel)
        {
            // verify the in-group properties
            if (!CheckElement(c_1) || !CheckElement(c_2))
            {
                return false;
            }

            // invoke CP(c_1, c_2/m, g, h; r) as verifier
            if (ToolsMathBigInteger.AreCoprime(m, p))
            {
                return false;
            }
            BigInteger foo = m.ModInverse(p);
            foo = foo * c_2;
            foo = foo % p;
            if (!CP_Verify(c_1, foo, g, h, input_channel, true))
            {
                return false;
            }

            // finish
            return true;

        }

        void VerifiableRemaskingProtocol_Mask(
            BigInteger c_1,
            BigInteger c_2,
            BigInteger c__1,
            BigInteger c__2,
            BigInteger r)
        {
            r = MaskingValue();

            // compute $c'_1 = c_1 \cdot g^r \bmod p$
            c__1 = g.ModPow(r, p);
            c__1 = c__1 * c_1;
            c__1 = c__1 % p;

            // compute $c'_2 = c_2 \cdot h^r \bmod p$
            c__2 = h.ModPow(r, p);
            c__2 = c__2 * c_2;
            c__2 = c__2 % p;
        }

        void VerifiableRemaskingProtocol_Remask(
            BigInteger c_1,
            BigInteger c_2,
            BigInteger c__1,
            BigInteger c__2,
            BigInteger r,
            bool TimingAttackProtection)
        {
            // compute $c'_1 = c_1 \cdot g^r \bmod p$
            if (TimingAttackProtection)
            {
                c__1 = g.ModPow(r, p);
            }
            else
            {
                c__1 = g.ModPow(r, p);
            }
            c__1 = c__1 * c_1;
            c__1 = c__1 % p;

            // compute $c'_2 = c_2 \cdot h^r \bmod p$
            if (TimingAttackProtection)
            {
                c__2 = h.ModPow(r, p);
            }
            else
            {
                c__2 = h.ModPow(r, p);
            }
            c__2 = c__2 * c_2;
            c__2 = c__2 % p;
        }

        void VerifiableRemaskingProtocol_Prove(
            BigInteger c_1,
            BigInteger c_2,
            BigInteger c__1,
            BigInteger c__2,
            BigInteger r,
            IOutputChannel output_channel)
        {
            BigInteger foo, bar;

            // invoke CP(c'_1/c_1, c'_2/c_2, g, h; r) as prover
            Debug.Assert (ToolsMathBigInteger.AreCoprime(c_1, p));
            foo = c_1.ModInverse(p);
            foo = foo * c__1;
            foo = foo % p;

            Debug.Assert (ToolsMathBigInteger.AreCoprime(c_2, p));
            bar = c_2.ModInverse(p);
            bar = bar * c__2;
            bar = bar % p;
            CP_Prove(foo, bar, g, h, r, output_channel, true);
        }

        bool VerifiableRemaskingProtocol_Verify(
            BigInteger c_1,
            BigInteger c_2,
            BigInteger c__1,
            BigInteger c__2,
            IInputChannel input_channel)
        {
            BigInteger foo = 0;
            BigInteger bar = 1;

            // verify the in-group properties
            if (!CheckElement(c__1) || !CheckElement(c__2))
            {
                return false;
            }

            // invoke CP(c'_1/c_1, c'_2/c_2, g, h; r) as verifier
            if (!ToolsMathBigInteger.AreCoprime(c_1, p))
            {
                return false;
            }
            foo = c_1.ModInverse(p);
            foo = foo * c__1;
            foo = foo % p;
            if (!ToolsMathBigInteger.AreCoprime(c_2, p))
            {
                return false;
            }
            bar = c_2.ModInverse(p);
            bar = bar * c__2;
            bar = bar % p;
            if (!CP_Verify(foo, bar, g, h, input_channel, true))
            {
                return false;
            }
            // finish
            return true;
        }

        void VerifiableDecryptionProtocol_Prove(
            BigInteger c_1,
            IOutputChannel output_channel)
        {
            // compute $d_i = {c_1}^{x_i} \bmod p$
            BigInteger d_i = c_1.ModPow(x_i, p);
            output_channel.Send(d_i);
            output_channel.Send(h_i_fp);

            // invoke CP(d_i, h_i, c_1, g; x_i) as prover
            CP_Prove(d_i, h_i, c_1, g, x_i, output_channel, false);

        }

        BigInteger VerifiableDecryptionProtocol_Verify_Initialize()
        {
            // compute $d = d_i = {c_1}^{x_i} \bmod p$
            return d.ModPow(x_i, p);//TODO check
        }

        bool VerifiableDecryptionProtocol_Verify_Update(
            BigInteger c_1,
            IInputChannel input_channel)
        {
            BigInteger d_j = input_channel.Recieve();
            BigInteger h_j_fp = input_channel.Recieve();

            // public key stored?
            String str = h_j_fp.ToString();
            if (!h_j.ContainsKey(str))
            {
                return false;
            }
            // verify the in-group property
            if (!CheckElement(d_j))
            {
                return false;
            }

            // invoke CP(d_j, h_j, c_1, g; x_j) as verifier
            if (!CP_Verify(d_j, h_j[str], c_1, g, input_channel, false))
            {
                return false;
            }
            // update the value of $d$
            d = d * d_j;
            d = d * p;

            // finish
            return true;
        }

        BigInteger VerifiableDecryptionProtocol_Verify_Finalize(
            BigInteger c_2)
        {
            Debug.Assert (ToolsMathBigInteger.AreCoprime(d, p));

            // finalize the decryption
            BigInteger m = d.ModInverse(p);
            m = m * c_2;
            m = m % p;
            return m;
        }

        public bool equals(
            Object other)
        {
            if (other is BarnettSmartVTMF_dlog)
            {
                BarnettSmartVTMF_dlog typed_other = (BarnettSmartVTMF_dlog) other;
                if (x_i != typed_other.x_i)
                {
                    return false;
                }
                if (h_i !=  typed_other.h_i)
                {
                    return false;
                }
                if (d!= typed_other.d)
                {
                    return false;
                }
                if (h_i_fp!= typed_other.h_i_fp)
                {
                    return false;
                }
                if (h_j!= typed_other.h_j)
                {
                    return false;
                }
                if (F_size != typed_other.F_size)
                {
                    return false;
                }
                if (G_size != typed_other.G_size)
                {
                    return false;
                }
                if (p  != typed_other.p)
                {
                    return false;
                }

                if (q!= typed_other.q)
                {
                    return false;
                }

                if (g!= typed_other.g)
                {
                    return false;
                }

                if (k!= typed_other.k)
                {
                    return false;
                }
                if (h!= typed_other.h)
                {
                    return false;
                }
            }
            return false;

        }

    }
}