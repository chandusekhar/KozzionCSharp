using System;
using System.Collections.Generic;
using KozzionCore.Collections;
using KozzionMathematics.Datastructure.Graph.implementation;
using KozzionMathematics.Function;
using KozzionMachineLearning.DataSet;
using KozzionCore.Collections.PriorityQueue;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
	public class TemplateClusteringHierarchy<DomainType, DissimilarityType , DataSetType> :
            ITemplateClusteringHierarchy<DomainType, DissimilarityType, CentroidHierarchy<DomainType, DissimilarityType>, DataSetType>
        where DissimilarityType : IComparable<DissimilarityType>
        where DataSetType : IDataSet<DomainType>
	{
		IFunctionDissimilarity<DomainType[], DissimilarityType> dissimilarity_measure;

		public TemplateClusteringHierarchy(	IFunctionDissimilarity<DomainType[], DissimilarityType> dissimilarity_measure)
		{
			this.dissimilarity_measure = dissimilarity_measure;
		}



        public IClusteringHierarchy<DomainType, DissimilarityType, CentroidHierarchy<DomainType, DissimilarityType>> Cluster(DataSetType data_set)
		{
            int instance_count = data_set.InstanceCount;
            IList<DomainType[]> instance_features_list = data_set.FeatureData;

            // Create template
             LinkElementTreeTemplate < DomainType, DissimilarityType> template = new LinkElementTreeTemplate<DomainType, DissimilarityType>(instance_features_list);

    
            // Create links
            IPriorityQueue<Link<int, DissimilarityType>> queue = new PriorityQueueS1<Link<int, DissimilarityType>>(null);
            for (int index_instance_0 = 0; index_instance_0 < instance_count; index_instance_0++)
			{
				for (int index_instance_1 = index_instance_0 + 1; index_instance_1 < instance_count; index_instance_1++)
				{
					DomainType [] instance_0 = instance_features_list[index_instance_0];
					DomainType [] instance_1 = instance_features_list[index_instance_1];
                    queue.Enqueue(new Link<int, DissimilarityType>(index_instance_0, index_instance_1, dissimilarity_measure
						.Compute(instance_0, instance_1)));
				}
			}


			// Start breaking them
			while (1 < template.ClusterCount)
			{
                Link<int, DissimilarityType> link = queue.DequeueFirst();
                template.Merge(link.Value, link.Node_0, link.Node_1);    
			}
            return template.Create(data_set.DataContext);
        }

    
    }
}