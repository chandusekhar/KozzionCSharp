package com.kozzion.library.machinelearning.transform;

import java.util.List;

import com.kozzion.library.math.function.IFunctionBijective;
import com.kozzion.library.math.function.implementation.FunctionIdentity;

public class TransformGeneratorIdentity
    implements
        ITransformGenerator<float []>
{

    @Override
    public IFunctionBijective<float [], float []> generate(
        List<float []> instances)
    {
        return new FunctionIdentity();
    }

}
