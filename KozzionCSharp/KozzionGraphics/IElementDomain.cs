namespace KozzionGraphics
{
    public interface IElementDomain<ElementType>
    {
        int ElementCount{ get; }
        ElementType GetElementValue(int element_index);
    }
}
