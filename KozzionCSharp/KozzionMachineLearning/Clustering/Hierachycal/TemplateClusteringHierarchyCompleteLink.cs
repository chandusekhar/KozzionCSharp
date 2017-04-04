using System;
using System.Collections.Generic;
using KozzionCore.Collections;
using KozzionMathematics.Datastructure.Graph.implementation;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
    public class TemplateClusteringHierarchyCompleteLink<DomainType, DissimilarityType, DataSetType> :
            ITemplateClusteringHierarchy<DomainType, DissimilarityType, ICentroidDistance<DomainType, DissimilarityType>, DataSetType>
            where DissimilarityType : IComparable<DissimilarityType>
    {

        //IDEA: figure out the CLINK algoritm

        public TemplateClusteringHierarchyCompleteLink()
		 {
            // TODO Auto-generated constructor stub
        }

        public IClusteringHierarchy<DomainType, DissimilarityType, ICentroidDistance<DomainType, DissimilarityType>> Cluster(DataSetType data_set)
        {
            throw new NotImplementedException();
        }
    }
}