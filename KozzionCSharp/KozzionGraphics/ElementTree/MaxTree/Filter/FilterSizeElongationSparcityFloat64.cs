using System.Collections.Generic;
using KozzionGraphics.ElementTree.Feature;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionMathematics.Function;

namespace KozzionImageProcessor.Models
{
    public class FilterSizeElongationSparcityFloat64 : IFunction<IMaxTreeNode<float>, bool>
    {
        public string FunctionType { get { return "FilterSizeElongationSparcityFloat64"; } }
        HashSet<int> filtered_nodes;

        public FilterSizeElongationSparcityFloat64(FeatureGeneratorElementNode3DDouble generator, double[,] features,
            double MinSize, double MaxSize,
            double MinElongation, double MaxElongation,
            double MinSparcity, double MaxSparcity)
        {
            filtered_nodes = new HashSet<int>();

            for (int node_index = 0; node_index < features.GetLength(0); node_index++)
            {
                double size = features[node_index, generator.IndexSize];
                double elongation = features[node_index, generator.IndexElongation];
                double sparcity = features[node_index, generator.IndexSparceness];
                if ((size < MinSize) || (MaxSize < size))
                {
                    filtered_nodes.Add(node_index);
                }
                else if ((elongation < MinElongation)  || (MaxElongation < elongation))
                {
                    filtered_nodes.Add(node_index);
                }
                else if ((sparcity < MinSparcity) || (MaxSparcity < sparcity))
                {
                    filtered_nodes.Add(node_index);
                }
            }
        }

        //returns true if node should be removed
        public bool Compute(IMaxTreeNode<float> value_domain)
        {
            return filtered_nodes.Contains(value_domain.NodeIndex);
        }
    }
}
