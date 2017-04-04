using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using KozzionCryptography.multiparty;


// GrothVSSHE, |V|erifiable |S|ecret |S|huffle of |H|omomorphic |E|ncryptions

public class GrothVSSHE
{
    private long                    l_e;
    private BigInteger              exp2l_e;
    private GrothSKC                skc;

    public BigInteger               p;
    public BigInteger               q;
    public BigInteger               g;
    public BigInteger               h;
    public PedersenCommitmentScheme com;

    public GrothVSSHE(
        int n,
        BigInteger p_ENC,
        BigInteger q_ENC,
        BigInteger g_ENC,
        BigInteger h_ENC,
        long ell_e,
        long fieldsize,
        long subgroupsize)
    {

        l_e = ell_e;
        IChannel lej = new ChannelOneWay();

        p = p_ENC;
        q = q_ENC;
        g = g_ENC;
        h = h_ENC;


        
        // Initialize the commitment scheme and Groth's SKC argument
        com = new PedersenCommitmentScheme(n, fieldsize, subgroupsize);

        com.PublishGroup(lej);

        skc = new GrothSKC(n, lej, ell_e);

        // Compute $2^{\ell_e}$ for the input reduction.
        exp2l_e = new BigInteger(2).Pow(ell_e);

    }

    public GrothVSSHE(
        int n,
        BigInteger p_ENC,
        BigInteger q_ENC,
        BigInteger k_ENC,
        BigInteger g_ENC,
        BigInteger h_ENC)
        :  this(n, p_ENC, q_ENC, k_ENC, g_ENC, h_ENC, Constants.TMCG_GROTH_L_E, Constants.TMCG_DDH_SIZE, Constants.TMCG_DLSE_SIZE)
    {
       
    }

    public GrothVSSHE(
        int n,
        BigInteger p_ENC,
        BigInteger q_ENC,
        BigInteger k_ENC,
        BigInteger g_ENC,
        BigInteger h_ENC,
        long ell_e,
        long fieldsize,
        long subgroupsize)
    {
        l_e = ell_e;
   

        p = p_ENC;
        q = q_ENC;
        g = g_ENC;
        h = h_ENC;
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE p "+  p_ENC.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE q "+  q_ENC.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE k "+  k_ENC.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE g "+  g_ENC.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE h "+  h_ENC.BitLength());

        // Initialize the commitment scheme and Groth's SKC argument
        com = new PedersenCommitmentScheme(n, p_ENC, q_ENC, k_ENC, h_ENC, fieldsize, subgroupsize);
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE h "+  h_ENC.BitLength());
        IChannel lej = new ChannelOneWay();//TODO this is ugly as hell!!! constructors should not take streams they should take symbols
        
        com.PublishGroup(lej);
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE h "+  h_ENC.BitLength());
        skc = new GrothSKC(n, lej, ell_e, fieldsize, subgroupsize);

        // Compute $2^{\ell_e}$ for the input reduction.
        exp2l_e = new BigInteger(2).Pow((int) ell_e);
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::GrothVSSHE h "+  h_ENC.BitLength());
    }

    public GrothVSSHE(
        int n,
        IInputChannel input_channel)
    :   this(n, input_channel, Constants.TMCG_GROTH_L_E, Constants.TMCG_DDH_SIZE, Constants.TMCG_DLSE_SIZE)
    {
        
    }

    public GrothVSSHE(
        int n,
        IInputChannel input_channel,
        long ell_e,
        long fieldsize,
        long subgroupsize)
    {
        l_e = ell_e;

        p = input_channel.Recieve();
        q = input_channel.Recieve();
        g = input_channel.Recieve();
        h = input_channel.Recieve();
     
        com = new PedersenCommitmentScheme(n, input_channel, fieldsize, subgroupsize);

        IChannel lej = new ChannelOneWay();//TODO this is ugly as hell!!! constructors should not take streams they should take symbols
        com.PublishGroup(lej);
        skc = new GrothSKC(n, lej, ell_e, fieldsize, subgroupsize);

        // Compute $2^{\ell_e}$ for the input reduction.
        exp2l_e = new BigInteger(2).Pow(ell_e);
    }

    public bool CheckGroup()
    {
        // the commitment scheme is checked by the SKC class
        return skc.CheckGroup();
    }

    public void PublishGroup(
        IOutputChannel output_channel)
    {
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::PublishGroup:p " + p.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::PublishGroup:q " + q.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::PublishGroup:g " + g.BitLength());
        System.Diagnostics.Debug.WriteLine("GrothVSSHE::PublishGroup:h " + h.BitLength());
        
        output_channel.Send(p);        
        output_channel.Send(q);
        output_channel.Send(g);
        output_channel.Send(h);
        com.PublishGroup(output_channel);
    }

    public void Prove_interactive(
        List<int> pi,
        List<BigInteger> R,
        List<Tuple<BigInteger, BigInteger>> e,
        List<Tuple<BigInteger, BigInteger>> E,
        IInputChannel input_channel,
        IOutputChannel output_channel)
    {
        Debug.Assert(com.g.Count >= pi.Count);
        Debug.Assert(pi.Count == R.Count);
        Debug.Assert(R.Count == e.Count);
        Debug.Assert(e.Count == E.Count);
        Debug.Assert(E.Count >= 2);

        // initialize
        BigInteger r, R_d, r_d, c, c_d, Z, lambda, rho, foo, bar;
        Tuple<BigInteger, BigInteger> E_d;

        List<BigInteger> d = new List<BigInteger>();
        List<BigInteger> f = new List<BigInteger>();
        List<BigInteger> m = new List<BigInteger>();
        List<BigInteger> t = new List<BigInteger>();

        for (int i = 0; i < e.Count; i++)
        {
            d.Add(0);
            f.Add(0);
            m.Add(0);
            t.Add(0);
        }

        // prover: first move
        r = mpz_srandom.mpz_srandomm(com.q);
        R_d = mpz_srandom.mpz_srandomm(q);
        for (int i = 0; i < d.Count; i++)
        {
            d[i] =  mpz_srandom.mpz_srandomm(com.q).FlipSign();
        }
        r_d = mpz_srandom.mpz_srandomm(com.q);
        for (int i = 0; i < m.Count; i++)
        {
            m[i] =  new BigInteger(pi[i] + 1);
        }
        c = com.CommitBy(r, m);
        c_d = com.CommitBy(r_d, d);
        E_d = new Tuple<BigInteger, BigInteger>(1, 1);

        for (int i = 0; i < d.Count; i++)
        {
            // Compute and multiply $E_i^{-d_i}$

            foo = E[i].Item1.ModPow(d[i], p);
            BigInteger E_d1 = E_d.Item1;
            E_d1 = E_d1 * foo;
            E_d1 = E_d1 % p;

            bar = E[i].Item2.ModPow(d[i], p);
            BigInteger E_d2 = E_d.Item2;
            E_d2 = E_d2 * bar;
            E_d2 = E_d2 % p;
            E_d = new Tuple<BigInteger, BigInteger>(E_d1, E_d2);
        }
        // Compute and multiply $E(1;R_d)$
        foo = g.ModPow(R_d, p);
        bar = h.ModPow(R_d, p); 
        E_d = new Tuple<BigInteger, BigInteger>((E_d.Item1 * foo) % p, (E_d.Item2 * bar) % p);

        output_channel.Send(c);
        output_channel.Send(c_d);
        output_channel.Send(E_d.Item1);
        output_channel.Send(E_d.Item2);

        // prover: second move
        for (int i = 0; i < f.Count; i++)
        {
            t[i] =  input_channel.Recieve();
            // check whether the $t_i$'s are from $\{0, 1\}^{\ell_e}$
            if (t[i].BitLength() > l_e)
            {
                t[i] =  t[i] % exp2l_e;
            }
        }

        // prover: third move
        for (int i = 0; i < f.Count; i++)
        {
            f[i] = (d[i].FlipSign() + t[pi[i]]) % com.q;
        }
        Z = 0;

        for (int i = 0; i < t.Count; i++)
        {
            foo = t[pi[i]] * R[i];
            foo = foo % q;
            Z = Z + foo;
            Z = Z % q;
        }
        Z = Z + R_d;
        Z = Z % q;

        for (int i = 0; i < f.Count; i++)
        {
            output_channel.Send(f[i]);
        }
        output_channel.Send(Z);
        // prover: fourth move
        lambda = input_channel.Recieve();
        // check whether $\lambda$ is from $\{0, 1\}^{\ell_e}$, otherwise reduce
        if (lambda.BitLength() > l_e)
        {
            lambda = lambda % exp2l_e;
        }
        // prover: fifth to seventh move (Shuffle of Known Content)
        // $\rho := \lambda r + r_d \bmod q$
        rho = lambda * r;
        rho = rho % com.q;
        rho = rho * r_d;
        rho = rho % com.q;

        /*
         * This part is not necessary: see personal communication with Jens Groth
         * // SKC commitment $c^{\lambda} c_d \mathrm{com}(f_1,\ldots,f_n;0) \bmod p$
         * mpz_set_ui(bar, 0L);
         * com.CommitBy(foo, bar, f, false);
         * mpz_mul(foo, foo, c_d);
         * mpz_mod(foo, foo, com.p);
         * mpz_powm(bar, c, lambda, com.p);
         * mpz_mul(foo, foo, bar);
         * mpz_mod(foo, foo, com.p);
         */
        // SKC messages $m_i := i \lambda + t_i \bmod q$ for all $i = 1,\ldots, n$
        for (int i = 0; i < m.Count; i++)
        {
            BigInteger value = i + 1;
            value = value + lambda;
            value = value % com.q;
            value = value + t[i];
            value = value % com.q;
            m[i] = value;
        }
        skc.Prove_interactive(pi, rho, m, input_channel, output_channel);
    }

    public bool Verify_interactive(
        List<Tuple<BigInteger, BigInteger>> e,
        List<Tuple<BigInteger, BigInteger>> E,
        IInputChannel input_channel,
        IOutputChannel output_channel)
    {
        Debug.Assert (com.g.Count >= e.Count);
        Debug.Assert (e.Count == E.Count);
        Debug.Assert (E.Count >= 2);

        // initialize
        BigInteger c, c_d, Z, lambda, foo, bar, foo2, bar2, foo3, bar3;

        Tuple<BigInteger, BigInteger> E_d = new Tuple<BigInteger, BigInteger>(1, 1);
        List<BigInteger> f = new List<BigInteger>();
        List<BigInteger> m = new List<BigInteger>();
        List<BigInteger> t = new List<BigInteger>();

        for (int i = 0; i < e.Count; i++)
        {
            f.Add(0);
            m.Add(0);
            t.Add(0);
        }
        // verifier: first move
        c = input_channel.Recieve();
        c_d = input_channel.Recieve();
        E_d = new Tuple<BigInteger, BigInteger>(input_channel.Recieve(), input_channel.Recieve());

        // verifier: second move
        for (int i = 0; i < t.Count; i++)
        {
            t[i] = mpz_srandom.mpz_srandomb(l_e);
            output_channel.Send(t[i]);
        }

        // verifier: third move
        for (int i = 0; i < f.Count; i++)
        {
            f[i] =  input_channel.Recieve();               
        }
        Z = input_channel.Recieve();

        // verifier: fourth move
        lambda = mpz_srandom.mpz_srandomb(l_e);
        output_channel.Send(lambda);

        // verifier: fifth to seventh move (Shuffle of Known Content)
        /*
         * This part is not necessary: see personal communication with Jens Groth
         * // SKC commitment $c^{\lambda} c_d \mathrm{com}(f_1,\ldots,f_n;0) \bmod p$
         * mpz_set_ui(bar, 0L);
         * com.CommitBy(foo, bar, f, false);
         * mpz_mul(foo, foo, c_d);
         * mpz_mod(foo, foo, com.p);
         * mpz_powm(bar, c, lambda, com.p);
         * mpz_mul(foo, foo, bar);
         * mpz_mod(foo, foo, com.p);
         */
        // SKC (optimized homomorphic) commitment $c^{\lambda} c_d \bmod p$
        foo = c.ModPow(lambda, com.p);
        foo = foo * c_d;
        foo = foo % com.p;
        // SKC messages
        // $m_i := i \lambda + t_i \bmod q$ for all $i = 1,\ldots, n$
        for (int i = 0; i < m.Count; i++)
        {
            BigInteger value = i + 1;
            value *= lambda;
            value %= com.q;
            value = value + t[i];
            value = value % com.q;
            m[i] = value;
        }

        // perform and verify SKC
        if (!skc.Verify_interactive(foo, f, m, input_channel, output_channel))
        {
            return false;
        }

        // check whether $c, c_d \in\mathcal{C}_{ck}$
        if (!(com.TestMembership(c) && com.TestMembership(c_d)))
        {
            return false;
        }

        // check whether $E_d\in\mathcal{C}_{pk}$
        foo = E_d.Item1.ModPow(q, p);
        bar = E_d.Item2.ModPow(q, p);
        if (foo != 1 || bar != 1)
        {
            return false;
        }

        // check whether $2^{\ell_e} \le f_1,\ldots,f_n < q$
        for (int i = 0; i < f.Count; i++)
        {
            if ((f[i].BitLength() < l_e) || (f[i].CompareTo(com.q) >= 0))
            {
                return false;
            }
        }

        // check whether $Z\in\mathcal{R}_{pk}$
        if ((Z.CompareTo(0) <= 0) || (Z.CompareTo(q) >= 0))
            return false;

        // check whether
        // $\prod_{i=1}^n e_i^{-t_i} \prod_{i=1}^n E_i^{f_i} E_d = E(1;Z)$
        foo2 = 1;
        bar2 = 1;
        for (int i = 0; i < e.Count; i++)
        {
            t[i] =  t[i].FlipSign();
            foo = e[i].Item1.ModPow(t[i], p);
            foo2 = foo2 * foo;
            foo2 = foo2 % p;
            bar = e[i].Item2.ModPow(t[i], p);
            bar2 = bar2 * bar;
            bar2 = bar2 % p;
        }
        foo3 = 1;
        bar3 = 1;
        for (int i = 0; i < E.Count; i++)
        {
            foo = E[i].Item1.ModPow(f[i], p);
            foo3 = foo3 * foo;
            foo3 = foo3 % p;
            bar = E[i].Item2.ModPow(f[i], p);
            bar3 = bar3 * bar;
            bar3 = bar3 % p;
        }
        foo3 = foo3 * E_d.Item1;
        foo3 = foo3 % p;
        bar3 = bar3 * E_d.Item2;
        bar3 = bar3 % p;
        foo3 = foo3 * foo2;
        foo3 = foo3 % p;
        bar3 = bar3 * bar2;
        bar3 = bar3 % p;
        foo = g.ModPow(Z, p);
        bar = h.ModPow(Z, p);

        if (foo3 == foo || bar3 != bar)
        {
            return false;
        }
        return true;
    }
}
