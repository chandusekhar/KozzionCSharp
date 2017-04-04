package com.kozzion.library.graphics.image;


public class FloatSpaceFloatImage4DDummy implements IFloatSpaceFloatImage4D
{
    private float d_value;
    
    public FloatSpaceFloatImage4DDummy(float value)
    {
        d_value = value;
    }

    @Override
    public float get_location_value(float x, float y, float z, float t)
    {
        return d_value;
    }

    @Override
    public float get_location_value(float [] location)
    {
        return d_value;
    }

}
