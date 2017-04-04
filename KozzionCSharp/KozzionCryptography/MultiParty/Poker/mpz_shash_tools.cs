using System;
using System.Numerics;
using System.Security.Cryptography;
using KozzionCore.Tools;

public class mpz_shash_tools
{
    private static HashAlgorithm s_algoritm = new SHA256Cng();

    // hash function h() (collision-resistant?)
    public static String h(
        String input)
    {
        return ToolsString.ConvertToString(s_algoritm.ComputeHash(ToolsString.ConvertToBytes(input)));
    }

    // hash function g() (The design is based on the ideas of [BR95].)
    //TODO shit is really dirty!!!!
    public static String g(
        String input,
        int hash_size)
    {
       
        int mdsize = s_algoritm.HashSize;
        int usesize = mdsize / 4;
        int times = (hash_size / usesize) + 1;
        String result = "";
        for (int i = 0; i < times; i++)
        {
            /* construct the expanded input y = x || TMCG<i> || x */
            String data = input + "libTMCG%02x" + input;

            /* using h(y) "in some nonstandard way" with "output truncated" [BR95] */
            result += h(data);
        }
        return result;
    }

    public static BigInteger mpz_shash(
        String input)
    {
        /* hash the input */
        String digest = h(input);

        /* convert the digest to a hexadecimal encoded string */
        String hex_digest = ToolsString.ConvertToStringHex(digest);
        
        /* convert the hexadecimal encoded string to an mpz-integer */
        return BigInteger.Parse(hex_digest, System.Globalization.NumberStyles.HexNumber);
    }

    /*
     * Hashing of the public inputs (aka Fiat-Shamir heuristic) with h(),
     * e.g. to make some proofs of knowledge non-interactive.
     */
    public static BigInteger mpz_shash(
        BigInteger [] array)
    {
        String acc = "";

        /* concatenate all the arguments */
        for (int i = 0; i < array.Length; i++)
        {
            acc += ToolsString.ConvertToStringHex(array[i].ToString()) + "|";
        }

        /* hash arguments */
        return mpz_shash(acc);
    }

    public static BigInteger mpz_shash(
        BigInteger value)
    {
        return mpz_shash(ToolsString.ConvertToStringHex(value.ToString()) + "|");
    }
}
