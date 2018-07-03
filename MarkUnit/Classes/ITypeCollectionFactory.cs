namespace MarkUnit.Classes
{
    internal interface ITypeCollectionFactory
    {
        ITypeCollection Create(ITypeCollector typeCollector, bool negate, bool not, string[] exceptions);
    }
}