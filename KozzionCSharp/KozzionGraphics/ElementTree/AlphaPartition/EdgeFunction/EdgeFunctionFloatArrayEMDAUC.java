package com.kozzion.library.graphics.elementtree.alphapartition.edgefunction;

import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.math.function.implementation.FunctionNormalizedEMDAUCFloat;
import com.kozzion.library.math.function.implementation.FunctionNormalizedEMDFloat;

public class EdgeFunctionFloatArrayEMDAUC
    implements
        IEdgeFunctionFloatArray
{
    float [] d_sample_times;
    float [] d_temp_0;
    float [] d_temp_1;
    float [] d_temp_2;

    public EdgeFunctionFloatArrayEMDAUC(
        float [] sample_times)
    {
        d_sample_times = ArrayTools.copy(sample_times);
        d_temp_0 = new float [sample_times.length];
        d_temp_1 = new float [sample_times.length];
        d_temp_2 = new float [sample_times.length];
    }

    @Override
    public float construct_edge(
        float [] element_0_features,
        float [] element_1_features)
    {
        return FunctionNormalizedEMDAUCFloat.compute(d_sample_times, element_0_features, element_1_features, d_temp_0, d_temp_1, d_temp_2);
    }

}
