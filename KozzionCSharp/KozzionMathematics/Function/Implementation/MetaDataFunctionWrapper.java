package com.kozzion.library.math.function.implementation;

import com.kozzion.library.math.function.IFunction;
import com.kozzion.library.math.function.IFunctionFloatArray;
import com.kozzion.library.math.function.IFunctionFloatArrayToFloat;
import com.kozzion.library.math.function.IFunctionMetaDataFloatArrayToFloat;

public class MetaDataFunctionWrapper implements IFunctionMetaDataFloatArrayToFloat<Object>
{
    IFunctionFloatArrayToFloat d_function;
    
    public MetaDataFunctionWrapper(IFunctionFloatArrayToFloat function)
    {
        d_function = function;
    }

    @Override
    public float compute(float [] input, Object meta_data)
    {
        return d_function.compute(input);
    }

}
