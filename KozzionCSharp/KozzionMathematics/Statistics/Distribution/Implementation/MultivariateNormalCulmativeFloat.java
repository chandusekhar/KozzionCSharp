package com.kozzion.library.math.statistics.distribution.implementation;

import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.math.function.IFunctionFloatArrayToFloat;

public class MultivariateNormalCulmativeFloat implements IFunctionFloatArrayToFloat
{
    
    float [] d_means;
    float [][] d_covariance_matrix;

    public MultivariateNormalCulmativeFloat(float [] means, float [][] covariance_matrix)
    {
        d_means= ArrayTools.copy(means);
        d_covariance_matrix= ArrayTools.copy(covariance_matrix);
    }

    
    @Override
    public float compute(float [] input)
    {
  
        return 0;
    }

}
