package com.kozzion.library.math.random.numbergenerator;

import java.util.Random;

import com.kozzion.library.core.utility.ToolsBinary;

public class RandomGeneratorDefault
    extends
        ARandomGenerator
{

    private static final int SEED_SIZE_BYTES = 8;

    private Random           d_random;
    private int              d_random_byte_index;

    private byte []          d_random_bytes;

    /**
     * Creates a new RNG and seeds it using the default seeding strategy.
     */
    public RandomGeneratorDefault()
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
    public RandomGeneratorDefault(
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
    public RandomGeneratorDefault(
        byte [] seed)
    {
        super(seed);
        if (seed == null || seed.length != SEED_SIZE_BYTES)
        {
            throw new IllegalArgumentException("Java RNG requires a 64-bit (8-byte) seed.");
        }
        
        d_random = new Random(ToolsBinary.convertBytesToLong(seed, 0));
        d_random_bytes = new byte [4];
        d_random_byte_index = 3;
    }

    public int next_int()
    {
        return d_random.nextInt();
    }
    
    public synchronized byte next_byte()
    {
        if(d_random_byte_index == 3)
        {
            d_random_byte_index = -1;
            ToolsBinary.convert_int_to_bytes(next_int(), d_random_bytes);
        }
        
        d_random_byte_index++;
        return d_random_bytes[d_random_byte_index];

    }
}
