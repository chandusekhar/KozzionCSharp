package com.kozzion.library.graphics.elementtree.alphapartition.applied;

import com.kozzion.library.graphics.elementtree.alphapartition.IEdgeExtracter;
import com.kozzion.library.graphics.elementtree.alphapartition.edgefunction.IEdgeFunctionInteger;
import com.kozzion.library.graphics.image.IIntegerRasterIntegerImage3D;

public class EdgeExtracterIntegerRasterFloatImage3D implements IEdgeExtracter<IIntegerRasterIntegerImage3D>
{
    IEdgeFunctionInteger d_edge_function;
    
    public EdgeExtracterIntegerRasterFloatImage3D(IEdgeFunctionInteger edge_function)
    {
        d_edge_function = edge_function;
    }

    @Override
    public float compute_edge(
        int element_0,
        int element_1,
        IIntegerRasterIntegerImage3D edge_domain)
    {
        int [] element_array = edge_domain.get_element_array();
        return d_edge_function.compute_edge(element_array[element_0],element_array[element_1]);
    }

}
