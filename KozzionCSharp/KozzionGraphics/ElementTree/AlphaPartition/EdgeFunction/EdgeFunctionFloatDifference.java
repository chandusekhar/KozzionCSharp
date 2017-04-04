package com.kozzion.library.graphics.elementtree.alphapartition.edgefunction;

public class EdgeFunctionFloatDifference implements IEdgeFunctionFloat
{

    @Override
    public float compute_edge(
        float element_0_feature,
        float element_1_feature)
    {
        return Math.abs(element_0_feature - element_1_feature);
    }

}
