package com.kozzion.library.machinelearning.featureselection;

import java.util.HashSet;
import java.util.Random;
import java.util.Set;

import com.kozzion.library.machinelearning.dataset.IDataContext;

public class RandomFeatureSelector implements IFeatureSetSelectorFloat
{
    int                       d_evaluated_feature_set_count;
    float                     d_feature_inclusion_chance;

    public RandomFeatureSelector(int evaluated_feature_set_count,
        float feature_inclusion_chance)
    {
        d_evaluated_feature_set_count = evaluated_feature_set_count;
        d_feature_inclusion_chance = feature_inclusion_chance;
    }

    @Override
    public Set<Integer> select_feature_set(IFeatureSetEvaluatorFloat feature_set_evaluator, IDataContext context)
    {
        Set<Integer> best_feature_set = null;
        float best_score = 0;
        for (int feature_set_index = 0; feature_set_index < d_evaluated_feature_set_count; feature_set_index++)
        {
            Set<Integer> random_feature_set = create_random_set(context);
            float score = feature_set_evaluator.evaluate_feature_set(random_feature_set);
            if (best_feature_set == null)
            {
                best_feature_set = random_feature_set;
                best_score = score;
            }
            else
            {
                if (best_score < score)
                {
                    best_feature_set = random_feature_set;
                    best_score = score;
                    System.out.println("new best score: " + best_score);
                    System.out.println("new best count: " + best_feature_set.size());
                    System.out.println("iteration     : " + feature_set_index + " of " + d_evaluated_feature_set_count);
                }
            }
        }
        return best_feature_set;
    }

    private Set<Integer> create_random_set(IDataContext context)
    {
        Set<Integer> feature_selection_set = new HashSet<Integer>();
        Random random = new Random();
        int feature_count = context.get_feature_count();
        for (int feature_index = 0; feature_index < feature_count; feature_index++)
        {
            if (random.nextFloat() < d_feature_inclusion_chance)
            {
                feature_selection_set.add(feature_index);
            }
        }
        return feature_selection_set;
    }

}
