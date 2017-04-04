using System;
using System.Collections.Generic;
using KozzionMathematics.Datastructure.Graph.implementation;
using KozzionMachineLearning.Clustering.Hierarchy;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
	public class LinkElementTreeTemplate<DomainType, DissimilarityType>
        where DissimilarityType : IComparable<DissimilarityType>
	{
        public int ClusterCount { get; private set; }
		// Cases
		private IList<DomainType[]> instance_features_list;

        // Path Compression
        private Dictionary<int, NodeTreeLink<DissimilarityType>> top_assignments;

		public LinkElementTreeTemplate(
			IList<DomainType []> instance_features_list)
		{
            this.instance_features_list = instance_features_list;
			this.ClusterCount = instance_features_list.Count;
            this.top_assignments = new Dictionary<int, NodeTreeLink<DissimilarityType>>();

		}

		public IList<DomainType[]> get_instances()
		{
			return instance_features_list;
		}

		private NodeTreeLink<DissimilarityType> GetTopLevelNode(
			int index_element)
		{
			if (!top_assignments.ContainsKey(index_element))
			{
				return null;
			}
			else
			{
				NodeTreeLink<DissimilarityType> node = top_assignments[index_element];
				if (node.get_parent() == null)
				{
					return node;
				}
				else
				{
					do
					{
						node = node.get_parent();
					}
					while (node.get_parent() != null);
					top_assignments[index_element] = node;
					return node;
				}
			}
		}

		public void Merge(
			DissimilarityType value,
			int index_element_0,
			int index_element_1)
		{
			NodeTreeLink<DissimilarityType> node_0 = GetTopLevelNode(index_element_0);
			NodeTreeLink<DissimilarityType> node_1 = GetTopLevelNode(index_element_1);

			if (node_0 == null)
			{

				if (node_1 == null)
				{
					// if both nodes are the same do nothing other wize add a new node
					NodeTreeLink<DissimilarityType> node_new = new NodeTreeLink<DissimilarityType>(value, index_element_0, index_element_1);
					top_assignments[index_element_0] = node_new;
					top_assignments[index_element_1] = node_new;
                    ClusterCount--;
				}
				else
				{
					NodeTreeLink<DissimilarityType> node_new = new NodeTreeLink<DissimilarityType>(value, index_element_0, node_1);
					top_assignments[index_element_0] = node_new;
					top_assignments[index_element_1] = node_new;
                    ClusterCount--;
				}
			}
			else
			{
				if (node_1 == null)
				{
					// if both nodes are the same do nothing other wize add a new node
					NodeTreeLink<DissimilarityType> node_new = new NodeTreeLink<DissimilarityType>(value, index_element_1, node_0);
					top_assignments[index_element_0] = node_new;
					top_assignments[index_element_1] = node_new;
                    ClusterCount--;
				}
				else
				{
					// Both are noded
					if (node_0 != node_1)
					{
						NodeTreeLink<DissimilarityType> node_new = new NodeTreeLink<DissimilarityType>(value, node_0, node_1);
						top_assignments[index_element_0] = node_new;
						top_assignments[index_element_1] = node_new;
                        ClusterCount--;
					}
					else
					{
						return;// already merged
					}

				}
			}
		}

        internal IClusteringHierarchy<DomainType, DissimilarityType, CentroidHierarchy<DomainType, DissimilarityType>> Create(IDataContext data_context)
        {
            return new ClusteringHierarchy<DomainType, DissimilarityType, CentroidHierarchy<DomainType, DissimilarityType>>(data_context, null);
        }
    }
}