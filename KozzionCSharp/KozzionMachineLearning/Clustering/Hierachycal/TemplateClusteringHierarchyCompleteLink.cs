using System;
using System.Collections.Generic;
using KozzionCore.Collections;
using KozzionMathematics.Datastructure.Graph.implementation;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
    public class TemplateClusteringHierarchyCompleteLink<DomainType, DataSetType> :
            ITemplateClusteringHierarchy<DomainType, ICentroidDistance<DomainType>, DataSetType>
    {

        //IDEA: figure out the CLINK algoritm

        public TemplateClusteringHierarchyCompleteLink()
		 {
            // TODO Auto-generated constructor stub
        }

        public IClusteringHierarchy<DomainType, ICentroidDistance<DomainType>> Cluster(DataSetType data_set)
        {
            throw new NotImplementedException();
        }
    }
}