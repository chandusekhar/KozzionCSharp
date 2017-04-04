package com.kozzion.library.math.random.numbergenerator;

import com.kozzion.library.core.utility.ToolsBinary;

public class RandomGeneratorCMWC
    extends
        ARandomGenerator
{
    private static final int  SEED_SIZE_BYTES = 16384; // Needs 4,096 32-bit integers.

    private static final long A               = 18782L;

    private final int []      state;
    private int               carry           = 362436; // TODO: This should be randomly generated.
    private int               index           = 4095;

    private int               d_random_byte_index;

    private byte []           d_random_bytes;

    /**
     * Creates a new RNG and seeds it using the default seeding strategy.
     */
    public RandomGeneratorCMWC()
    {
        this(new SeedGeneratorDefault().generate_seed(SEED_SIZE_BYTES));
    }

    /**
     * Seed the RNG using the provided seed generation strategy.
     * 
     * @param seedGenerator
     *            The seed generation strategy that will provide
     *            the seed value for this RNG.
     * @throws SeedException
     *             If there is a problem generating a seed.
     */
    public RandomGeneratorCMWC(
        ISeedGenerator seedGenerator)
    {
        this(seedGenerator.generate_seed(SEED_SIZE_BYTES));
    }

    /**
     * Creates an RNG and seeds it with the specified seed data.
     * 
     * @param seed
     *            The seed data used to initialise the RNG.
     */
    public RandomGeneratorCMWC(
        byte [] seed)
    {
        super(seed);
        if (seed == null || seed.length != SEED_SIZE_BYTES)
        {
            throw new IllegalArgumentException("CMWC RNG requires 16kb of seed data.");
        }
        this.state = ToolsBinary.convertBytesToInts(seed);
        d_random_bytes = new byte [4];
        d_random_byte_index = 3;
    }

    private int next_int_private()
    {
        index = (index + 1) & 4095;
        long t = A * (state[index] & 0xFFFFFFFFL) + carry;
        carry = (int) (t >> 32);
        int x = ((int) t) + carry;
        if (x < carry)
        {
            x++;
            carry++;
        }
        state[index] = 0xFFFFFFFE - x;
        return state[index];

    }

    @Override
    public byte next_byte()
    {
        if (d_random_byte_index == 3)
        {
            d_random_byte_index = -1;
            ToolsBinary.convert_int_to_bytes(next_int_private(), d_random_bytes);
        }

        d_random_byte_index++;
        return d_random_bytes[d_random_byte_index];
    }
}
