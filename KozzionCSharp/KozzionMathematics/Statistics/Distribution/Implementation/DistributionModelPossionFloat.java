package com.kozzion.library.math.statistics.distribution.implementation;

import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionFloat;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionFloatNumber;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionModelFloatNumber;
import com.kozzion.library.math.tools.MathToolsIntegerArray;

public class DistributionModelPossionFloat
    implements
        IDistributionModelFloatNumber<Integer>
{

    @Override
    public IDistributionFloatNumber<Integer> get_number_distribution(
        Integer [] sample)
    {
        // MLE Estimator is mean of sample
        int [] sample_primitive = ArrayTools.convert_to_integer_array(sample);
        return new DistributionPossionFloat(MathToolsIntegerArray.mean(sample_primitive));
    }

    @Override
    public IDistributionFloat<Integer> get_distribution(
        Integer [] sample)
    {
        // MLE Estimator is mean of sample
        return get_number_distribution(sample);
    }

    @Override
    public int get_parameter_count()
    {
        return 1;
    }

}
