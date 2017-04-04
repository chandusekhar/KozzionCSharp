package com.kozzion.library.math.function.implementation;

import java.util.HashSet;
import java.util.List;
import java.util.PriorityQueue;
import java.util.Set;
import java.util.Vector;

import com.kozzion.library.math.function.IFunctionIntegerToBinary;
import com.kozzion.library.math.function.domain.IDomainExplicit;
import com.kozzion.library.math.function.domain.INumberDomain;
import com.kozzion.library.math.function.domain.implementation.IntegerInterval;

public class IntegerIntervalList implements IFunctionIntegerToBinary, IDomainExplicit<Integer>, INumberDomain<Integer>
{

    final List<IntegerInterval> d_integer_intervals;

    public IntegerIntervalList()
    {
        d_integer_intervals = new Vector<IntegerInterval>();
    }

    public IntegerIntervalList(final IntegerInterval d_integer_interval)
    {
        d_integer_intervals = new Vector<IntegerInterval>();
    }

    public IntegerIntervalList(final List<IntegerIntervalList> functions)
    {
        d_integer_intervals = new Vector<IntegerInterval>();
        final PriorityQueue<Integer> beginnings = new PriorityQueue<Integer>();
        final PriorityQueue<Integer> endings = new PriorityQueue<Integer>();

        if (beginnings.isEmpty())
        {
            return;
        }

        for (final IntegerIntervalList list : functions)
        {
            beginnings.addAll(list.get_beginnings());
            endings.addAll(list.get_endings());
        }

        int start_location = beginnings.poll();
        boolean in_interval = true;
        int number_true = 1;

        while (!beginnings.isEmpty() || !endings.isEmpty())
        {
            if (beginnings.peek() < endings.peek())
            {
                final int new_location = endings.poll();
                number_true--;
                if (number_true == 0)
                {
                    in_interval = false;
                    d_integer_intervals.add(new IntegerInterval(start_location, new_location));
                }
            }
            else
            {
                final int new_location = beginnings.poll();
                number_true++;
                if (!in_interval)
                {
                    in_interval = true;
                    start_location = new_location;
                }
            }
        }

        // 0010
        // 00011

    }

    private List<Integer> get_endings()
    {
        final List<Integer> endings = new Vector<Integer>();
        for (final IntegerInterval integer_interval : d_integer_intervals)
        {
            endings.add(integer_interval.get_upper_bound());
        }
        return endings;
    }

    private List<Integer> get_beginnings()
    {
        final List<Integer> beginnings = new Vector<Integer>();
        for (final IntegerInterval integer_interval : d_integer_intervals)
        {
            beginnings.add(integer_interval.get_lower_bound());
        }
        return beginnings;
    }

    public IntegerIntervalList union(final IntegerIntervalList other)
    {
        final List<IntegerIntervalList> integer_interval_lists = new Vector<IntegerIntervalList>();
        integer_interval_lists.add(this);
        integer_interval_lists.add(other);
        return new IntegerIntervalList(integer_interval_lists);
    }

    @Override
    public Boolean compute(final Integer input)
    {
        for (final IntegerInterval interval : d_integer_intervals)
        {
            if (interval.get_lower_bound() <= input)
            {
                if (input < interval.get_upper_bound())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    @Override
    public int get_true_count()
    {
        int true_count = 0;
        for (final IntegerInterval interval : d_integer_intervals)
        {
            true_count += interval.get_size();
        }
        return true_count;
    }

    @Override
    public boolean contains(final Integer domain_value)
    {
        return compute(domain_value);
    }

    @Override
    public Integer get_lower_bound()
    {
        return d_integer_intervals.get(0).get_lower_bound();
    }

    @Override
    public Integer get_upper_bound()
    {
        return d_integer_intervals.get(d_integer_intervals.size() - 1).get_upper_bound();
    }

    @Override
    public boolean has_lower_bound()
    {
        return true;
    }

    @Override
    public boolean has_upper_bound()
    {
        return true;
    }

    @Override
    public boolean has_inclusive_lower_bound()
    {
        return true;
    }

    @Override
    public boolean has_inclusive_upper_bound()
    {
        return true;
    }

    @Override
    public Set<Integer> get_values()
    {
        final Set<Integer> values = new HashSet<Integer>();
        for (final IntegerInterval interval : d_integer_intervals)
        {
            values.addAll(interval.get_values());
        }
        return null;
    }
}
