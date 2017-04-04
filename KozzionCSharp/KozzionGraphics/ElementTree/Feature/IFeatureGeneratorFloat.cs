using KozzionMathematics.Function;

namespace KozzionGraphics.ElementTree.Feature
{
    public interface IFeatureGeneratorFloat<Domain> : IFunction<Domain, float[]>
    {
        int FeatureCount {get;}

        void GenerateFeatures(Domain input, float[] features);

    }
}