package com.kozzion.library.machinelearning.clustering;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.kozzion.library.core.utility.CollectionTools;
import com.kozzion.library.machinelearning.classifier.IClassifier;
import com.kozzion.library.machinelearning.voting.ToolsVoting;
import com.kozzion.library.math.function.IFunction;
import com.kozzion.library.math.function.measures.IDistanceMeasure;
import com.kozzion.library.math.tools.MathToolsIntegerArray;

public class ClassifierClusterModel<DomainType, LabelType, DistanceType extends Comparable<DistanceType>>
    implements
        IClassifier<DomainType, LabelType>
{

    IDistanceMeasure<DomainType, DistanceType> d_distance_measure;
    List<DomainType>                    d_centroids;
    Map<Integer, LabelType>             d_cluster_index_to_label;

    public ClassifierClusterModel(
        List<DomainType> instances,
        List<LabelType> labels,
        int [] cluster_assignment,
        IDistanceMeasure<DomainType, DistanceType> distance_measure,
        IFunction<List<DomainType>, DomainType> centroid_calculator)
    {
        int cluster_count = MathToolsIntegerArray.max_value(cluster_assignment) + 1;
        List<List<DomainType>> clustered_instances = CollectionTools.group(instances, cluster_assignment, cluster_count);
        List<List<LabelType>> clustered_labels = CollectionTools.group(labels, cluster_assignment, cluster_count);

        d_distance_measure = distance_measure;
        d_centroids = new ArrayList<>();
        d_cluster_index_to_label = new HashMap<Integer, LabelType>();
        for (int cluster_index = 0; cluster_index < cluster_count; cluster_index++)
        {
            d_centroids.add(centroid_calculator.compute(clustered_instances.get(cluster_index)));
            d_cluster_index_to_label.put(cluster_index, ToolsVoting.get_most_common(clustered_labels.get(cluster_index)));
        }
    }

    @Override
    public LabelType compute(
        DomainType input)
    {
        int best_cluster_index = 0;
        DistanceType best_distance = d_distance_measure.compute(input, d_centroids.get(0));

        for (int cluster_index = 1; cluster_index < d_centroids.size(); cluster_index++)
        {
            DistanceType distance = d_distance_measure.compute(input, d_centroids.get(cluster_index));
            if (best_distance.compareTo(distance) == 1)
            {
                best_cluster_index = cluster_index;
                best_distance = distance;
            }
        }
        return d_cluster_index_to_label.get(best_cluster_index);
    }

    @Override
    public LabelType classify(
        DomainType input)
    {
        return compute(input);
    }

}
