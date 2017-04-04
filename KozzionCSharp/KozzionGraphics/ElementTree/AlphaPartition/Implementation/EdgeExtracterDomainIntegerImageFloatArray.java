package com.kozzion.library.graphics.elementtree.alphapartition.applied;

import com.kozzion.library.graphics.elementtree.alphapartition.IEdgeExtracter;
import com.kozzion.library.graphics.elementtree.alphapartition.edgefunction.IEdgeFunctionFloatArray;
import com.kozzion.library.graphics.image.element.IElementDomainIntegerImageFloatArray;

public class EdgeExtracterDomainIntegerImageFloatArray
    implements
        IEdgeExtracter<IElementDomainIntegerImageFloatArray>
{
    IEdgeFunctionFloatArray d_edge_function;
    float []                d_features_0;
    float []                d_features_1;

    public EdgeExtracterDomainIntegerImageFloatArray(
        IEdgeFunctionFloatArray edge_function,
        int array_size)
    {
        d_edge_function = edge_function;
        d_features_0 = new float [array_size];
        d_features_1 = new float [array_size];
    }

    @Override
    public float compute_edge(
        int element_0,
        int element_1,
        IElementDomainIntegerImageFloatArray edge_domain)
    {
        edge_domain.get_element_values_fill(element_0, d_features_0);
        edge_domain.get_element_values_fill(element_1, d_features_1);
        return d_edge_function.construct_edge(d_features_0, d_features_1);
    }

}
