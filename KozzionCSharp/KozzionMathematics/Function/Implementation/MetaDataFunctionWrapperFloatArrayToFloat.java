package com.kozzion.library.math.function.implementation;

import com.kozzion.library.math.function.IFunctionFloatArrayToFloat;
import com.kozzion.library.math.function.IFunctionMetaDataFloatArrayToFloat;

public class MetaDataFunctionWrapperFloatArrayToFloat<MetaDataType> implements IFunctionFloatArrayToFloat
{
    IFunctionMetaDataFloatArrayToFloat<MetaDataType> d_function;
    MetaDataType                                     d_meta_data;

    public MetaDataFunctionWrapperFloatArrayToFloat(IFunctionMetaDataFloatArrayToFloat<MetaDataType> function, MetaDataType meta_data)
    {
        d_function = function;
        d_meta_data = meta_data;
    }

    @Override
    public float compute(float [] input)
    {
        return d_function.compute(input, d_meta_data);
    }

}
