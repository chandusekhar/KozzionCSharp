package com.kozzion.library.math.random;

import java.security.SecureRandom;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Random;
import java.util.Set;

import com.kozzion.library.math.random.numbergenerator.IRandomGenerator;

public class RandomTools
{

    public static Set<Integer> draw_unique_set(final int draw_count, final int inclusive_lower_bound, final int exclusive_upper_bound)
    {
        final int interval_size = exclusive_upper_bound - inclusive_lower_bound;
        if (interval_size < draw_count)
        {
            throw new IllegalArgumentException("Error: draw_count to large");
        }
        final Set<Integer> drawings = new HashSet<Integer>();
        final Random random = new SecureRandom();
        while (drawings.size() < draw_count)
        {
            drawings.add(random.nextInt(interval_size) + inclusive_lower_bound);
        }
        return drawings;

    }

    public static List<Integer> draw_unique_list(final int draw_count, final int inclusive_lower_bound, final int exclusive_upper_bound)
    {
        final Set<Integer> drawings = draw_unique_set(draw_count, inclusive_lower_bound, exclusive_upper_bound);
        final List<Integer> drawings_list = new ArrayList<Integer>(drawings);
        Collections.shuffle(drawings_list);
        return drawings_list;
    }

//    public static int draw_integer(int value_at_least, int value_smaller_than, IRandomGenerator random)
//    {
//        int size = value_smaller_than - value_at_least;
//        int value = (random.next_int() % size) + (random.next_int() % size ) % size;
// 
//
//        return 0;
//    }

}
