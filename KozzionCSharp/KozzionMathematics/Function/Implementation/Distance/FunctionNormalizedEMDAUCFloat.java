package com.kozzion.library.math.function.implementation;

import com.kozzion.library.core.utility.CollectionTools;
import com.kozzion.library.math.numeric.intergral.TrapeziodIntegralEvaluatorFloat;
import com.kozzion.library.math.tools.MathToolsFloatArray;

public class FunctionNormalizedEMDAUCFloat
{
    public static float compute(
        float [] domain,
        float [] values_0,
        float [] values_1)
    {
        float [] temp_0 = new float [domain.length];
        float [] temp_1 = new float [domain.length];
        float [] temp_2 = new float [domain.length];
        return compute(domain, values_0, values_1, temp_0, temp_1, temp_2);
    }

    public static float compute(
        float [] domain,
        float [] values_0,
        float [] values_1,
        float [] domain_norm,
        float [] diff,
        float [] temp_2)
    {
        float duration = domain[domain.length - 1] - domain[0];
        MathToolsFloatArray.subtract_fill(domain, domain[0], domain_norm);
        MathToolsFloatArray.divide_in_place(domain_norm, duration);

        float auc_0 = TrapeziodIntegralEvaluatorFloat.evaluate_static_value(domain_norm, values_0);
        float auc_1 = TrapeziodIntegralEvaluatorFloat.evaluate_static_value(domain_norm, values_1);
        
        for (int index = 0; index < diff.length; index++)
        {
            diff[index] = (values_0[index] - auc_0) - (values_1[index] - auc_1);
        }

        TrapeziodIntegralEvaluatorFloat.evaluate_static_series(domain_norm, diff, temp_2);
        
        MathToolsFloatArray.abs_in_place(temp_2);
        return TrapeziodIntegralEvaluatorFloat.evaluate_static_value(domain_norm, temp_2);

    }
}
