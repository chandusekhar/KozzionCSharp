using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using KozzionCryptography.multiparty;
using KozzionMathematics.Tools;

public class PedersenCommitmentScheme
{
    private long                F_size;
    private long                G_size;

    public BigInteger           p;
    public BigInteger           q;
    public BigInteger           k;
    public BigInteger           h;
    public List<BigInteger>     g;

    public PedersenCommitmentScheme(
        int n,
        long fieldsize,
        long subgroupsize)
    {
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme");
        Debug.Assert (n >= 1);
        F_size = fieldsize;
        G_size = subgroupsize;

        // Initialize and choose the parameters of the commitment scheme.
        Tuple<BigInteger, BigInteger, BigInteger> primes = MPPrime.MPZLPrime(fieldsize, subgroupsize, Constants.TMCG_MR_ITERATIONS);
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::Got primes");
        p = primes.Item1;
        q = primes.Item2;
        k = primes.Item3;
        h = 1;
        g = new List<BigInteger>();
        
        BigInteger foo = p - 1; // compute $p-1$
        for (int i = 0; i <= n; i++)
        {
            BigInteger tmp = 0;

            // choose uniformly at random an element of order $q$
            do
            {
                tmp = mpz_srandom.mpz_wrandomm(p);
                tmp = tmp.ModPow(k, p);
            }
            while (tmp == 0 || tmp == 1 || tmp == foo); // check, whether $1 < tmp < p-1$

            if (i < n)
            {
                // store the elements $g_1, \ldots, g_n$
                g.Add(tmp);
            }
            else
            {
                // the last element is called $h$
                h = tmp;
            }
        }
    }

    public PedersenCommitmentScheme(
        int n,
        BigInteger p_ENC,
        BigInteger q_ENC,
        BigInteger k_ENC,
        BigInteger h_ENC,
        long fieldsize,
        long subgroupsize)
    {
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme");
        F_size = fieldsize;
        G_size = subgroupsize;

        Debug.Assert (n >= 1);

        // Initialize and choose the parameters of the commitment scheme.
        p = p_ENC;
        q = q_ENC;
        k = k_ENC;
        h = h_ENC;
        g = new List<BigInteger>();
        BigInteger foo = p - 1;// compute $p-1$
        for (int i = 0; i < n; i++)
        {
            BigInteger tmp = 0;

            // choose uniformly at random an element of order $q$
            do
            {
                tmp = mpz_srandom.mpz_wrandomm(p);
                tmp = tmp.ModPow(k, p);
            }
            while (tmp.CompareTo(1) < 0 || tmp.CompareTo(foo) > 0); // check, whether $1 < tmp < p-1$

            // store the elements $g_1, \ldots, g_n$
            g.Add(tmp);
        }
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme::Done");
    }

    public PedersenCommitmentScheme(
        int n,
        IInputChannel chanal,
        long fieldsize,
        long subgroupsize)

    {
        F_size = fieldsize;
        G_size = subgroupsize;
        Debug.Assert(n >= 1);

        // Initialize the parameters of the commitment scheme.
        p = chanal.Recieve();
        q = chanal.Recieve();
        k = chanal.Recieve();
        h = chanal.Recieve();
        
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme p " + p.BitLength());
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme q " + q.BitLength());
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme k " + k.BitLength());
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PedersenCommitmentScheme h " + h.BitLength());
        
        g = new List<BigInteger>();
        for (int i = 0; i < n; i++)
        {
            g.Add(chanal.Recieve());
        }
    }

    public bool CheckGroup()
    {

        // Check whether $p$ and $q$ have appropriate sizes.
        if ((p.BitLength() < F_size) || (q.BitLength() < G_size))
        {
            throw new Exception("$p$ and $q$ do not have appropriate sizes.");
        }

        // Check whether $p$ has the correct form, i.e. $p = kq + 1$.
        if (p != (k * q) + 1)
        {
            throw new Exception("$p$ does not have the correct form.");
        }
        // Check whether $p$ and $q$ are both (probable) prime with
        // a soundness error probability ${} \le 4^{-TMCG_MR_ITERATIONS}$.
        if (!p.IsProbablePrime(Constants.TMCG_MR_ITERATIONS) || !q.IsProbablePrime(Constants.TMCG_MR_ITERATIONS))
        {
            throw new Exception("$p$ and $q$ are not both (probable) prime with a large soundness error probability.");
        }

        // Check whether $k$ is not divisible by $q$, i.e. $q, k$ are coprime.

        if (!ToolsMathBigInteger.AreCoprime(q, k))
        {
            throw new Exception("$q, k$ are not coprime.");
        }
        // Check whether the elements $h, g_1, \ldots, g_n$ are of order $q$.
        BigInteger foo = h.ModPow(q, p);

        if (foo != 1)
        {
            throw new Exception("the elements $h, g_1, ldots, g_n$ are not of order $q$.");
        }

        for (int i = 0; i < g.Count; i++)
        {
            foo = g[i].ModPow(q, p);
            if (foo != 1)
            {
                throw new Exception("the elements $h, g_1, ldots, g_n$ are not of order $q$.");
            }

        }

        // Check whether the elements $h, g_1, \ldots, g_n$ are different
        // and non-trivial, i.e., $1 < h, g_1, \ldots, g_n < p-1$.
        foo = p - 1; // compute $p-1$
        if ((h.CompareTo(new BigInteger(1)) <= 0) || (h.CompareTo(foo) >= 0))
        {
            return false;
        }
        for (int i = 0; i < g.Count; i++)
        {
            if ((g[i].CompareTo(1) <= 0) || (g[i].CompareTo(foo) >= 0) || g[i] == h)
            {
                return false;
            }
            for (int j = (i + 1); j < g.Count; j++)
            {
                if (g[i] == g[j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PublishGroup(
        IOutputChannel output_channel)
    {
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PublishGroup:p " + p.BitLength());
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PublishGroup:q " + q.BitLength());
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PublishGroup:k " + k.BitLength());
        System.Diagnostics.Debug.WriteLine("PedersenCommitmentScheme::PublishGroup:h " + h.BitLength());
        output_channel.Send(p);
        output_channel.Send(q);
        output_channel.Send(k);
        output_channel.Send(h);       
        
        
        for (int i = 0; i < g.Count; i++)
        {
            output_channel.Send(g[i]);
        }
    }

    BigInteger Commit(
        BigInteger r,
        List<BigInteger> m)
    {
        Debug.Assert (m.Count <= g.Count);

        // Choose a randomizer from $\mathbb{Z}_q$
        r = mpz_srandom.mpz_srandomm(q);

        // Compute the commitment $c := g_1^{m_1} \cdots g_n^{m_n} h^r \bmod p$
        BigInteger c = h.ModPow(r, p);
        for (int i = 0; i < m.Count; i++)
        {
            BigInteger tmp = g[i].ModPow(m[i], p);
            c = c * tmp;
            c = c % p;
        }
        return c;
    }

    public BigInteger CommitBy(
        BigInteger r,
        List<BigInteger> m,
        bool TimingAttackProtection)
    {
        Debug.Assert (m.Count <= g.Count);
        Debug.Assert (r.CompareTo(q) < 0);
        BigInteger c;
        // Compute the commitment $c := g_1^{m_1} \cdots g_n^{m_n} h^r \bmod p$
        BigInteger tmp = 0;
        if (TimingAttackProtection)
        {
            c = h.ModPow(r, p);
        }
        else
        {
            c = h.ModPow(r, p);
        }

        for (int i = 0; i < m.Count; i++)
        {
            if (TimingAttackProtection)
            {
                tmp = g[i].ModPow(m[i], p);
            }
            else
            {
                tmp = g[i].ModPow(m[i], p);
            }
            c = (c * tmp) % p;
        }
        return c;
    }

    public BigInteger CommitBy(
        BigInteger r,
        List<BigInteger> m)
    {
        return CommitBy(r, m, false);
    }

    public bool TestMembership(
        BigInteger c)
    {
        if ((c.CompareTo(0) > 0) && (c.CompareTo(p) < 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Verify(
        BigInteger c,
        BigInteger r,
        BigInteger [] m)
    {
        Debug.Assert (m.Length <= g.Count);

        // Compute the commitment $c' := g_1^{m_1} \cdots g_n^{m_n} h^r \bmod p$
        BigInteger c2 = h.ModPow(r, p);
        for (int i = 0; i < m.Length; i++)
        {
            BigInteger tmp = g[i].ModPow(m[i], p);
            c2 = c2 * tmp;
            c2 = c2 % p;
        }

        // Verify the commitment: 1. $c\in\mathbb{Z}_p$ and 2. $c = c'$
        if ((c.CompareTo(0) < 0) || (c.CompareTo(p) >= 0) || c != c2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
