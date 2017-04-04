using KozzionMathematics.Function;

namespace KozzionGraphics.ElementTree.MaxTree.Filter
{
    public sealed class FilterMinimalSize : IFunction<IMaxTreeNode<float>, bool>
    {
        public string FunctionType { get { return "FilterMinimalSize"; } }

        private int minimal_component_size;

        public FilterMinimalSize(int minimal_component_size)
        {
            this.minimal_component_size = minimal_component_size;
        }

        public bool Compute(IMaxTreeNode<float> value_domain)
        {
            return value_domain.CulmativeRealSize < minimal_component_size;
        }
    }
}
