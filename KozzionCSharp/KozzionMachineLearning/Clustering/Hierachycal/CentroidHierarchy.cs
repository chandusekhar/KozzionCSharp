using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering.Hierarchy
{
    public class CentroidHierarchy<DomainType> : ICentroidHierarchy<DomainType, CentroidHierarchy<DomainType>>
    {
        private IFunctionDissimilarity<DomainType[], double> dissimilarity_function;
        private DomainType[] location;
        private IList<CentroidHierarchy<DomainType>> children;

        public IList<DomainType[]> Members
        {
            get
            {
                List<DomainType[]> members = new List<DomainType[]>();
                Queue<CentroidHierarchy<DomainType>> queue = new Queue<CentroidHierarchy<DomainType>>();
                queue.Enqueue(this);
                while (0 < queue.Count)
                {
                    CentroidHierarchy<DomainType> element = queue.Dequeue();
                    if (element.children.Count == 0)
                    {
                        members.Add(element.location);
                    }
                    else
                    {
                        foreach (CentroidHierarchy<DomainType> child in element.children)
                        {
                            queue.Enqueue(child);
                        }                        
                    }
                }
                return members;
            }
        }

        public CentroidHierarchy(IFunctionDissimilarity<DomainType[], double> dissimilarity_function, DomainType[] location, IList<CentroidHierarchy<DomainType>> children)
        {
            this.dissimilarity_function = dissimilarity_function;
            this.location = location;
            this.children = children;
        }

        public double ComputeDistance(DomainType[] instance_features)
        {
            return this.dissimilarity_function.Compute(this.location, instance_features);
        }

        public double GetDissimilarity(CentroidHierarchy<DomainType> other)
        {
            return this.dissimilarity_function.Compute(this.location, other.location);
        }
    }
}
