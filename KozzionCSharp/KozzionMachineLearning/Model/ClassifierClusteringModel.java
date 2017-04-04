package com.kozzion.library.machinelearning.classifier;

import com.kozzion.library.machinelearning.clustering.IClustererModel;

public class ClassifierClusteringModel<DomainType>
    implements
        IClassifier<DomainType, Integer>
{
    IClustererModel<DomainType> d_clustering_model;

    public ClassifierClusteringModel(
        IClustererModel<DomainType> clustering_model)
    {
        d_clustering_model = clustering_model;
    }

    @Override
    public Integer compute(DomainType input)
    {
        return d_clustering_model.get_cluster_index(input);
    }

    @Override
    public Integer classify(DomainType input)
    {
        return compute(input);
    }

}
