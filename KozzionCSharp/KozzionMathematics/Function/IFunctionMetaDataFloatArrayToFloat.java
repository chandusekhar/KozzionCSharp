package com.kozzion.library.math.function;

/**
 * This class exists to prevent boxing and unboxing;
 * 
 * @author Joosterb
 */
public interface IFunctionMetaDataFloatArrayToFloat<MetaDataType>
{
    public float compute(float [] input, MetaDataType meta_data);
}
