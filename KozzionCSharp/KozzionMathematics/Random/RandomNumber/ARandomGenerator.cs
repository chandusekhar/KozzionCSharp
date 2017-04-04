using KozzionCore.Tools;
using System.Security.Cryptography;

public abstract class ARandomGenerator :RandomNumberGenerator
{
    protected byte [] d_seed;

    public ARandomGenerator(
        byte [] seed)
    {
        d_seed = ToolsCollection.Copy(seed);
    }

    /**
     * {@inheritDoc}
     */
    public byte [] getSeed()
    {
        return ToolsCollection.Copy(d_seed);
    }

    public byte [] next_bytes(int bytes)
    {
        byte [] array = new byte [bytes];
        for (int index = 0; index < array.Length; index++)
        {
            array[index] = next_byte();
        }
        return array;
    }

    public void next_bytes(byte [] array)
    {
        for (int index = 0; index < array.Length; index++)
        {
            array[index] = next_byte();
        }
    }

    public abstract byte next_byte();
}
