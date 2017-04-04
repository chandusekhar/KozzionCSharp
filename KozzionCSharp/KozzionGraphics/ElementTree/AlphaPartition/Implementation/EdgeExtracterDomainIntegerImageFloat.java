package com.kozzion.library.graphics.elementtree.alphapartition.applied;

import com.kozzion.library.graphics.elementtree.alphapartition.IEdgeExtracter;
import com.kozzion.library.graphics.elementtree.alphapartition.edgefunction.IEdgeFunctionFloat;
import com.kozzion.library.graphics.image.element.IElementDomainIntegerImageFloat;

public class EdgeExtracterDomainIntegerImageFloat implements IEdgeExtracter<IElementDomainIntegerImageFloat>
{
    IEdgeFunctionFloat d_edge_function;
    
    public EdgeExtracterDomainIntegerImageFloat(IEdgeFunctionFloat edge_function)
    {
        d_edge_function = edge_function;
    }

    @Override
    public float compute_edge(
        int element_0,
        int element_1,
        IElementDomainIntegerImageFloat edge_domain)
    {
        float [] element_array = edge_domain.get_element_array();
        return d_edge_function.compute_edge(element_array[element_0], element_array[element_1]);
    }

}
