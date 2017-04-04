package com.kozzion.library.graphics.elementtree.alphapartition.edgefunction;


public class EdgeFunctionFloatArrayHamming
    implements
        IEdgeFunctionFloatArray
{

    @Override
    public float construct_edge(
        float [] element_0_features,
        float [] element_1_features)
    {
        float error = 0;
        for (int index = 0; index < element_1_features.length; index++)
        {
            error += Math.abs(element_0_features[index] - element_1_features[index]);
        }
        return error;
    }

}
