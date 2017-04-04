package com.kozzion.library.math.function.implementation;

import java.util.HashMap;

import com.kozzion.library.math.function.IFunctionDomainExplicit;
import com.kozzion.library.math.function.domain.IDomainExplicit;
import com.kozzion.library.math.function.domain.implementation.HashSetDomain;

public class FunctionIntegerMap implements IFunctionDomainExplicit<Integer, Double>
{
    public HashMap<Integer, Double> d_value_map;

    @Override
    public Double compute(final Integer input)
    {
        if (d_value_map.containsKey(input))
        {
            return d_value_map.get(input);
        }
        else
        {
            throw new IllegalArgumentException("Input: " + input + " is outside domain");
        }
    }

    @Override
    public IDomainExplicit<Integer> get_explicit_domain()
    {
        return new HashSetDomain<Integer>(d_value_map.keySet());
    }

}
