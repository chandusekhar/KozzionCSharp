using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
    public class CentroidHierarchy<DomainType, DissimilarityType> : ICentroidHierarchy<DomainType, DissimilarityType, CentroidHierarchy<DomainType, DissimilarityType>>
    {
        private IFunctionDissimilarity<DomainType[], DissimilarityType> dissimilarity_function;
        private DomainType[] location;
        private IList<CentroidHierarchy<DomainType, DissimilarityType>> children;

        public IList<DomainType[]> Members
        {
            get
            {
                List<DomainType[]> members = new List<DomainType[]>();
                Queue<CentroidHierarchy<DomainType, DissimilarityType>> queue = new Queue<CentroidHierarchy<DomainType, DissimilarityType>>();
                queue.Enqueue(this);
                while (0 < queue.Count)
                {
                    CentroidHierarchy<DomainType, DissimilarityType> element = queue.Dequeue();
                    if (element.children.Count == 0)
                    {
                        members.Add(element.location);
                    }
                    else
                    {
                        foreach (CentroidHierarchy<DomainType, DissimilarityType> child in element.children)
                        {
                            queue.Enqueue(child);
                        }                        
                    }
                }
                return members;
            }
        }

        public CentroidHierarchy(IFunctionDissimilarity<DomainType[], DissimilarityType> dissimilarity_function, DomainType[] location, IList<CentroidHierarchy<DomainType, DissimilarityType>> children)
        {
            this.dissimilarity_function = dissimilarity_function;
            this.location = location;
            this.children = children;
        }

        public DissimilarityType ComputeDistance(DomainType[] instance_features)
        {
            return this.dissimilarity_function.Compute(this.location, instance_features);
        }

        public DissimilarityType GetDissimilarity(CentroidHierarchy<DomainType, DissimilarityType> other)
        {
            return this.dissimilarity_function.Compute(this.location, other.location);
        }
    }
}
