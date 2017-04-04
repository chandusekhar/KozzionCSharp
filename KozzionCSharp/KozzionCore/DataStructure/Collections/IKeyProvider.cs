namespace KozzionCore.DataStructure
{
    public interface IKeyProvider<KeyType>
    {
        KeyType Key { get; }
    }
}