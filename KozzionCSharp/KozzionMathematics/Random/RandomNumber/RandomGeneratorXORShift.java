package com.kozzion.library.math.random.numbergenerator;

import java.util.Random;

import com.kozzion.library.core.utility.ToolsBinary;

/**
 * <p>
 * Very fast pseudo random number generator. See <a
 * href="http://school.anhb.uwa.edu.au/personalpages/kwessen/shared/Marsaglia03.html">this page</a> for a description.
 * This RNG has a period of about 2^160, which is not as long as the {@link RandomGeneratorMersenneTwister} but it is
 * faster.
 * </p>
 * <p>
 * <em>NOTE: Because instances of this class require 160-bit seeds, it is not
 * possible to seed this RNG using the {@link #setSeed(long)} method inherited
 * from {@link Random}.  Calls to this method will have no effect.
 * Instead the seed must be set by a constructor.</em>
 * </p>
 *
 * @author Daniel Dyer
 * @since 1.2
 */
public class RandomGeneratorXORShift
    extends
        ARandomGenerator
    implements
        IRandomGenerator
{

    private static final int SEED_SIZE_BYTES = 20; // Needs 5 32-bit integers.

    // Previously used an array for state but using separate fields proved to be
    // faster.
    private int              state1;
    private int              state2;
    private int              state3;
    private int              state4;
    private int              state5;

    private int              d_random_byte_index;

    private byte []          d_random_bytes;

    /**
     * Creates a new RNG and seeds it using the default seeding strategy.
     */
    public RandomGeneratorXORShift()
    {
        this(new SeedGeneratorDefault());
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
    public RandomGeneratorXORShift(
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
    public RandomGeneratorXORShift(
        byte [] seed)
    {
        super(seed);
        if (seed == null || seed.length != SEED_SIZE_BYTES)
        {
            throw new IllegalArgumentException("XOR shift RNG requires 160 bits of seed data.");
        }
        d_random_bytes = new byte [4];
        d_random_byte_index = 3;
        int [] state = ToolsBinary.convertBytesToInts(d_seed);
        this.state1 = state[0];
        this.state2 = state[1];
        this.state3 = state[2];
        this.state4 = state[3];
        this.state5 = state[4];
    }

    private int next_int_private()
    {
        int t = (state1 ^ (state1 >> 7));
        state1 = state2;
        state2 = state3;
        state3 = state4;
        state4 = state5;
        state5 = (state5 ^ (state5 << 6)) ^ (t ^ (t << 13));
        return (state2 + state2 + 1) * state5;
    }

    public synchronized byte next_byte()
    {
        if(d_random_byte_index == 3)
        {
            d_random_byte_index = -1;
            ToolsBinary.convert_int_to_bytes(next_int_private(), d_random_bytes);
        }
        
        d_random_byte_index++;
        return d_random_bytes[d_random_byte_index];

    }
}
