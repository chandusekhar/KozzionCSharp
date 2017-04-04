package com.kozzion.library.machinelearning.featureselection;

import java.util.Set;

import com.kozzion.library.machinelearning.dataset.IDataContext;

public class ExaustiveFeatureSelector implements IFeatureSetSelectorFloat
{
    int   d_runs;
    float d_train_fraction;

    public ExaustiveFeatureSelector(int runs, float train_fraction)
    {
        d_runs = runs;
        d_train_fraction = train_fraction;
    }

    @Override
    public Set<Integer> select_feature_set(IFeatureSetEvaluatorFloat evaluator, IDataContext context)
    {
        int feature_count  = context.get_feature_count();
      //  System.out.println(feature_count);
      //  int configurations = MathToolsInteger.pow(2, feature_count);
      //  System.out.println(configurations);
        //TODO make an estimate if it is even feasable
        return null;
    }

}
