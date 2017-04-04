using System.Collections.Generic;
using KozzionGraphics.ElementTree.Feature;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionMathematics.Function;

namespace KozzionImageProcessor.Models
{
    public class FilterSizeElongationSparcityFloat32 : IFunction<IMaxTreeNode<float>, bool>
    {
        public string FunctionType { get { return "FilterSizeElongationSparcityFloat32"; } }
        HashSet<int> filtered_nodes;

        public FilterSizeElongationSparcityFloat32(FeatureGeneratorElementNode3DFloat32 generator, float[,] features,
            float MinSize, float MaxSize,
            float MinElongation, float MaxElongation,
            float MinSparcity, float MaxSparcity)
        {
            this.filtered_nodes = new HashSet<int>();

            for (int node_index = 0; node_index < features.GetLength(0); node_index++)
            {
                float size = features[node_index, generator.IndexSize];
                float elongation = features[node_index, generator.IndexElongation];
                float sparcity = features[node_index, generator.IndexSparceness];
                if ((size < MinSize) || (MaxSize < size))
                {
                    this.filtered_nodes.Add(node_index);
                }
                else if ((elongation < MinElongation)  || (MaxElongation < elongation))
                {
                    this.filtered_nodes.Add(node_index);
                }
                else if ((sparcity < MinSparcity) || (MaxSparcity < sparcity))
                {
                    this.filtered_nodes.Add(node_index);
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
